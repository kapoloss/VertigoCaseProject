using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using VertigoCaseProject.Configs;
using VertigoCaseProject.Core.Interfaces;
using VertigoCaseProject.UI;

namespace VertigoCaseProject.Core.Handlers
{
    /// <summary>
    /// Manages the prize panel by handling the collection, instantiation, and resetting of prize content.
    /// </summary>
    public class PrizePanelHandler : MonoBehaviour, IPrizePanelHandler
    {
        #region Serialized Fields

        [SerializeField] private GameObject collectedPrizePrefab;
        [SerializeField] private Transform collectedPrizeContainer;
        [SerializeField] private ScrollRect scrollRect;

        #endregion

        #region Private Fields

        private readonly List<CollectedPrizeContent> _collectedPrizes = new List<CollectedPrizeContent>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves or creates a collected prize content based on the provided prize data.
        /// </summary>
        /// <param name="prizeData">The prize data to initialize the content with.</param>
        /// <returns>The collected prize content.</returns>
        public CollectedPrizeContent GetCollectedPrize(PrizeData prizeData)
        { 
            var content = GetOrInstantiateCollectedPrize(prizeData);

            content.transform.SetParent(collectedPrizeContainer, false);
            content.Initialize(prizeData);
            _collectedPrizes.Add(content);

            return content;
        }

        /// <summary>
        /// Clears and destroys all collected prize content in the panel.
        /// </summary>
        public void ResetCollectedPrizes()
        {
            foreach (var prize in _collectedPrizes)
            {
                Destroy(prize.gameObject);
            }
            _collectedPrizes.Clear();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieves an existing collected prize content or instantiates a new one if it does not exist.
        /// </summary>
        /// <param name="prizeData">The prize data to match or initialize with.</param>
        /// <returns>The collected prize content.</returns>
        private CollectedPrizeContent GetOrInstantiateCollectedPrize(PrizeData prizeData)
        {
            var existingContent = _collectedPrizes
                .FirstOrDefault(c => c.item == prizeData.ItemSO);

            if (existingContent != null)
            {
                return existingContent;
            }

            return InstantiateCollectedPrize();
        }

        /// <summary>
        /// Instantiates a new collected prize content and adds it to the container.
        /// </summary>
        /// <returns>The newly instantiated collected prize content.</returns>
        private CollectedPrizeContent InstantiateCollectedPrize()
        {
            StartCoroutine(SmoothScroll(scrollRect.verticalNormalizedPosition, 0f));

            return Instantiate(collectedPrizePrefab, collectedPrizeContainer).GetComponent<CollectedPrizeContent>();
        }
        
        private IEnumerator SmoothScroll(float startValue, float endValue)
        {
            float elapsedTime = 0f;

            while (elapsedTime < 0.2f)
            {
                elapsedTime += Time.deltaTime;

                float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / 0.2f);

                scrollRect.verticalNormalizedPosition = newValue;

                yield return null;
            }

            scrollRect.verticalNormalizedPosition = endValue;

        }

        #endregion
    }
}
