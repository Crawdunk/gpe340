using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : WeaponAgent
{
    [Header("Movement Settings")]
    [SerializeField, Tooltip("The max speed of the player")]
    public float speed;

    [Header("Health Settings")]
    [SerializeField, Tooltip("Health Bar")]
    public HealthBar healthBar;
    public Health health;

    private Animator animator;
    private Vector3 targetPos; //Set all this stuff
    private Vector3 startPos;
    private Vector3 mousePos;

    public GameManager gm;

    private void Awake()
    {
        animator = GetComponent<Animator>(); //Grab the animator on our player
        health = GetComponent<Health>();
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        health.currentHealth = health.maxHealth;
        healthBar.SetHealth(health.maxHealth);
    }

    private void Update()
    {
        if (gm.isPaused)
            return;

        //Look at mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f; //Grab a plane where the player is and a ray where the mouse is located
        
        if (playerPlane.Raycast(ray, out hitDist))
            {
                Vector3 targetPoint = ray.GetPoint(hitDist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                targetRotation.x = 0;
                targetRotation.z = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);

            }//Rotate towards the direction of the ray created before

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f); //Lock the player from rotating within a certain area and making it so the player doesn't move towards rotation angle
        input = transform.InverseTransformDirection(input);
        animator.SetFloat("Right", input.x);
        animator.SetFloat("Forward", input.z);

        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isSprinting", true);
            speed = 4f;                        
        }
        else
        {
            animator.SetBool("isSprinting", false);
            speed = 2f;
        }

        //Unequip Weapon
        if (Input.GetKeyDown(KeyCode.V))
        {
            base.UnequipWeapon();
        }
    }    

    public void OnAnimatorIK(int layerIndex)
    {
        if (weaponIsEquipped == true)
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, equippedWeapon.leftHandPoint.position);
            animator.SetIKPosition(AvatarIKGoal.RightHand, equippedWeapon.rightHandPoint.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, equippedWeapon.leftHandPoint.rotation);
            animator.SetIKRotation(AvatarIKGoal.RightHand, equippedWeapon.rightHandPoint.rotation);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        }
        
    }

    void LateUpdate()
    {
        healthBar.SetHealth(health.currentHealth);
    }
}
