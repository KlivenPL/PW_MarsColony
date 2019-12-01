namespace MarsColonyEngine.Business.Stats {
    interface IStatsAffector {
        IStats BaseStats { get; }
        IStats DeltaDayStats { get; }
    }
}
