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

        public override void Update()
        {
            
        }
    }
}