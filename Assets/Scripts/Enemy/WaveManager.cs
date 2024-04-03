// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pause;
using UnityEngine;
using TestPool;

public class WaveManager : MonoBehaviour, IPauseHandler
{
    [SerializeField] private List<WaveConfig> _waveConfig;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private Transform _waveSpawner;

    public static WaveManager Instance { get; private set; }

    private readonly List<Enemy> _enemiesOnScene = new();

    private int _waveIndex = -1;
    private int _kills;
    private int _enemiesValue;
    private int _maxHealth;

    private bool _paused;

    public void SetPaused(bool isPaused)
    {
        _paused = isPaused;
    }

    public void NextWave()
    {
        _waveIndex++;
        _maxHealth++;
        _enemiesValue = 0;
        _kills = 0;

        if (_waveIndex > _waveConfig.Count - 1)
            _waveIndex = 0;

        EnemiesCount(_waveIndex);
        StartCoroutine(EnemySpawn(_waveIndex));
    }

    public int GetWaveCount()
    {
        return _maxHealth;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        CheckForNextWave();
        PauseManager.Register(this);
    }

    private void CheckForNextWave()
    {
        if (_enemiesOnScene.Count == 0 && _kills >= _enemiesValue)
            UIController.Instance.StopWave();
    }

    private void EnemiesCount(int waveIndex)
    {
        var waveConfig = _waveConfig[waveIndex];
        _enemiesValue += waveConfig.Wave.Values.Sum();
    }

    private IEnumerator EnemySpawn(int waveIndex)
    {
        yield return new WaitWhile(() => _paused);

        var waveConfig = _waveConfig[waveIndex];
        var enemyConfigs = waveConfig.Wave.Keys;
        var enemyCounts = waveConfig.Wave.Values;

        for (var enemyType = 0; enemyType < enemyConfigs.Count; enemyType++)
        {
            var enemyConfig = enemyConfigs[enemyType];
            var enemyCount = enemyCounts[enemyType];

            for (var j = 0; j < enemyCount; j++)
            {
                yield return new WaitWhile(() => _paused);

                var enemy = Pool.Get<Enemy>();
                enemy.SetData(_waypoints, waveConfig.Duration, _maxHealth);

                enemy.Died += ReleaseEnemy;
                _enemiesOnScene.Add(enemy);
                enemy.transform.position = _waveSpawner.transform.position;

                yield return new WaitForSeconds(waveConfig.SpawnDelay);
            }
        }

        yield return new WaitForSeconds(waveConfig.SpawnDelay);
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        enemy.Died -= ReleaseEnemy;
        _enemiesOnScene.Remove(enemy);
        Pool.Release(enemy);
        _kills++;
        CheckForNextWave();
    }

    private void OnDestroy()
    {
        PauseManager.UnRegister(this);
    }
}