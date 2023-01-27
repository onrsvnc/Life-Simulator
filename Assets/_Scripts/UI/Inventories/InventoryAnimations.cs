using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace UI.Inventories
{
    public class InventoryAnimations : MonoBehaviour
    {
        [SerializeField] float hiddenInventoryRectTransform = 1000f;
        [SerializeField] float hiddenEquipmentRectTransform = 1000f;

        [SerializeField] RectTransform inventoryRectTransform;
        [SerializeField] RectTransform equipmentRectTransform;

        [SerializeField] float activateAnimationTime = 1f;
        [SerializeField] float deactivateAnimationTime = 1f;


        public void ActivateInventoryPanelAnimation()
        {
            inventoryRectTransform.transform.localPosition = new Vector3(1259f, 157f, 0f);
            inventoryRectTransform.DOAnchorPos(new Vector2(-346.48f, -383f), activateAnimationTime, false).SetEase(Ease.OutBack);
        }

        public void DeactivateInventoryPanelAnimation()
        {
            inventoryRectTransform.DOAnchorPos(new Vector2(hiddenInventoryRectTransform, -383f), deactivateAnimationTime, false).SetEase(Ease.InBack);
        }




        public void ActivateEquipmentPanelAnimation()
        {
            equipmentRectTransform.transform.localPosition = new Vector3(51f, 876f, 0f);
            equipmentRectTransform.DOAnchorPos(new Vector2(44f, 157f), activateAnimationTime, false).SetEase(Ease.OutBack);
        }

        public void DeactivateEquipmentPanelAnimation()
        {
            equipmentRectTransform.DOAnchorPos(new Vector2(44f, hiddenEquipmentRectTransform), deactivateAnimationTime, false).SetEase(Ease.InBack);
        }





        public void DeactivatePanels()
        {
            StartCoroutine("Deactivation");
        }

        private void OnEnable()
        {
            ActivateInventoryPanelAnimation();
            ActivateEquipmentPanelAnimation();
        }

        IEnumerator Deactivation()
        {
            DeactivateInventoryPanelAnimation();
            DeactivateEquipmentPanelAnimation();
            yield return new WaitForSeconds(deactivateAnimationTime);
            gameObject.SetActive(false);
        }

    }

}

