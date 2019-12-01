using MarsColonyEngine.Business.Stats;

namespace MarsColonyEngine.Simulation {
    public struct Turn {
        public ColonyStats BaseColonyStats { get; private set; }
        public ColonyStats DeltaDayColonyStats { get; private set; }
        public ColonyStats TotalColonyStats => BaseColonyStats + DeltaDayColonyStats;

        public int Day { get; internal set; }
        public int AvailableMoves {
            get {
                return ColonyActions.ColonyActions.GetAvailableActions().Length;
            }
        }

        internal Turn (int day, ColonyStats baseColonyStats, ColonyStats deltaDayColonyStats) {
            Day = day;
            BaseColonyStats = baseColonyStats;
            DeltaDayColonyStats = deltaDayColonyStats;
        }

    }
}
