using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace Game.Action
{
    public class EntityActionFactory
    {
        private Unit unit;
        private List<EntityActionBase> actions;

        public void init(Unit unit)
        {
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

            for (int i = 0; i < root.ChildNodes.Count; i++)
			{
                if (root.ChildNodes[i] == null) {
                    UnityEngine.Debug.LogError("not ChildNodes found");
                    return;
                }
                XmlElement idle = (XmlElement)root.ChildNodes[i];

                if (idle.HasAttribute("idle")) {
                    IdleAction idleAc = new IdleAction(unit,idle);
                }
			}
             

            this.actions = new List<EntityActionBase>();
            RunAction run = new RunAction();
            run.Init();
            NormalAttackAction attack = new NormalAttackAction();
            attack.Init();
            actions.Add(run);
            actions.Add(attack);

        }
    }
}