using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Simulation;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Logger;
using MarsColonyEngine.Technical;
using MarsColonyEngine.Technical.Misc;
using System;

namespace MarsColonyEngine.Colonizers {
    public abstract class Colonizer : Registrator, IActionHandler, IColonyStatsAffector, IDestructable, IOnFirstTurnStartedRec, IOnTurnStartedRec, IOnTurnFinishedRec {
        public ColonizerStats Stats { get; protected set; } = new ColonizerStats();
        public ColonyStats BaseColonyStatsAffect { get; protected set; } = new ColonyStats();
        public ColonyStats DeltaDayColonyStatsAffect { get; protected set; } = new ColonyStats();
        public bool IsAlive => Stats.HP > 0;
        public void AffectHp (float signeDeltaHp) {
            if (signeDeltaHp == 0)
                return;
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(signeDeltaHp + Stats.HP >= 0 ? signeDeltaHp : -Stats.HP, 0, 0, 0, 0));
            KLogger.Log.Message("Colonizer " + Name + "(" + GetType().Name + ") HP affected: " + signeDeltaHp + ", current value: " + Stats.HP);
            if (IsAlive == false) {
                Destroy();
                KLogger.Log.Message("Colonizer " + Name + "(" + GetType().Name + ") has died");
            }
        }
        public Action Destroy => () => {
            Unregister();
        };

        protected Colonizer (string name, ColonizerStats stats, ColonyStats baseColonyStatsAffect, ColonyStats deltaDayColonyStatsAffect) {
            Name = name;
            Stats = stats;
            BaseColonyStatsAffect = baseColonyStatsAffect;
            DeltaDayColonyStatsAffect = deltaDayColonyStatsAffect;
        }

        public virtual bool IsActive => Stats.HP > 0 && Stats.Efficiency > 0;

        public virtual AvailableActions[] GetAvailableActions () {
            return new AvailableActions[]{

            };
        }
        public void OnFirstTurnStarted () {
            Register();
        }

        public void OnTurnFinished () {
            DeltaDayColonyStatsAffect = new ColonyStats(
            oxygen: -Stats.OxygenUsage,
            population: 0,
            populationLimit: 0,
            food: -Stats.Hunger + Math.Min(0, Stats.Efficiency)
            );
        }

        public void OnTurnStarted () {
            DeltaDayColonyStatsAffect = new ColonyStats();
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, 100 - Stats.Efficiency, 0, 0, 0)); // restoring efficiency to 100%
        }
    }
}
