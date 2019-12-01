using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Simulation;

namespace MarsColonyEngine.Technical {
    public abstract class Registrator {
        public Registrator () {
            if (this is IActionHandler) {
                ColonyActions.ColonyActions.RegisterHandler((IActionHandler)this);
            }
            if (this is IStatsAffector) {
                Simulator.Current?.RegisterStatsAffector((IStatsAffector)this);
            }
        }

        ~Registrator () {
            if (this is IActionHandler) {
                ColonyActions.ColonyActions.UnregisterHandler((IActionHandler)this);
            }
            if (this is IStatsAffector) {
                Simulator.Current?.UnregisterStatsAffector((IStatsAffector)this);
            }
        }
    }
}
