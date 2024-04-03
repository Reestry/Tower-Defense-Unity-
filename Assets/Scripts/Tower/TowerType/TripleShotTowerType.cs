// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using TestPool;

public class TripleShotTowerType : ITowerType
{
    public void Shoot(Animator animator, Transform enemy, Transform gun, float bulletSpeed, int damage)
    {
        if (enemy == null)
            return;

        AudioManager.Instance.PlaySound("Shoot");
        animator.SetTrigger("IsShoot");
        
        for (var i = 0; i < 3; i++)
        {
            var bullet = Pool.Get<Bullet>();
            bullet.transform.position = gun.transform.position;

            var direction = enemy.transform.position - gun.transform.position;
            direction.Normalize();

            var rotationZ = i switch
            {
                0 => // Первая пуля: +30 градусов
                    30f,
                1 => // Вторая пуля: направление врага
                    0f,
                2 => // Третья пуля: -30 градусов
                    -30f,
                _ => 0f
            };

            var targetRotation = Quaternion.LookRotation(Vector3.forward, direction) *
                                 Quaternion.Euler(0, 0, 90 + rotationZ);

            bullet.SetDirection(direction, targetRotation, bulletSpeed, damage);
        }
    }
}