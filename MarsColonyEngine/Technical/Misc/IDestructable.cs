using System;

namespace MarsColonyEngine.Technical.Misc {
    public interface IDestructable {
        bool IsAlive { get; }
        void AffectHp (float signeDeltaHp);
        Action Destroy { get; }
    }
}
