using System.Collections.Generic;
using UnityEngine;

namespace VertigoCaseProject.Configs
{
    /// <summary>
    /// Represents the configuration and data for a spin game, including slice count, rounds, and zone intervals.
    /// </summary>
    [CreateAssetMenu(fileName = "SpinGameSO", menuName = "SpinGame/SpinGameSO")]
    public class SpinGameSO : ScriptableObject
    {
        /// <summary>
        /// The total number of slices on the wheel.
        /// </summary>
        public int sliceCount;

        /// <summary>
        /// The total number of rounds in the spin game.
        /// </summary>
        public int roundCount;

        /// <summary>
        /// The interval for placing safe zones on the wheel.
        /// </summary>
        public int safeZoneInterval;

        /// <summary>
        /// The interval for placing super zones on the wheel.
        /// </summary>
        public int superZoneInterval;

        /// <summary>
        /// The list of round data configurations for the spin game.
        /// </summary>
        public List<RoundDataSO> rounds;
    }
}