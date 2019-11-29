using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MarsColonyEngine.Actions {
    public static class Actions {
        private static Dictionary<AvailableActions, StoredAction> actions = null;
        public static void RegisterActions () {
            var tmpActions = Assembly.GetExecutingAssembly().GetTypes().SelectMany(e => e.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static)).
                Where(m => m.GetCustomAttributes(typeof(ActionProcedureAttribute), false).Length > 0);
            actions = new Dictionary<AvailableActions, StoredAction>();
            tmpActions.Select(action => {
                return new KeyValuePair<AvailableActions, MethodInfo>(
                action.GetCustomAttribute<ActionProcedureAttribute>().Name, action);
            }).ToList().ForEach(e => {
                var param = e.Value.GetParameters();
                if (param.Length == 0 || param[0].ParameterType.IsByRef == false)
                    throw new Exception("Procedure actions 1st parameter must be an out string result.");
                actions.Add(e.Key, new StoredAction {
                    procedure = e.Value,
                    procedureAttribute = e.Value.GetCustomAttribute<ActionProcedureAttribute>()
                });
                }
            );

            GetRequirements();
        }
        private static void GetRequirements () {
            var tmpActions = Assembly.GetExecutingAssembly().GetTypes().SelectMany(e => e.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static)).
            Where(m => m.GetCustomAttributes(typeof(ActionRequirementAttribute), false).Length > 0);
            tmpActions.Select(action => {
                return new KeyValuePair<AvailableActions, MethodInfo>(
                action.GetCustomAttribute<ActionRequirementAttribute>().Name, action);
            }).ToList().ForEach(e => {
                if (e.Value.ReturnType != typeof(bool))
                    throw new Exception("Requirement actions must return bool.");
                var param = e.Value.GetParameters();
                if (param.Length != 1 || param[0].ParameterType.IsByRef == false)
                    throw new Exception("Requirement actions must have only 1 parameter: out string result.");
                actions[e.Key].requirement = e.Value;
                actions[e.Key].requirementAttribute = e.Value.GetCustomAttribute<ActionRequirementAttribute>();
            });
        }

        public static T ExecuteAction<T> (AvailableActions actionName, T handler, out string result, params object[] args) where T : IActionHandler {
            result = "Action execution failed.";
            if (actions == null) {
                throw new Exception("Actions.RegisterActions() must be called before using Actions.");
            }
            if (actions.ContainsKey(actionName) == false) {
                throw new Exception("Given action is not registered.");
            }
            var storedAction = actions[actionName];

            if (handler == null) {
                if (storedAction.procedure.IsStatic == false) {
                    throw new Exception("This action requires ActionHandler. Call Actions.GetActionHandlers() to get available Action Handlers.");
                }
            } else {
                if (handler.GetType() != storedAction.procedureAttribute.HandlerType) {
                    throw new Exception($"This action requires ActionHandler of type {storedAction.procedureAttribute.HandlerType.Name}. Call Actions.GetActionHandlers() to get available Action Handlers.");
                }
            }
            var parameters = storedAction.procedure.GetParameters();
            if (args == null && parameters.Length > 1 || parameters.Length-1 != args.Length) {
                throw new Exception("This action requires parameters of type: " + string.Join(", ", parameters.Select(p => $"{p.Name}: {p.ParameterType.Name}").ToArray()));
            }
            for (int i = 0; i < parameters.Length-1; i++) {
                if (parameters[i+1].ParameterType != args[i].GetType())
                    throw new Exception("This action requires parameters of type: " + string.Join(", ", parameters.Select(p => $"{p.Name}: {p.ParameterType.Name}").ToArray()));
            }

            if (storedAction.requirement != null) {
                var par = new object[] { result };
                if ((bool)storedAction.requirement.Invoke(handler, par) == false) {
                    result = (string)par[0] == result ? "Action cannot be executed in this circumstances." : (string)par[0];
                    return default;
                }
            }

            var mergedArgs = new object[] { result }.Concat(args).ToArray();
            var outcome = (T)storedAction.procedure.Invoke(handler, mergedArgs);
            result = (string)mergedArgs[0] == result ? "Action executed successfully." : (string)mergedArgs[0];
            return outcome;
        }

        private class StoredAction {
            public MethodInfo requirement;
            public MethodInfo procedure;
            public ActionRequirementAttribute requirementAttribute;
            public ActionProcedureAttribute procedureAttribute;
        }
    }
}
