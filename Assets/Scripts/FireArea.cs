using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireArea : MonoBehaviour
{
    [SerializeField] private float DPS = 5;
    [SerializeField] private float changeSpeed = 3; 
    
    private Dictionary<EnemyHealth, Coroutine> damageCoroutines;
    private Coroutine playerDamageCoroutine;

    private void Start()
    {
        damageCoroutines = new Dictionary<EnemyHealth, Coroutine>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerDamageCoroutine = StartCoroutine(Damage(playerHealth));
        }
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.ChangeSpeed(-changeSpeed);
        }
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            damageCoroutines.TryAdd(enemyHealth, StartCoroutine(Damage(enemyHealth)));
        }
        if (other.TryGetComponent(out EnemyMovement enemyMovement))
        {
            enemyMovement.ChangeSpeed(-changeSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            StopCoroutine(playerDamageCoroutine);
        }
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.ChangeSpeed(changeSpeed);
        }
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            if (damageCoroutines.TryGetValue(enemyHealth, out Coroutine enemyDamageCoroutine))
            {
                StopCoroutine(enemyDamageCoroutine);
                damageCoroutines.Remove(enemyHealth);
            }
        }
        if (other.TryGetComponent(out EnemyMovement enemyMovement))
        {
            enemyMovement.ChangeSpeed(changeSpeed);
        }
    }

    private IEnumerator Damage(PlayerHealth playerHealth)
    {
        yield return new WaitForSeconds(1f);
        
        if (!playerHealth)
        {
            StopCoroutine(playerDamageCoroutine);

            yield break;
        }
        
        playerHealth.TakeDamage(DPS);
        
        yield return Damage(playerHealth);
    }
    
    private IEnumerator Damage(EnemyHealth enemyHealth)
    {
        yield return new WaitForSeconds(1f);
        
        if (!enemyHealth)
        {
            if (damageCoroutines.TryGetValue(enemyHealth, out Coroutine enemyDamage))
            {
                StopCoroutine(enemyDamage);
                damageCoroutines.Remove(enemyHealth);
            }

            yield break;
        }
        
        enemyHealth.TakeDamage(DPS);
        
        yield return Damage(enemyHealth);
    }
}
