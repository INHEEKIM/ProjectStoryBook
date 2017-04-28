using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AplicationBehavior : MonoBehaviour {

    public void Quit()
    {

        Application.Quit();
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
