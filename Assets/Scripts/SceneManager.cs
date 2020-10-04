using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{

    public void LoadSquareScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadCircleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void LoadMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
