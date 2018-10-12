using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Action
{
    public enum ActionState
    {
        None,
        Idle,
        Run
    }

    public abstract class EntityActionBase
    {
        public string StateName;
        public abstract void Init();
        public abstract void Act();
        public abstract void Update();
    }
}