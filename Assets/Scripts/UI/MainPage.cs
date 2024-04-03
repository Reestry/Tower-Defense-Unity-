// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UI;

public class MainPage : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(LoadGame);
        _settingsButton.onClick.AddListener(LoadSettings);
        _exitButton.onClick.AddListener(LoadExit);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("MenuMusic");
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadSettings()
    {
        WindowManager.OpenWindow<SettingsWindow>();
    }

    private void LoadExit()
    {
        WindowManager.OpenWindow<QuitWindow>();
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(LoadGame);
        _settingsButton.onClick.RemoveListener(LoadSettings);
        _exitButton.onClick.RemoveListener(LoadExit);
    }
}