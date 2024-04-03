// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections;
using UnityEngine;
using TestPool;

public class ParticleController : MonoBehaviour
{
    private const float ParticleLifeTime = 2f;

    private void OnEnable()
    {
        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(ParticleLifeTime);
        Pool.Release(this);
    }
}