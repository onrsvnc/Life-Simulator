using UnityEngine;
using Inventories;

namespace Player
{
    [CreateAssetMenu(fileName = "Clothing", menuName = "Clothing/Make New Clothing", order = 0)]
    public class ClothingConfig : EquipableItem
    {
        [SerializeField] Clothing equippedPrefab = null;

        const string clothingName = "Clothing";

        public Clothing Spawn(Transform clothingTransform)
        {
            DestroyOldClothing(clothingTransform);

            Clothing clothing = null;

            if (equippedPrefab != null)
            {
                clothing = Instantiate(equippedPrefab, clothingTransform);
                clothing.gameObject.name = clothingName;
            }

            return clothing;
        }

        private void DestroyOldClothing(Transform clothingTransform)
        {
            Transform oldClothing = clothingTransform.Find(clothingName);  
            if (oldClothing == null) return;

            oldClothing.name = "DESTROYING";
            Destroy(oldClothing.gameObject);
        }

    }
}


