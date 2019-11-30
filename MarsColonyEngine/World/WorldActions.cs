using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsColonyEngine.World {
    internal class WorldActions {

        [ActionRequirement(AvailableActions.GenerateNewWorld_Static_User)]
        private static bool GenerateNewWorldRequirement (ref string res) {
            return ColonyContext.Current.World == null;
        }

        [ActionProcedure(AvailableActions.GenerateNewWorld_Static_User, null)]
        private static World GenerateNewWorldProcedure (ref string res) {
            var world = new World();
            ColonyContext.Current.World = world;
            return world;
        }
    }
}
