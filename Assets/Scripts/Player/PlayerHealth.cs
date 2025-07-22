using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image playerHealthBarImage;
    
    [SerializeField] private float maxHealth;

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
        GameManager.Instance.SaveHighScore();
    }

    private void SetHealthBar()
    {
        playerHealthBarImage.fillAmount = currentHealth / maxHealth;
    }
}
