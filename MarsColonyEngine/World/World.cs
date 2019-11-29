using MarsColonyEngine.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsColonyEngine.World {
    public class World : IActionHandler {
        public const int size = 50;

        [ActionRequirement(AvailableActions.GenerateNewWorld)]
        internal static bool SprawdzSwiat (ref string res) {

            return false;
        }


        [ActionProcedure(AvailableActions.GenerateNewWorld, null)]
        internal static World GenerateNewWorld (ref string res) {
            return new World();
        }

        public AvailableActions[] GetAvailableActions () {
            return new AvailableActions[] {
                AvailableActions.GenerateNewWorld,
            };
        }
    }
}
