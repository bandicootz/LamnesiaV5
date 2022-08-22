using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Lamnesia.Player
{
    public class DeathZone : MonoBehaviour
    {
        HealthScript healthScript;

        private void Start()
        {
            healthScript = GetComponentInParent<HealthScript>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (CompareTag("Player"))
                GetComponent<HealthScript>().PlayDeath();
            Debug.Log("You went where the devil says good night");
        }
    }
}
