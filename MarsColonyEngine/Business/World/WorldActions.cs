using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;

namespace MarsColonyEngine.World {
    internal class WorldActions {

        [ActionRequirement(AvailableActions.GenerateNewWorld_Static_User_Paramless)]
        private static bool GenerateNewWorldRequirement (ref string res) {
            return ColonyContext.Current.World == null;
        }

        [ActionProcedure(AvailableActions.GenerateNewWorld_Static_User_Paramless, null)]
        private static World GenerateNewWorldProcedure (ref string res) {
            var world = new World();
            ColonyContext.Current.World = world;
            return world;
        }

        [ActionRequirement(AvailableActions.NextTurn_Static_User_Paramless)]
        private static bool NextTurnRequirement (ref string res) {
            return ColonyContext.Current.World != null && Simulation.Simulator.Current != null;
        }

        [ActionProcedure(AvailableActions.NextTurn_Static_User_Paramless, null)]
        private static void NextTurnProcedure (ref string res) {
            Simulation.Simulator.Current.NextTurn();
        }

    }
}
