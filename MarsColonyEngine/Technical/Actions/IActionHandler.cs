using MarsColonyEngine.Misc;

namespace MarsColonyEngine.ColonyActions {
    public interface IActionHandler : IIdentifiable {

        bool IsActive { get; }
        AvailableActions[] GetAvailableActions ();
    }
}