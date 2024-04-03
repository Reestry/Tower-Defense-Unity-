// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Pause;
using UI;

public class DeathUI : Window
{
    [SerializeField] private Button _restartButton;

    private void Awake()
    {
        _restartButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        PauseManager.Instance.SetPaused(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
    }
}