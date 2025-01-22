using VertigoCaseProject.Core.StateMachine;

namespace VertigoCaseProject.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for managing the flow and logic of the spin game.
    /// </summary>
    public interface ISpinGameFlow
    {
        /// <summary>
        /// Initializes the game logic and prepares the game for the first round.
        /// </summary>
        void InitializeGameLogic();

        /// <summary>
        /// Advances the game to the next round, updating related components.
        /// </summary>
        void SetNextRound();

        /// <summary>
        /// Gets the prize panel handler.
        /// </summary>
        IPrizePanelHandler PrizePanelHandler { get; }

        /// <summary>
        /// Gets the flowing prize handler.
        /// </summary>
        IFlowingPrizeHandler FlowingPrizeHandler { get; }

        /// <summary>
        /// Gets the state machine managing the spin game flow.
        /// </summary>
        SpinFlowStateMachine SpinFlowStateMachine { get; }

        /// <summary>
        /// Gets the UI controller for the spin game.
        /// </summary>
        ISpinGameUIController SpinGameUIController { get; }

        /// <summary>
        /// Gets the wheel handler managing the spin game's wheel.
        /// </summary>
        IWheelHandler WheelHandler { get; }
    }
}