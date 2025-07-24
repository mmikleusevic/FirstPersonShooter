using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image playerHealthBarImage;

    [SerializeField] private int maxHealthPoints = 5;
    [SerializeField] private float maxHealth;
    [SerializeField] private float timerMax;
    
    private int healthPoints;
    private float currentHealth;
    private float timer;
    
    private void Start()
    {
        currentHealth = maxHealth;
        healthPoints = maxHealthPoints;
        SetHealthBar();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (healthPoints == maxHealthPoints || timer < timerMax) return;
        
        healthPoints = Mathf.Min(healthPoints + 1, maxHealthPoints);
        timer = 0;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        SetHealthBar();

        if (currentHealth > 0) return;
        
        Destroy(gameObject);
        GameManager.Instance.SaveHighScore();
    }

    private void SetHealthBar()
    {
        playerHealthBarImage.fillAmount = currentHealth / maxHealth;
    }

    public void Heal(float heal)
    {
        if (Mathf.Approximately(currentHealth, maxHealth) || healthPoints <= 0) return;
        
        currentHealth = Mathf.Min(currentHealth + heal, maxHealth);
        healthPoints--;
        SetHealthBar();
    }
}
