using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VertigoCaseProject.Core.Interfaces;
using VertigoCaseProject.Helpers;
using VertigoCaseProject.UI;
using Random = UnityEngine.Random;

namespace VertigoCaseProject.Core.Handlers
{
    /// <summary>
    /// Handles the flow animation of prizes from the spinning slice to the collected prize content.
    /// </summary>
    public class FlowingPrizeHandler : MonoBehaviour, IFlowingPrizeHandler
    {
        #region Serialized Fields

        [SerializeField]
        private List<Image> flowingPrizePool;

        #endregion

        #region Private Fields

        private FlowingAnimationHelper _animationHelper;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (_animationHelper == null)
            {
                _animationHelper = new FlowingAnimationHelper();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sends flowing prize images from the spinning slice to the prize content with an animation.
        /// </summary>
        /// <param name="slice">The spinning slice containing prize data.</param>
        /// <param name="content">The target collected prize content.</param>
        /// <param name="onFlowComplete">The callback action triggered when the flow animation is complete.</param>
        public void SendFlowingPrize(SpinSlice slice, CollectedPrizeContent content, Action onFlowComplete)
        {
            bool hasCalledComplete = false;
            var prizes = GetFlowingPrizes();

            foreach (var prizeImage in prizes)
            {
                prizeImage.rectTransform.ResizeToSprite(slice.GetPrizeData().ItemSO.icon, new Vector2(100, 100));
                prizeImage.sprite = slice.GetPrizeData().ItemSO.icon;
                prizeImage.gameObject.SetActive(true);

                prizeImage.transform.position = slice.GetIconPos();
                prizeImage.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);

                Vector3 midPos = transform.position + RandomizePosition();
                Vector3 endPos = content.GetIconPos();

                var sequence = _animationHelper.AnimateFlow(prizeImage, midPos, endPos, 0.5f);

                sequence.OnComplete(() =>
                {
                    prizeImage.gameObject.SetActive(false);
                    flowingPrizePool.Add(prizeImage);

                    if (!hasCalledComplete)
                    {
                        onFlowComplete?.Invoke();
                        hasCalledComplete = true;
                    }
                });
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieves a random subset of images from the flowing prize pool.
        /// </summary>
        /// <returns>A list of images to be animated.</returns>
        private List<Image> GetFlowingPrizes()
        {
            int randomAmount = Mathf.Clamp(Random.Range(3, 7), 0, flowingPrizePool.Count);
            var result = new List<Image>();

            for (int i = 0; i < randomAmount; i++)
            {
                var img = flowingPrizePool[0];
                flowingPrizePool.RemoveAt(0);
                result.Add(img);
            }
            return result;
        }

        /// <summary>
        /// Generates a random offset position for the mid-point of the prize animation.
        /// </summary>
        /// <returns>A randomized vector offset.</returns>
        private Vector3 RandomizePosition()
        {
            float interval = 100;
            return new Vector3(
                Random.Range(-interval, interval),
                Random.Range(-interval, interval),
                0
            );
        }

        #endregion
    }
}
