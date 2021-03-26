using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [Header("IK Points")]
    public Transform rightHandPoint;
    public Transform leftHandPoint;

    [Header("Firing Events")]
    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackEnd;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Ammo Settings")]
    public float maxAmmo;
    public float currentAmmo;
    public bool isReloading;

    public virtual void Start()
    {
        currentAmmo = maxAmmo;
    }

    public virtual void Update()
    {

    }

    public virtual void AttackStart()
    {
        OnAttackStart.Invoke();
    }

    public virtual void AttackEnd()
    {
        OnAttackEnd.Invoke();
    }
}
