using System;

namespace MarsColonyEngine.ColonyActions {
    [AttributeUsage(AttributeTargets.Method)]
    internal class ActionProcedureAttribute : Attribute {
        private ActionProcedureAttribute () { }
        internal Type HandlerType { get; private set; }
        internal AvailableActions Name { get; private set; }
        internal ActionProcedureAttribute (AvailableActions name, Type handlerType) {
            HandlerType = handlerType;
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    internal class ActionRequirementAttribute : Attribute {
        private ActionRequirementAttribute () { }
        internal AvailableActions Name { get; private set; }
        internal ActionRequirementAttribute (AvailableActions name) {
            Name = name;
        }
    }
}
