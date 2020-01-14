using ExtentionMethods;
using MarsColonyEngine.Business.Structures;
using MarsColonyEngine.Colonizers;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Logger;
using MarsColonyEngine.Simulation;
using MarsColonyEngine.World;
using System;
using System.IO;
using System.Linq;

namespace MarsColonyTests {
    class SimplifiedGameplay {

        static void Main (string[] args) {
            KLogger.LogQuietMessages = true;
            KLogger.LogWhisperMessages = true;

            bool loaded = false;
            if (args.Length > 0) {
                loaded = ColonyContext.Load(new FileInfo(args[0]));
                if (!loaded) {
                    KLogger.Log.Error("Could not load save file, creating new Context instead...");
                    ColonyContext.Create();
                }
            } else {
                ColonyContext.Create();
            }
            ColonyActions.Initalize();

            if (loaded == false)
                ColonyContext.InitNewContext();

            var sg = new SimplifiedGameplay();
            bool simulationOver = false;

            while (simulationOver == false) {
                simulationOver = Simulator.Current.CheckForGameOver();
                sg.MainGameLoop();
            }
            KLogger.Log.Error("Simulation is over - all Colonizers are dead or all Structures are destroyed!");
            Console.ReadLine();
        }

        Views currentView = Views.ChooseMenu;
        void MainGameLoop () {
            switch (currentView) {
                case Views.ChooseMenu:
                    ChooseMenu();
                    break;
                case Views.ShowAvailableActions:
                    ShowAvailableActions();
                    currentView = Views.ChooseMenu;
                    break;
                case Views.ShowColonyStats:
                    ShowColonyStats();
                    currentView = Views.ChooseMenu;
                    break;
                case Views.ShowColonizerStats:
                    ShowColonizerStats();
                    currentView = Views.ChooseMenu;
                    break;
                case Views.ShowStructuresStats:
                    ShowStructuresStats();
                    currentView = Views.ChooseMenu;
                    break;
                case Views.ListAllItems:
                    ListAllItems();
                    currentView = Views.ChooseMenu;
                    break;
                case Views.Save:
                    Save();
                    currentView = Views.ChooseMenu;
                    break;
            }
        }

        void ChooseMenu () {
            KLogger.Log.Message("\nChoose menu: ");
            var menus = (Views[])Enum.GetValues(typeof(Views));
            menus = menus.Skip(1).ToArray();
            if (CheckIfChosen(menus, out var chosenMenu, true)) {
                currentView = chosenMenu;
            }
        }

        AvailableActions[] availableActions = null;
        void ShowAvailableActions () {
            // StringBuilder sb = new StringBuilder();
            KLogger.Log.Message("Available actions: \r\n");
            if (availableActions == null) {
                availableActions = ColonyActions.GetAvailableActions();
            }

            int i = 1;
            foreach (Enum action in availableActions) {
                KLogger.Log.Message($"{i++}: {action.GetDescription()}");
            }
            if (CheckIfChosen(availableActions, out var chosenAction)) {
                KLogger.Log.Message("\nChosen action: " + chosenAction.GetDescription());
                IActionHandler chosenHandler = null;
                if (chosenAction.ToString().ToLower().Contains("handler")) {
                    KLogger.Log.Message("Choose one of the Handlers shown below:");
                    var availableHandlers = ColonyActions.GetActionHandlers(e => e.GetAvailableActions().Contains(chosenAction));
                    int j = 1;
                    foreach (var handler in availableHandlers) {
                        KLogger.Log.Message(j++ + ": " + handler.Name + $" ({handler.GetType().Name})");
                    }
                    while (CheckIfChosen(availableHandlers, out chosenHandler) == false) {
                        KLogger.Log.Error("Incorrect handler was chosen. Enter handler id and try again");
                    }
                }
                if (ExecuteAction(chosenAction, out string result, chosenHandler) == false) {
                    KLogger.Log.Error(result);
                }
                availableActions = null;
            }
        }

        void ShowColonyStats () {
            KLogger.Log.Message("\nColony stats:");
            KLogger.Log.Message($"Day: { ColonyContext.Current.Turn.Day }\n");
            KLogger.Log.Message("Total stats:");
            KLogger.Log.Message(ColonyContext.Current.Turn.TotalColonyStats.ToString());
        }

        void ShowColonizerStats () {
            var colonizers = ColonyContext.Current.Colonizers.ToArray();
            if (colonizers.Length == 0) {
                KLogger.Log.Message("No colonizers were spawned.");
                return;
            }
            KLogger.Log.Message("Choose one of the Colonizers shown below:");
            int j = 1;
            foreach (var handler in colonizers) {
                KLogger.Log.Message(j++ + ": " + handler.Name + $" ({handler.GetType().Name})");
            }
            if (CheckIfChosen(colonizers, out var chosenColonizer)) {
                KLogger.Log.Message($"Colonizer {chosenColonizer.Name} stats:");
                KLogger.Log.Message(chosenColonizer.Stats.ToString());
            }
        }

        void ShowStructuresStats () {
            var structures = ColonyContext.Current.Structures.ToArray();
            if (structures.Length == 0) {
                KLogger.Log.Message("No structures were built.");
                return;
            }
            KLogger.Log.Message("Choose one of the Structures shown below:");
            int j = 1;
            foreach (var structure in structures) {
                KLogger.Log.Message(j++ + ": " + structure.Name);
            }
            if (CheckIfChosen(structures, out var chosenStructure)) {
                KLogger.Log.Message($"Structure {chosenStructure.Name} stats:");
                KLogger.Log.Message(chosenStructure.Stats.ToString());
            }
        }

