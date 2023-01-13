using UnityEngine;
using Inventories;
using Utils;

namespace Player
{
    public class PlayerClothes : MonoBehaviour
    {

        [SerializeField] Transform shirtTransform;
        [SerializeField] ClothingConfig defaultClothing;

        Equipment equipment;
        ClothingConfig currentClothingConfig;
        LazyValue<Clothing> currentClothing;

        private void Awake()
        {
            currentClothingConfig = defaultClothing;
            currentClothing = new LazyValue<Clothing>(SetupDefaultClothing);
            equipment = GetComponent<Equipment>();
            if (equipment)
            {
                equipment.equipmentUpdated += UpdateClothing;
            }
        }

        private Clothing SetupDefaultClothing()
        {
            return AttachClothing(defaultClothing);
        }

        private void Start()
        {
            currentClothing.ForceInit();
        }

        public void EquipClothing(ClothingConfig clothing)
        {
            currentClothingConfig = clothing;
            currentClothing.value = AttachClothing(clothing);
        }

        private void UpdateClothing()
        {
            var clothing = equipment.GetItemInSlot(EquipLocation.Shirt) as ClothingConfig;
            if (clothing == null)
            {
                EquipClothing(defaultClothing);
            }
            else
            {
                EquipClothing(clothing);
            }
        }

        private Clothing AttachClothing(ClothingConfig clothing)
        {
            return clothing.Spawn(shirtTransform);
        }



        
    }

}

