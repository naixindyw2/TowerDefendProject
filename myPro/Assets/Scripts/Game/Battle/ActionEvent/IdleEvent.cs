using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Event
{
    public class IdleEvent : ActionEventBase
    {
        private Unit unit;

        public override void Bind(Unit unit)
        {
            this.unit = unit;
            this.EventName = "IdleEvent";
        }

        public override void Unbind(Unit unit)
        {
            
        }

        public override void Execute()
        {
            unit.agent.animator.Play("idle_01");
            unit.EntityActions.TransferState("idle");
        }
    }
}


