namespace StatePattern
{
    internal abstract class State
    {
        internal static readonly State Disabled = new DisabledState();
        internal static readonly State Enabled = new EnabledState();

        internal virtual void EnableEvent(ActivationLogic logic) { }
        internal virtual void DisableEvent(ActivationLogic logic) { }
    }

    sealed class DisabledState : State
    {
        internal override void EnableEvent(ActivationLogic logic)
        {
            logic.SetState(Enabled);
            logic.EnableAction();
        }
    }
    sealed class EnabledState : State
    {
        internal override void DisableEvent(ActivationLogic logic)
        {
            logic.SetState(Disabled);
            logic.DisableAction();
        }
    }
}
