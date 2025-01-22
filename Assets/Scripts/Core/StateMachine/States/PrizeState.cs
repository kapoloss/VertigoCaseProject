using VertigoCaseProject.Core.Interfaces;

namespace VertigoCaseProject.Core.StateMachine.States
{
    /// <summary>
    /// Represents the prize state of the spin game flow, handling logic related to awarding prizes.
    /// </summary>
    public class PrizeState : ISpinFlowState
    {
        private readonly ISpinGameFlow _flow;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrizeState"/> class.
        /// </summary>
        /// <param name="flow">The current game flow instance.</param>
        public PrizeState(ISpinGameFlow flow)
        {
            _flow = flow;
        }

        /// <summary>
        /// Called when entering the prize state. Logic for entering the state can be added here.
        /// </summary>
        public void OnEnter()
        {
            // Add logic for entering the prize state if necessary.
        }

        /// <summary>
        /// Called on each update frame while in this state. Logic for updating the state can be added here.
        /// </summary>
        public void Update()
        {
            // Add logic for updating the prize state if necessary.
        }

        /// <summary>
        /// Called when exiting the prize state. Logic for exiting the state can be added here.
        /// </summary>
        public void OnExit()
        {
            // Add logic for exiting the prize state if necessary.
        }
    }
}