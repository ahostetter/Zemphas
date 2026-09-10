using System.Text.Json.Serialization;

namespace Zemphas
{
    // The shapes the world file deserialises into. Content lives in
    // Content/world.json so rooms, text and layout can change without a rebuild.

    internal sealed class WorldData
    {
        [JsonPropertyName("startRoom")]
        public string StartRoom { get; set; } = "";

        [JsonPropertyName("rooms")]
        public List<RoomData> Rooms { get; set; } = new List<RoomData>();

        // Named lists of enemy types. A randomEncounter names the table it draws
        // from, so the cave can stay survivable while the highlands are not.
        [JsonPropertyName("encounterTables")]
        public Dictionary<string, List<string>> EncounterTables { get; set; } = new Dictionary<string, List<string>>();
    }

    internal sealed class RoomData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        // Rooms sharing a pool are interchangeable. An exit pointing at
        // "pool:cave" lands on a random unvisited room from that pool, which is
        // what makes a run's layout differ from the last one.
        [JsonPropertyName("pool")]
        public string? Pool { get; set; }

        [JsonPropertyName("text")]
        public List<string> Text { get; set; } = new List<string>();

        [JsonPropertyName("event")]
        public RoomEventData? Event { get; set; }

        [JsonPropertyName("exits")]
        public List<ExitData> Exits { get; set; } = new List<ExitData>();

        // "victory" or "flee". A room with an ending finishes the run.
        [JsonPropertyName("ending")]
        public string? Ending { get; set; }
    }

    internal sealed class RoomEventData
    {
        // swordChoice | randomEncounter | encounter | boss | findSword | fountain | rest
        [JsonPropertyName("kind")]
        public string Kind { get; set; } = "";

        // Encounter table a randomEncounter draws from
        [JsonPropertyName("table")]
        public string? Table { get; set; }

        // Enemy type name for the encounter and boss kinds
        [JsonPropertyName("enemy")]
        public string? Enemy { get; set; }
    }

    internal sealed class ExitData
    {
        [JsonPropertyName("label")]
        public string Label { get; set; } = "";

        // A room id, or "pool:<name>" for a random room from that pool
        [JsonPropertyName("to")]
        public string To { get; set; } = "";

        // When set, taking this exit is an evasion check. Failing it triggers
        // the room's encounter instead of slipping past it.
        [JsonPropertyName("sneak")]
        public bool Sneak { get; set; }

        // Where a failed sneak leads. That room holds the fight you did not avoid.
        [JsonPropertyName("failTo")]
        public string? FailTo { get; set; }

        // Shown when a sneak attempt succeeds or fails
        [JsonPropertyName("sneakSuccessText")]
        public string? SneakSuccessText { get; set; }

        [JsonPropertyName("sneakFailText")]
        public string? SneakFailText { get; set; }
    }
}
