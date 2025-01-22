using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VertigoCaseProject.Configs;
using VertigoCaseProject.Core.Interfaces;
using VertigoCaseProject.Helpers;
using VertigoCaseProject.UI;
using Random = UnityEngine.Random;

namespace VertigoCaseProject.Core.Handlers
{
    /// <summary>
    /// Handles the wheel functionality in the spin game, including initialization, spinning, and resetting.
    /// </summary>
    public class WheelHandler : MonoBehaviour, IWheelHandler
    {
        #region Serialized Fields

        [SerializeField] private List<SpinSlice> slices;
        [SerializeField] private WheelConfigSO wheelConfig;
        [SerializeField] private Image wheel;
        [SerializeField] private Image wheelIndicator;
        [SerializeField] private GameObject rotatePartsParent;

        #endregion

        #region Private Fields

        private WheelSpinCalculator _spinCalculator;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _spinCalculator ??= new WheelSpinCalculator();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the wheel with the first round of the spin game.
        /// </summary>
        /// <param name="spinGameSO">The spin game data.</param>
        public void InitializeWheel(SpinGameSO spinGameSO)
        {
            SetRound(spinGameSO.rounds[0]);
        }

        /// <summary>
        /// Sets the wheel to the specified round, updating its appearance and slices.
        /// </summary>
        /// <param name="roundData">The data for the round to set.</param>
        public void SetRound(RoundDataSO roundData)
        {
            rotatePartsParent.transform.localEulerAngles = Vector3.zero;
            var spritesStruct = wheelConfig.GetWheelSpritesStruct(roundData.roundZoneType);

            if (wheel != null) wheel.sprite = spritesStruct.wheelSprite;
            if (wheelIndicator != null) wheelIndicator.sprite = spritesStruct.indicatorSprite;

            SetSlices(roundData);
        }

        /// <summary>
        /// Spins the wheel and invokes the callback when the spin ends.
        /// </summary>
        /// <param name="onSpinEnded">The callback action invoked with the chosen slice when the spin ends.</param>
        public void Spin(Action<SpinSlice> onSpinEnded)
        {
            var chosenSlice = slices[Random.Range(0, slices.Count)];
            int sliceIndex = slices.IndexOf(chosenSlice);

            float turnDegree = _spinCalculator.CalculateSpinAngle(
                sliceIndex,
                slices.Count,
                wheelConfig.defaultSpinRound,
                rotatePartsParent.transform.localEulerAngles.z
            );

            rotatePartsParent.transform
                .DORotate(new Vector3(0, 0, turnDegree), wheelConfig.spinTime, RotateMode.WorldAxisAdd)
                .SetEase(wheelConfig.spinCurve)
                .OnComplete(() =>
                {
                    onSpinEnded?.Invoke(chosenSlice);
                });
        }

        /// <summary>
        /// Resets the wheel to its initial state using the spin game data.
        /// </summary>
        /// <param name="spinGameSO">The spin game data for resetting the wheel.</param>
        public void ResetWheel(SpinGameSO spinGameSO)
        {
            InitializeWheel(spinGameSO);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the prizes for the wheel slices based on the round data.
        /// </summary>
        /// <param name="roundData">The round data containing slice information.</param>
        private void SetSlices(RoundDataSO roundData)
        {
            var prizes = new List<PrizeData>();
            foreach (var sliceInfo in roundData.slices)
            {
                prizes.Add(sliceInfo.GetPrize());
            }
            prizes.Shuffle();

            for (int i = 0; i < prizes.Count; i++)
            {
                slices[i].SetPrize(prizes[i]);
            }
        }

        #endregion
    }
}
