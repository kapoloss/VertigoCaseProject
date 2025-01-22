using VertigoCaseProject.Core.Interfaces;

namespace VertigoCaseProject.Core.StateMachine.States
{
    /// <summary>
    /// Represents the state where the spin game is waiting for the player to initiate a spin.
    /// </summary>
    public class WaitingForSpinState : ISpinFlowState
    {
        private readonly ISpinGameFlow _flow;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitingForSpinState"/> class.
        /// </summary>
        /// <param name="flow">The current game flow instance.</param>
        public WaitingForSpinState(ISpinGameFlow flow)
        {
            _flow = flow;
        }

        /// <summary>
        /// Called when entering the waiting for spin state. Enables the spin button and subscribes to the spin event.
        /// </summary>
        public void OnEnter()
        {
            _flow.SpinGameUIController.EnableSpinButtonActive(true);
            _flow.SpinGameUIController.OnSpinClicked += OnSpinClicked;
        }

        /// <summary>
        /// Called on each update frame while in this state. Does nothing in this state.
        /// </summary>
        public void Update() { }

        /// <summary>
        /// Called when exiting the waiting for spin state. Disables the spin button event subscription.
        /// </summary>
        public void OnExit()
        {
            _flow.SpinGameUIController.OnSpinClicked -= OnSpinClicked;
        }

        /// <summary>
        /// Handles the spin button click event and transitions to the spinning state.
        /// </summary>
        private void OnSpinClicked()
        {
            _flow.SpinFlowStateMachine.SetState(new SpinningState(_flow));
            _flow.SpinGameUIController.EnableSpinButtonActive(false);
            _flow.SpinGameUIController.SetExitButtonActive(false);
        }
    }
}