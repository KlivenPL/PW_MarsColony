using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Stats;

namespace MarsColonyEngine.Business.Structures {

    public enum AvailableStructures {
        RescueCapsule,
        SimpleShelter,
        PotatoFarm,
        ArmoredBedroom,
        AluminiumMine
    }
    public static class StructuresBuilder {
        public static Structure RescueCapsule () {
            return new Structure(
                structureEnum: AvailableStructures.RescueCapsule,
                baseColonizerStatsAffect: new ColonizerStats(0, 0, 0, 0, 10f),
                deltaDayColonizerStatsAffect: default,
                baseColonyStatsAffect: new ColonyStats(0, 0, 3, 0),
                deltaDayColonyStatsAffect: new ColonyStats(3f, 0, 0, 0),
                stats: new StructureStats(500, 100)
                );
        }

        public static Structure SimpleShelter () {
            return new Structure(
                structureEnum: AvailableStructures.SimpleShelter,
                baseColonizerStatsAffect: new ColonizerStats(0, 0, 0, 0, 10f),
                deltaDayColonizerStatsAffect: default,
                baseColonyStatsAffect: new ColonyStats(0, 0, 1, 0),
                deltaDayColonyStatsAffect: default,
                stats: new StructureStats(50, 100)
                );
        }

        public static Structure PotatoFarm () {
            return new Structure(
                structureEnum: AvailableStructures.PotatoFarm,
                baseColonizerStatsAffect: default,
                deltaDayColonizerStatsAffect: default,
                baseColonyStatsAffect: default,
                deltaDayColonyStatsAffect: new ColonyStats(0, 0, 0, 125f),
                stats: new StructureStats(50, 100)
                );
        }

        public static Structure AluminiumMine () {
            return new Structure(
                structureEnum: AvailableStructures.AluminiumMine,
                baseColonizerStatsAffect: default,
                deltaDayColonizerStatsAffect: default,
                baseColonyStatsAffect: default,
                deltaDayColonyStatsAffect: new ColonyStats(-1f, 0, 0, 0),
                stats: new StructureStats(100, 100)
                );
        }


        public static Structure ArmoredBedroom () {
            return new Structure(
                structureEnum: AvailableStructures.ArmoredBedroom,
                baseColonizerStatsAffect: new ColonizerStats(0, 0, 0, 0, 50f),
                deltaDayColonizerStatsAffect: default,
                baseColonyStatsAffect: new ColonyStats(0, 0, 2, 0),
                deltaDayColonyStatsAffect: default,
                stats: new StructureStats(250, 100)
                );
        }

    }
}
