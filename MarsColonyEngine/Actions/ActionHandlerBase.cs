namespace MarsColonyEngine.ColonyActions {
    public abstract class ActionHandlerBase : IActionHandler {
        public abstract bool IsActive { get; }

        public abstract AvailableActions[] GetAvailableActions ();
        internal ActionHandlerBase () {
            ColonyActions.RegisterHandler(this);
        }
        ~ActionHandlerBase () {
            ColonyActions.UnregisterHandler(this);
        }
    }
}
