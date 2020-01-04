using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Stats;

namespace MarsColonyEngine.Business.Structures {

    public enum AvailableStructures {
        RescueCapsule,
        SimpleShelter,
        PotatoFarm,
        ArmoredBedroom
    }
    public static class StructuresBuilder {
        public static Structure RescueCapsule () {
            return new Structure(
                structureEnum: AvailableStructures.RescueCapsule,
                name: "Rescue Capsule",
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
                name: "Simple Shelter",
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
                name: "Potato Farm",
                baseColonizerStatsAffect: default,
                deltaDayColonizerStatsAffect: default,
                baseColonyStatsAffect: default,
                deltaDayColonyStatsAffect: new ColonyStats(0, 0, 0, 125f),
                stats: new StructureStats(50, 100)
                );
        }

        public static Structure ArmoredBedroom () {
            return new Structure(
                structureEnum: AvailableStructures.ArmoredBedroom,
                name: "Armored Bedroom",
                baseColonizerStatsAffect: new ColonizerStats(0, 0, 0, 0, 50f),
                deltaDayColonizerStatsAffect: default,
                baseColonyStatsAffect: new ColonyStats(0, 0, 2, 0),
                deltaDayColonyStatsAffect: default,
                stats: new StructureStats(250, 100)
                );
        }

    }
}
