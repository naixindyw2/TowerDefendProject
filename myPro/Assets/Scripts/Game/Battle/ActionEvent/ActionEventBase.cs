
namespace Game.Event
{
    public abstract class ActionEventBase
    {
        public string EventName;
        public abstract void Bind(Unit unit);
        public abstract void Unbind(Unit unit);
        public abstract void Execute();
    }
}