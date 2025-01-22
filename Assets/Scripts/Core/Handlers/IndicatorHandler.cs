using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VertigoCaseProject.Configs;
using VertigoCaseProject.Core.Interfaces;
using VertigoCaseProject.UI;

namespace VertigoCaseProject.Core.Handlers
{
    /// <summary>
    /// Handles the UI indicators for displaying and animating the current round information in the spin game.
    /// </summary>
    public class IndicatorHandler : MonoBehaviour, IIndicatorHandler
    {
        #region Serialized Fields

        [SerializeField] private IndicatorConfigSO config;
        [SerializeField] private List<IndicatorNumber> numberPool;
        [SerializeField] private List<Image> currentNumberBackgrounds;

        #endregion

        #region Private Fields

        private RingBuffer<IndicatorNumber> _ringBuffer;
        private SpinGameSO _spinGameSO;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the indicators with the provided spin game data.
        /// </summary>
        /// <param name="spinGameSO">The spin game data.</param>
        public void InitializeIndicator(SpinGameSO spinGameSO)
        {
            foreach (var number in numberPool)
            {
                number.gameObject.SetActive(false);
            }

            _spinGameSO = spinGameSO;
            _ringBuffer = new RingBuffer<IndicatorNumber>(
                numberPool.ToList(), 
                config.spaceBetweenNumbers, 
                config.indicatorCapacity
            );
            SetUpInitialNumbers();
            SetUpBackgrounds();
        }

        /// <summary>
        /// Updates the indicator to reflect the current round index.
        /// </summary>
        /// <param name="currentIndex">The current round index.</param>
        public void IncreaseIndicator(int currentIndex)
        {
            StopAllNumberAnimations();

            if (currentIndex + (config.indicatorCapacity + 1) / 2 < _spinGameSO.rounds.Count)
            {
                AddNextNumber();
            }

            AnimateBackgroundTransition(currentIndex);
            AnimateNumbersTransition(currentIndex);
        }

        /// <summary>
        /// Resets the indicator with the provided spin game data.
        /// </summary>
        /// <param name="spinGameSO">The spin game data to reset the indicators.</param>
        public void ResetIndicator(SpinGameSO spinGameSO)
        {
            InitializeIndicator(spinGameSO);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets up the initial numbers in the indicator based on the configuration.
        /// </summary>
        private void SetUpInitialNumbers()
        {
            int initialCount = (config.indicatorCapacity + 1) / 2;
            for (int i = 0; i < initialCount; i++)
            {
                AddNextNumber();
            }
        }

        /// <summary>
        /// Sets up the background sprites and positions for the indicators.
        /// </summary>
        private void SetUpBackgrounds()
        {
            for (int i = 0; i < currentNumberBackgrounds.Count; i++)
            {
                var bg = currentNumberBackgrounds[i];
                var zoneType = _spinGameSO.rounds[i].roundZoneType;
                bg.sprite = config.GetBackgroundSprite(zoneType);
                bg.transform.localPosition = new Vector3(i * config.spaceBetweenNumbers, 0, 0);
            }
        }

        /// <summary>
        /// Adds the next number to the indicator and updates its UI.
        /// </summary>
        private void AddNextNumber()
        {
            var indicatorNumber = _ringBuffer.GetNumber(out float xPos, out int index);
            indicatorNumber.gameObject.SetActive(true);
            var roundZoneType = _spinGameSO.rounds[index].roundZoneType;

            indicatorNumber.index = index;
            indicatorNumber.SetUI(
                index + 1,
                config.GetNumberColor(roundZoneType, index == 0),
                config.GetBackgroundSprite(roundZoneType)
            );
            indicatorNumber.SetLocalPosition(new Vector3(xPos, 0, 0));
        }

        /// <summary>
        /// Stops all ongoing animations for the indicator numbers.
        /// </summary>
        private void StopAllNumberAnimations()
        {
            foreach (var number in numberPool)
            {
                number.transform.DOComplete();
            }
        }

        /// <summary>
        /// Animates the background transition for the indicators.
        /// </summary>
        /// <param name="currentIndex">The current round index.</param>
        private void AnimateBackgroundTransition(int currentIndex)
        {
            for (int i = 0; i < currentNumberBackgrounds.Count; i++)
            {
                var bg = currentNumberBackgrounds[i];
                var zoneType = _spinGameSO.rounds[currentIndex + i].roundZoneType;

                bg.transform.DOComplete();
                bg.sprite = config.GetBackgroundSprite(zoneType);
                bg.transform.localPosition = new Vector3(i * config.spaceBetweenNumbers, 0, 0);
                bg.transform.DOLocalMoveX(bg.transform.localPosition.x - config.spaceBetweenNumbers, 0.2f)
                    .SetEase(config.easeType);
            }
        }

        /// <summary>
        /// Animates the transition of the numbers in the indicator.
        /// </summary>
        /// <param name="currentIndex">The current round index.</param>
        private void AnimateNumbersTransition(int currentIndex)
        {
            foreach (var number in numberPool)
            {
                float targetX = number.transform.localPosition.x - config.spaceBetweenNumbers;
                number.transform.DOLocalMoveX(targetX, 0.2f).SetEase(config.easeType);

                if (number.index == currentIndex)
                {
                    var zoneType = _spinGameSO.rounds[currentIndex].roundZoneType;
                    number.SetUI(
                        number.index + 1,
                        config.GetNumberColor(zoneType, false),
                        config.GetBackgroundSprite(zoneType)
                    );
                }
                else if (number.index == currentIndex + 1)
                {
                    var zoneType = _spinGameSO.rounds[currentIndex + 1].roundZoneType;
                    number.SetUI(
                        number.index + 1,
                        config.GetNumberColor(zoneType, true),
                        config.GetBackgroundSprite(zoneType)
                    );
                }
            }
        }

        #endregion
    }
}