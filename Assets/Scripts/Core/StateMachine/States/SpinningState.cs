using VertigoCaseProject.Core.Interfaces;
using VertigoCaseProject.EventBuses;

namespace VertigoCaseProject.Core.StateMachine.States
{
    /// <summary>
    /// Represents the spinning state of the spin game flow, handling the spin logic and transitioning to the next state.
    /// </summary>
    public class SpinningState : ISpinFlowState
    {
        private readonly ISpinGameFlow _flow;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpinningState"/> class.
        /// </summary>
        /// <param name="flow">The current game flow instance.</param>
        public SpinningState(ISpinGameFlow flow)
        {
            _flow = flow;
        }

        /// <summary>
        /// Called when entering the spinning state. Starts the spinning process.
        /// </summary>
        public void OnEnter()
        {
            GameEventBus.RaiseSpinStarted();

            _flow.WheelHandler.Spin(slice =>
            {
                GameEventBus.RaiseSpinEnded();
                slice.SpinAction.Execute(_flow, slice);

                
            });
        }

        /// <summary>
        /// Called on each update frame while in this state. Logic for updating the state can be added here.
        /// </summary>
        public void Update()
        {
            // Add logic for updating the spinning state if necessary.
        }

        /// <summary>
        /// Called when exiting the spinning state. Logic for cleanup can be added here.
        /// </summary>
        public void OnExit()
        {
            // Add logic for exiting the spinning state if necessary.
        }
    }
}