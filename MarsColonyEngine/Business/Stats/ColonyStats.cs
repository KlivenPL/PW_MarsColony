﻿using MarsColonyEngine.Logger;

namespace MarsColonyEngine.Business.Stats {
    public struct ColonyStats : IStats {
        public float OxygenLevel { get; set; }

        public int Polulation { get; set; }

        public int PopulationLimit { get; set; }

        public IStats Add (IStats other) {
            if (other.GetType().IsEquivalentTo(typeof(ColonyStats))) {
                KLogger.Log.Error($"Cannot add ColonyStats to {other.GetType()}");
                return null;
            }
            var otherColStats = (ColonyStats)other;
            return new ColonyStats {
                OxygenLevel = OxygenLevel + otherColStats.OxygenLevel,
                Polulation = Polulation + otherColStats.Polulation,
                PopulationLimit = PopulationLimit + otherColStats.PopulationLimit,
            };
        }

        public static ColonyStats operator + (ColonyStats a, ColonyStats b) {
            return (ColonyStats)a.Add(b);
        }
    }
}
