using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Event
{
    public class EntityEventManager
    {
        private Unit unit;
        public List<ActionEventBase> EntityEvents;

        public EntityEventManager(Unit unit)
        {
            init(unit);
        }

        public void init(Unit unit)
        {
            this.unit = unit;
            this.EntityEvents = new List<ActionEventBase>();
            RunEvent run = new RunEvent();
            run.Bind(this.unit);
            EntityEvents.Add(run);

            IdleEvent idle = new IdleEvent();
            idle.Bind(this.unit);
            EntityEvents.Add(idle);
        }

        public void Trigger(string eventName)
        {
            ActionEventBase actionState = null;
            for (int i = 0; i < this.EntityEvents.Count; i++) {
                if (this.EntityEvents[i].EventName == eventName) {
                    actionState = this.EntityEvents[i];
                    break;
                }
            }
            if (actionState == null) {
                Debug.Log("dont exist state" + eventName);
                return;
            }
            actionState.Execute();


            //if (eventName == "RunStart") {
            //    (RunEvent)actionState;
            //} else if (eventName == "hit") {

            //} else if (eventName == "idle") {

            //}
        }
        public void TransferState(string stateName)
        {

        }
    }
}


