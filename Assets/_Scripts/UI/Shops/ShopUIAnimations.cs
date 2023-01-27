using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UI.Shops
{
    public class ShopUIAnimations : MonoBehaviour
    {
        [SerializeField] RectTransform shopRectTransform;
        [SerializeField] float hiddenShopRectTransform = 1000f;
        [SerializeField] float activateAnimationTime = 1f;
        [SerializeField] float deactivateAnimationTime = 1f;


        public void ActivateShopPanelAnimation()
        {
            shopRectTransform.transform.localPosition = new Vector3(-hiddenShopRectTransform, 155f, 0f);
            shopRectTransform.DOAnchorPos(new Vector2(345f, 155f), activateAnimationTime, false).SetEase(Ease.OutBack);
        }

        public void DeactivateShopPanelAnimation()
        {
            shopRectTransform.DOAnchorPos(new Vector3(-hiddenShopRectTransform, 155f, 0f), deactivateAnimationTime, false).SetEase(Ease.InBack);
        }

        void OnEnable()
        {
            ActivateShopPanelAnimation();
        }

    }

}

