using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public bool isWeapons = false;

    void OnMouseDown()
    {
        Debug.Log("Clicked: " + gameObject.name);
    
        if (isWeapons)
        {
            GameManager.Instance.Inventory_Options.SetActive(false);
            GameManager.Instance.RenderWeaponList();
        }
        else
        {
            GameManager.Instance.Inventory_Options.SetActive(false);
            GameManager.Instance.RenderItemList(GameManager.Instance.inventory);
        }
    }
}