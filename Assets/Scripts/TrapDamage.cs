using System.Collections;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damagePerSecond = 1f;

    [Header("Trap State")]
    public bool isActive = true;

    private PlayerHealth playerHealth;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health != null && isActive)
        {
            playerHealth = health;

            damageCoroutine = StartCoroutine(DamagePlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }

            playerHealth = null;
        }
    }

    private IEnumerator DamagePlayer()
    {
        while (isActive && playerHealth != null)
        {
            playerHealth.TakeDamage(damagePerSecond);

            yield return new WaitForSeconds(1f);
        }
    }

    public void SetTrapActive(bool active)
    {
        isActive = active;
    }
}