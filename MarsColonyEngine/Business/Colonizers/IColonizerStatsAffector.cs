using MarsColonyEngine.Business.Stats;

namespace MarsColonyEngine.Business.Colonizers {
    interface IColonizerStatsAffector : IStatsAffector {
        ColonizerStats BaseColonizerStatsAffect { get; }
        ColonizerStats DeltaDayColonizerStatsAffect { get; }
    }
}
