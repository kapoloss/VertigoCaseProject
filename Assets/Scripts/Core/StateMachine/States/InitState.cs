
namespace VertigoCaseProject.Core.StateMachine.States
{
    /// <summary>
    /// Represents the initialization state of the spin game flow.
    /// </summary>
    public class InitState : ISpinFlowState
    {
        private readonly SpinGameFlow _flow;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitState"/> class.
        /// </summary>
        /// <param name="flow">The current game flow instance.</param>
        public InitState(SpinGameFlow flow)
        {
            _flow = flow;
        }

        /// <summary>
        /// Called when entering the initialization state.
        /// </summary>
        public void OnEnter()
        {
            _flow.InitializeGameLogic();
            _flow.StateMachine.SetState(new WaitingForSpinState(_flow));
        }

        /// <summary>
        /// Called on each update frame while in this state. Does nothing in this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// Called when exiting this state. Does nothing in this state.
        /// </summary>
        public void OnExit() { }
    }
}