using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int currentHealth;
    public int maxHealth;

    private ItemDrops drops;
    private RagdollController ragdoll;
    private WeaponAgent agent;
    private bool dropIsSpawned;

    public GameManager gm;

    void Awake()
    {
        agent = GetComponent<WeaponAgent>();
    }

    void Start()
    {
        drops = GetComponent<ItemDrops>();
        ragdoll = GetComponent<RagdollController>();
        maxHealth = 100;
        currentHealth = maxHealth;
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(currentHealth <= 0 && gameObject.tag != "Player" && ragdoll != null)
        {           
            EnemyDie();
        }

        if (currentHealth >= maxHealth)
        {
            currentHealth = 100;
        }

        if (gameObject.tag == "Player" && currentHealth <= 0)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    public void EnemyDie()
    {
        Ragdoll();
        Destroy(agent.equippedWeapon);
        Destroy(gameObject, 5);
        
        if (drops != null && dropIsSpawned == false)
        {
            drops.DropRandomItem();
            dropIsSpawned = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
    }

    public void Heal(int healing)
    {
        currentHealth += healing;
    }

    public void Ragdoll()
    {
        ragdoll.StartRagdoll();
    }

    public IEnumerator PlayerDeath()
    {
        Ragdoll();
        gm.lives--;
        currentHealth = 100;
        yield return new WaitForSeconds(3);
        ragdoll.StopRagdoll();
        gm.Pause();
    }
}
