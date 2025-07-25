using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    
    private float currentHealth;
    public float radius = 5.0f;
    public float power = 10.0f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0) return;
        
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 6.0f);
            }
        }
    }
}
