using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationMaker.Gamer.StateMachine
{
    public class StateManager
    {
        List<State> states = new List<State>();

        public void AddState(State state)
        {
            state.StateManager = this;
            states.Add(state);
        }

        public void AddChildState(State parent, State child)
        {
            child.StateManager = this;
            parent.ChildStates.Add(child);
        }

        public void Clear()
        {
            states.Clear();
        }

        public void Update(GameTime gameTime)
        {
            for(int i = 0; i < states.Count; i++)
            {
                states[i].Update(gameTime);
                if(states[i].IsExiting)
                    states.RemoveAt(i);
            }
        }

    }
}

