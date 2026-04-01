using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Button : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("OverWorld");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("Cutscene");
    }
}