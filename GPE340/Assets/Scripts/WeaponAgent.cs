using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAgent : MonoBehaviour
{
    public Weapon equippedWeapon;
    public bool weaponIsEquipped;
    public Transform attachPoint;

    public void EquipWeapon(Weapon weapon)
    {
        equippedWeapon = Instantiate (weapon) as Weapon;
        weaponIsEquipped = true;
        equippedWeapon.transform.SetParent(attachPoint);
        equippedWeapon.transform.localPosition = weapon.transform.localPosition;
        equippedWeapon.transform.localRotation = weapon.transform.localRotation;
    }

    public void UnequipWeapon()
    {
        if (equippedWeapon)
        {
            Destroy (equippedWeapon.gameObject);
            equippedWeapon = null;
            weaponIsEquipped = false;
        }
    }
}
