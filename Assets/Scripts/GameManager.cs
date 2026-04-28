using UnityEngine;
using System.Collections.Generic;

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
public class Weapon{
    public string weaponName;
    public int attack;
    public float critChance;
    public Weapon(string name, int attackPts, float crit){
        weaponName = name;
        attack = attackPts;
        critChance = crit;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public Vector2 playerPosition;
    public int playerHP = 50;
    public int playerMaxHP = 50;
    public bool returningFromBattle = false;
    public bool isTransitioning = false;

    public List<Item> inventory = new List<Item>();
    public List<Weapon> weapons = new List <Weapon>();

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
    }

    public void AddWeapon(string weaponName, int attack, float critChance){
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
     public void TryEncounter()
    {
        float chance = 0.2f; // tweak this

        if (Random.value < chance)
        {
            StartBattle();
        }
    }

    void StartBattle()
    {
        // set enemy, store data, etc.
        GameManager.Instance.playerPosition = transform.position;
        GameManager.Instance.returningFromBattle = true;
        GameManager.Instance.isTransitioning = true;
        Scene_Manager.Instance.LoadScene("RecoveredScene");
    }
}