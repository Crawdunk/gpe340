using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField, Tooltip("The max speed of the player")]
    private float speed;

    private Animator animator;

    private Vector3 targetPos; //Set all this stuff
    private Vector3 startPos;
    private Vector3 mousePos;

    private void Awake()
    {
        animator = GetComponent<Animator>(); //Grab the animator on our player
    }

    private void Update()
    {
        animator.SetFloat("Forward", Input.GetAxis("Vertical"));
        animator.SetFloat("Right", Input.GetAxis("Horizontal")); //Set the animator bools of Forward and Right to the V/H axis

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

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);
        input = transform.InverseTransformDirection(input);
        animator.SetFloat("Right", input.x);
        animator.SetFloat("Forward", input.z); //Grabbed this from the lecture, prevents the player from moving towards where they are looking

        mousePos = Input.mousePosition; //grab the mouses position
        Ray mouseCast = Camera.main.ScreenPointToRay(mousePos); //make a raycast on where my mouse is currently at
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        RaycastHit hit;
        float rayLength;
        if (Physics.Raycast(mouseCast, out hit, 100))
        {
            Debug.DrawLine(mousePos, targetPos, Color.blue);
            targetPos = new Vector3(hit.point.x, 0f, hit.point.z);

            // We need some distance margin with our movement
            // or else the character could twitch back and forth with slight movement
            if (Vector3.Distance(targetPos, transform.position) >= 2f)
            {
                transform.LookAt(targetPos);
            }
        }
    }
}
