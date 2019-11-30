using MarsColonyEngine.Colonizers;
using MarsColonyEngine.Logger;
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
                    KLogger.Log.Error("Context does not exist. Use Context.Load() or Context.Create() first.");
                    return null;
                }
                return _current;
            }
        }

        internal Action OnUnload { get; set; }
        public World.World World { get; internal set; }
        public List<Colonizer> Colonizers { get; internal set; }
        public static bool IsInitalized {
            get {
                return _current != null;
            }
        }

        public static void Load (FileInfo file) {
            if (_current != null) {
                KLogger.Log.Error("Cannot load - save is already loaded. Call Context.Current.Unload() first.");
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
            KLogger.Log.Quiet("Context created successfully.");
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
