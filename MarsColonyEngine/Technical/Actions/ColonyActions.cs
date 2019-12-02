using MarsColonyEngine.Context;
using MarsColonyEngine.Logger;
using MarsColonyTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace MarsColonyEngine.ColonyActions {
    public static class ColonyActions {
        private static Dictionary<AvailableActions, StoredAction> actions = null;

        private static List<IActionHandler> Handlers { get; set; } = new List<IActionHandler>();

        public static void Initalize () {
            if (ColonyContext.Current == null) {
                KLogger.Log.Error("Cannot initalize Actions - Context does not exist. Load or create new Context first.");
                return;
            }
            RegisterActions();
            GetRequirements();

            ColonyContext.Current.OnUnload += () => {
                Handlers.Clear();
                actions?.Clear();
                actions = null;
            };
        }
        private static void RegisterActions () {
            var tmpActions = Assembly.GetExecutingAssembly().GetTypes().SelectMany(e => e.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static)).
                Where(m => m.GetCustomAttributes(typeof(ActionProcedureAttribute), false).Length > 0);

            actions = new Dictionary<AvailableActions, StoredAction>();

            tmpActions.Select(action => {
                return new KeyValuePair<AvailableActions, MethodInfo>(
                action.GetCustomAttribute<ActionProcedureAttribute>().Name, action);

            }).ToList().ForEach(e => {
                var param = e.Value.GetParameters();
                if (param.Length == 0 || param[0].ParameterType.IsByRef == false)
                    KLogger.Log.Error("Procedure actions 1st parameter must be an out string result.");

                actions.Add(e.Key, new StoredAction {
                    procedure = e.Value,
                    procedureAttribute = e.Value.GetCustomAttribute<ActionProcedureAttribute>()

                });
            }
            );
        }

        private static void GetRequirements () {
            var tmpActions = Assembly.GetExecutingAssembly().GetTypes().SelectMany(e => e.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static))
                .Where(m => m.GetCustomAttributes(typeof(ActionRequirementAttribute), false).Length > 0);
            tmpActions.Select(action => {
                return new KeyValuePair<AvailableActions, MethodInfo>(
                action.GetCustomAttribute<ActionRequirementAttribute>().Name, action);

            }).ToList().ForEach(e => {
                if (e.Value.ReturnType != typeof(bool)) {
                    KLogger.Log.Error("Requirement actions must return bool.");
                    return;
                }
                var param = e.Value.GetParameters();
                if (param.Length != 1 || param[0].ParameterType.IsByRef == false) {
                    KLogger.Log.Error("Requirement actions must have only 1 parameter: out string result.");
                    return;
                }
                actions[e.Key].requirement = e.Value;
                actions[e.Key].requirementAttribute = e.Value.GetCustomAttribute<ActionRequirementAttribute>();

            });
        }

        public static object ExecuteAction<T> (AvailableActions actionName, T handler, params object[] args) where T : IActionHandler {
            string result = "Action execution failed.";
            if (actions == null) {
                KLogger.Log.Error("Actions.Initalize() must be called before using Actions.");
                return default;
            }
            if (actions.ContainsKey(actionName) == false) {
                KLogger.Log.Error("Given action is not registered.");
                return default;
            }
            var storedAction = actions[actionName];

            if (handler == null) {
                if (storedAction.procedure.IsStatic == false) {
                    KLogger.Log.Error("This action requires ActionHandler. Call Actions.GetActionHandlers() to get available Action Handlers.");
                    return default;
                }
            } else {
                if (handler.GetType() != storedAction.procedureAttribute.HandlerType) {
                    KLogger.Log.Error($"This action requires ActionHandler of type {storedAction.procedureAttribute.HandlerType.Name}. Call Actions.GetActionHandlers() to get available Action Handlers.");
                    return default;
                }
            }
            var parameters = storedAction.procedure.GetParameters();
            if (args == null && parameters.Length > 1 || parameters.Length - 1 != args.Length) {
                KLogger.Log.Error("This action requires parameters of type: " + string.Join(", ", parameters.Select(p => $"{p.Name}: {p.ParameterType.Name}").ToArray()));
                return default;
            }
            for (int i = 0; i < parameters.Length - 1; i++) {
                //var type1 = parameters[i + 1].ParameterType.GetType();
                //var type2 = args[i].GetType();

                if (parameters[i + 1].ParameterType.GetType() != args[i].GetType()) {
                    KLogger.Log.Error("This action requires parameters of type: " + string.Join(", ", parameters.Select(p => $"{p.Name}: {p.ParameterType.Name}").ToArray()));
                    return default;
                }
            }

            if (CheckIfRequirementsAreMet(actionName, handler, ref result) == false) {
                KLogger.Log.Whisper($"Requirements for {actionName.GetDescription()} action are not met.");
                return default;
            }

            var mergedArgs = new object[] { result }.Concat(args).ToArray();
            var outcome = storedAction.procedure.Invoke(handler, mergedArgs);
            result = (string)mergedArgs[0] == result ? "Action executed successfully." : (string)mergedArgs[0];
            KLogger.Log.Quiet(result);
            return outcome;
        }

        internal static bool CheckIfRequirementsAreMet<T> (AvailableActions actionName, T handler, ref string result) where T : IActionHandler {
            if (actions == null) {
                KLogger.Log.Error("Actions.Initalize() must be called before using Actions.");
                return default;
            }
            if (ColonyContext.IsInitalized == false)
                return false;
            if (handler != null && handler.IsActive == false)
                return false;
            var storedAction = actions[actionName];
            if (storedAction.requirement == null)
                return true;
            var par = new object[] { result };
            if ((bool)storedAction.requirement.Invoke(handler, par) == false) {
                result = (string)par[0] == result ? "Action cannot be executed in this circumstances." : (string)par[0];
                return false;
            }
            return true;
        }

        public static AvailableActions[] GetAvailableActions (IActionHandler handler) {
            if (actions == null) {
                KLogger.Log.Error("Actions.Initalize() must be called before using Actions.");
                return default;
            }
            string result = "";
            return handler.GetAvailableActions().Where(e => CheckIfRequirementsAreMet(e, handler, ref result)).ToArray();
        }

        public static AvailableActions[] GetAvailableActions () {
            if (actions == null) {
                KLogger.Log.Error("Actions.Initalize() must be called before using Actions.");
                return default;
            }
            string str = "";
            return Handlers.SelectMany(handler => GetAvailableActions(handler))
                .Concat(((AvailableActions[])Enum.GetValues(typeof(AvailableActions)))
                .Where(actionEnum => actionEnum.ToString().ToLower().Contains("static") && CheckIfRequirementsAreMet<IActionHandler>(actionEnum, null, ref str)))
                .Distinct().ToArray();
        }

        internal static void RegisterHandler (IActionHandler handler) {
            Handlers.Add(handler);
        }
        internal static void UnregisterHandler (IActionHandler handler) {
            Handlers.Remove(handler);
        }

        private class StoredAction {
            public MethodInfo requirement;
            public MethodInfo procedure;
            public ActionRequirementAttribute requirementAttribute;
            public ActionProcedureAttribute procedureAttribute;
        }
    }
}
