using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    public bool isWeapon = false;
    public string itemName; // store the actual item name here

    void OnMouseDown()
    {
        if (isWeapon)
        {
            Debug.Log("Equipped: " + itemName + " - WIP");
        }
        else
        {
            Debug.Log("Clicked: " + itemName);
            GameManager.Instance.UseItem(itemName);
        }
    }
}