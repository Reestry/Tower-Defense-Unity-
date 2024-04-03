// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using DG.Tweening;

namespace UI
{
    /// <summary>
    /// Родительский класс окон
    /// </summary>
    public class Window : MonoBehaviour
    {
        private const float MinimizedScale = 0.4f;
        private const float ScaleAnimationDuration = 0.4f;

        public void Show()
        {
            transform.localScale = new Vector3(MinimizedScale, MinimizedScale, MinimizedScale);
            transform.DOScale(Vector3.one, ScaleAnimationDuration)
                .SetAutoKill(true);

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}