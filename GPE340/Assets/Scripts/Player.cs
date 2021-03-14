using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField, Tooltip("The max speed of the player")]
    private float speed;
    public bool hasWeapon;
    public GameObject equippedWeapon;
    public Health health;
    public HealthBar healthBar;
    private Animator animator;

    private Vector3 targetPos; //Set all this stuff
    private Vector3 startPos;
    private Vector3 mousePos;

    public bool isDead;

    [Header("Weapons")]
    public Weapon weapon;


    private void Awake()
    {
        animator = GetComponent<Animator>(); //Grab the animator on our player
    }

    private void Start()
    {
        hasWeapon = true;
        equippedWeapon = GameObject.FindWithTag("Weapon");
        healthBar.SetMaxHealth(health.maxHealth);
        isDead = false;
    }

    private void Update()
    {
        animator.SetFloat("Forward", Input.GetAxis("Vertical"));
        animator.SetFloat("Right", Input.GetAxis("Horizontal")); //Set the animator bools of Forward and Right to the V/H axis

        if (isDead == false)
        {
            //Look at mouse
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDist = 0.0f;

            if (playerPlane.Raycast(ray, out hitDist))
            {
                Vector3 targetPoint = ray.GetPoint(hitDist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                targetRotation.x = 0;
                targetRotation.z = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);

            }

            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            input = Vector3.ClampMagnitude(input, 1f);
            input = transform.InverseTransformDirection(input);
            animator.SetFloat("Right", input.x);
            animator.SetFloat("Forward", input.z);
        }
        

        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Up the speed of movement when sprinting
            speed = 4f;
            animator.SetBool("isSprinting", true);
        }
        else
        {
            //When not h olding lower the speed and set sprinting to false
            speed = 2f;
            animator.SetBool("isSprinting", false);
        }

        if (health.currentHealth >= 100)
        {
            health.currentHealth = 100;
            healthBar.SetMaxHealth(health.maxHealth);
        }

        if (health.currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
            Debug.Log("You Died.");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            weapon.AttackStart();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            health.TakeDamage(10);
        }

    }

    public void OnAnimatorIK(int layerIndex)
    {
        if (hasWeapon == true)
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.leftHandPoint.position);
            animator.SetIKPosition(AvatarIKGoal.RightHand, weapon.rightHandPoint.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.leftHandPoint.rotation);
            animator.SetIKRotation(AvatarIKGoal.RightHand, weapon.rightHandPoint.rotation);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        }
        
    }
}
