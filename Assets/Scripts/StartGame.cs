using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("Level_1");
    }
    public void QuitGame() {
        Debug.Log("QUITED");
        Application.Quit();
    }
}
