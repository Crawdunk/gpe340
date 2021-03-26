using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField, Tooltip("Lifespan of the pickup")]
    public float lifespan;
    [SerializeField, Tooltip("The rotate speed of the pickup object")]
    public float rotateSpeed;

    public GameManager gm;

    private void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player)
        {
            OnPickUp(player);
        }
    }

    protected virtual void OnPickUp(Player player)
    {
        Destroy(gameObject);
    }
}
