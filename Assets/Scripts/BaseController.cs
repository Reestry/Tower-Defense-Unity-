// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UI;

public class BaseController : MonoBehaviour
{
    private int _health = 20;

    private void Start()
    {
        UIController.Instance.SetHealthNumber(_health);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
            _health -= 1;

        UIController.Instance.SetHealthNumber(_health);
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_health > 0)
            return;

        WindowManager.OpenWindow<DeathUI>();
    }
}