using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lamnesia.InGame.Menu
{
    public class Credits : MonoBehaviour
    {
        public float secondsToClose;

        private void Awake()
        {
            StartCoroutine(CloseCredits());
        }

        IEnumerator CloseCredits()
        {
            yield return new WaitForSeconds(secondsToClose);
            gameObject.SetActive(false);
        }
    }
}