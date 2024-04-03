// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _health;
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _smoothSpeed;

    private Tween _fillTween;

    private Transform _target;

    public void UpdateHealthBar(float fill, int healthCount)
    {
        if (_fillTween != null && _fillTween.IsActive())
            _fillTween.Kill();

        _health.text = healthCount.ToString();
        _fillTween = _healthBar.DOFillAmount(fill, _smoothSpeed * Time.deltaTime);
    }

    public void SetData(GameObject target)
    {
        _target = target.transform;
    }

    private void Update()
    {
        var targetPosition = _target.position;
        transform.position = targetPosition;
    }
}