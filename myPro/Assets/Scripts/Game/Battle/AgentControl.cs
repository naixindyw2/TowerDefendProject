using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Action;
using Game.Battle;
using System;

namespace Game
{
    public class AgentControl : MonoBehaviour
    {
        private Unit unit;
        public Unit Unit
        {
            get { return unit; }
        }

        public Animator animator;

        void Awake()
        {


            Init();
            //this.animator = this.gameObject.GetComponent<Animator>();

        }

        void Start()
        {
            UnitManager.Instance.AddUnit(this.unit);
            UnitManager.Instance.SetSelfUnit(this.unit);
        }

        public void Init()
        {
            this.unit = new Unit();
            this.unit.agent = this;
            this.unit.EntityActions = new EntityActionsManager(this.unit);
            this.unit.EntityEvents = new Event.EntityEventManager(this.unit);
        }

        public void UnitMove(Vector3 v3)
        {
            if (v3 == Vector3.zero) {
                if (this.unit.CurActionStatus == null || this.unit.CurActionStatus.StateName != "idle") {
                    this.unit.EntityEvents.Trigger("IdleEvent");
                }
                return;
            }
            if (this.unit.CurActionStatus == null || this.unit.CurActionStatus.StateName != "run") {
                this.unit.EntityEvents.Trigger("RunStart");
            }

            this.transform.position += v3 * 0.2f;
            //float x = 0;
            //float y = 0;
            //float z = 0;
            //float radiu = 180/(float)Math.PI;

            
            //if (v3.x == 0) {
            //    y = SetAbs(v3.z, 90);
            //} else {
            //    y = (float)Math.Atan(v3.z / v3.x);
            //}

            //if (v3.x == 0) {
            //    z = SetAbs(v3.x, 90);
            //} else {
            //    z = (float)Mathf.Atan(v3.y / v3.x);
            //}
            
            //if (v3.z == 0) {
            //    x = SetAbs(v3.z, 90);
            //} else {
            //    x = -(float)Mathf.Atan(v3.y / v3.z);
            //}
            
            //Vector3 euler = new Vector3(x * radiu, y * radiu, z * radiu);
            this.transform.rotation = Quaternion.FromToRotation(Vector3.right, v3);// Quaternion.Euler(euler);

        }

        private float SetAbs(float baseNum,float num)
        {
            if (baseNum > 0) {
                return Mathf.Abs(num);
            } else {
                return -Mathf.Abs(num);
            }
        }

        void Update()
        {

        }
    }
}