using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Action;
using Game.Event;

namespace Game
{
    public class Unit
    {
        private int heroId;
        public int HeroId
        {
            get { return heroId; }
        }

        private long playerId;
        public long PlayerId
        {
            get { return playerId; }
        }

        public AgentControl agent;
        public EntityActionBase CurActionStatus;
        public EntityActionsManager EntityActions;

        public EntityEventManager EntityEvents;
    }
}