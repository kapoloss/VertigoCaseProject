using UnityEngine;
using VertigoCaseProject.Core.Interfaces;
using VertigoCaseProject.UI;

namespace VertigoCaseProject.SpinActions
{
    /// <summary>
    /// Represents an abstract base class for all spin actions in the spin game.
    /// </summary>
    public abstract class SpinAction : ScriptableObject
    {
        /// <summary>
        /// Executes the spin action logic.
        /// </summary>
        /// <param name="gameFlow">The current game flow instance.</param>
        /// <param name="slice">The spin slice that triggered this action.</param>
        public abstract void Execute(ISpinGameFlow gameFlow, SpinSlice slice);
    }
}