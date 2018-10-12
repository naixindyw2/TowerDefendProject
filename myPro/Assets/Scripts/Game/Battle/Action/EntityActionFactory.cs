using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Action
{
    public class EntityActionFactory
    {
        private Unit unit;
        private List<EntityActionBase> actions;

        public void init(Unit unit)
        {
            this.actions = new List<EntityActionBase>();
            RunAction run = new RunAction();
            run.Init();
            NormalAttackAction attack = new NormalAttackAction();
            attack.Init();
            actions.Add(run);
            actions.Add(attack);
        }
    }
}