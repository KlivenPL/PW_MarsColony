using MarsColonyEngine.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsColonyEngine.World {
    public class World : IActionHandler {
        public const int size = 50;

        [ActionRequirement(AvailableActions.GenerateNewWorld)]
        internal static bool SprawdzSwiat (ref string dupa) {

            return true;
        }


        [ActionProcedure(AvailableActions.GenerateNewWorld, typeof(World))]
        internal static World GenerateNewWorld (ref string dupa) {
            //  dupa = "chuj";
            return new World();
        }
    }
}
