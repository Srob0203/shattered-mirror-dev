using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GoToNextScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(NextScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(6.0f);
        SceneManager.LoadScene("OverWorld");
    }
}
