using VertigoCaseProject.SpinActions;

namespace VertigoCaseProject.Configs
{
    /// <summary>
    /// Represents data related to a prize, including the associated item, amount, and spin action.
    /// </summary>
    public struct PrizeData
    {
        #region Properties

        /// <summary>
        /// The associated item of the prize.
        /// </summary>
        public readonly ItemSO ItemSO;

        /// <summary>
        /// The amount of the prize.
        /// </summary>
        public readonly int Amount;

        /// <summary>
        /// The spin action associated with the prize.
        /// </summary>
        public readonly SpinAction SpinAction;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PrizeData"/> struct.
        /// </summary>
        /// <param name="prizeItemSO">The PrizeItemSO containing item and spin action details.</param>
        /// <param name="amount">The amount of the prize.</param>
        public PrizeData(PrizeItemSO prizeItemSO, int amount)
        {
            Amount = amount;
            ItemSO = prizeItemSO.item;
            SpinAction = prizeItemSO.spinAction;
        }

        #endregion
    }
}