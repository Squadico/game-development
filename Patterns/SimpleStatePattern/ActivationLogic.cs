using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
namespace SimpleStatePattern
{
    public class ActivationLogic
    {
        public event Action Enabled;
        public event Action Disabled;

        State m_State;

        public ActivationLogic()
        {
            m_State = State.Disabled;
        }
        public void Enable()
        {
            m_State.EnableEvent(this);
        }
        public void Disable()
        {
            m_State.DisableEvent(this);
        }

        internal void SetState(State state)
        {
            m_State = state;
        }

        internal void EnableAction()
        {
            Enabled?.Invoke();
        }
        internal void DisableAction()
        {
            Disabled?.Invoke();
        }
    }
}
