namespace VertigoCaseProject.Core.StateMachine
{
    /// <summary>
    /// Defines the contract for states in the spin game flow state machine.
    /// </summary>
    public interface ISpinFlowState
    {
        /// <summary>
        /// Called when entering the state.
        /// </summary>
        void OnEnter();

        /// <summary>
        /// Called on each update frame while in this state.
        /// </summary>
        void Update();

        /// <summary>
        /// Called when exiting the state.
        /// </summary>
        void OnExit();
    }
}