// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using TestPool;

public class EnemyDamage : MonoBehaviour
{
    public void TakeDamage()
    {
        var particle = Pool.Get<ParticleController>();
        particle.transform.position = transform.position;
    }
}