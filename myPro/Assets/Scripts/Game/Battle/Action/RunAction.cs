using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace Game.Action
{
    public class RunAction : EntityActionBase
    {
        private int velocity = 10;
        private Unit unit;
        private float velocityX;
        private float velocityY;

        private float dirX;
        private float dirY;

        public override void Init()
        {
            this.StateName = "move";
        }

        public override void Act()
        {

        }

        public RunAction(Unit unit, XmlElement xml)
        {
            XmlElement play_animation = (XmlElement)xml.SelectSingleNode("play_animation");
            if (play_animation != null) {
                if (play_animation.HasAttribute("name")) {
                    playAnimation = play_animation.GetAttribute("name");
                }
            }

            XmlNode velocity = xml.SelectSingleNode("velocity");
            if (velocity != null) {
                if (xml.HasAttribute("x")) {
                    if (!float.TryParse(xml.GetAttribute("x"), out this.velocityX)) {
                        Debug.LogError("parse float error");
                    }
                }

                if (xml.HasAttribute("y")) {
                    if (!float.TryParse(xml.GetAttribute("y"), out this.velocityY)) {
                        Debug.LogError("parse float error");
                    }
                }
            }
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