using UnityEngine;
using VertigoCaseProject.Configs;
using VertigoCaseProject.Core.Handlers;
using VertigoCaseProject.Core.Interfaces;

namespace VertigoCaseProject.Core
{
    /// <summary>
    /// Manages the flow and dependencies of the Spin Game, coordinating interactions between various handlers and the game logic.
    /// </summary>
    public class SpinGameController : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private SpinGameSOHandlerSO spinGameSOHandlerSO;

        [SerializeField] private WheelHandler wheelHandler;
        [SerializeField] private IndicatorHandler indicatorHandler;
        [SerializeField] private PrizePanelHandler prizePanelHandler;
        [SerializeField] private FlowingPrizeHandler flowingPrizeHandler;
        [SerializeField] private SpinGameUIController spinGameUIController;

        #endregion

        #region Private Fields

        private SpinGameFlow _spinGameFlow;

        #endregion

        #region Unity Methods

        /// <summary>
        /// Initializes the Spin Game and its flow on Awake.
        /// </summary>
        private void Awake()
        {
            var spinGameSO = spinGameSOHandlerSO.GetCurrentSpinGameSO(0);

            IWheelHandler iWheel = wheelHandler;
            IIndicatorHandler iIndicator = indicatorHandler;
            IPrizePanelHandler iPrize = prizePanelHandler;
            IFlowingPrizeHandler iFlowing = flowingPrizeHandler;
            ISpinGameUIController iUIController = spinGameUIController;

            _spinGameFlow = new SpinGameFlow(
                spinGameSOHandlerSO,
                iWheel,
                iIndicator,
                iPrize,
                iFlowing,
                iUIController
            );

            _spinGameFlow.StartFlow();
        }

        /// <summary>
        /// Updates the Spin Game flow every frame.
        /// </summary>
        private void Update()
        {
            _spinGameFlow.UpdateFlow();
        }

        #endregion
    }
}