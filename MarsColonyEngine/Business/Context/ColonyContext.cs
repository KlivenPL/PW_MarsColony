using MarsColonyEngine.Business.Structures;
using MarsColonyEngine.Colonizers;
using MarsColonyEngine.Logger;
using MarsColonyEngine.Simulation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarsColonyEngine.Context {
    public class ColonyContext {
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

        public static void Create (World.World world = null) {
            if (_current == null) {
                _current = new ColonyContext();
                _current.World = world == null ? new World.World() : world;
                _current.Turn = new Turn();
                KLogger.Log.Quiet("ColonyContext created successfully.");
            }
        }

        public void Unload () {
            OnUnload?.Invoke();
            OnUnload = null;

            World = null;
            Colonizers?.Clear();
            _current = null;
        }

    }
}
