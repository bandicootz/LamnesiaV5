using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lamnesia.Player
{
    public class HealthScript : MonoBehaviour
    {
        public float health { get; private set; }
        
        private const float LowerBound = 0;
        private const float HigherBound = 100f;
        
        [Header("Health")]
        public float maxHealth = 100f;
        public Image healthBar;

        private void Awake()
        {
            //to be improved
            health = maxHealth;
        }

        public void ChangeHealth(float value)
        {
            health += value;
            CheckBounds();
            UpdateHealthBar();
        }

        private void CheckBounds()
        {
            if (health <= LowerBound) health = LowerBound;
            if (health >= HigherBound) health = HigherBound;
        }

        private void UpdateHealthBar()
        {
            healthBar.fillAmount = health / maxHealth;
        }
    }
}
