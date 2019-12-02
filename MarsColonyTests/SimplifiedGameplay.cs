using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Logger;
using MarsColonyEngine.World;
using System;
using System.Text;

namespace MarsColonyTests {
    class SimplifiedGameplay {
        static void Main (string[] args) {
            KLogger.LogQuietMessages = true;
            KLogger.LogWhisperMessages = true;
            ColonyContext.Create();
            ColonyActions.Initalize();
            var sg = new SimplifiedGameplay();
            while (true) {
                sg.MainGameLoop();
            }
        }

        Views currentView = Views.ShowAvailableActions;
        void MainGameLoop () {
            switch (currentView) {
                case Views.ShowAvailableActions:
                    Render(ShowAvailableActions());
                break;
            }
        }
        AvailableActions[] availableActions = null;
        string ShowAvailableActions () {
            StringBuilder sb = new StringBuilder();
            sb.Append("Available actions: \r\n");
            if (availableActions == null) {
                availableActions = ColonyActions.GetAvailableActions();
            }

            int i = 0;
            foreach (Enum action in availableActions) {
                sb.Append($"{i++}: ");
                sb.Append(action.GetDescription()).Append("\r\n");
            }
            if(CheckIfChosen(availableActions, out var chosenAction)){
                KLogger.Log.Message("\nChosen action: " + chosenAction.GetDescription());
                KLogger.Log.Message("Enter args and press enter:");
                var read = Console.ReadLine();
                ExecuteAction(chosenAction, read);
                //prevRender = "";
                availableActions = null;
            }
            
            return sb.ToString();
        }

        void ExecuteAction(AvailableActions action, string args) {
            var rawArgs = args.Split(new string[] { ",", "\n", "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries);
            switch (action) {
                case AvailableActions.GenerateNewWorld_Static_User:
                    ColonyActions.ExecuteAction<World>(AvailableActions.GenerateNewWorld_Static_User, null);
                    break;
                case AvailableActions.SpawnColonizer_Static_Simulation:
                    ColonyActions.ExecuteAction<World>(AvailableActions.GenerateNewWorld_Static_User, null, Type.GetType(rawArgs[0], false, true));
                    break;
                case AvailableActions.BuildStructureSimpleShelter_Handler_User:
                    break;
            }
        }

        bool CheckIfChosen<T> (T[] array, out T chosen) {
            chosen = default;
            var value = Input.PressedKey;
            if (value == null)
                return false;
            if (int.TryParse(value.Value.KeyChar.ToString(), out int index) == false)
                return false;
            if (index >= array.Length)
                return false;
            chosen = array[index];
            return true;
        }


        string prevRender;
        void Render(string render) {
            if (render != prevRender) {
                KLogger.Log.Message(render);
                prevRender = render;
            }
        }

        public enum Views {
            ShowAvailableActions
        }
    }
}
