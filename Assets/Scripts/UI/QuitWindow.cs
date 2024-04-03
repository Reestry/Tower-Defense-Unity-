// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UI;

public class QuitWindow : Window
{
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    private const float Duration = 0.4f;

    private void Awake()
    {
        _yesButton.onClick.AddListener(Quit);
        _noButton.onClick.AddListener(Close);
    }

    private void Quit()
    {
        Debug.Log("Вы вышли из игры");
        Application.Quit();
    }

    private void Close()
    {
        transform.DOScale(Vector3.zero, Duration).SetAutoKill(true).OnComplete(WindowManager.CloseLast)
            .SetAutoKill(true);
    }

    private void OnDestroy()
    {
        _yesButton.onClick.RemoveListener(Quit);
        _noButton.onClick.RemoveListener(Close);
    }
}