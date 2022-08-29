using System;
using System.Collections;
using System.Collections.Generic;
using Lamnesia.InGame.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lamnesia.Player
{
    public class HealthScript : MonoBehaviour
    {
        public float health { get; private set; }

        public float enemyDamage = 10;

        private const float LowerBound = 0;
        private const float HigherBound = 100f;
        private bool isDead = false;
        private PlayerMovement playerMovemementScript;

        [SerializeField] private GameObject loseScreen;
        [SerializeField] private GameObject cameraLook;
        [SerializeField] private GameObject weaponObject;
        [SerializeField] private GameObject weaponObject2;
        [SerializeField] private GameObject startPoint;

        [Header("Health")]
        public float maxHealth = 100f;
        public Image healthBar;

        [Header("Death sound/music")] 
        public AudioClip deathMusic;
        public AudioClip deathSound;

        private void Awake()
        {
            //to be improved
            health = maxHealth;
            playerMovemementScript = GetComponentInParent<PlayerMovement>();
        }

        public void ChangeHealth(float value)
        {
            health += value;
            CheckBounds();
            UpdateHealthBar();
        }

        private void CheckBounds()
        {
            if (health <= LowerBound)
            {
                health = LowerBound;
                PlayDeath();
            }
            if (health >= HigherBound) health = HigherBound;
        }

        private void UpdateHealthBar()
        {
            healthBar.fillAmount = health / maxHealth;
        }

        public void PlayDeath()
        {
            if (loseScreen != null) loseScreen.SetActive(true);
            isDead = true;

            playerMovemementScript.isDead = true;
            cameraLook.GetComponent<MouseLook>().isDead = true;
            weaponObject.GetComponent<Weapon>().isDead = true;
            weaponObject2.GetComponent<Weapon>().isDead = true;
            GetComponent<WeaponManager>().isDead = true;

            Cursor.lockState = CursorLockMode.None;
            AudioManager.Instance.PlaySound(deathSound);
            AudioManager.Instance.PlayMusic(deathMusic);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (loseScreen.activeSelf) loseScreen.SetActive(false);
                SceneManager.LoadScene(0);
            }
            if (isDead)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    startPoint.SetActive(false);
                    loseScreen.SetActive(false);
                    SceneManager.LoadScene(1);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Contains("Enemy"))
            {
                ChangeHealth(-enemyDamage);
            }
        }
    }
}
