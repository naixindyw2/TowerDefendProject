using System.Xml;

namespace Game.Action
{
    public class NormalAttackAction : EntityActionBase
    {
        public string animatorName;
        private int velocity = 10;
        private Unit unit;       

        public override void Init()
        {
            this.animatorName = "run_01";
        }

        public override void Act()
        {
            unit.agent.animator.Play(animatorName);
        }

        public NormalAttackAction(Unit unit, XmlElement xml)
        {
            XmlElement play_animation = (XmlElement)xml.SelectSingleNode("play_animation");
            if (play_animation != null) {
                if (play_animation.HasAttribute("name")) {
                    playAnimation = play_animation.GetAttribute("name");
                }
            }
        }

        public override void Update()
        {
            
        }
    }
}