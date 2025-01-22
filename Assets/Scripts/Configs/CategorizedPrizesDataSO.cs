using System.Collections.Generic;
using UnityEngine;

namespace VertigoCaseProject.Configs
{
    /// <summary>
    /// Holds a categorized list of prizes for the spin game.
    /// </summary>
    [CreateAssetMenu(fileName = "CategorizedPrizesData", menuName = "SpinGame/CategorizedPrizesData")]
    public class CategorizedPrizesDataSO : ScriptableObject
    {
        /// <summary>
        /// The list of prize items in this category.
        /// </summary>
        public List<PrizeItemSO> prizes = new List<PrizeItemSO>();
    }
}