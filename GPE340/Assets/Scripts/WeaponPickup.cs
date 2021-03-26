using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public WeaponAgent weaponAgent;
    public Weapon weaponToEquip;


    protected override void OnTriggerEnter(Collider collider)
    {
        weaponAgent = collider.gameObject.GetComponent<WeaponAgent>();
        base.OnTriggerEnter(collider);
    }

    void Update()
    {
        if(gm.isPaused)
            return;

        transform.Rotate(0, rotateSpeed, 0);
    }

    protected override void OnPickUp(Player player)
    {
        SpawnWeapon();
        base.OnPickUp(player);
    }

    void SpawnWeapon()
    {
        weaponAgent.EquipWeapon(weaponToEquip);
    }
}
