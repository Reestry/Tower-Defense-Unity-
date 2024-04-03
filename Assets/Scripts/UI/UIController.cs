// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button _nextWave;
    [SerializeField] private Button _placeTower;
    [SerializeField] private TMP_Text _waveNumber;
    [SerializeField] private TMP_Text _moneyNumber;
    [SerializeField] private TMP_Text _healthNumber;

    public static UIController Instance { get; private set; }

    public void SetMoneyNumber(int money)
    {
        _moneyNumber.text = money.ToString();
    }

    public void SetHealthNumber(int health)
    {
        _healthNumber.text = health.ToString();
    }

    public void StopWave()
    {
        _nextWave.gameObject.SetActive(true);
        _placeTower.gameObject.SetActive(true);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        _nextWave.onClick.AddListener(NextWave);
        _placeTower.onClick.AddListener(PlaceTower);
    }

    private void PlaceTower()
    {
        TowerSelection.Instance.StartPlacingTower();
    }

    private void NextWave()
    {
        _nextWave.gameObject.SetActive(false);
        _placeTower.gameObject.SetActive(false);
        WaveManager.Instance.NextWave();
        SetWaveNumber();
    }

    private void SetWaveNumber()
    {
        _waveNumber.text = WaveManager.Instance.GetWaveCount().ToString();
    }
}