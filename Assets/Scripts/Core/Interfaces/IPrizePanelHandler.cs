using VertigoCaseProject.Configs;
using VertigoCaseProject.UI;

namespace VertigoCaseProject.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for managing the prize panel in the spin game.
    /// </summary>
    public interface IPrizePanelHandler
    {
        /// <summary>
        /// Retrieves or creates a collected prize content based on the provided prize data.
        /// </summary>
        /// <param name="prizeData">The prize data to initialize or match the content with.</param>
        /// <returns>The collected prize content.</returns>
        CollectedPrizeContent GetCollectedPrize(PrizeData prizeData);

        /// <summary>
        /// Resets the prize panel by clearing all collected prize content.
        /// </summary>
        void ResetCollectedPrizes();
    }
}