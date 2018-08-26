using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    // Loading the level by scene name for main menu or game over screen
    public void LoadLevel(string name)
    {
        // setting game to normal speed
        Time.timeScale = 1;
        SceneManager.LoadScene(name);

    }

    // Quit the game for main menu or game over screen
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
