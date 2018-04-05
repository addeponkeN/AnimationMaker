using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationMaker.Gamer.StateMachine
{
    public class State
    {
        public StateManager StateManager { get; set; }

        public List<State> ChildStates = new List<State>();

        bool entered;

        public bool IsExiting;

        public State() { }

        public virtual void Init()
        {
            entered = true;
        }

        public virtual void Update(GameTime gt)
        {
            if(!entered)
                Init();

            for(int i = 0; i < ChildStates.Count; i++)
            {
                ChildStates[i].Update(gt);
                if(ChildStates[i].IsExiting)
                    ChildStates.RemoveAt(i);
            }

        }

        public virtual void ExitState()
        {
            IsExiting = true;
        }
    }
}
