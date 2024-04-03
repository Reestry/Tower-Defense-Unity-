// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using DG.Tweening;
using UI;

public class SettingsWindow : Window
{
    [SerializeField] private Toggle _soundToggle;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Button _backButton;

    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

    public AudioMixerGroup MixerVolume;
    private const float OnSoundVariable = 0f;
    private const float OffSoundVariable = -45f;

    private const string MusicMixer = "MusicVolume";
    private const string SoundMixer = "SoundVolume";

    private const float Duration = 0.4f;

    private void Awake()
    {
        _backButton.onClick.AddListener(Close);

        _musicToggle.onValueChanged.AddListener(OnMusicToggleValueChanged);
        _soundToggle.onValueChanged.AddListener(OnSoundToggleValueChanged);

        _musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        _soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
    }

    private void OnEnable()
    {
        MixerVolume.audioMixer.GetFloat("MusicVolume", out var musicVolume);
        MixerVolume.audioMixer.GetFloat("SoundVolume", out var soundVolume);
        _musicSlider.value = musicVolume;
        _soundSlider.value = soundVolume;
    }

    private void OnMusicSliderValueChanged(float value)
    {
        MixerVolume.audioMixer.SetFloat(MusicMixer, value);
    }

    private void OnSoundSliderValueChanged(float value)
    {
        MixerVolume.audioMixer.SetFloat(SoundMixer, value);
    }

    private void OnMusicToggleValueChanged(bool isOn)
    {
        var volume = isOn ? OnSoundVariable : OffSoundVariable;
        MixerVolume.audioMixer.SetFloat("MusicVolume", volume);
    }

    private void OnSoundToggleValueChanged(bool isOn)
    {
        var volume = isOn ? OnSoundVariable : OffSoundVariable;
        MixerVolume.audioMixer.SetFloat("SoundVolume", volume);
    }

    private void Close()
    {
        transform.DOScale(Vector3.zero, Duration).SetAutoKill(true).OnComplete(WindowManager.CloseLast)
            .SetAutoKill(true);
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveListener(Close);

        _musicToggle.onValueChanged.RemoveListener(OnMusicToggleValueChanged);
        _soundToggle.onValueChanged.RemoveListener(OnSoundToggleValueChanged);

        _musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
        _soundSlider.onValueChanged.RemoveListener(OnSoundSliderValueChanged);
    }
}