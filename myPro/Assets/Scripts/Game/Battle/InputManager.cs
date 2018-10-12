using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

namespace Game.Battle
{
    public class InputManager : Singleton<InputManager>
    {
        public float dirX = 0;
        public float dirY = 0;

        public void Init()
        {
            TimelineManager.Instance.EventStep
                += Update;
        }

        public void SetDir(Vector2 v2)
        {
            dirX = v2.x;
            dirY = v2.y;
        }

        public void Update()
        {
            Vector3 v3 = new Vector3(dirX, 0, dirY);
            
            if (UnitManager.Instance.playerSelfUnit != null) {
                UnitManager.Instance.playerSelfUnit.agent.UnitMove(v3.normalized);            }
        }
    }
}


