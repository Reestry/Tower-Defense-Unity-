// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

public interface ITowerType
{
    void Shoot(Animator animator, Transform enemy, Transform gun, float bulletSpeed, int damage);
}