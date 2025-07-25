using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWave : MonoBehaviour
{
    [SerializeField] private EnemyMovement[] enemyPrefabs;
    [SerializeField] private float spawnTimer = 3;
    [SerializeField] private float maxDuration;
    
    private Transform player;
    
    private float timer;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform;
        Invoke(nameof(Destroy), maxDuration);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer < spawnTimer) return;
        
        timer = 0;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        
        float xPosition = Random.Range(30, 50f);
        float zPosition = Random.Range(30, 50f);

        EnemyMovement enemyPrefab = enemyPrefabs[enemyIndex];
        
        EnemyMovement enemyMovement = Instantiate(enemyPrefab, new Vector3(xPosition, -0.5f, zPosition), Quaternion.identity);
        enemyMovement.SetTarget(player);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}