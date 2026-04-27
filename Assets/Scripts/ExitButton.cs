using UnityEngine;

public class ExitButton : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("Clicked: " + gameObject.name);
    
        if (GameManager.Instance.InventoryMenu.activeSelf && GameManager.Instance.Inventory_Options.activeSelf == false)
        {
            foreach (GameObject t in GameManager.Instance.renderedTextItems)
            {
                Destroy(t);
            }
            GameManager.Instance.renderedTextItems.Clear();

            GameManager.Instance.Inventory_Options.SetActive(true);
            Debug.Log("Successful branch reached");

        }

        else if (GameManager.Instance.InventoryMenu.activeSelf)
        {
            GameManager.Instance.InventoryMenu.SetActive(false);
            Time.timeScale = 1f;
            Debug.Log("Different branch reached");
        }
    }
}