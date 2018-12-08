using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

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
        protected string playAnimation;
        public abstract void Init();
        public abstract void Act();
        public abstract void Update();
    }
}