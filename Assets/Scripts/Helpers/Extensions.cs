using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace VertigoCaseProject.Helpers
{
    /// <summary>
    /// Provides extension methods for generic lists and Unity RectTransform manipulation.
    /// </summary>
    public static class Extensions
    {
        #region List Extensions

        /// <summary>
        /// Shuffles the elements of a list in a random order.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to shuffle.</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        #endregion

        #region RectTransform Extensions

        /// <summary>
        /// Resizes a RectTransform to match the dimensions of a sprite while maintaining the specified maximum rectangle boundaries.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to resize.</param>
        /// <param name="sprite">The sprite to use as the size reference.</param>
        /// <param name="maxRect">The maximum allowable dimensions (width and height).</param>
        public static void ResizeToSprite(this RectTransform rectTransform, Sprite sprite, Vector2 maxRect)
        {
            if (rectTransform == null || sprite == null)
            {
                Debug.LogError("RectTransform or Sprite is null.");
                return;
            }

            float spriteWidth = sprite.rect.width;
            float spriteHeight = sprite.rect.height;
            float spriteAspectRatio = spriteWidth / spriteHeight;

            float maxWidth = maxRect.x;
            float maxHeight = maxRect.y;
            float maxAspectRatio = maxWidth / maxHeight;

            float targetWidth, targetHeight;

            if (spriteAspectRatio > maxAspectRatio) // Width-limited
            {
                targetWidth = maxWidth;
                targetHeight = maxWidth / spriteAspectRatio;
            }
            else // Height-limited
            {
                targetWidth = maxHeight * spriteAspectRatio;
                targetHeight = maxHeight;
            }

            rectTransform.sizeDelta = new Vector2(targetWidth, targetHeight);
        }

        #endregion
    }
}
