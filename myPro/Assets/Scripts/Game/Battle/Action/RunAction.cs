using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Action
{
    public class RunAction : EntityActionBase
    {
        private int velocity = 10;
        private Unit unit;
        private float dirX;
        private float dirY;

        public override void Init()
        {
            this.StateName = "run";
        }

        public override void Act()
        {
            
        }

        public void SetDir(Vector2 v2)
        {
            this.dirX = v2.x;
            this.dirY = v2.y;
        }

        public override void Update()
        {
            Vector3 v3 = new Vector3(dirX, 0, dirY);

            if (unit != null) {
                unit.agent.UnitMove(v3.normalized);
            }
        }
    }
}