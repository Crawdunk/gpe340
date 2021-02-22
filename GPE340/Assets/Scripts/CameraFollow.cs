using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //set a target to follow

    public Vector3 offset; //Set an offset so the camera doesnt clip to the player object

    void LateUpdate()
    {
        transform.position = target.position + offset; //move the position with the players position with the added offset
    }

}
