using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace VertigoCaseProject.Helpers
{
    /// <summary>
    /// Provides animation utilities for creating flowing animations for prizes.
    /// </summary>
    public class FlowingAnimationHelper 
    {
        /// <summary>
        /// Animates the flow of a prize image through a midpoint to an endpoint.
        /// </summary>
        /// <param name="prizeImage">The image to animate.</param>
        /// <param name="midPos">The midpoint position for the animation.</param>
        /// <param name="endPos">The endpoint position for the animation.</param>
        /// <param name="durationPerStep">The duration of each step in the animation.</param>
        /// <returns>A DOTween Sequence representing the animation.</returns>
        public Sequence AnimateFlow(Image prizeImage, Vector3 midPos, Vector3 endPos, float durationPerStep)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(prizeImage.transform.DOMove(midPos, durationPerStep));
            sequence.Append(prizeImage.transform.DOMove(endPos, durationPerStep).SetEase(Ease.Linear));
            return sequence;
        }
    }
}