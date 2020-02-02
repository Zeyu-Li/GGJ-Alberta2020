using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


// Quits the player when the user hits escape

public class Quit : MonoBehaviour {
    void Update() {
        // TODO: if something, quit
        if (Input.GetKey("escape")) {
            Debug.Log("QUIT");
            SceneManager.LoadScene("startMenu");
        }
    }
}
