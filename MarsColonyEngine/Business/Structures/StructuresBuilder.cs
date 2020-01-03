using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Stats;

namespace MarsColonyEngine.Business.Structures {
    public static class StructuresBuilder {
        public static Structure RescueCapsule () {
            return new Structure(
                name: "RescueCapsule",
                baseColonizerStatsAffect: new ColonizerStats(0, 0, 0, 0, 10f),
                deltaDayColonizerStatsAffect: default,
                baseColonyStatsAffect: new ColonyStats(0, 0, 3, 0),
                deltaDayColonyStatsAffect: new ColonyStats(3f, 0, 0, 0),
                stats: new StructureStats(50, 100)
                );
        }

        public static Structure SimpleShelter () {
            return new Structure(
                name: "SimpleShelter",
                baseColonizerStatsAffect: new ColonizerStats(0, 0, 0, 0, 10f),
                deltaDayColonizerStatsAffect: default,
                baseColonyStatsAffect: new ColonyStats(0, 0, 1, 0),
                deltaDayColonyStatsAffect: default,
                stats: new StructureStats(50, 100)
                );
        }

    }
}
