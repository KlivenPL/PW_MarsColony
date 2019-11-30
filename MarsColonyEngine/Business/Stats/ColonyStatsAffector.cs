using MarsColonyEngine.Simulation;

namespace MarsColonyEngine.Business.Stats {
    internal abstract class ColonyStatsAffector : IStatsAffector {
        internal ColonyStats BaseColonyStats { get; private set; }
        internal ColonyStats DeltaDayColonyStats { get; private set; }
        public IStats BaseStats => BaseColonyStats;

        public IStats DayDeltaStats => DeltaDayColonyStats;

        public ColonyStatsAffector () {
            RegisterStatsAffector();
        }
        ~ColonyStatsAffector () {
            UnregisterStatsAffector();
        }

        public void RegisterStatsAffector () {
            Simulator.Current?.RegisterStatsAffector(this);
        }
        public void UnregisterStatsAffector () {
            Simulator.Current?.UnregisterStatsAffector(this);
        }
    }
}
