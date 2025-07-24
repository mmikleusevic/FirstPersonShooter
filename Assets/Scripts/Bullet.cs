using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxTimeAlive;
    [SerializeField] private float damage;
    
    private Rigidbody bulletRigidbody;
    
    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        bulletRigidbody.linearVelocity = transform.forward * speed;
        DestroyBullet(5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(damage);
            DestroyBullet();
        }
        else if (other.TryGetComponent(out TerrainCollider terrainCollider))
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet(float timeToDestroy = 0f)
    {
        Destroy(gameObject, timeToDestroy);
    }
}
