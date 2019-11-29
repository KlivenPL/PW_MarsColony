using MarsColonyEngine.Actions;
using MarsColonyEngine.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsColonyGame {
    class Program {
        static void Main (string[] args) {
            Actions.RegisterActions();
            var w = Actions.ExecuteAction<World>(AvailableActions.GenerateNewWorld, null, out string result);
            Console.WriteLine(result);
        }
    }
}
