using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using SceneManager = UnityEngine.SceneManagement.SceneManager;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class Item
{
    public string itemName;
    public int quantity;

    public Item(string name, int amount)
    {
        itemName = name;
        quantity = amount;
    }
}

[System.Serializable]
public class Weapon
{
    public string weaponName;
    public int attack;
    public float critChance;

    public Weapon(string name, int attackPts, float crit)
    {
        weaponName = name;
        attack = attackPts;
        critChance = crit;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerHP = 50;
    public int playerMaxHP = 50;

    public List<Item> inventory = new List<Item>();
    public List<Weapon> weapons = new List<Weapon>();

    public List<GameObject> renderedTextItems = new List<GameObject>();

    bool isOnCooldown = false;
    bool inventoryOpen = false;

    public GameObject textPrefab;
    public Vector3 listStartOffset = new Vector3(-2f, 1f, 0);
    public float lineSpacing = 0.5f;

    public GameObject InventoryMenu;
    public GameObject Inventory_Options;
    public GameObject player;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.Find("Player");
        InventoryMenu = GameObject.Find("Inventory");
        Inventory_Options = GameObject.Find("Inventory_Options");
        DontDestroyOnLoad(InventoryMenu);
        InventoryMenu.SetActive(false);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.Find("Player");

        if (player != null)
        {
            InventoryMenu.transform.position = player.transform.position;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("i") && !isOnCooldown)
        {
            StartCoroutine(ToggleInventory());
        }
    }

    public IEnumerator ToggleInventory()
    {
        foreach (GameObject t in renderedTextItems)
        {
            Destroy(t);
        }
        renderedTextItems.Clear();

        isOnCooldown = true;
        Inventory_Options.SetActive(true);

        if (player == null)
        {
            isOnCooldown = false;
            yield break;
        }

        if (inventoryOpen)
        {
            inventoryOpen = false;
            InventoryMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            InventoryMenu.transform.position = player.transform.position;
            inventoryOpen = true;
            InventoryMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        yield return new WaitForSecondsRealtime(0.2f);
        isOnCooldown = false;
    }

    public void AddWeapon(string weaponName, int attack, float critChance)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.weaponName == weaponName)
            {
                return;
            }
        }

        weapons.Add(new Weapon(weaponName, attack, critChance));
    }

    public void AddItem(string itemName, int amount = 1)
    {
        foreach (Item item in inventory)
        {
            if (item.itemName == itemName)
            {
                item.quantity += amount;
                return;
            }
        }

        inventory.Add(new Item(itemName, amount));
    }

    public bool HasItem(string itemName)
    {
        foreach (Item item in inventory)
        {
            if (item.itemName == itemName && item.quantity > 0)
            {
                return true;
            }
        }

        return false;
    }

    public bool UseItem(string itemName)
    {
        foreach (Item item in inventory)
        {
            if (item.itemName == itemName && item.quantity > 0)
            {
                item.quantity--;

                if (item.quantity <= 0)
                {
                    inventory.Remove(item);
                }

                return true;
            }
        }

        return false;
    }

    public int GetItemCount(string itemName)
    {
        foreach (Item item in inventory)
        {
            if (item.itemName == itemName)
            {
                return item.quantity;
            }
        }

        return 0;
    }

    public void RenderItemList(List<Item> items)
    {
        foreach (GameObject t in renderedTextItems)
        {
            Destroy(t);
        }
        renderedTextItems.Clear();

        for (int i = 0; i < items.Count; i++)
        {
            Vector3 spawnPosition = player.transform.position + listStartOffset + new Vector3(0, -lineSpacing * i, 0);
            GameObject textObj = Instantiate(textPrefab, spawnPosition, Quaternion.identity);
            textObj.GetComponent<TextMeshPro>().text = items[i].itemName + ": " + items[i].quantity;
        
            InventoryItems inventoryItem = textObj.GetComponent<InventoryItems>();
            inventoryItem.isWeapon = false;
            inventoryItem.itemName = items[i].itemName;
        
            renderedTextItems.Add(textObj);
        }
    }
    
    public void RenderWeaponList()
    {
        foreach (GameObject t in renderedTextItems)
        {
            Destroy(t);
        }
        renderedTextItems.Clear();

        for (int i = 0; i < weapons.Count; i++)
        {
            Vector3 spawnPosition = player.transform.position + listStartOffset + new Vector3(0, -i * lineSpacing, 0);
            GameObject textObj = Instantiate(textPrefab, spawnPosition, Quaternion.identity);
            textObj.GetComponent<TextMeshPro>().text = weapons[i].weaponName + " | ATK: " + weapons[i].attack + " | CRIT: " + weapons[i].critChance + "%\n";

            InventoryItems inventoryItem = textObj.GetComponent<InventoryItems>();
            inventoryItem.isWeapon = true;
            inventoryItem.itemName = weapons[i].weaponName;

            renderedTextItems.Add(textObj);
        }
    }

    public void ManageItems()
    {
        RenderItemList(inventory);
    }

    public void ManageWeapons()
    {
        RenderWeaponList();
    }
}