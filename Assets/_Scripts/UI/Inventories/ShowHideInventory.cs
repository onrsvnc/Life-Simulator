using System;
using System.Collections;
using System.Collections.Generic;
using Shops;
using UnityEngine;

namespace UI.Inventories
{
    public class ShowHideInventory : MonoBehaviour
    {
        [SerializeField] KeyCode toggleKey = KeyCode.Mouse1;
        [SerializeField] GameObject uiContainer = null;

        Shopper shopper;

        void Awake() 
        {
            shopper = GetComponent<Shopper>();
            uiContainer.SetActive(false);
        }

        void Start()
        {
            
            shopper.activeShopChange += OpenInventoryOnShop;
        }

        void Update()
        {
            ToggleInventory();
            CloseInventory();
        }

        public void OpenInventoryOnShop()
        {
            if (shopper.GetActiveShop() != null)
            {
                if (!uiContainer.activeSelf)
                {
                    uiContainer.SetActive(true);
                }
            }
        }

        public void ToggleInventory()
        {
            if (Input.GetKeyDown(toggleKey) && !uiContainer.activeSelf)
            {
                uiContainer.SetActive(true);
            }

            else if(Input.GetKeyDown(toggleKey) && uiContainer.activeSelf)
            {
                uiContainer.GetComponent<InventoryAnimations>().DeactivatePanels();
            }
        }

        public void CloseInventory()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && uiContainer.activeSelf)
            {
                uiContainer.GetComponent<InventoryAnimations>().DeactivatePanels();
            }
            
        }

    }
}