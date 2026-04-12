using UnityEngine;

public class Scene_Boundary : MonoBehaviour
{
    public string sceneToLoad;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Scene_Manager.Instance.LoadScene(sceneToLoad);
        }
    }
}