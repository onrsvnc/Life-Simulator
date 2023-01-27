using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Shops;
using System.Collections;

namespace UI.Shops
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] Transform listRoot;
        [SerializeField] RowUI rowPrefab;
        [SerializeField] TextMeshProUGUI totalField;
        [SerializeField] Button confirmBuyButton;
        [SerializeField] Button switchModeButton;
        
        Shopper shopper = null;
        Shop currentShop = null;
        ShopUIAnimations shopUianimations;

        Color originalTotalTextColor;

        void Awake() 
        {
            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();
            shopUianimations = GetComponent<ShopUIAnimations>();
        }

        void Start()
        {
            originalTotalTextColor = totalField.color;

            if(shopper == null) { return; }

            shopper.activeShopChange += ShopChange;
            confirmBuyButton.onClick.AddListener(ConfirmTransaction);
            switchModeButton.onClick.AddListener(SwitchMode);

            ShopChange();
        }

        void Update()
        {
            CloseShopOnEscapeKey();
        }

        

        private void ShopChange()
        {
            if(currentShop != null)
            {
                currentShop.onChange -= RefreshUI;
            }
            currentShop = shopper.GetActiveShop();
            gameObject.SetActive(currentShop != null);

            foreach(FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.SetShop(currentShop);
            }

            if(currentShop == null) return;

            currentShop.onChange += RefreshUI;

            RefreshUI();
        }

        private void RefreshUI()
        {
            foreach (Transform child in listRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (ShopItem item in currentShop.GetFilteredItems())
            {
                RowUI row = Instantiate<RowUI>(rowPrefab, listRoot);
                row.Setup(currentShop, item);
            }

            totalField.text = $"Total: {currentShop.TransactionTotal():N1}$";
            totalField.color = currentShop.HasSufficientFunds() ? originalTotalTextColor : Color.red;
            confirmBuyButton.interactable = currentShop.CanTransact();
            TextMeshProUGUI switchModeText = switchModeButton.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI confirmText = confirmBuyButton.GetComponentInChildren<TextMeshProUGUI>();
            if (currentShop.IsBuyingMode())
            {
                switchModeText.text = "Switch To Selling";
                confirmText.text = "Buy";
            }
            else
            {
                switchModeText.text = "Switch to Buying";
                confirmText.text = "Sell";
            }

            foreach (FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.RefreshUI();
            }

        }

        public void CloseShop()
        {
            StartCoroutine("CloseShopWithAnimation");
        }

        void CloseShopOnEscapeKey()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                StartCoroutine("CloseShopWithAnimation");
            }
        }

        IEnumerator CloseShopWithAnimation()
        {
            shopUianimations.DeactivateShopPanelAnimation();
            yield return new WaitForSeconds(1f);
            shopper.SetActiveShop(null);
        } 

    
        public void ConfirmTransaction()
        {
            currentShop.ConfirmTransaction();
        }
        
        public void SwitchMode()
        {
            currentShop.SelectMode(!currentShop.IsBuyingMode());
        }


        
    }

}
