using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Inventories
{
    public class ShowHideInventory : MonoBehaviour
    {
        [SerializeField] KeyCode toggleKey = KeyCode.Mouse1;
        [SerializeField] GameObject uiContainer = null;

        void Start()
        {
            uiContainer.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                uiContainer.SetActive(!uiContainer.activeSelf);
            }
        }
    }
}