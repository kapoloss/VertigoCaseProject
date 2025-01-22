using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoCaseProject.Configs;
using VertigoCaseProject.Helpers;
using VertigoCaseProject.SpinActions;

namespace VertigoCaseProject.UI
{
    /// <summary>
    /// Represents a slice of the spin wheel, displaying prize information and managing spin actions.
    /// </summary>
    public class SpinSlice : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private TMP_Text amountText;
        [SerializeField] private Image icon;
        [SerializeField] private Vector2 iconMaxSize;

        #endregion

        #region Private Fields

        private PrizeData _prizeData;

        #endregion

        #region Public Properties

        /// <summary>
        /// The spin action associated with this slice.
        /// </summary>
        public SpinAction SpinAction { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the prize data for this slice and updates its UI elements.
        /// </summary>
        /// <param name="prizeData">The prize data to set.</param>
        public void SetPrize(PrizeData prizeData)
        {
            _prizeData = prizeData;
            if (icon != null)
            {
                icon.rectTransform.ResizeToSprite(_prizeData.ItemSO.icon, iconMaxSize);
                icon.sprite = _prizeData.ItemSO.icon;
            }
            amountText.text = "x" + prizeData.Amount;
            SpinAction = prizeData.SpinAction;
        }

        /// <summary>
        /// Sets a custom spin action for this slice.
        /// </summary>
        /// <param name="action">The spin action to set.</param>
        public void SetSpinAction(SpinAction action)
        {
            SpinAction = action;
        }

        /// <summary>
        /// Retrieves the prize data for this slice.
        /// </summary>
        /// <returns>The prize data associated with this slice.</returns>
        public PrizeData GetPrizeData() => _prizeData;

        /// <summary>
        /// Gets the world position of the slice's icon.
        /// </summary>
        /// <returns>The world position of the icon, or the slice's position if the icon is null.</returns>
        public Vector3 GetIconPos() => icon != null ? icon.transform.position : transform.position;

        #endregion
    }
}