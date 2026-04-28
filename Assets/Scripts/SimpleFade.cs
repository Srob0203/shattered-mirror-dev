using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SimpleFade : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image transitionImage;
    public float fadeDuration = 0.5f;
    void Start()
    {
        
    }
    void Awake(){
        if(transitionImage==null){
            transitionImage = GetComponent<Image>();
        }
        Color c = transitionImage.color;
        c.a = 0f; // MUST start invisible
        transitionImage.color = c;
    }
     public IEnumerator FadeOut()
    {
        float t = 0f;
        Color c = transitionImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = t / fadeDuration;
            transitionImage.color = c;
            yield return null;
        }

        c.a = 1f;
        transitionImage.color = c;
    }

    public IEnumerator FadeIn()
    {
        float t = 0f;
        Color c = transitionImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = 1f - (t / fadeDuration);
            transitionImage.color = c;
            yield return null;
        }

        c.a = 0f;
        transitionImage.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
