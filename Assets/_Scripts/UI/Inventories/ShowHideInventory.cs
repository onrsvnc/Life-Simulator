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
        }

        void Start()
        {
            uiContainer.SetActive(false);
            shopper.activeShopChange += OpenInventory;
        }

        void Update()
        {
            ToggleInventory();
        }

        public void OpenInventory()
        {
            if(shopper.GetActiveShop() != null)
            {
                if (!uiContainer.activeSelf)
                {
                    uiContainer.SetActive(true);
                }
            }
            
        }

        private void ToggleInventory()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                uiContainer.SetActive(!uiContainer.activeSelf);
            }
        }
    }
}