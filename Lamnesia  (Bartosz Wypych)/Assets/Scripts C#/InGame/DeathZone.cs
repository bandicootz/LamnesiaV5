using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Lamnesia.Player
{
    public class DeathZone : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (CompareTag("Player"))
            {
                Debug.Log("You went where the devil says good night");
                collision.gameObject.GetComponent<HealthScript>().PlayDeath();
            }
        }
    }
}
