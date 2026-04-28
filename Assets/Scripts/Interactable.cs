using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    TextMeshPro interactPrompt;
    bool playerInRange = false;

    public string itemName;
    public int quantity;
    public bool isWeapon = false;
    public int attack;
    public float critChance;

    void Start()
    {
        interactPrompt = GetComponentInChildren<TextMeshPro>();
        interactPrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown("e"))
        {
            if (isWeapon)
            {
                GameManager.Instance.AddWeapon(itemName, attack, critChance);
            }
            else
            {
                GameManager.Instance.AddItem(itemName, quantity);
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactPrompt.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactPrompt.gameObject.SetActive(false);
        }
    }
}