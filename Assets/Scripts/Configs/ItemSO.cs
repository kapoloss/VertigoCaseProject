using UnityEngine;

namespace VertigoCaseProject.Configs
{
    /// <summary>
    /// Represents an item in the spin game, including its description, name, and icon.
    /// </summary>
    [CreateAssetMenu(fileName = "ItemSO", menuName = "SpinGame/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        
        /// <summary>
        /// The name of the item.
        /// </summary>
        public string itemName;

        /// <summary>
        /// The icon representing the item.
        /// </summary>
        public Sprite icon;
    }
}