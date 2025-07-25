using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealPackSpawner : MonoBehaviour
{
    [SerializeField] private HealthPack healthPackPrefab;
    [SerializeField] private float maxTimer = 20;
    
    private float timer;
    
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer < maxTimer) return;

        SpawnHealthPack();
        timer = 0;
    }

    private void SpawnHealthPack()
    {
        float xPosition = Random.Range(0, 50f);
        float zPosition = Random.Range(0, 50f);
        
        Instantiate(healthPackPrefab, new Vector3(xPosition, -0.5f, zPosition), Quaternion.identity);
    }
}