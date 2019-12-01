namespace MarsColonyEngine.Business.Stats {
    interface IColonyStatsAffector : IStatsAffector {
        ColonyStats BaseColonyStatsAffect { get; }
        ColonyStats DeltaDayColonyStatsAffect { get; }
    }
}
