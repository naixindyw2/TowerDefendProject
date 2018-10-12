using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Action
{
    public class EntityActionsManager
    {
        private Unit unit;
        private List<EntityActionBase> actions;

        public EntityActionsManager(Unit unit)
        {
            init(unit);
        }

        public void init(Unit unit)
        {
            this.unit = unit;
            this.actions = new List<EntityActionBase>();
            RunAction run = new RunAction();
            run.Init();
            IdleAction idle = new IdleAction();
            idle.Init();

            NormalAttackAction attack = new NormalAttackAction();
            attack.Init();
            actions.Add(run);
            actions.Add(attack);
        }

        public void TransferState(string stateName)
        {
            EntityActionBase actionState = null;
            for (int i = 0; i < this.actions.Count; i++) {
                if (this.actions[i].StateName == stateName) {
                    actionState = this.actions[i];
                    break;
                }
            }

            if (actionState == null) {
                Debug.Log("dont exist state" + stateName);
            }

            if (stateName == "run") {
                this.unit.CurActionStatus = (RunAction)actionState;
            } else if (stateName == "hit") {

            } else if (stateName == "idle") {
                this.unit.CurActionStatus = (IdleAction)actionState;
            }
        }

    }
}