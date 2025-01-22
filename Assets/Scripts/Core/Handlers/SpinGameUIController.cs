using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using VertigoCaseProject.Core.Interfaces;
using VertigoCaseProject.EventBuses;

namespace VertigoCaseProject.Core.Handlers
{
    /// <summary>
    /// Controls the UI for the spin game, managing button interactions and panel visibility.
    /// </summary>
    public class SpinGameUIController : MonoBehaviour, ISpinGameUIController
    {
        #region Serialized Fields

        [Header("Buttons")]
        public Button spinButton;
        public Button exitButton;
        public Button giveUpButton;
        public Button reviveButton;
        public Button collectButton;
        public Button goBackButton;

        [Header("Panels")]
        public GameObject exitPanel;
        public GameObject losePanel;

        #endregion

        #region Events

        public event Action OnSpinClicked;
        public event Action OnExitClicked;
        public event Action OnGiveUpClicked;
        public event Action OnReviveClicked;
        public event Action OnCollectClicked;
        public event Action OnGoBackClicked;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            spinButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
            giveUpButton.onClick.RemoveAllListeners();
            reviveButton.onClick.RemoveAllListeners();
            collectButton.onClick.RemoveAllListeners();
            goBackButton.onClick.RemoveAllListeners();

            spinButton.onClick.AddListener(() => OnSpinClicked?.Invoke());
            exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());
            giveUpButton.onClick.AddListener(() => OnGiveUpClicked?.Invoke());
            reviveButton.onClick.AddListener(() => OnReviveClicked?.Invoke());
            collectButton.onClick.AddListener(() => OnCollectClicked?.Invoke());
            goBackButton.onClick.AddListener(() => OnGoBackClicked?.Invoke());
        }

        private void OnEnable()
        {
            OnExitClicked += HandleExitClicked;
            OnGoBackClicked += HandleGoBackClicked;
            OnReviveClicked += HandleReviveClicked;
            OnGiveUpClicked += HandleGiveUpClicked;
            OnCollectClicked += HandleCollectClicked;
        }

        private void OnDisable()
        {
            OnExitClicked -= HandleExitClicked;
            OnGoBackClicked -= HandleGoBackClicked;
            OnReviveClicked -= HandleReviveClicked;
            OnGiveUpClicked -= HandleGiveUpClicked;
            OnCollectClicked -= HandleCollectClicked;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Resets the UI elements to their default state.
        /// </summary>
        public void ResetSpinGameUI()
        {
            SetExitPanelActive(false);
            SetLosePanelActive(false);
            SetExitButtonActive(true);
            EnableSpinButtonActive(true);
        }

        /// <summary>
        /// Enables or disables the spin button.
        /// </summary>
        /// <param name="isActive">Whether the spin button should be active.</param>
        public void EnableSpinButtonActive(bool isActive)
        {
            spinButton.interactable = isActive;
        }

        /// <summary>
        /// Sets the exit button's active state.
        /// </summary>
        /// <param name="isActive">Whether the exit button should be active.</param>
        public void SetExitButtonActive(bool isActive)
        {
            exitButton.gameObject.SetActive(isActive);
        }

        /// <summary>
        /// Enables or disables the revive button.
        /// </summary>
        /// <param name="isActive">Whether the revive button should be active.</param>
        public void EnableReviveButtonActive(bool isActive)
        {
            reviveButton.interactable = isActive;
        }
        
        public void SetLosePanelActive(bool isActive)
        {
            losePanel.SetActive(isActive);
        }

        #endregion

        #region Private Methods

        private void SetExitPanelActive(bool isActive)
        {
            exitPanel.SetActive(isActive);
        }

        private void HandleExitClicked()
        {
            SetExitPanelActive(true);
        }

        private void HandleGoBackClicked()
        {
            SetExitPanelActive(false);
        }

        private void HandleReviveClicked()
        {
            SetLosePanelActive(false);
            
        }

        private void HandleGiveUpClicked()
        {
            GameEventBus.RaiseGameOver();
        }

        private void HandleCollectClicked()
        {
            GameEventBus.RaiseRewardsCollected();
        }
        

        #endregion

        #region Unity Editor Methods

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (spinButton == null)
            {
                var t = GameObject.Find("ui_spin_button");
                if (t != null) spinButton = t.GetComponent<Button>();
            }

            if (exitButton == null)
            {
                var t = GameObject.Find("ui_exit_button");
                if (t != null) exitButton = t.GetComponent<Button>();
            }

            if (giveUpButton == null)
            {
                var t = GameObject.Find("ui_lose_give_up_button");
                if (t != null) giveUpButton = t.GetComponent<Button>();
            }

            if (reviveButton == null)
            {
                var t = GameObject.Find("ui_lose_revive_button");
                if (t != null) reviveButton = t.GetComponent<Button>();
            }

            if (collectButton == null)
            {
                var t = GameObject.Find("ui_exit_collect_button");
                if (t != null) collectButton = t.GetComponent<Button>();
            }

            if (goBackButton == null)
            {
                var t = GameObject.Find("ui_exit_go_back_button");
                if (t != null) goBackButton = t.GetComponent<Button>();
            }
        }
#endif

        #endregion
    }
}
