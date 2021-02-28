using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRifle : Weapon
{
    public float timeBetweenShots;

    public GameObject bulletPrefab;
    public int damageDone;
    public float fireSpeed;

    public Transform firePoint;

    private float nextShootTime;
    public bool _isShootingFullAuto;

    public override void Start()
    {
        nextShootTime = Time.time;
        base.Start();
    }

    public override void Update()
    {
        if (_isShootingFullAuto)
        {
            ShootBullet();
        }

        base.Update();
    }

    public override void AttackStart()
    {
        base.AttackStart();
    }

    public override void AttackEnd()
    {
        base.AttackEnd();
    }

    public void StartFullAuto()
    {
        _isShootingFullAuto = true;
    }

    public void StopFullAuto()
    {
        _isShootingFullAuto = false;
    }

    public void ShootBullet()
    {

        if(Time.time >= nextShootTime)
        {
            //instantiate a bullet
            GameObject myBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet myBulletScript = myBullet.GetComponent<Bullet>();

            myBulletScript.damageDone = damageDone;
            myBulletScript.fireSpeed = fireSpeed;
            //Delay the next shot
            nextShootTime = Time.time + timeBetweenShots;
        }


    }
}
