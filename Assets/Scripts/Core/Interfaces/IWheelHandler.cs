using System;
using VertigoCaseProject.Configs;
using VertigoCaseProject.UI;

namespace VertigoCaseProject.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for handling the functionality of the wheel in the spin game.
    /// </summary>
    public interface IWheelHandler
    {
        /// <summary>
        /// Initializes the wheel with the specified spin game data.
        /// </summary>
        /// <param name="spinGameSO">The spin game data used to initialize the wheel.</param>
        void InitializeWheel(SpinGameSO spinGameSO);

        /// <summary>
        /// Sets the wheel to the specified round configuration.
        /// </summary>
        /// <param name="roundData">The data for the round to configure the wheel.</param>
        void SetRound(RoundDataSO roundData);

        /// <summary>
        /// Spins the wheel and invokes the callback when the spin ends.
        /// </summary>
        /// <param name="onSpinEnded">The callback invoked with the chosen slice when the spin ends.</param>
        void Spin(Action<SpinSlice> onSpinEnded);

        /// <summary>
        /// Resets the wheel to its initial state using the specified spin game data.
        /// </summary>
        /// <param name="spinGameSO">The spin game data used to reset the wheel.</param>
        void ResetWheel(SpinGameSO spinGameSO);
    }
}