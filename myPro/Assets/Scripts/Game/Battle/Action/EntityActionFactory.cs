using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace Game.Action
{
    public class EntityActionFactory
    {        
        public List<EntityActionBase> actions;

        public void init(Unit unit)
        {
            this.actions = new List<EntityActionBase>();

            string path = "Config/States/elven_archer";
            string text = ConfigManager.LoadConfigFile(path);
            XmlDocument document = new XmlDocument();
            try {
                document.LoadXml(text);
            } catch (XmlException e) {
                UnityEngine.Debug.LogError(e.Message);
            }

            XmlElement root = document.DocumentElement;
            if (root == null) {
                UnityEngine.Debug.LogError("path not found");
                return;
            }

            for (int i = 0; i < root.ChildNodes.Count; i++) {
                if (root.ChildNodes[i] == null) {
                    UnityEngine.Debug.LogError("not ChildNodes found");
                    return;
                }
                XmlElement element = (XmlElement)root.ChildNodes[i];

                if (element.GetAttribute("name") == "idle") {
                    IdleAction idleAc = new IdleAction(unit, element);
                    actions.Add(idleAc);
                } else if (element.GetAttribute("name") == "move") {
                    RunAction runAc = new RunAction(unit, element);
                    actions.Add(runAc);
                } else if (element.GetAttribute("name") == "attack_01") {
                    NormalAttackAction attack = new NormalAttackAction(unit, element);
                    actions.Add(attack);
                }
            }
        }
    }
}