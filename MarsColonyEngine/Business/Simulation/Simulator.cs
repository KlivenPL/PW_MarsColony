using MarsColonyEngine.Business.Simulation;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Context;
using MarsColonyEngine.Logger;
using MarsColonyEngine.Technical.Misc;
using System;
using System.Linq;

namespace MarsColonyEngine.Simulation {
    public class Simulator {
        internal Simulator () { }
        private static Simulator _current;
        internal AffectorsManager affectorsManager = new AffectorsManager();
        public static Simulator Current {
            get {
                if (ColonyContext.Current == null) {
                    KLogger.Log.Error("Simulator cannot be used: ColonyContext does not exist. Use ColonyContext.Load() or ColonyContext.Create() first.");
                    return null;
                }
                return _current ?? (_current = new Simulator());
            }
        }

        internal Action OnTurnFinished { get; private set; }
        internal Action OnTurnStarted { get; private set; }
        internal Action OnFirstTurnStarted { get; private set; }

        internal void RegisterIOnFirstTurnStartedReciever (Action action) {
            OnFirstTurnStarted += action;
        }
        internal void RegisterIOnNextTurnStartedReciever (Action action) {
            OnTurnStarted += action;
        }
        internal void RegisterOnTurnFinishedReviever (Action action) {
            OnTurnFinished += action;
        }

        internal void UnregisterIOnNextTurnStartedReciever (Action action) {
            OnTurnStarted -= action;
        }
        internal void UnregisterOnTurnFinishedReviever (Action action) {
            OnTurnFinished -= action;
        }
        public void NextTurn () {
            OnTurnFinished?.Invoke();
            AffectColonizers();
            AffectStructures();
            var currTurn = ColonyContext.Current.Turn;
            affectorsManager.GetTotalColonyStats(out ColonyStats baseStats, out ColonyStats deltaDayStats);
            currTurn.SetNextTurn(currTurn.DeltaDayColonyStats);
            OnFirstTurnStarted?.Invoke();
            OnFirstTurnStarted = null;
            OnTurnStarted?.Invoke();
        }

        private void AffectColonizers () {
            var totalColonyStats = ColonyContext.Current.Turn.TotalColonyStats;
            var totalDamage = 0f;

            if (totalColonyStats.Food < 0) {
                var totalHunger = ColonyContext.Current.Colonizers.Sum(col => col.Stats.Hunger);
                var deltaHunger = totalColonyStats.Food + totalHunger;
                var hungerDamage = (1 - deltaHunger / totalHunger) * 25;
                totalDamage += hungerDamage;
                KLogger.Log.Message("Colonizers are starving!");
            }

            if (totalColonyStats.Oxygen < 0) {
                var totalOxygenUsage = ColonyContext.Current.Colonizers.Sum(col => col.Stats.OxygenUsage);
                var deltaOxygen = totalColonyStats.Oxygen + totalOxygenUsage;
                var lackOfOxygenDamage = (1 - deltaOxygen / totalOxygenUsage) * 200;
                totalDamage += lackOfOxygenDamage;
                KLogger.Log.Message("Colonizers are suffocating!");
            }

            foreach (IDestructable colonizer in ColonyContext.Current.Colonizers.ToList()) {
                colonizer.AffectHp(-totalDamage);
            }
        }

        private void AffectStructures () {
            KLogger.Log.Whisper("Structures take decay damage. You can order an Engineer to fix Structures.");
            const float damagePerTurn = 5f;
            foreach (IDestructable structure in ColonyContext.Current.Structures.ToList()) {
                structure.AffectHp(-damagePerTurn);
            }
        }
    }
}
