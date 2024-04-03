// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    /// <summary>
    /// Менеджер окон
    /// </summary>
    public class WindowManager : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;

        private static WindowManager _instance;
        private static Window _currentWindow;

        private const float EndTransparency = 0.5f;
        private const float StartTransparency = 0f;
        private const float Duration = 0.5f;

        private void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(gameObject);
            else
                _instance = this;

            HideBackground();
        }

        public static T OpenWindow<T>() where T : Window
        {
            if (_currentWindow != null)
                _currentWindow.Hide();

            if (_instance._backgroundImage != null)
                _instance._backgroundImage.transform.SetAsLastSibling();

            var window = WindowPool.Get<T>();
            _currentWindow = window;

            window.transform.SetParent(_instance.transform, false);
            window.Show();
            ShowBackground();

            return window;
        }

        public static void Close(GameObject window)
        {
            WindowPool.Release(window);
            HideBackground();
        }

        public static void CloseLast()
        {
            WindowPool.ReleaseLast();
            HideBackground();
        }

        private static void ShowBackground()
        {
            _instance._backgroundImage.gameObject.SetActive(true);
            _instance._backgroundImage.DOFade(EndTransparency, Duration);
        }

        private static void HideBackground()
        {
            _instance._backgroundImage.DOFade(StartTransparency, Duration)
                .OnComplete(() => _instance._backgroundImage.gameObject.SetActive(false));
        }
    }
}