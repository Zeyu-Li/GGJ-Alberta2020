using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("startMenu");
    }
}
