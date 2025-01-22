using System;

namespace VertigoCaseProject.EventBuses
{
    /// <summary>
    /// A static class that manages game events using the observer pattern.
    /// Provides a central hub for raising and subscribing to game-related events.
    /// </summary>
    public static class GameEventBus
    {
        #region Events

        /// <summary>
        /// Invoked when a spin starts.
        /// </summary>
        public static event Action SpinStarted;

        /// <summary>
        /// Invoked when a spin ends.
        /// </summary>
        public static event Action SpinEnded;

        /// <summary>
        /// Invoked when the round changes, passing the new round index.
        /// </summary>
        public static event Action<int> RoundChanged;

        /// <summary>
        /// Invoked when rewards are collected.
        /// </summary>
        public static event Action RewardsCollected;

        /// <summary>
        /// Invoked when the game is over.
        /// </summary>
        public static event Action GameOver;

        #endregion

        #region Raise Event Methods

        /// <summary>
        /// Raises the SpinStarted event.
        /// </summary>
        public static void RaiseSpinStarted()
        {
            SpinStarted?.Invoke();
        }

        /// <summary>
        /// Raises the SpinEnded event.
        /// </summary>
        public static void RaiseSpinEnded()
        {
            SpinEnded?.Invoke();
        }

        /// <summary>
        /// Raises the RoundChanged event with the specified round index.
        /// </summary>
        /// <param name="roundIndex">The new round index.</param>
        public static void RaiseRoundChanged(int roundIndex)
        {
            RoundChanged?.Invoke(roundIndex);
        }

        /// <summary>
        /// Raises the RewardsCollected event.
        /// </summary>
        public static void RaiseRewardsCollected()
        {
            RewardsCollected?.Invoke();
        }

        /// <summary>
        /// Raises the GameOver event.
        /// </summary>
        public static void RaiseGameOver()
        {
            GameOver?.Invoke();
        }

        #endregion
    }
}
