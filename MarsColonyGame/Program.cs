using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Colonizers;
using MarsColonyEngine.Context;
using MarsColonyEngine.Logger;
using MarsColonyEngine.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsColonyGame {
    class Program {
        static void Main (string[] args) {
            KLogger.LogQuietMessages = true;
            ColonyContext.Create();
            ColonyActions.Initalize();
            var actions = ColonyActions.GetAvailableActions();
            KLogger.Log.Message(string.Join(", ", actions));
           // var w = ColonyActions.ExecuteAction<World>(AvailableActions.GenerateNewWorld, null);


            var xd = new Engineer();

        }
    }
}
