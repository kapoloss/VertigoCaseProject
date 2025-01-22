using TMPro;
using UnityEngine;

namespace VertigoCaseProject.UI
{
    /// <summary>
    /// Represents an indicator number in the spin game UI, handling its visual appearance and position.
    /// </summary>
    public class IndicatorNumber : MonoBehaviour
    {
        #region Public Fields

        /// <summary>
        /// The text component displaying the indicator number.
        /// </summary>
        public TMP_Text text;

        /// <summary>
        /// The index of this indicator number.
        /// </summary>
        public int index;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the UI elements of the indicator number, including its value, color, and optional sprite.
        /// </summary>
        /// <param name="number">The number to display.</param>
        /// <param name="textColor">The color of the text.</param>
        /// <param name="indicatorSprite">The sprite for the indicator (currently unused).</param>
        public void SetUI(int number, Color textColor, Sprite indicatorSprite)
        {
            text.text = number.ToString();
            text.color = textColor;
            // Future feature: Uncomment and assign the sprite if needed.
            // indicator.sprite = indicatorSprite;
        }

        /// <summary>
        /// Sets the local position of the indicator number.
        /// </summary>
        /// <param name="localPosition">The new local position.</param>
        public void SetLocalPosition(Vector3 localPosition)
        {
            transform.localPosition = localPosition;
        }

        #endregion
    }
}