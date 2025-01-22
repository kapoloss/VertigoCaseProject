using UnityEngine;
using VertigoCaseProject.SpinActions;

namespace VertigoCaseProject.Configs
{
    /// <summary>
    /// Represents a prize item in the spin game, including its associated item, amount range, and spin action.
    /// </summary>
    [CreateAssetMenu(fileName = "PrizeItem", menuName = "SpinGame/PrizeItem")]
    public class PrizeItemSO : ScriptableObject
    {
        /// <summary>
        /// The item associated with this prize.
        /// </summary>
        public ItemSO item;

        /// <summary>
        /// The interval defining the possible amount range for this prize.
        /// </summary>
        public Vector2 amountInterval;

        /// <summary>
        /// The spin action associated with this prize.
        /// </summary>
        public SpinAction spinAction;
    }
}