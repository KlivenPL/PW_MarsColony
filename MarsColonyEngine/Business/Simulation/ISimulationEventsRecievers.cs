namespace MarsColonyEngine.Business.Simulation {

    public interface IOnTurnFinishedRec {
        void OnTurnFinished ();
    }
    public interface IOnTurnStartedRec {
        void OnTurnStarted ();
    }

    public interface IOnFirstTurnStartedRec {
        void OnFirstTurnStarted ();
    }
}
