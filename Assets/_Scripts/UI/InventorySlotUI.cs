using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Dragging;

namespace UI.Inventory
{
    public class InventorySlotUI : MonoBehaviour, IDragContainer<Sprite>
    {
        // CONFIG DATA
        [SerializeField] InventoryItemIcon icon = null;

        // PUBLIC

        public int MaxAcceptable(Sprite item)
        {
            if (GetItem() == null)
            {
                return int.MaxValue;
            }
            return 0;
        }

        public void AddItems(Sprite item, int number)
        {
            print(gameObject + "Add Item" + item);
            icon.SetItem(item);
        }

        public Sprite GetItem()
        {
            print(gameObject + "Get Item" + icon.GetItem());
            return icon.GetItem();
        }

        public int GetNumber()
        {
            return 1;
        }

        public void RemoveItems(int number)
        {
            print(gameObject + "Remove Item" + icon.GetItem());
            icon.SetItem(null);
        }
    }
}
