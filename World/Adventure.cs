using System.Text.Json;
using Spectre.Console;
using Zemphas.Enemies;

namespace Zemphas
{
    internal enum RunOutcome
    {
        Victory,
        Fled,
        Died,
    }

    // Walks the room graph loaded from Content/world.json. Replaces the hardcoded
    // Level1 and Level2 methods: adding a room, a branch or a whole region is a
    // content edit now, not a code change.
    internal static class Adventure
    {
        private const string worldFileName = "Content/world.json";

        public static WorldData Load(string? path = null)
        {
            string file = path ?? Path.Combine(AppContext.BaseDirectory, worldFileName);

            if (!File.Exists(file))
            {
                throw new FileNotFoundException($"World file not found at {file}");
            }

            WorldData? world = JsonSerializer.Deserialize<WorldData>(File.ReadAllText(file));

            if (world == null)
            {
                throw new InvalidDataException($"World file at {file} could not be read");
            }

            Validate(world);
            return world;
        }

        // Catches a broken world file at startup with a useful message, rather than
        // dropping the player into a dead end halfway through a run.
        public static void Validate(WorldData world)
        {
            Dictionary<string, RoomData> byId = new Dictionary<string, RoomData>();

            foreach (RoomData room in world.Rooms)
            {
                if (string.IsNullOrWhiteSpace(room.Id))
                {
                    throw new InvalidDataException("A room is missing its id");
                }
                if (byId.ContainsKey(room.Id))
                {
                    throw new InvalidDataException($"Duplicate room id '{room.Id}'");
                }
                byId[room.Id] = room;
            }

            if (!byId.ContainsKey(world.StartRoom))
            {
                throw new InvalidDataException($"startRoom '{world.StartRoom}' is not a room");
            }

            HashSet<string> pools = new HashSet<string>();
            foreach (RoomData room in world.Rooms)
            {
                if (!string.IsNullOrEmpty(room.Pool))
                {
                    pools.Add(room.Pool);
                }
            }

            foreach (RoomData room in world.Rooms)
            {
                if (room.Exits.Count == 0 && string.IsNullOrEmpty(room.Ending))
                {
                    throw new InvalidDataException($"Room '{room.Id}' has no exits and no ending, so a run would stall there");
                }

                foreach (ExitData exit in room.Exits)
                {
                    CheckTarget(room.Id, exit.To, byId, pools);

                    if (exit.Sneak && string.IsNullOrEmpty(exit.FailTo))
                    {
                        throw new InvalidDataException($"Room '{room.Id}' has a sneak exit with no failTo");
                    }
                    if (!string.IsNullOrEmpty(exit.FailTo))
                    {
                        CheckTarget(room.Id, exit.FailTo!, byId, pools);
                    }
                }

                if (room.Event != null && room.Event.Kind == "encounter" || room.Event != null && room.Event.Kind == "boss")
                {
                    if (string.IsNullOrEmpty(room.Event.Enemy))
                    {
                        throw new InvalidDataException($"Room '{room.Id}' has a {room.Event.Kind} event with no enemy");
                    }
                    EnemyFor(room.Event.Enemy!); // throws if the name is unknown
                }
            }
        }

        private static void CheckTarget(string roomId, string target, Dictionary<string, RoomData> byId, HashSet<string> pools)
        {
            if (target.StartsWith("pool:"))
            {
                string pool = target.Substring("pool:".Length);
                if (!pools.Contains(pool))
                {
                    throw new InvalidDataException($"Room '{roomId}' exits to pool '{pool}', which no room belongs to");
                }
                return;
            }

            if (!byId.ContainsKey(target))
            {
                throw new InvalidDataException($"Room '{roomId}' exits to '{target}', which is not a room");
            }
        }

