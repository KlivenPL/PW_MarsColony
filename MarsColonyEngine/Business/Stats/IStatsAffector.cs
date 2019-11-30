namespace MarsColonyEngine.Business.Stats {
    interface IStatsAffector {
        IStats BaseStats { get; }
        IStats DayDeltaStats { get; }

        void RegisterStatsAffector ();
        void UnregisterStatsAffector ();
    }
}
