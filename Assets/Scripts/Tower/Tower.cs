// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;
using Pause;
using TestPool;

public class Tower : MonoBehaviour, IPauseHandler
{
    [SerializeField] private TowerCollision _tower;
    [SerializeField] private int _damage = 1;
    [SerializeField] private int _upgradeCost = 10;
    [SerializeField] private int _minLevel = 10;
    [SerializeField] private int _upgradeTypeCost = 200;

    [SerializeField] private Transform _gun;
    [SerializeField] private TowerConfig _towerConfig;
    [SerializeField] private Animator _animator;

    private ITowerType _towerType;

    private int _level = 1;
    private int _towerPrice;

    private float _fireTimer;
    private Transform _closestEnemy;
    private readonly List<Collider2D> _colliders = new();

    private bool _isTypeUpgradet;

    private bool _isPaused;

    private void UpgradeType()
    {
        _isTypeUpgradet = true;
    }

    public void SetPaused(bool isPaused)
    {
        _isPaused = isPaused;
    }

    public bool ReturnIsTypeUpgradet()
    {
        return _isTypeUpgradet;
    }

    public void Sell()
    {
        var sellprice = ReturnSellPrice();

        MoneyManager.Instance.AddMoney(sellprice);
        Pool.Release(_tower);
    }

    public void Upgrade()
    {
        if (!CanUpgrade())
            return;

        _damage += GetDamageIncrease();
        _level++;

        MoneyManager.Instance.SpendMoney(_upgradeCost);
        UpdateUpgradeCost();
        UIController.Instance.SetMoneyNumber(MoneyManager.Instance.GetMoney());
    }

    public void SetBurstType()
    {
        if (_isTypeUpgradet || _level < _minLevel)
            return;

        if (!CanUpgradeType())
            return;

        MoneyManager.Instance.SpendMoney(_upgradeTypeCost);
        _towerPrice += _upgradeTypeCost;

        SetTowerType(new BurstTowerType());
        UpgradeType();
    }

    public void SetTripleShotType()
    {
        if (_isTypeUpgradet || _level < _minLevel)
            return;

        if (!CanUpgradeType())
            return;

        MoneyManager.Instance.SpendMoney(_upgradeTypeCost);
        _towerPrice += _upgradeTypeCost;

        SetTowerType(new TripleShotTowerType());
        UpgradeType();
    }

    private void OnEnable()
    {
        _damage = 1;
        _level = 1;
        _upgradeCost = 10;

        _towerPrice = 20;
        _isTypeUpgradet = false;

        SetTowerType(new BasicTowerType());
        PauseManager.Register(this);
    }

    private void Update()
    {
        if (_isPaused)
            return;

        FindClosestEnemy();
        Rotate();

        _fireTimer += Time.deltaTime;
        if (_fireTimer >= _towerConfig.FireRate)
        {
            _fireTimer = 0f;

            if (_closestEnemy != null)
                Shoot(_closestEnemy);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
            _colliders.Add(col);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            _colliders.Remove(col);
            _closestEnemy = null;
        }
    }

    private void FindClosestEnemy()
    {
        var minDistance = Mathf.Infinity;

        foreach (var collider in _colliders)
        {
            var enemy = collider.gameObject;
            var distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (!(distance < minDistance))
                continue;

            minDistance = distance;
            _closestEnemy = enemy.transform;
        }
    }

    private void Rotate()
    {
        if (_closestEnemy == null)
            return;

        var moveDirection = (_closestEnemy.transform.position - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);

        transform.rotation = targetRotation * Quaternion.Euler(0, 0, 90);
    }

    private void SetTowerType(ITowerType towerType)
    {
        _towerType = towerType;
    }

    protected virtual void Shoot(Transform enemy)
    {
        _towerType.Shoot(_animator, enemy, _gun, _towerConfig.BulletSpeed, _damage);
    }

    private int GetDamageIncrease()
    {
        if (_level < 10)
            return 2 << (_level - 1);

        return 50 + 5 * (_level - 9);
    }

    private void UpdateUpgradeCost()
    {
        _towerPrice += _upgradeCost;
        _upgradeCost *= 2;
    }

    public int ReturnLevel()
    {
        return _level;
    }

    public int ReturnDamage()
    {
        return _damage;
    }

    public int ReturnUpgradeCost()
    {
        return _upgradeCost;
    }

    public int ReturnSellPrice()
    {
        return (int) (_towerPrice * 0.75f);
    }

    private bool CanUpgrade()
    {
        return MoneyManager.Instance.CanAfford(_upgradeCost);
    }

    private bool CanUpgradeType()
    {
        return MoneyManager.Instance.CanAfford(_upgradeTypeCost);
    }

    private void OnDisable()
    {
        PauseManager.UnRegister(this);
    }
}