using System.Collections;
using System.Collections.Generic;
using Lamnesia.InGame.Managers;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private UnityEvent interactAction;
    [SerializeField] private AudioClip clip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactAction.Invoke();
            if (clip != null) AudioManager.Instance.PlaySound(clip);
        }
    }
}