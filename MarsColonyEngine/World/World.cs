using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using Newtonsoft.Json.Linq;

namespace MarsColonyEngine.World {
    public class World : ActionHandlerBase {
        public const int size = 100;

        public override bool IsActive => ColonyContext.IsInitalized;
        public World GetContext () {
            return this;
        }

        public JObject SerializeContext () {
            return JObject.FromObject(this);
        }

        public override AvailableActions[] GetAvailableActions () {
            return new AvailableActions[] {
            };
        }
    }
}
