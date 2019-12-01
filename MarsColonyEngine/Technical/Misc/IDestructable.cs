using System;

namespace MarsColonyEngine.Technical.Misc {
    public interface IDestructable {
        bool IsAlive { get; }
        Action Destroy { get; }
    }
}
