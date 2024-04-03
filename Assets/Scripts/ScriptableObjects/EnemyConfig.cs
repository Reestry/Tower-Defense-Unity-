// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Game/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _moneyForKill;

    public GameObject Prefab => _prefab;

    public int MoneyForKill => _moneyForKill;
}