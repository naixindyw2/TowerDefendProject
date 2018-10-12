using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

namespace Game.Battle
{
    public class UnitManager : Singleton<UnitManager>
    {
        Dictionary<long, Unit> UnitDic = new Dictionary<long,Unit>();

        public Unit playerSelfUnit;

        public void SetSelfUnit(Unit unit)
        {
            this.playerSelfUnit = unit;
        }

        public void AddUnit(Unit unit)
        {
            this.UnitDic.Add(unit.PlayerId, unit);
        }
    }
}