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
using MarsColonyEngine.Simulation;
using MarsColonyEngine.Business.Structures;

namespace MarsColonyTests {
    class Program {
        static void Main (string[] args) {

            ColonyContext.Create();
            ColonyActions.Initalize();
            var actions = ColonyActions.GetAvailableActions();
            var world = ColonyActions.ExecuteAction<World>(AvailableActions.GenerateNewWorld_Static_User, null);
            actions = ColonyActions.GetAvailableActions();
            var engineer = ColonyActions.ExecuteAction<Engineer>(AvailableActions.SpawnColonizer_Static_Simulation, null, typeof(Engineer));

            var structure = new Structure(StructuresSeeds.AvailableStructures.Where(e => e.Name.ToLower().Contains("shelter")).FirstOrDefault());

            while (true) {
                KLogger.Log.Message($"Day: {ColonyContext.Current.Turn.Day}" +
                    $", population: {ColonyContext.Current.Turn.TotalColonyStats.Population}" +
                    $" oxygen: {ColonyContext.Current.Turn.TotalColonyStats.Oxygen}" +
                    $" maxPopulation: {ColonyContext.Current.Turn.TotalColonyStats.PopulationLimit}");
                Console.ReadKey();
                Simulator.Current.NextTurn();
                ColonyActions.ExecuteAction<Engineer>(AvailableActions.SpawnColonizer_Static_Simulation, null, typeof(Engineer));
            }

            /* KLogger.LogQuietMessages = true;
             ColonyContext.Create();
             ColonyActions.Initalize();
             var actions = ColonyActions.GetAvailableActions();
             KLogger.Log.Message(string.Join(", ", actions));*/
            /*var w = ColonyActions.ExecuteAction<World>(AvailableActions.GenerateNewWorld_Static_User, null);
            var eng = ColonyActions.ExecuteAction<Explorer>(AvailableActions.SpawnColonizer_Static_Simulation, null, typeof(World));

            var x2d = ColonyContext.Current.Colonizers;

            var xd = new Engineer();
            */
        }
    }
}
