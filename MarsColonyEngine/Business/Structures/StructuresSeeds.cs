namespace MarsColonyEngine.Business.Structures {
    public static class StructuresSeeds {
        public static Structure[] AvailableStructures = new Structure[] {
            new Structure("Basic shelter", new StructureStats(50f, 100f), new Stats.ColonyStats(0, 0, 1, 0), new Stats.ColonyStats(),
                new Colonizers.ColonizerStats(0, 0, 0, 0.25f, 0.25f), default),

        };
    }
}
