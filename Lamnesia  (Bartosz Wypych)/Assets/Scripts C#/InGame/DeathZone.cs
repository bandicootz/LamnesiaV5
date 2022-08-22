using UnityEngine;

namespace Lamnesia.Player
{
    public class DeathZone : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("You went where the devil says good night");
                if (collision.gameObject.TryGetComponent(out HealthScript healthScript))
                    healthScript.PlayDeath();
                else print("There is no HealthScript!");
            }
        }
    }
}
