using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private EnemyWave[] waves;
    [SerializeField] private TextMeshProUGUI waveText;
    
    [SerializeField] private float maxTime;
    
    private int waveIndex;
    private float timer;

    private void Start()
    {
         StartCoroutine(SetWaveText($"Wave {waveIndex + 1}"));
         SpawnWave();
    }

    private void Update()
    {
        if (waveIndex + 1 == waves.Length) return;
        
        timer += Time.deltaTime;

        if (timer < maxTime) return;
        
        timer = 0;
        waveIndex++;

        if (waveIndex + 1 == waves.Length)
        {
            StartCoroutine(SetWaveText($"Last Wave"));
        }
        else
        {
            StartCoroutine(SetWaveText($"Wave {waveIndex + 1}"));
        }
        
        SpawnWave();
    }

    private IEnumerator SetWaveText(string text)
    {
        waveText.gameObject.SetActive(true);
        waveText.text = text;
        
        Color color = waveText.color;
        int alpha = 255;

        for (int i = alpha; i > 0; i--)
        {
            alpha--;
            color.a = alpha / 255f;
            
            waveText.color = color;

            yield return new WaitForSeconds(0.01f);
        }
        
        waveText.gameObject.SetActive(false);
    }

    private void SpawnWave()
    {
        EnemyWave enemyWave = waves[waveIndex];
        
        Instantiate(enemyWave, Vector3.zero, Quaternion.identity);
    }
}