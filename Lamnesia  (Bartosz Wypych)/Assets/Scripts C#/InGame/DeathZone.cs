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

        void LateUpdate()
        {
            Debug.Log("You went where the devil says good night");
            healthScript.PlayDeath();
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}
