namespace Engine
{
    public class GameState
    {
        public enum State
        {
            NotStarted,
            RunningDistribution,
            RunningSecret,
            Eval
        }

        public State Value { get; set; }
    }
}