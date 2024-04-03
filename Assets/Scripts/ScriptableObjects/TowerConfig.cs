// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

/// <summary>
/// Пока не используется
/// </summary>
[CreateAssetMenu(fileName = "Tower", menuName = "Configs/Towers")]
public class TowerConfig : ScriptableObject
{
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;

    public float FireRate => _fireRate;

    public float BulletSpeed => _bulletSpeed;
}