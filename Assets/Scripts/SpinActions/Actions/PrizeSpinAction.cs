using System.Threading.Tasks;
using UnityEngine;
using VertigoCaseProject.Core.Interfaces;
using VertigoCaseProject.Core.StateMachine.States;
using VertigoCaseProject.UI;

namespace VertigoCaseProject.SpinActions.Actions
{
    /// <summary>
    /// Represents a spin action that handles awarding prizes in the spin game.
    /// </summary>
    [CreateAssetMenu(fileName = "PrizeSpinAction", menuName = "SpinGame/SpinActions/PrizeSpinAction")]
    public class PrizeSpinAction : SpinAction
    {
        /// <summary>
        /// Executes the prize spin action logic.
        /// </summary>
        /// <param name="gameFlow">The current game flow instance.</param>
        /// <param name="slice">The spin slice that triggered this action.</param>
        public override void Execute(ISpinGameFlow gameFlow, SpinSlice slice)
        {
            gameFlow.SpinFlowStateMachine.SetState(new PrizeState(gameFlow));
            
            var panel = gameFlow.PrizePanelHandler;
            var flow = gameFlow.FlowingPrizeHandler;
            var prizeData = slice.GetPrizeData();

            // Retrieve or create the prize content in the prize panel.
            var content = panel.GetCollectedPrize(prizeData);

            // Animate the flowing prize and update the prize amount upon completion.
            flow.SendFlowingPrize(slice, content, () =>
            {
                content.IncreaseAmount(prizeData.Amount);
                gameFlow.SetNextRound();
                gameFlow.SpinFlowStateMachine.SetState(new WaitingForSpinState(gameFlow));
            });
        }
    }
}