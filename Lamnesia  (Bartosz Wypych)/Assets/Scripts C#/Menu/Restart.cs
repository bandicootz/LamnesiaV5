using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] private GameObject loseScreen;

    public void RestartLevel()
    {
        loseScreen.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
