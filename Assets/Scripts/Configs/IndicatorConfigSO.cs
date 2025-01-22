using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace VertigoCaseProject.Configs
{
    /// <summary>
    /// Configuration data for the indicator system in the spin game.
    /// </summary>
    [CreateAssetMenu(fileName = "IndicatorConfigSO", menuName = "SpinGame/IndicatorConfigSO")]
    public class IndicatorConfigSO : ScriptableObject
    {
        #region Fields

        /// <summary>
        /// The space between consecutive indicator numbers.
        /// </summary>
        public float spaceBetweenNumbers;

        /// <summary>
        /// The maximum number of indicators that can be displayed.
        /// </summary>
        public int indicatorCapacity;

        /// <summary>
        /// The easing type used for indicator animations.
        /// </summary>
        public Ease easeType;

        /// <summary>
        /// Colors for normal, safe, and super zones.
        /// </summary>
        public Color normalZoneColor;
        public Color safeZoneColor;
        public Color superZoneColor;

        /// <summary>
        /// Colors for the current number in different zones.
        /// </summary>
        public Color normalZoneCurrentNumberColor;
        public Color safeZoneCurrentNumberColor;
        public Color superZoneCurrentNumberColor;

        /// <summary>
        /// Background sprites for different zones.
        /// </summary>
        public Sprite normalZoneBg;
        public Sprite safeZoneBg;
        public Sprite superZoneBg;

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the appropriate number color based on the zone type and whether it is the current number.
        /// </summary>
        /// <param name="roundZoneType">The type of the round zone.</param>
        /// <param name="isCurrentNumber">Whether the number is the current one.</param>
        /// <returns>The color for the number.</returns>
        public Color GetNumberColor(RoundZoneType roundZoneType, bool isCurrentNumber)
        {
            return isCurrentNumber ? roundZoneType switch
            {
                RoundZoneType.StandardZone => normalZoneCurrentNumberColor,
                RoundZoneType.SafeZone => safeZoneCurrentNumberColor,
                RoundZoneType.SuperZone => superZoneCurrentNumberColor,
                _ => normalZoneColor
            }
            : roundZoneType switch
            {
                RoundZoneType.StandardZone => normalZoneColor,
                RoundZoneType.SafeZone => safeZoneColor,
                RoundZoneType.SuperZone => superZoneColor,
                _ => normalZoneColor
            };
        }

        /// <summary>
        /// Retrieves the background sprite for a specific zone type.
        /// </summary>
        /// <param name="roundZoneType">The type of the round zone.</param>
        /// <returns>The background sprite.</returns>
        public Sprite GetBackgroundSprite(RoundZoneType roundZoneType)
        {
            return roundZoneType switch
            {
                RoundZoneType.StandardZone => normalZoneBg,
                RoundZoneType.SafeZone => safeZoneBg,
                RoundZoneType.SuperZone => superZoneBg,
                _ => normalZoneBg
            };
        }

        #endregion
    }
}