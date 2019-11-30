namespace MarsColonyEngine.ColonyActions {
    public interface IActionHandler {

        bool IsActive { get; }
        AvailableActions[] GetAvailableActions ();
    }
}