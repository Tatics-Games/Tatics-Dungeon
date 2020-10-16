﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame
{
    public class CharacterHealth : MonoBehaviour
    {
        public int maxHealth;
        public int currentHealth;
        public CharacterStats characterStats;
        public HealthBar healthBar;
        public PlayerManager playerManager;
        public GameObject statusPanel;

        // Start is called before the first frame update

        void Start()
        {
            healthBar = statusPanel.GetComponentInChildren<HealthBar>();
            characterStats = GetComponent<CharacterStats>();
            SetMaxHealthFromVitality();
            currentHealth = maxHealth;
        }

        public void SetMaxHealthFromVitality()
        {
            maxHealth = (int)characterStats.Vitality.Value * 10;
        }

        public void Heal(int healValue)
        {
            currentHealth = currentHealth + healValue <= maxHealth ? currentHealth + healValue : maxHealth;
        }

        public void TakeDamage(int damange)
        {
            if (damange == 0)
                return;

            currentHealth -= damange;
            healthBar.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                playerManager.HandleDeath();
            }
        }
    }
}
