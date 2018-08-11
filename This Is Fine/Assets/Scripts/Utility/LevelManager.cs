using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    // Loading the level by scene name
    public void LoadLevel(string name)
    {
        // setting game to normal speed
        Time.timeScale = 1;
        SceneManager.LoadScene(name);

    }

    // Quit the game
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
