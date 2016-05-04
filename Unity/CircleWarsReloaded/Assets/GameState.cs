using System.Collections.Generic;

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
        
        public abstract class Listener
        {
            public abstract void OnStateChange(State state);
        }

        public GameState()
        {
            listeners = new List<Listener>();
        }
        public State Value
        {
            get { return value; }
            set
            {
                if (value == this.value)
                    return;
                this.value = value;
                foreach( Listener listener in listeners )
                {
                    listener.OnStateChange(value);
                }
            }
        }

        public void RegisterOnStateChange(Listener listener)
        {
            listeners.Add(listener);
        }

        private State value;
        private List<Listener> listeners;
    }
}