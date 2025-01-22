using System;
using UnityEngine;

namespace VertigoCaseProject.Configs
{
    /// <summary>
    /// Holds configuration data for the wheel in the spin game, including spin settings and sprite mappings.
    /// </summary>
    [CreateAssetMenu(fileName = "WheelConfigSO", menuName = "SpinGame/WheelConfigSO")]
    public class WheelConfigSO : ScriptableObject
    {
        #region Fields

        /// <summary>
        /// The default number of spins the wheel performs before stopping.
        /// </summary>
        public int defaultSpinRound;

        /// <summary>
        /// The time it takes for the wheel to complete a spin.
        /// </summary>
        public float spinTime;

        /// <summary>
        /// The animation curve used for the wheel's spin easing.
        /// </summary>
        public AnimationCurve spinCurve;

        /// <summary>
        /// The sprites configuration for the normal zone.
        /// </summary>
        public WheelSpritesStruct normalZoneWheelSpritesStruct;

        /// <summary>
        /// The sprites configuration for the safe zone.
        /// </summary>
        public WheelSpritesStruct safeZoneWheelSpritesStruct;

        /// <summary>
        /// The sprites configuration for the super zone.
        /// </summary>
        public WheelSpritesStruct superZoneWheelSpritesStruct;

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the appropriate wheel sprites structure based on the zone type.
        /// </summary>
        /// <param name="roundZoneType">The type of the round zone.</param>
        /// <returns>The corresponding <see cref="WheelSpritesStruct"/>.</returns>
        public WheelSpritesStruct GetWheelSpritesStruct(RoundZoneType roundZoneType)
        {
            return roundZoneType switch
            {
                RoundZoneType.StandardZone => normalZoneWheelSpritesStruct,
                RoundZoneType.SafeZone => safeZoneWheelSpritesStruct,
                RoundZoneType.SuperZone => superZoneWheelSpritesStruct,
                _ => normalZoneWheelSpritesStruct
            };
        }

        #endregion
    }

    /// <summary>
    /// Represents a set of sprites used for the wheel and indicator in a specific zone.
    /// </summary>
    [Serializable]
    public struct WheelSpritesStruct
    {
        /// <summary>
        /// The sprite used for the wheel.
        /// </summary>
        public Sprite wheelSprite;

        /// <summary>
        /// The sprite used for the wheel indicator.
        /// </summary>
        public Sprite indicatorSprite;
    }
}