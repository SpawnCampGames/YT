using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private bool damagingPlayer;
    private PlayerHealth playerHealth;

    public int damageAmount = 5;
    public float timeBetweenDamage = 1.5f;

    private float damageCount;

    void Start()
    {
        damageCount = timeBetweenDamage;
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        if (damagingPlayer)
        {
            if (damageCount >= timeBetweenDamage)
            {
                // damage count over, damage the player
                playerHealth.DamagePlayer(damageAmount);
                damageCount = 0f;
            }

            damageCount += Time.deltaTime;
        }
        else
        {
            damageCount = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageCount = timeBetweenDamage;
            damagingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damagingPlayer = false;
        }
    }
}
