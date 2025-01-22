using System;

namespace VertigoCaseProject.EventBuses
{
    /// <summary>
    /// A static class that manages UI-related events using the observer pattern.
    /// Provides a centralized mechanism for opening and closing panels.
    /// </summary>
    public static class UIEventBus
    {
        #region Events

        /// <summary>
        /// Invoked when a panel is opened, providing the panel name.
        /// </summary>
        public static event Action<string> PanelOpened;

        /// <summary>
        /// Invoked when a panel is closed, providing the panel name.
        /// </summary>
        public static event Action<string> PanelClosed;

        #endregion

        #region Raise Event Methods

        /// <summary>
        /// Raises the PanelOpened event with the specified panel name.
        /// </summary>
        /// <param name="panelName">The name of the panel that was opened.</param>
        public static void RaisePanelOpened(string panelName)
        {
            PanelOpened?.Invoke(panelName);
        }

        /// <summary>
        /// Raises the PanelClosed event with the specified panel name.
        /// </summary>
        /// <param name="panelName">The name of the panel that was closed.</param>
        public static void RaisePanelClosed(string panelName)
        {
            PanelClosed?.Invoke(panelName);
        }

        #endregion
    }
}