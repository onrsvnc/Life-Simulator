using UnityEngine;
using Inventories;
using Utils;

namespace Player
{
    public class PlayerTrousers : MonoBehaviour
    {

        [SerializeField] Transform r_legTransform;
        [SerializeField] Transform l_legTransform;
        [SerializeField] ClothingConfig defaultClothing;

        Equipment equipment;
        ClothingConfig currentClothingConfig;
        ClothingConfig currentClothingConfig2;
        LazyValue<Clothing> currentClothing;
        LazyValue<Clothing> currentClothing2;

        private void Awake()
        {
            currentClothingConfig = defaultClothing;
            currentClothing = new LazyValue<Clothing>(SetupDefaultClothing);
            currentClothing2 = new LazyValue<Clothing>(SetupDefaultClothing2);
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

        private Clothing SetupDefaultClothing2()
        {
            return AttachClothing2(defaultClothing);
        }

        private void Start()
        {
            currentClothing.ForceInit();
            currentClothing2.ForceInit();
        }

        public void EquipClothing(ClothingConfig clothing)
        {
            currentClothingConfig = clothing;
            currentClothing.value = AttachClothing(clothing);
            currentClothingConfig2 = clothing;
            currentClothing2.value = AttachClothing2(clothing);
        }

        private void UpdateClothing()
        {
            var clothing = equipment.GetItemInSlot(EquipLocation.Trousers) as ClothingConfig;
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
            return clothing.Spawn(r_legTransform);
        }
        private Clothing AttachClothing2(ClothingConfig clothing)
        {
            return clothing.Spawn(l_legTransform);
        }




    }

}

