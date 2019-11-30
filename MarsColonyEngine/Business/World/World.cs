using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;

namespace MarsColonyEngine.World {
    public class World : ActionHandlerBase {
        public const int size = 100;

        public override bool IsActive => ColonyContext.IsInitalized;

        public override AvailableActions[] GetAvailableActions () {
            return new AvailableActions[] {
            };
        }
    }
}
