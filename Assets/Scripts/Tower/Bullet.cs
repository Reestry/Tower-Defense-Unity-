// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using Pause;
using TestPool;

public class Bullet : MonoBehaviour, IPauseHandler
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private float _speed;
    private Vector3 _direction;
    private Vector2 _velocity;

    private int _damage;

    public void SetPaused(bool isPaused)
    {
        _rigidbody2D.velocity = isPaused ? Vector2.zero : _velocity;
    }

    public void SetDirection(Vector3 direction, Quaternion rotation, float bulletSpeed, int damage)
    {
        transform.rotation = rotation;
        _speed = bulletSpeed;
        _direction = direction;
        _damage = damage;

        _direction = Quaternion.Euler(0, 0, -90) * transform.up;
        _velocity = _direction * _speed;
        _rigidbody2D.velocity = _velocity;
    }

    private void OnEnable()
    {
        PauseManager.Register(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            var enemy = col.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(_damage);

            ReleaseObject();
        }

        if (col.gameObject.CompareTag("Wall"))
            ReleaseObject();
    }

    private void ReleaseObject()
    {
        _speed = 0;
        _damage = 0;
        _direction = Vector3.zero;
        Pool.Release(this);
    }

    private void OnDestroy()
    {
        PauseManager.UnRegister(this);
    }
}