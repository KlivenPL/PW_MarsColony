namespace MarsColonyEngine.Turn {
    public class Turn {
        public int Day { get; private set; }
        public int AvailableMoves {
            get {
                return ColonyActions.ColonyActions.GetAvailableActions().Length;
            }
        }
    }
}
