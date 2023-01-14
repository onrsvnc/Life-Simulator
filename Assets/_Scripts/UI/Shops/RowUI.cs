using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Shops;


namespace UI.Shops
{
    public class RowUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameField;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI availabilityField;
        [SerializeField] TextMeshProUGUI priceField;
        [SerializeField] TextMeshProUGUI quantityField;

        Shop currentShop = null;
        ShopItem item = null;
       

        public void Setup(Shop currentShop, ShopItem item)
        {
            this.currentShop = currentShop;
            this.item = item;
            nameField.text = item.GetName();
            icon.sprite = item.GetIcon();
            availabilityField.text = $"{item.GetAvailability()}";
            priceField.text = $"{item.GetPrice():N1}$";
            quantityField.text = $"{item.GetQuantityInTransaction()}";
        }

        public void Add()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), 1);
        }

        public void Remove()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), -1);
        }

        
    }
}
    
