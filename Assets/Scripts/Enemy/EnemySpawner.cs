using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float maxTime;
    
    private Transform player;
    private float timer;

    private void Start()
    {
         player = FindFirstObjectByType<PlayerMovement>().transform;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer < maxTime) return;
        
        timer = 0;
        EnemyMovement enemyMovement = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemyMovement.SetTarget(player);
    }
}