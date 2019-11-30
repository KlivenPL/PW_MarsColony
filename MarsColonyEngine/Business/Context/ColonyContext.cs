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

        internal Action OnUnload { get; set; }

        public Turn Turn { get; internal set; }
        public World.World World { get; internal set; } = null;
        public List<Colonizer> Colonizers { get; internal set; } = new List<Colonizer>();
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

        public static void Create () {
            _current = new ColonyContext();
            KLogger.Log.Quiet("ColonyContext created successfully.");
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
