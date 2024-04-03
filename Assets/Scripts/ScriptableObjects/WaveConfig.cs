// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Configs/Waves")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private SerializableDictionary<EnemyConfig, int> _wave;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _duration;

    public SerializableDictionary<EnemyConfig, int> Wave => _wave;

    public float SpawnDelay => _spawnDelay;

    public float Duration => _duration;
}