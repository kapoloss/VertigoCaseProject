using System.Collections.Generic;
using UnityEngine;

namespace VertigoCaseProject.Configs
{
    /// <summary>
    /// Handles a collection of spin game configurations and provides access to specific configurations by level.
    /// </summary>
    [CreateAssetMenu(fileName = "SpinGameSOHandlerSO", menuName = "SpinGame/SpinGameSOHandlerSO")]
    public class SpinGameSOHandlerSO : ScriptableObject
    {
        /// <summary>
        /// A list of spin game configurations.
        /// </summary>
        public List<SpinGameSO> spinGameSOs;

        /// <summary>
        /// Retrieves the spin game configuration for the specified level.
        /// </summary>
        /// <param name="level">The level index for which to retrieve the spin game configuration.</param>
        /// <returns>The <see cref="SpinGameSO"/> for the specified level.</returns>
        public SpinGameSO GetCurrentSpinGameSO(int level)
        {
            return spinGameSOs[level];
        }
    }
}