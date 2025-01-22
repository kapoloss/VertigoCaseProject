using System;
using VertigoCaseProject.UI;

namespace VertigoCaseProject.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for handling flowing prize animations.
    /// </summary>
    public interface IFlowingPrizeHandler
    {
        /// <summary>
        /// Sends a flowing prize from the specified slice to the collected prize content.
        /// </summary>
        /// <param name="slice">The spin slice containing the prize data.</param>
        /// <param name="content">The target collected prize content.</param>
        /// <param name="onFlowComplete">The callback invoked when the flow animation is complete.</param>
        void SendFlowingPrize(SpinSlice slice, CollectedPrizeContent content, Action onFlowComplete);
    }
}