﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace Game.Action
{
    public class IdleAction : EntityActionBase
    {
        private Unit unit;
        public string StateName = "idle";
        protected string playAnimation = "";

        public override void Init()
        {
            this.StateName = "idle";
        }

        public IdleAction(Unit unit, XmlElement xml)
        {
            XmlElement play_animation = (XmlElement)xml.SelectSingleNode("play_animation");
            if (play_animation != null) {
                if (play_animation.HasAttribute("name")) {
                    playAnimation = play_animation.GetAttribute("name");
                }
            }
            //for (int i = 0; i < xml.ChildNodes.Count; i++) {
            //    XmlElement el = (XmlElement)xml.ChildNodes[i];
            //    if (el.HasAttribute("play_animation")) {
            //        playAnimation = el.GetAttribute("play_animation");
            //    }
            //}
        }

        public override void Act()
        {

        }

        public override void Update()
        {

        }
    }
}