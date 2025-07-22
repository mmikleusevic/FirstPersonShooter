using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Image enemyHealthBarImage;
    
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private int score = 5;
    
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        SetHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        SetHealthBar();

        if (currentHealth > 0) return;
        
        Destroy(gameObject);
        GameManager.Instance.AddScore(score);
    }

    private void SetHealthBar()
    {
        enemyHealthBarImage.fillAmount = currentHealth / maxHealth;
    }
}