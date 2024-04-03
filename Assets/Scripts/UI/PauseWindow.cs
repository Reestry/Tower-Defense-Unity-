// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Pause;
using UI;

public class PauseWindow : Window
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(Resume);
        _exitButton.onClick.AddListener(ExitGame);
    }

    private void Resume()
    {
        PauseManager.Instance.SetPaused(false);
        WindowManager.CloseLast();
    }

    private void Update()
    {
        if (!PauseManager.IsPaused)
            return;

        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        PauseManager.Instance.SetPaused(false);
        WindowManager.Close(gameObject);
    }

    private void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        _resumeButton.onClick.RemoveListener(Resume);
        _exitButton.onClick.RemoveListener(ExitGame);
    }
}