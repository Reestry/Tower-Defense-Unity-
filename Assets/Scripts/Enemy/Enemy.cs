// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System;
using UnityEngine;
using DG.Tweening;
using Pause;
using TestPool;

public class Enemy : MonoBehaviour, IPauseHandler
{
    [SerializeField] private EnemyConfig _enemyConfig;
    [SerializeField] private EnemyDamage _enemyDamage;

    private Animator _animator;

    private const int StopAnimation = 0;
    private const int PlayAnimation = 1;

    private const float RotationSpeed = 0.01f;
    private Vector3[] _positions;
    private Tween _pathTween;

    private HealthBar _healthBar;

    public event Action<Enemy> Died;

    private int _health;
    private int _maxHealth;

    public void SetData(Transform[] waypoints, float duration, int health)
    {
        _health = health;
        _maxHealth = health;
        _positions = new Vector3[waypoints.Length];

        UpdateHealth(_maxHealth);

        for (var i = 0; i < waypoints.Length; i++)
            _positions[i] = waypoints[i].position;

        _pathTween = transform.DOPath(_positions, duration, PathType.Linear, PathMode.TopDown2D)
            .SetEase(Ease.Linear)
            .SetLookAt(RotationSpeed)
            .SetAutoKill(true);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _enemyDamage.TakeDamage();

        UpdateHealth(_health);

        if (_health > 0)
            return;

        MoneyManager.Instance.AddMoney(_enemyConfig.MoneyForKill);

        Death();
    }

    private void UpdateHealth(float newHealth)
    {
        var healthFill = newHealth / (_maxHealth);

        _healthBar.UpdateHealthBar(healthFill, _health);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Heart"))
            return;

        Death();
    }

    private void OnEnable()
    {
        _healthBar = Pool.Get<HealthBar>();
        _healthBar.SetData(gameObject);
        _animator = GetComponent<Animator>();

        PauseManager.Register(this);
    }

    private void Death()
    {
        if (_pathTween != null && _pathTween.IsActive())
            _pathTween.Kill();

        _animator = null;
        PauseManager.UnRegister(this);
        Pool.Release(_healthBar);
        Died?.Invoke(this);
    }

    public void SetPaused(bool isPaused)
    {
        if (_animator == null)
            return;

        if (isPaused)
        {
            _pathTween.Pause();
            _animator.speed = StopAnimation;
        }
        else
        {
            _pathTween.Play();
            _animator.speed = PlayAnimation;
        }
    }

    private void OnDestroy()
    {
        _animator = null;
    }
}