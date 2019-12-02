using MarsColonyEngine.Business.Simulation;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Technical;

namespace MarsColonyEngine.World {
    public class World : Registrator, IActionHandler, IOnFirstTurnStartedRec {
        public const int size = 100;

        public bool IsActive => ColonyContext.IsInitalized;


        public AvailableActions[] GetAvailableActions () {
            return new AvailableActions[] {
            };
        }

        public void OnFirstTurnStarted () {
            Register();
        }
    }
}
