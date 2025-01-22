using System;

namespace VertigoCaseProject.Configs
{
    /// <summary>
    /// Represents the data for a slice prize's possibility, including the categorized prize and its probability percentage.
    /// </summary>
    [Serializable]
    public struct SlicePrizePossibilityData
    {
        #region Fields

        /// <summary>
        /// The categorized prize associated with this possibility.
        /// </summary>
        public CategorizedPrizesDataSO categorizedPrize;

        /// <summary>
        /// The probability percentage of this prize being selected.
        /// </summary>
        public float possibilityPercent;

        #endregion
    }
}