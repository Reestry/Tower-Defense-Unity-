// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Threading.Tasks;
using UnityEngine;
using TestPool;

public class BurstTowerType : ITowerType
{
    public void Shoot(Animator animator, Transform enemy, Transform gun, float bulletSpeed, int damage)
    {
        if (enemy == null)
            return;

        BurstShoot(animator, enemy, gun, bulletSpeed, damage);
    }

    private async void BurstShoot(Animator animator, Transform enemy, Transform gun, float bulletSpeed, int damage)
    {
        for (var i = 0; i < 2; i++)
        {
            AudioManager.Instance.PlaySound("Shoot");
            animator.SetTrigger("IsShoot");

            var bullet = Pool.Get<Bullet>();
            bullet.transform.position = gun.transform.position;

            var direction = enemy.transform.position - gun.transform.position;
            direction.Normalize();

            var targetRotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0, 0, 90);

            bullet.SetDirection(direction, targetRotation, bulletSpeed, damage);
            
            await Task.Delay(200);
        }
    }
}