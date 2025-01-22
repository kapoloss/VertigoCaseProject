namespace VertigoCaseProject.Core.StateMachine
{
    /// <summary>
    /// Manages the state transitions and updates for the spin game flow.
    /// </summary>
    public class SpinFlowStateMachine
    {
        private ISpinFlowState _currentState;

        /// <summary>
        /// Sets the current state of the state machine, exiting the previous state and entering the new state.
        /// </summary>
        /// <param name="newState">The new state to transition to.</param>
        public void SetState(ISpinFlowState newState)
        {
            _currentState?.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }

        /// <summary>
        /// Updates the current state, if one exists.
        /// </summary>
        public void Update()
        {
            _currentState?.Update();
        }
    }
}