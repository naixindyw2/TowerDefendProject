using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Action
{
    public class IdleAction : EntityActionBase
    {       
        private Unit unit;

        public override void Init()
        {
            this.StateName = "idle";
        }

        public override void Act()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}