// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI;

public class UpgradeWindow : Window
{
    [SerializeField] private int _minLevel = 10;

    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _upgradeCost;
    [SerializeField] private TMP_Text _sell;

    [SerializeField] private TMP_Text _burstButtonText;
    [SerializeField] private TMP_Text _tripleShotButtonText;

    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _tripleTowerUpgrade;
    [SerializeField] private Button _burstTowerUpgrade;

    [SerializeField] private Sprite _lockedSprite;
    [SerializeField] private Sprite _tripleShotButtonSprite;
    [SerializeField] private Sprite _burstButtonSprite;

    private Tower _currentTower;

    public void SetData(Tower tower)
    {
        _currentTower = tower;

        UpdateText();
    }

    private void Start()
    {
        _upgradeButton.onClick.AddListener(Upgrade);
        _exitButton.onClick.AddListener(CloseWindow);
        _sellButton.onClick.AddListener(Sell);
        _tripleTowerUpgrade.onClick.AddListener(SetTripleShotType);
        _burstTowerUpgrade.onClick.AddListener(SetBurstType);
    }

    private void Upgrade()
    {
        _currentTower.Upgrade();

        UpdateText();
    }

    private void SetBurstType()
    {
        if (_currentTower.ReturnIsTypeUpgradet() || _currentTower.ReturnLevel() < _minLevel)
            return;

        if (_currentTower != null)
            _currentTower.SetBurstType();

        UpdateText();
    }

    private void SetTripleShotType()
    {
        if (_currentTower.ReturnIsTypeUpgradet() || _currentTower.ReturnLevel() < _minLevel)
            return;

        if (_currentTower != null)
            _currentTower.SetTripleShotType();

        UpdateText();
    }

    private void Sell()
    {
        _currentTower.Sell();
        CloseWindow();
    }

    private void UpdateText()
    {
        _level.text = _currentTower.ReturnLevel().ToString();
        _damage.text = _currentTower.ReturnDamage().ToString();
        _upgradeCost.text = _currentTower.ReturnUpgradeCost().ToString();
        _sell.text = $"Sell (+{_currentTower.ReturnSellPrice().ToString()})";

        if (_currentTower.ReturnIsTypeUpgradet() || _currentTower.ReturnLevel() < 10)
        {
            _tripleTowerUpgrade.image.sprite = _lockedSprite;
            _burstTowerUpgrade.image.sprite = _lockedSprite;
            _burstButtonText.text = "Locked";
            _tripleShotButtonText.text = "Locked";
        }
        else
        {
            _tripleTowerUpgrade.image.sprite = _tripleShotButtonSprite;
            _burstTowerUpgrade.image.sprite = _burstButtonSprite;
            _burstButtonText.text = "Burst Shot Type";
            _tripleShotButtonText.text = "Triple Shot Type";
        }
    }

    private void CloseWindow()
    {
        WindowManager.CloseLast();
        TowerCollision.UpgradeWindow = null;
    }

    private void OnDestroy()
    {
        _upgradeButton.onClick.RemoveListener(Upgrade);
        _exitButton.onClick.RemoveListener(CloseWindow);
        _sellButton.onClick.RemoveListener(Sell);
        _tripleTowerUpgrade.onClick.RemoveListener(SetTripleShotType);
        _burstTowerUpgrade.onClick.RemoveListener(SetBurstType);
    }
}