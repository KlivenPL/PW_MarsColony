﻿using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Simulation;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Helpers;
using MarsColonyEngine.Logger;
using MarsColonyEngine.Technical;
using MarsColonyEngine.Technical.Misc;
using System;

namespace MarsColonyEngine.Colonizers {
    public abstract class Colonizer : Registrator, IActionHandler, IColonyStatsAffector, IDestructable, IOnFirstTurnStartedRec, IOnTurnStartedRec, IOnTurnFinishedRec {
        public ColonizerStats Stats { get; private set; } = new ColonizerStats();
        public ColonyStats BaseColonyStatsAffect { get; private set; } = new ColonyStats();
        public ColonyStats DeltaDayColonyStatsAffect { get; private set; } = new ColonyStats();
        public bool IsAlive => Stats.HP > 0;
        public Action Destroy => () => {
            Unregister();
            ColonyContext.Current.Colonizers.Remove(this);
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
            DeltaDayColonyStatsAffect.Modify(new ColonyStats(
            oxygen: -0.78f * KRandom.Float(0.9f, 1.1f),
            population: 0,
            populationLimit: 0,
            food: -(int)Stats.Hunger
            ));
        }

        public void OnTurnStarted () {
            DeltaDayColonyStatsAffect.Modify(new ColonyStats());
            if (IsAlive == false) {
                Destroy();
                KLogger.Log.Message("Colonizer " + Name + "(" + GetType().Name + ") has died");
            }
        }
    }
}