        // Walks the graph from the start room and reports what it found. Used by
        // the --validate flag so a content edit can be checked without playing.
        public static string Describe(WorldData world)
        {
            Dictionary<string, RoomData> byId = new Dictionary<string, RoomData>();
            Dictionary<string, List<string>> poolMembers = new Dictionary<string, List<string>>();

            foreach (RoomData room in world.Rooms)
            {
                byId[room.Id] = room;
                if (!string.IsNullOrEmpty(room.Pool))
                {
                    if (!poolMembers.ContainsKey(room.Pool)) poolMembers[room.Pool] = new List<string>();
                    poolMembers[room.Pool].Add(room.Id);
                }
            }

            HashSet<string> reached = new HashSet<string>();
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(world.StartRoom);

            while (queue.Count > 0)
            {
                string id = queue.Dequeue();
                if (!reached.Add(id)) continue;

                foreach (ExitData exit in byId[id].Exits)
                {
                    foreach (string? target in new string?[] { exit.To, exit.FailTo })
                    {
                        if (string.IsNullOrEmpty(target)) continue;

                        if (target!.StartsWith("pool:"))
                        {
                            foreach (string member in poolMembers[target.Substring("pool:".Length)])
                            {
                                queue.Enqueue(member);
                            }
                        }
                        else
                        {
                            queue.Enqueue(target!);
                        }
                    }
                }
            }

            List<string> unreachable = new List<string>();
            foreach (RoomData room in world.Rooms)
            {
                if (!reached.Contains(room.Id)) unreachable.Add(room.Id);
            }

            List<string> endings = new List<string>();
            foreach (RoomData room in world.Rooms)
            {
                if (!string.IsNullOrEmpty(room.Ending)) endings.Add($"{room.Id}({room.Ending})");
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine($"rooms       : {world.Rooms.Count}");
            sb.AppendLine($"start       : {world.StartRoom}");
            sb.AppendLine($"reachable   : {reached.Count}/{world.Rooms.Count}");
            sb.AppendLine($"unreachable : {(unreachable.Count == 0 ? "none" : string.Join(", ", unreachable))}");
            sb.AppendLine($"endings     : {(endings.Count == 0 ? "NONE - a run could never finish" : string.Join(", ", endings))}");

            foreach (KeyValuePair<string, List<string>> pool in poolMembers)
            {
                sb.AppendLine($"pool '{pool.Key}' : {pool.Value.Count} rooms ({string.Join(", ", pool.Value)})");
            }

            return sb.ToString();
        }

        public static Enemy EnemyFor(string name)
        {
            switch (name)
            {
                case "Ogre": return new Ogre();
                case "Warlock": return new Warlock();
                case "Warlord": return new Warlord();
                default: throw new InvalidDataException($"Unknown enemy '{name}' in the world file");
            }
        }

        public static RunOutcome Run(Hero hero, WorldData world)
        {
            Dictionary<string, RoomData> byId = new Dictionary<string, RoomData>();
            foreach (RoomData room in world.Rooms)
            {
                byId[room.Id] = room;
            }

            HashSet<string> visited = new HashSet<string>();
            string currentId = world.StartRoom;
            bool bossDefeated = false;

            while (true)
            {
                RoomData room = byId[currentId];
                visited.Add(room.Id);

                foreach (string line in room.Text)
                {
                    AnsiConsole.Write(new Markup($"[blue]{Markup.Escape(line)}[/]"));
                    Console.WriteLine();
                }
                Console.WriteLine();

                if (room.Event != null)
                {
                    if (!RunEvent(hero, room.Event, ref bossDefeated))
                    {
                        return RunOutcome.Died;
                    }
                }

                if (!string.IsNullOrEmpty(room.Ending))
                {
                    return room.Ending == "victory" && bossDefeated ? RunOutcome.Victory : RunOutcome.Fled;
                }

                currentId = ChooseExit(hero, room, byId, visited);
            }
        }

        // Returns false if the Hero died carrying out the event
        private static bool RunEvent(Hero hero, RoomEventData roomEvent, ref bool bossDefeated)
        {
            switch (roomEvent.Kind)
            {
                case "swordChoice":
                    HeroManagement.HeroChooseStartingSword(hero);
                    return true;

                case "findSword":
                    HeroManagement.HeroFindSword(hero);
                    return true;

                case "fountain":
                    return Events.Fountain(hero);

                case "randomEncounter":
                    Encounters.randomEcounter(hero);
                    return HeroManagement.HeroAliveCheck(hero);

                case "encounter":
                    Encounters.Encounter(hero, EnemyFor(roomEvent.Enemy!));
                    return HeroManagement.HeroAliveCheck(hero);

                case "boss":
                {
                    // The run ends after the boss, so no growth prompt
                    EncounterOutcome outcome = Encounters.Encounter(hero, EnemyFor(roomEvent.Enemy!), offerBoons: false);
                    if (outcome == EncounterOutcome.Victory)
                    {
                        bossDefeated = true;
                    }
                    return HeroManagement.HeroAliveCheck(hero);
                }

                case "none":
                    return true;

                default:
                    throw new InvalidDataException($"Unknown event kind '{roomEvent.Kind}'");
            }
        }

        private static string ChooseExit(Hero hero, RoomData room, Dictionary<string, RoomData> byId,
            HashSet<string> visited)
        {
            ExitData chosen;

            if (room.Exits.Count == 1)
            {
                chosen = room.Exits[0];
            }
            else
            {
                List<string> labels = new List<string>();
                foreach (ExitData exit in room.Exits)
                {
                    labels.Add(exit.Label);
                }

                string picked = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Which way do you go?")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                        .AddChoices(labels));

                chosen = room.Exits[labels.IndexOf(picked)];
            }

            if (chosen.Sneak)
            {
                bool slipped = HeroManagement.HeroEvasionCheck(hero);
                Console.WriteLine(slipped
                    ? chosen.SneakSuccessText ?? "You slip past unnoticed."
                    : chosen.SneakFailText ?? "You are spotted!");
                Console.WriteLine();

                // A failed sneak lands in the room holding the fight you did not avoid
                return Resolve(slipped ? chosen.To : chosen.FailTo!, byId, visited);
            }

            return Resolve(chosen.To, byId, visited);
        }

        // "pool:<name>" picks a random room from that pool, preferring one not yet
        // seen this run so a playthrough covers different ground each time.
        private static string Resolve(string target, Dictionary<string, RoomData> byId, HashSet<string> visited)
        {
            if (!target.StartsWith("pool:"))
            {
                return target;
            }

            string pool = target.Substring("pool:".Length);

            List<string> candidates = new List<string>();
            List<string> unvisited = new List<string>();

            foreach (KeyValuePair<string, RoomData> entry in byId)
            {
                if (entry.Value.Pool == pool)
                {
                    candidates.Add(entry.Key);
                    if (!visited.Contains(entry.Key))
                    {
                        unvisited.Add(entry.Key);
                    }
                }
            }

            List<string> from = unvisited.Count > 0 ? unvisited : candidates;
            return from[Random.Shared.Next(from.Count)];
        }
    }
}
