using UnityEngine;
using VertigoCaseProject.Core.Interfaces;
using VertigoCaseProject.Core.StateMachine.States;
using VertigoCaseProject.UI;

namespace VertigoCaseProject.SpinActions.Actions
{
    /// <summary>
    /// Represents a spin action that handles losing scenarios in the spin game.
    /// </summary>
    [CreateAssetMenu(fileName = "LoseSpinAction", menuName = "SpinGame/SpinActions/LoseSpinAction")]
    public class LoseSpinAction : SpinAction
    {
        /// <summary>
        /// Executes the lose spin action logic.
        /// </summary>
        /// <param name="gameFlow">The current game flow instance.</param>
        /// <param name="slice">The spin slice that triggered this action.</param>
        public override void Execute(ISpinGameFlow gameFlow, SpinSlice slice)
        {
            gameFlow.SpinGameUIController.SetLosePanelActive(true);
            gameFlow.SetNextRound();
            gameFlow.SpinFlowStateMachine.SetState(new WaitingForSpinState(gameFlow));
        }
    }
}