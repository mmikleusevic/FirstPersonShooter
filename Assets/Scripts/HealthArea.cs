using System;
using System.Collections;
using UnityEngine;

public class HealthArea : MonoBehaviour
{
    [SerializeField] private float healAmount;

    private bool isHealing;

    private Coroutine healCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            healCoroutine = StartCoroutine(Heal(playerHealth));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            StopCoroutine(healCoroutine);
        }
    }

    private IEnumerator Heal(PlayerHealth playerHealth)
    {
        playerHealth.Heal(healAmount);
        
        yield return new WaitForSeconds(3f);
        
        yield return Heal(playerHealth);
    }
}
