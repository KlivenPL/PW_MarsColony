using System.ComponentModel;

namespace MarsColonyEngine.ColonyActions {

    /// Naming convention: [Name]_[Static/Handler]_[User/Simulation/Both_[Args/Paramless]
    public enum AvailableActions {
        [Description("Generate new world. ( Handler: none, args: none)")]
        GenerateNewWorld_Static_User_Paramless,
        [Description("Spawn Colonizer. ( Handler: none, args: <type: [explorer/scientist/engineer]> )")]
        SpawnColonizer_Static_Simulation_Args,
        [Description("Build Simple Shelter structure. ( Handler: engineer, args: none )")]
        BuildStructureSimpleShelter_Handler_User_Paramless,
        NextTurn_Static_User_Paramless
    }
}
