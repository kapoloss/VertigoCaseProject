using System;

namespace VertigoCaseProject.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for managing the UI interactions in the spin game.
    /// </summary>
    public interface ISpinGameUIController
    {
        #region Events

        /// <summary>
        /// Invoked when the spin button is clicked.
        /// </summary>
        event Action OnSpinClicked;

        /// <summary>
        /// Invoked when the exit button is clicked.
        /// </summary>
        event Action OnExitClicked;

        /// <summary>
        /// Invoked when the give up button is clicked.
        /// </summary>
        event Action OnGiveUpClicked;

        /// <summary>
        /// Invoked when the revive button is clicked.
        /// </summary>
        event Action OnReviveClicked;

        /// <summary>
        /// Invoked when the collect button is clicked.
        /// </summary>
        event Action OnCollectClicked;

        /// <summary>
        /// Invoked when the go back button is clicked.
        /// </summary>
        event Action OnGoBackClicked;

        #endregion

        #region Methods

        /// <summary>
        /// Enables or disables the spin button.
        /// </summary>
        /// <param name="isActive">Whether the spin button should be active.</param>
        void EnableSpinButtonActive(bool isActive);

        /// <summary>
        /// Sets the active state of the exit button.
        /// </summary>
        /// <param name="isActive">Whether the exit button should be active.</param>
        void SetExitButtonActive(bool isActive);

        /// <summary>
        /// Enables or disables the revive button.
        /// </summary>
        /// <param name="isActive">Whether the revive button should be active.</param>
        void EnableReviveButtonActive(bool isActive);

        public void SetLosePanelActive(bool isActive);

        /// <summary>
        /// Resets the UI elements to their default state.
        /// </summary>
        void ResetSpinGameUI();

        #endregion
    }
}