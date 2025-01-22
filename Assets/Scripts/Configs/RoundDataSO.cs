using System.Collections.Generic;
using UnityEngine;

namespace VertigoCaseProject.Configs
{
    /// <summary>
    /// Represents the data for a specific round in the spin game, including zone type and slice prizes.
    /// </summary>
    [CreateAssetMenu(fileName = "RoundData", menuName = "SpinGame/RoundData")]
    public class RoundDataSO : ScriptableObject
    {
        /// <summary>
        /// The type of the zone for this round.
        /// </summary>
        public RoundZoneType roundZoneType;

        /// <summary>
        /// The list of slice prizes for this round.
        /// </summary>
        public List<SlicePrizeDataSO> slices;
    }

    /// <summary>
    /// Enum representing the different types of zones in a round.
    /// </summary>
    public enum RoundZoneType
    {
        StandardZone,
        SafeZone,
        SuperZone
    }
}