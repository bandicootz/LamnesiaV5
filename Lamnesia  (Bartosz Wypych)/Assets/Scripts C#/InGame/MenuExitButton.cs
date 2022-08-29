using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuExitButton : MonoBehaviour
{
    public void Func()
    {
        SceneManager.LoadScene(0);
    }
}
