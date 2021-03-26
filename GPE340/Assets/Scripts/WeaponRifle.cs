using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRifle : Weapon
{
    public Transform owner;
    public float timeBetweenShots;
    private float nextShootTime;
    public float reloadTime;
    private bool isShootingFullAuto;
    public bool canShoot;

    public int damageDone;

    private Enemy enemy;

    private GameManager gm;

    public void Awake()
    {
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    public override void Start()
    {
        nextShootTime = Time.time;
        isShootingFullAuto = false;
        isReloading = false;
        owner = transform.parent.parent;
        base.Start();      
    }

    public override void Update()
    {
        if(gm.isPaused)
            return;

        if (currentAmmo > 0 && !isReloading)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }

        if(canShoot)
        {
            if (owner.tag == "Player")
            {
                if (Input.GetButton("Fire1"))
                {
                    StartFullAuto();
                }
                else
                {
                    StopFullAuto();
                }

                if (isShootingFullAuto)
                {
                    AttackStart();
                }
            }
        }

        if (owner.tag == "Enemy" && enemy.playerDistance <= enemy.shootRange)
        {
            StartFullAuto();

            if(isShootingFullAuto)
            {
                AttackStart();
            }
        }
        else if (owner.tag == "Enemy" && enemy.playerDistance > enemy.shootRange)
        {
            StopFullAuto();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        base.Update();
    }

    public override void AttackStart()
    {
        ShootBullet();
        base.AttackStart();
    }

    public override void AttackEnd()
    {
        base.AttackEnd();
    }

    public void StartFullAuto()
    {
        isShootingFullAuto = true;
    }

    public void StopFullAuto()
    {
        isShootingFullAuto = false;
    }

    public void ShootBullet()
    {
        if (Time.time >= nextShootTime)
        {
            GameObject myBullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            currentAmmo--;
            Bullet myBulletScript = myBullet.GetComponent<Bullet>();

            myBulletScript.damageDone = damageDone;

            nextShootTime = Time.time + timeBetweenShots;
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
            
    }
}
