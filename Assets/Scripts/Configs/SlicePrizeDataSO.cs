using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VertigoCaseProject.Configs
{
    /// <summary>
    /// Represents the data for a slice prize in the spin game, including its type, prize, and possibilities.
    /// </summary>
    [CreateAssetMenu(fileName = "SlicePrizeData", menuName = "SpinGame/SlicePrizeData")]
    public class SlicePrizeDataSO : ScriptableObject
    {
        /// <summary>
        /// A description of the slice prize.
        /// </summary>
        [TextArea(3, 5)]
        public string description;

        /// <summary>
        /// The type of the slice prize (Direct or Possibility).
        /// </summary>
        public SlicePrizeDataType type;

        /// <summary>
        /// The direct prize item for this slice.
        /// </summary>
        public PrizeItemSO prize;

        /// <summary>
        /// The list of possibilities for categorized prizes.
        /// </summary>
        public List<SlicePrizePossibilityData> possibilities;

        /// <summary>
        /// Retrieves the prize data for this slice.
        /// </summary>
        /// <returns>A <see cref="PrizeData"/> object containing the prize item and amount.</returns>
        public PrizeData GetPrize()
        {
            PrizeItemSO selectedPrize = GetPrizeItem();
            int amount = SetRandomAmount(selectedPrize);

            return new PrizeData(selectedPrize, amount);
        }

        /// <summary>
        /// Retrieves a prize item based on the slice type.
        /// </summary>
        /// <returns>The selected <see cref="PrizeItemSO"/>.</returns>
        private PrizeItemSO GetPrizeItem()
        {
            if (type == SlicePrizeDataType.Direct)
            {
                return prize;
            }
            else
            {
                SlicePrizePossibilityData slicePrizePossibility = GetRandomPossibility();
                int randomIndex = Random.Range(0, slicePrizePossibility.categorizedPrize.prizes.Count);
                return slicePrizePossibility.categorizedPrize.prizes[randomIndex];
            }
        }

        /// <summary>
        /// Selects a random possibility from the list based on probability percentages.
        /// </summary>
        /// <returns>The selected <see cref="SlicePrizePossibilityData"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if the possibilities list is empty.</exception>
        private SlicePrizePossibilityData GetRandomPossibility()
        {
            if (possibilities == null || possibilities.Count == 0)
                throw new ArgumentException("Possibilities list is empty!");

            float totalProbability = possibilities.Sum(p => p.possibilityPercent);
            float randomValue = Random.Range(0f, totalProbability);

            float cumulativeProbability = 0f;
            foreach (var possibility in possibilities)
            {
                cumulativeProbability += possibility.possibilityPercent;
                if (randomValue <= cumulativeProbability)
                {
                    return possibility;
                }
            }

            throw new Exception("No possibility selected. Check your probabilities.");
        }

        /// <summary>
        /// Sets a random amount within the specified interval for a prize.
        /// </summary>
        /// <param name="prize">The prize item to determine the amount for.</param>
        /// <returns>A random amount as an integer.</returns>
        private int SetRandomAmount(PrizeItemSO prize)
        {
            return (int)Random.Range(prize.amountInterval.x, prize.amountInterval.y);
        }
    }

    /// <summary>
    /// Enum representing the types of slice prize data.
    /// </summary>
    public enum SlicePrizeDataType
    {
        Direct,
        Possibility
    }
}