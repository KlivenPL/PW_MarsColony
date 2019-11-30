using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using System;

namespace MarsColonyEngine.Colonizers {
    internal class ColonizerActions {

        [ActionRequirement(AvailableActions.SpawnColonizer_Static_Simulation)]
        private static bool SpawnColonizerRequirement (ref string res) {
            return ColonyContext.Current.World != null;
        }

        [ActionProcedure(AvailableActions.SpawnColonizer_Static_Simulation, null)]
        private static Colonizer SpawnColonizerProcedure (ref string res, Type type) {
            Colonizer spawnedColonizer = null;
            if (type == typeof(Engineer))
                spawnedColonizer = new Engineer();
            else
            if (type == typeof(Scientist))
                spawnedColonizer = new Scientist();
            else
            if (type == typeof(Explorer))
                spawnedColonizer = new Explorer();

            return spawnedColonizer;
        }
    }
}
