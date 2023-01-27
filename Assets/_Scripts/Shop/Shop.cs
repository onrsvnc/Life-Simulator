using UnityEngine;
using System;
using System.Collections.Generic;
using Inventories;
using Player;
using System.Collections;
using UI.Shops;

namespace Shops
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] StockItemConfig[] stockConfig;
        [Range(0, 100)]
        [SerializeField] float sellingPercentage = 60f;
        [SerializeField] Shopper shopper;
        [SerializeField] ShopUIAnimations shopUianimations;


        [System.Serializable] class StockItemConfig
        {
            public InventoryItem item;
            public int initialStock;
            [Range(0, 100)]
            public float buyingDiscountPercentage;
        }

        Dictionary<InventoryItem, int> transaction = new Dictionary<InventoryItem, int>();
        Dictionary<InventoryItem, int> stock = new Dictionary<InventoryItem, int>();
        Shopper currentShopper = null;
        bool isBuyingMode = true;
        ItemCategory filter = ItemCategory.None;

        public event Action onChange;

        private void Awake() 
        {
            foreach(StockItemConfig config in stockConfig)
            {
                stock[config.item] = config.initialStock;
            }
        }

        public void SetShopper(Shopper shopper)
        {
            this.currentShopper = shopper;
        }

        public IEnumerable<ShopItem> GetFilteredItems() 
        { 
            foreach (ShopItem shopItem in GetAllItems())
            {
                InventoryItem item = shopItem.GetInventoryItem();
                if (filter == ItemCategory.None || item.GetCategory() == filter)
                {
                    yield return shopItem;
                }
            }
        }

        public IEnumerable<ShopItem> GetAllItems()
        {
            foreach (StockItemConfig config in stockConfig)
            {
                float price = GetPrice(config);
                int quantityInTransaction = 0;
                transaction.TryGetValue(config.item, out quantityInTransaction);
                int availability = GetAvailability(config.item);
                yield return new ShopItem(config.item, availability, price, quantityInTransaction);
            }
        }

        public void SelectFilter(ItemCategory category) 
        {
            filter = category;

            if (onChange != null)
            {
                onChange();
            }
        }

        public ItemCategory GetFilter() 
        { 
            return filter; 
        }
        
        public void SelectMode(bool isBuying) 
        {
            isBuyingMode = isBuying;
            if (onChange != null)
            {
                onChange();
            }
        }
        
        public bool IsBuyingMode() 
        { 
            return isBuyingMode;
        }

        public bool CanTransact() 
        {
            //Empty Transaction
            if (IsTransactionEmpty()) return false;
            //Not sufficent funds
            if (!HasSufficientFunds()) return false;
            //Not sufficent inventory space
            if (!HasInventorySpace()) return false;
            
            return true;
        }

        public bool HasInventorySpace()
        {
            Inventory shopperInventory = currentShopper.GetComponent<Inventory>();
            if (shopperInventory == null) return false;

            List<InventoryItem> flatItems = new List<InventoryItem>();
            foreach (ShopItem shopItem in GetAllItems())
            {
                InventoryItem item = shopItem.GetInventoryItem();
                int quantity = shopItem.GetQuantityInTransaction();
                for (int i = 0; i < quantity; i++)
                {
                    flatItems.Add(item);
                }
            }

            return shopperInventory.HasSpaceFor(flatItems);
        }

        public bool HasSufficientFunds()
        {
            Purse purse = currentShopper.GetComponent<Purse>();
            if (purse == null) return false;

            return purse.GetBalance() > TransactionTotal();
        }

        private bool IsTransactionEmpty()
        {
            return transaction.Count == 0;
        }

        public void ConfirmTransaction() 
        {
            Inventory shopperInventory = currentShopper.GetComponent<Inventory>();
            Purse shopperPurse = currentShopper.GetComponent<Purse>();
            if (shopperInventory == null || shopperPurse == null) return;

            // Transfer to or from the inventory
            foreach (ShopItem shopItem in GetAllItems())
            {
                InventoryItem item = shopItem.GetInventoryItem();
                int quantity = shopItem.GetQuantityInTransaction();
                float price = shopItem.GetPrice();
                for (int i = 0; i < quantity; i++)
                {
                    if(isBuyingMode)
                    {
                        BuyItem(shopperInventory, shopperPurse, item, price);
                    }
                    else
                    {
                        SellItem(shopperInventory, shopperPurse, item, price);
                    }

                }
            }

            if (onChange != null)
            {
                onChange();
            }

        }

        

        public float TransactionTotal() 
        {
            float total = 0;
            foreach (ShopItem item in GetAllItems())
            {
                total += item.GetPrice() * item.GetQuantityInTransaction();
            }
            return total;
        }

        public void AddToTransaction(InventoryItem item, int quantity) 
        {
            if (!transaction.ContainsKey(item))
            {
                transaction[item] = 0;
            }

            int availability = GetAvailability(item);
            if(transaction[item] + quantity > availability)
            {
                transaction[item] = availability;
            }
            else
            {
                transaction[item] += quantity;
            }

            if (transaction[item] <= 0)
            {
                transaction.Remove(item);
            }

            if (onChange != null)
            {
                onChange();
            }
        }

        private int GetAvailability(InventoryItem item)
        {
            if (isBuyingMode)
            {
                return stock[item];
            }

            return CountItemsInInventory(item);
        }

        private int CountItemsInInventory(InventoryItem item)
        {
            Inventory inventory = currentShopper.GetComponent<Inventory>();
            if (inventory == null) return 0;

            int total = 0;
            for (int i = 0; i < inventory.GetSize(); i++)
            {
                if(inventory.GetItemInSlot(i) == item)
                {
                    total += inventory.GetNumberInSlot(i);
                }
            }
            return total;
        }

        private float GetPrice(StockItemConfig config)
        {
            if (isBuyingMode)
            {
                return config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
            }

            return config.item.GetPrice() * (sellingPercentage / 100);
        }

        private void SellItem(Inventory shopperInventory, Purse shopperPurse, InventoryItem item, float price)
        {
            int slot = FindFirstItemSlot(shopperInventory, item);
            if (slot == -1) return;

            AddToTransaction(item, -1);
            shopperInventory.RemoveFromSlot(slot, 1);
            stock[item]++;
            shopperPurse.UpdateBalance(price);
        }



        private void BuyItem(Inventory shopperInventory, Purse shopperPurse, InventoryItem item, float price)
        {
            if (shopperPurse.GetBalance() < price) return;

            bool success = shopperInventory.AddToFirstEmptySlot(item, 1);
            if (success)
            {
                AddToTransaction(item, -1);
                stock[item]--;
                shopperPurse.UpdateBalance(-price);
            }
        }

        private int FindFirstItemSlot(Inventory shopperInventory, InventoryItem item)
        {
            for (int i = 0; i < shopperInventory.GetSize(); i++)
            {
                if(shopperInventory.GetItemInSlot(i) == item)
                {
                    return i;
                }
            }
            return -1;
        }

        public void OpenShop()
        {
            shopper.SetActiveShop(this);
        }

        // private void OnTriggerStay2D(Collider2D other) 
        // {
        //     if(other.gameObject.tag == "Player")
        //     {
        //         if (Input.GetKey(KeyCode.E))
        //         {
        //             other.gameObject.GetComponent<Shopper>().SetActiveShop(this);
        //         }
        //     }
        // }

        private void OnTriggerExit2D(Collider2D other) 
        {
            if (other.gameObject.tag == "Player")
            {
                {
                    StartCoroutine("CloseShopWithAnimation");
                }
            }
        }

        IEnumerator CloseShopWithAnimation()
        {
            shopUianimations.DeactivateShopPanelAnimation();
            yield return new WaitForSeconds(1f);
            shopper.SetActiveShop(null);
        }








    }
    
    
    


}

