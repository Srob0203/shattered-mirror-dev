using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        SceneManager.LoadScene("OverWorld");
    }

    public void QuitGame(){
        //used to end game if desired
        Application.Quit();
        Debug.Log("Quit game");
    }

    public void ContinueGame()
    {
        Debug.Log("Continues from last saved game - WIP");
    }   

    public void OpenSettings()
    {
        Debug.Log("Settings Screen - WIP"); //TO BE WRITTEN
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
