using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Event
{
    public class RunEvent : ActionEventBase
    {
        private Unit unit;

        public override void Bind(Unit unit)
        {
            this.unit = unit;
            this.EventName = "RunStart";
        }

        public override void Unbind(Unit unit)
        {
            
        }

        public override void Execute()
        {
            unit.agent.animator.Play("run_01");
            unit.EntityActions.TransferState("run");
        }
    }
}


