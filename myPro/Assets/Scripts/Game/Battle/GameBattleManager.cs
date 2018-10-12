using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

namespace Game
{
    public class GameBattleManager : Singleton<GameBattleManager>
    {
        public List<Unit> UnitList;

        public void Init()
        {
            this.UnitList = new List<Unit>();
        }

        public void AddUnit(Unit unit)
        {
            this.UnitList.Add(unit);
        }
    }
}