        void ListAllItems () {
            var items = ColonyContext.Current.Items;
            if (items.Count == 0) {
                KLogger.Log.Message("No items available.");
                return;
            }
            KLogger.Log.Message("\nAvailable Items:");
            int i = 1;
            foreach (var item in items) {
                KLogger.Log.Message($"{i}: {item.Amount}x {item.Name}");
                i++;
            }
        }

        bool ExecuteAction (AvailableActions action, out string result, IActionHandler handler = null) {
            result = "OK";
            switch (action) {
                case AvailableActions.GenerateNewWorld_Static_User_Paramless:
                    ColonyActions.ExecuteAction<World>(AvailableActions.GenerateNewWorld_Static_User_Paramless, null);
                    break;
                case AvailableActions.SpawnColonizer_Static_Simulation_Args:
                    var availableColonizerTypes = new string[] { nameof(Engineer), nameof(Scientist), nameof(Explorer) };
                    if (CheckIfChosen<string>(availableColonizerTypes, out string chosenColonizerType, true)) {
                        ColonyActions.ExecuteAction<Engineer>(AvailableActions.SpawnColonizer_Static_Simulation_Args, null, chosenColonizerType);
                    }
                    break;
                case AvailableActions.BuildStructureSimpleShelter_Handler_User_Paramless:
                    ColonyActions.ExecuteAction<Engineer>(AvailableActions.BuildStructureSimpleShelter_Handler_User_Paramless, (Engineer)handler);
                    break;
                case AvailableActions.NextTurn_Static_User_Paramless:
                    ColonyActions.ExecuteAction<World>(AvailableActions.NextTurn_Static_User_Paramless, null);
                    break;
                case AvailableActions.BuildRescueCapsule_Static_User_Paramless:
                    ColonyActions.ExecuteAction<World>(AvailableActions.BuildRescueCapsule_Static_User_Paramless, null);
                    break;
                case AvailableActions.Explore_Handler_User_Paramless:
                    ColonyActions.ExecuteAction<Explorer>(AvailableActions.Explore_Handler_User_Paramless, (Explorer)handler);
                    break;
                case AvailableActions.DoResearch_Handler_User_Paramless:
                    ColonyActions.ExecuteAction<Scientist>(AvailableActions.DoResearch_Handler_User_Paramless, (Scientist)handler);
                    break;
                case AvailableActions.BuildStructureAluminiumMine_Handler_User_Paramless:
                    ColonyActions.ExecuteAction<Engineer>(AvailableActions.BuildStructureAluminiumMine_Handler_User_Paramless, (Engineer)handler);
                    break;
                case AvailableActions.BuildStructurePotatoFarm_Handler_User_Paramless:
                    ColonyActions.ExecuteAction<Engineer>(AvailableActions.BuildStructurePotatoFarm_Handler_User_Paramless, (Engineer)handler);
                    break;
                case AvailableActions.BuildStructureOxygenGenerator_Handler_User_Paramless:
                    ColonyActions.ExecuteAction<Engineer>(AvailableActions.BuildStructureOxygenGenerator_Handler_User_Paramless, (Engineer)handler);
                    break;
                case AvailableActions.BuildStructureResearchStation_Handler_User_Paramless:
                    ColonyActions.ExecuteAction<Engineer>(AvailableActions.BuildStructureResearchStation_Handler_User_Paramless, (Engineer)handler);
                    break;
                case AvailableActions.BuildStructureArmoredBedroom_Handler_User_Paramless:
                    ColonyActions.ExecuteAction<Engineer>(AvailableActions.BuildStructureArmoredBedroom_Handler_User_Paramless, (Engineer)handler);
                    break;
                case AvailableActions.BuildStructureRepairStation_Handler_User_Paramless:
                    ColonyActions.ExecuteAction<Engineer>(AvailableActions.BuildStructureRepairStation_Handler_User_Paramless, (Engineer)handler);
                    break;
                case AvailableActions.RepairStructure_Handler_User_Args:
                    var structuresToRepair = ColonyContext.Current.Structures.Where(e => e.Stats.HP != e.Stats.MaxHP).ToArray();
                    var descriptions = structuresToRepair.Select(e => $"{e.Name}: {e.Stats.HP} / {e.Stats.MaxHP} HP").ToArray();

                    if (CheckIfChosen<Structure>(structuresToRepair, out Structure structureToRepair, true, descriptions)) {
                        ColonyActions.ExecuteAction<Engineer>(AvailableActions.RepairStructure_Handler_User_Args, (Engineer)handler, structureToRepair);
                    }
                    break;
            }
            return true;
        }

        bool CheckIfChosen<T> (T[] array, out T chosen, bool printOptions = false, string[] optionDescriptions = null) {
            int i = 1;
            if (printOptions) {
                foreach (var item in array) {
                    KLogger.Log.Message(i + ": " + (optionDescriptions == null ? item.ToString().SplitCamelCase() : optionDescriptions[i - 1]));
                    i++;
                }
            }
            chosen = default;
            var value = Console.ReadLine();
            if (value == null)
                return false;
            if (int.TryParse(value, out int index) == false)
                return false;
            index--;
            if (index >= array.Length || index < 0)
                return false;
            chosen = array[index];
            return true;
        }


        void Save () {
            KLogger.Log.Message("\nEnter save file name:");
            var save = Console.ReadLine();
            save = MakeValidFileName(save);
            ColonyContext.Save(new FileInfo(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/" + save + ".json"));
            KLogger.Log.Message("In order to load saved file, run this executable with full path to saved file as a parameter.");
        }
        private static string MakeValidFileName (string name) {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }
        public enum Views {
            ChooseMenu,
            ShowAvailableActions,
            ShowColonyStats,
            ShowColonizerStats,
            ShowStructuresStats,
            ListAllItems,
            Save
        }
    }
}
