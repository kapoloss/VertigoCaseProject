using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoCaseProject.Configs;
using VertigoCaseProject.Helpers;

namespace VertigoCaseProject.UI
{
    /// <summary>
    /// Manages the visual representation and data of a collected prize in the spin game.
    /// </summary>
    public class CollectedPrizeContent : MonoBehaviour
    {
        #region Private Fields

        private int _collectedAmount;

        #endregion

        #region Public Fields

        /// <summary>
        /// The item associated with this collected prize.
        /// </summary>
        [HideInInspector]
        public ItemSO item;

        #endregion

        #region Serialized Fields

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text countText;
        [SerializeField] private Vector2 maxRectSize;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the collected prize content with the provided prize data.
        /// </summary>
        /// <param name="prizeData">The data of the prize to initialize.</param>
        public void Initialize(PrizeData prizeData)
        {
            item = prizeData.ItemSO;
            icon.rectTransform.ResizeToSprite(prizeData.ItemSO.icon, maxRectSize);
            icon.sprite = prizeData.ItemSO.icon;
            countText.text = _collectedAmount.ToString();
        }

        /// <summary>
        /// Increases the amount of the collected prize and updates the UI.
        /// </summary>
        /// <param name="amount">The amount to increase.</param>
        public void IncreaseAmount(int amount)
        {
            _collectedAmount += amount;
            countText.text = _collectedAmount.ToString();
        }

        /// <summary>
        /// Gets the world position of the prize icon.
        /// </summary>
        /// <returns>The world position of the icon.</returns>
        public Vector3 GetIconPos()
        {
            return icon.transform.position;
        }

        #endregion
    }
}