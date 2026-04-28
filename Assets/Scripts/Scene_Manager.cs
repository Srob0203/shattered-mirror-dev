using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager Instance;
    public SimpleFade fade;

    void Awake()
    {
        // Make this persist across scene loads
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if(fade==null){
            fade = FindObjectOfType<SimpleFade>();
        }
    }

    public void LoadScene(string sceneName)
    {
        if(GameManager.Instance !=null){
            GameManager.Instance.isTransitioning = true;
            StartCoroutine(FadeAndLoad(sceneName));
        }
        
    }
    
    IEnumerator FadeAndLoad(string sceneName){
        yield return fade.FadeOut();

        
        SceneManager.LoadScene(sceneName);
        yield return null;

        //fade = FindObjectOfType<SimpleFade>();

       //if (GameManager.Instance != null && GameManager.Instance.isTransitioning)
    //{
        yield return fade.FadeIn();
        GameManager.Instance.isTransitioning = false;
    //}
    }
}