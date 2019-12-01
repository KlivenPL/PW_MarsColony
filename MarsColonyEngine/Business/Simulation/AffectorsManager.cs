using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Stats;
using System.Collections.Generic;

namespace MarsColonyEngine.Business.Simulation {
    internal class AffectorsManager {
        private List<IColonyStatsAffector> colonyStatsAffectors = new List<IColonyStatsAffector>();
        internal void RegisterColonyStatsAffector (IColonyStatsAffector affector) {
            colonyStatsAffectors.Add(affector);
        }
        internal void UnregisterColonyStatsAffector (IColonyStatsAffector affector) {
            colonyStatsAffectors.Remove(affector);
        }
        internal ColonyStats GetTotalColonyStats (out ColonyStats baseAffect, out ColonyStats deltaDayAffect) {
            baseAffect = new ColonyStats();
            deltaDayAffect = new ColonyStats();

            foreach (var affector in colonyStatsAffectors) {
                baseAffect = (ColonyStats)baseAffect.Add(affector.BaseColonyStatsAffect);
                deltaDayAffect = (ColonyStats)deltaDayAffect.Add(affector.DeltaDayColonyStatsAffect);

            }
            return (ColonyStats)baseAffect.Add(deltaDayAffect);
        }


        private List<IColonizerStatsAffector> colonizerStatsAffectors = new List<IColonizerStatsAffector>();
        internal void RegisterColonizerStatsAffector (IColonyStatsAffector affector) {
            colonyStatsAffectors.Add(affector);
        }
        internal void UnregisterColonizerStatsAffector (IColonyStatsAffector affector) {
            colonyStatsAffectors.Remove(affector);
        }
        internal ColonizerStats GetTotalColonizerStats (out ColonizerStats baseAffect, out ColonizerStats deltaDayAffect) {
            baseAffect = new ColonizerStats();
            deltaDayAffect = new ColonizerStats();

            foreach (var affector in colonizerStatsAffectors) {
                baseAffect = (ColonizerStats)baseAffect.Add(affector.BaseColonizerStatsAffect);
                deltaDayAffect = (ColonizerStats)deltaDayAffect.Add(affector.DeltaDayColonizerStatsAffect);

            }
            return (ColonizerStats)baseAffect.Add(deltaDayAffect);
        }
    }
}
