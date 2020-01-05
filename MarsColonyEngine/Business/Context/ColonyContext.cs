using MarsColonyEngine.Business.Items;
using MarsColonyEngine.Business.Structures;
using MarsColonyEngine.Colonizers;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Logger;
using MarsColonyEngine.Simulation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace MarsColonyEngine.Context {
    [Serializable]
    public class ColonyContext : ISerializable {
        private static ColonyContext _current = null;
        public static ColonyContext Current {
            get {
                if (_current == null) {
                    KLogger.Log.Error("Context does not exist. Use ColonyContext.Load() or ColonyContext.Create() first.");
                    return null;
                }
                return _current;
            }
        }
        private ColonyContext () { }

        internal Action OnUnload { get; private set; }

        internal void RegisterOnUnloadReciever (Action action) {
            OnUnload += action;
        }

        public Turn Turn { get; private set; }
        private World.World _world = null;
        public World.World World {
            get => _world;
            set {
                if (_world == null) {
                    _world = value;
                    return;
                }
                KLogger.Log.Error("Cannot set World. It is already set. Unload Context first.");
            }
        }
        public List<Colonizer> Colonizers { get; private set; } = new List<Colonizer>();
        public List<Structure> Structures { get; private set; } = new List<Structure>();
        public List<Item> Items { get; private set; } = new List<Item>();
        public static bool IsInitalized {
            get {
                return _current != null;
            }
        }

        public static void Load (FileInfo file) {
            if (_current != null) {
                KLogger.Log.Error("Cannot load - save is already loaded. Call ColonyContext.Current.Unload() first.");
                return;
            }
            if (file.Exists) {
                try {
                    using (StreamReader sr = new StreamReader(file.FullName)) {
                        var str = sr.ReadToEnd();
                        _current = JObject.Parse(str).ToObject<ColonyContext>();
                    }
                } catch (Exception e) {
                    KLogger.Log.Error($"Error while loading context occured:\n{e.Message},\n{e.StackTrace}");
                    return;
                }
                KLogger.Log.Quiet("Context loaded successfully.");
                return;
            }
            KLogger.Log.Error($"Error while loading context occured:\n file {file.FullName} does not exist");
        }

        public static void Save (FileInfo file) {
            if (_current == null) {
                KLogger.Log.Error("Cannot save - ColonyContext is not initialized. Create new or load Context first.");
                return;
            }

            try {
                using (StreamWriter sw = new StreamWriter(file.FullName)) {
                    var jObject = JObject.FromObject(_current);
                    var json = jObject.ToString();
                    sw.Write(json);
                }
            } catch (Exception e) {
                KLogger.Log.Error($"Error while saving context occured:\n{e.Message},\n{e.StackTrace}");
                return;
            }
            KLogger.Log.Quiet("Context saved successfully.");
        }

        public static void Create () {
            if (_current == null) {
                _current = new ColonyContext();
                _current.World = new World.World();
                _current.Turn = new Turn();
                KLogger.Log.Quiet("ColonyContext created successfully.");
            }
        }

        public static void InitNewContext () {
            ColonyActions.ColonyActions.ExecuteAction<World.World>(AvailableActions.BuildRescueCapsule_Static_User_Paramless, null);
            ColonyActions.ColonyActions.ExecuteAction<World.World>(AvailableActions.SpawnColonizer_Static_Simulation_Args, null, typeof(Engineer));
            ColonyActions.ColonyActions.ExecuteAction<World.World>(AvailableActions.SpawnColonizer_Static_Simulation_Args, null, typeof(Scientist));
            ColonyActions.ColonyActions.ExecuteAction<World.World>(AvailableActions.SpawnColonizer_Static_Simulation_Args, null, typeof(Explorer));
            ColonyActions.ColonyActions.ExecuteAction<World.World>(AvailableActions.NextTurn_Static_User_Paramless, null);
        }

        public void Unload () {
            OnUnload?.Invoke();
            OnUnload = null;

            World = null;
            Colonizers?.Clear();
            _current = null;
        }

        public void GetObjectData (SerializationInfo info, StreamingContext context) {
            info.AddValue(nameof(Turn), Turn);
            info.AddValue("Explorers", Colonizers.Where(e => e is Explorer).ToList());
            info.AddValue("Engineers", Colonizers.Where(e => e is Engineer).ToList());
            info.AddValue("Scientists", Colonizers.Where(e => e is Scientist).ToList());
            info.AddValue(nameof(Structures), Structures);
            info.AddValue(nameof(Items), Items);
        }

        public ColonyContext (SerializationInfo info, StreamingContext context) {
            _current = this;
            Turn = (Turn)info.GetValue(nameof(Turn), typeof(Turn));

            // No need to store those, they are picked up and registered automatically by Registrator
            info.GetValue("Explorers", typeof(List<Explorer>));
            info.GetValue("Engineers", typeof(List<Engineer>));
            info.GetValue("Scientists", typeof(List<Scientist>));

            info.GetValue(nameof(Structures), typeof(List<Structure>));

            Items = (List<Item>)info.GetValue(nameof(Items), typeof(List<Item>));

            World = new World.World();

            Simulator.Current.OnFirstTurnStarted?.Invoke();
            Simulator.Current.ClearOnFirstTurnStarted();
        }
    }
}
