using VertigoCaseProject.Configs;
using VertigoCaseProject.Core.Interfaces;
using VertigoCaseProject.Core.StateMachine;
using VertigoCaseProject.Core.StateMachine.States;
using VertigoCaseProject.EventBuses;

namespace VertigoCaseProject.Core
{
    /// <summary>
    /// Manages the core game flow for the spin game, coordinating state transitions and interactions between handlers.
    /// </summary>
    public class SpinGameFlow : ISpinGameFlow
    {
        #region Private Fields

        private readonly IWheelHandler _wheelHandler;
        private readonly IIndicatorHandler _indicatorHandler;
        private readonly IPrizePanelHandler _prizePanelHandler;
        private readonly IFlowingPrizeHandler _flowingPrizeHandler;
        private readonly ISpinGameUIController _spinGameUIController;

        private readonly SpinGameSOHandlerSO _spinGameSOHandlerSO;
        private SpinGameSO _spinGameSO;
        private int _currentRound;

        #endregion

        #region Properties

        /// <summary>
        /// The state machine managing the flow of the spin game.
        /// </summary>
        public SpinFlowStateMachine StateMachine { get; private set; }

        public IPrizePanelHandler PrizePanelHandler => _prizePanelHandler;
        public IFlowingPrizeHandler FlowingPrizeHandler => _flowingPrizeHandler;
        public SpinFlowStateMachine SpinFlowStateMachine => StateMachine;
        public IWheelHandler WheelHandler => _wheelHandler;
        public ISpinGameUIController SpinGameUIController => _spinGameUIController;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SpinGameFlow"/> class.
        /// </summary>
        /// <param name="spinGameSOHandlerSO">The handler for spin game scriptable objects.</param>
        /// <param name="wheelHandler">The handler for the spin game wheel.</param>
        /// <param name="indicatorHandler">The handler for round indicators.</param>
        /// <param name="prizePanelHandler">The handler for prize panels.</param>
        /// <param name="flowingPrizeHandler">The handler for flowing prize animations.</param>
        /// <param name="spinGameUIController">The UI controller for the spin game.</param>
        public SpinGameFlow(
            SpinGameSOHandlerSO spinGameSOHandlerSO,
            IWheelHandler wheelHandler,
            IIndicatorHandler indicatorHandler,
            IPrizePanelHandler prizePanelHandler,
            IFlowingPrizeHandler flowingPrizeHandler,
            ISpinGameUIController spinGameUIController
        )
        {
            _spinGameSOHandlerSO = spinGameSOHandlerSO;
            _spinGameSO = spinGameSOHandlerSO.GetCurrentSpinGameSO(0);
            _wheelHandler = wheelHandler;
            _indicatorHandler = indicatorHandler;
            _prizePanelHandler = prizePanelHandler;
            _flowingPrizeHandler = flowingPrizeHandler;
            _spinGameUIController = spinGameUIController;

            StateMachine = new SpinFlowStateMachine();

            GameEventBus.RewardsCollected += CollectRewards;
            GameEventBus.GameOver += GameOver;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the game flow by setting the initial state.
        /// </summary>
        public void StartFlow()
        {
            StateMachine.SetState(new InitState(this));
        }

        /// <summary>
        /// Updates the game flow logic.
        /// </summary>
        public void UpdateFlow()
        {
            StateMachine.Update();
        }

        /// <summary>
        /// Initializes the game logic and prepares the first round.
        /// </summary>
        public void InitializeGameLogic()
        {
            _currentRound = 0;

            _indicatorHandler.InitializeIndicator(_spinGameSO);
            _wheelHandler.InitializeWheel(_spinGameSO);
        }

        /// <summary>
        /// Sets the next round in the game flow and updates related handlers.
        /// </summary>
        public void SetNextRound()
        {
            if (_currentRound < _spinGameSO.roundCount - 1)
            {
                _currentRound++;

                StateMachine.SetState(new WaitingForSpinState(this));

                _indicatorHandler.IncreaseIndicator(_currentRound - 1);
                _wheelHandler.SetRound(_spinGameSO.rounds[_currentRound]);

                GameEventBus.RaiseRoundChanged(_currentRound);

                _spinGameUIController.SetExitButtonActive(
                    _spinGameSO.rounds[_currentRound].roundZoneType != RoundZoneType.StandardZone
                );
            }
            else
            {
                GameEventBus.RaiseRewardsCollected();
            }
        }

        /// <summary>
        /// Determines whether the game has ended.
        /// </summary>
        /// <returns>True if the game has ended; otherwise, false.</returns>
        public bool IsGameEnded()
        {
            return _currentRound >= _spinGameSO.roundCount - 1;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Resets the game logic to its initial state.
        /// </summary>
        private void ResetGameLogic()
        {
            _spinGameSO = _spinGameSOHandlerSO.GetCurrentSpinGameSO(0);
            _currentRound = 0;

            StateMachine.SetState(new WaitingForSpinState(this));

            _wheelHandler.ResetWheel(_spinGameSO);
            _indicatorHandler.ResetIndicator(_spinGameSO);
            _prizePanelHandler.ResetCollectedPrizes();
            _spinGameUIController.ResetSpinGameUI();
            
        }

        /// <summary>
        /// Handles the rewards collection event.
        /// </summary>
        private void CollectRewards()
        {
            ResetGameLogic();
        }

        /// <summary>
        /// Handles the game over event.
        /// </summary>
        private void GameOver()
        {
            ResetGameLogic();
        }

        #endregion
    }
}