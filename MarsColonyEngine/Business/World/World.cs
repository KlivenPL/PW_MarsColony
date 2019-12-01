using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Technical;

namespace MarsColonyEngine.World {
    public class World : Registrator, IActionHandler {
        public const int size = 100;

        public bool IsActive => ColonyContext.IsInitalized;

        internal World () {
            Register();
        }

        public AvailableActions[] GetAvailableActions () {
            return new AvailableActions[] {
            };
        }
    }
}
