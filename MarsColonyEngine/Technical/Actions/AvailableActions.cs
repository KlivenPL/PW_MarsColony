using System.ComponentModel;

namespace MarsColonyEngine.ColonyActions {

    /// Naming convention: [Name]_[Static/Handler]_[User/Simulation/Both_[Args/Paramless]
    public enum AvailableActions {
        [Description("Generate new world ( Handler: none, args: none)")]
        GenerateNewWorld_Static_User_Paramless,

        [Description("Spawn Colonizer ( Handler: none, args: <type: [explorer/scientist/engineer]> )")]
        SpawnColonizer_Static_Simulation_Args,

        [Description("Build Simple Shelter. Requires 50 Aluminium ( Handler: engineer, args: none )")]
        BuildStructureSimpleShelter_Handler_User_Paramless,

        [Description("Next turn ( Handler: none, args: none )")]
        NextTurn_Static_User_Paramless,

        [Description("Build rescue capsule ( Handler: none, args: none)")]
        BuildRescueCapsule_Static_User_Paramless,

        [Description("Order an Explorer to explore. ( Handler: explorer, args: none)")]
        Explore_Handler_User_Paramless,

        [Description("Build Aluminium Mine. Requires 1 Aluminium Ore Map ( Handler: engineer, args: none)")]
        BuildStructureAluminiumMine_Handler_User_Paramless,

        [Description("Build Potato farm. Requires 25 Aluminium ( Handler: engineer, args: none)")]
        BuildStructurePotatoFarm_Handler_User_Paramless,

        [Description("Build Oxygen Generator. Requires 25 Aluminium ( Handler: engineer, args: none)")]
        BuildStructureOxygenGenerator_Handler_User_Paramless,
    }
}
