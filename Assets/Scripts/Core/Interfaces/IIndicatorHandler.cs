using VertigoCaseProject.Configs;

namespace VertigoCaseProject.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for managing and updating indicators in the spin game.
    /// </summary>
    public interface IIndicatorHandler
    {
        /// <summary>
        /// Initializes the indicators based on the provided spin game data.
        /// </summary>
        /// <param name="spinGameSO">The spin game data to initialize the indicators with.</param>
        void InitializeIndicator(SpinGameSO spinGameSO);

        /// <summary>
        /// Updates the indicators to reflect the current round index.
        /// </summary>
        /// <param name="currentIndex">The current round index to update the indicators.</param>
        void IncreaseIndicator(int currentIndex);

        /// <summary>
        /// Resets the indicators using the provided spin game data.
        /// </summary>
        /// <param name="spinGameSO">The spin game data for resetting the indicators.</param>
        void ResetIndicator(SpinGameSO spinGameSO);
    }
}