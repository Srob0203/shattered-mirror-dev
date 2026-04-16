using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
        public enum BattleState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Won,
        Lost,
        Escaped
    }
    public Button[] itemButtons;
    public TMP_Text[] itemButtonTexts;
     public GameObject itemPanel;
    public GameObject attackPanel;


    public BattleState state;
    public int playerTurns = 1;

    [Header("Player Stats")]
    //public int playerHP = 50;
    public int playerMaxHP = 50;
    public int playerDamage = 10;
    public int potionCount = 2;
    public int potionHeal = 20;

    public Slider PlayerHealth;
    public Slider EnemyHealth;

    [Header("Enemy Stats")]
    public string enemyName = "Dragon";
    public int enemyHP = 50;
    public int enemyDamage = 6;

    [Header("UI")]
    public TMP_Text battleText;
    public TMP_Text playerHPText;
    public TMP_Text enemyHPText;

    void Start()
    {
        StartBattle();
        ShowAttack();
    }

    void StartBattle()
    {
        state = BattleState.Start;

        battleText.text = "A wild " + enemyName + " appeared!";
        LoadInventory();
        UpdateUI();

        bool playerGoesFirst = Random.value < 0.5f;

        if (playerGoesFirst)
        {
            state = BattleState.PlayerTurn;
            battleText.text += "\nYou go first!";
        }
        else
        {
            state = BattleState.EnemyTurn;
            battleText.text += "\nEnemy goes first!";
            Invoke(nameof(EnemyTurn), 1.5f);
        }
    }



void LoadInventory()
{
    Debug.Log("Player Inventory:");
    if(GameManager.Instance.inventory == null || GameManager.Instance.inventory.Count ==0){
        LoadDefaultTestInventory();
    }
    else{
    foreach (Item item in GameManager.Instance.inventory)
    {
        Debug.Log(item.itemName + ": " + item.quantity);
    }
    }
}
void LoadDefaultTestInventory()
{
    if (GameManager.Instance.inventory.Count == 0)
    {
        GameManager.Instance.AddItem("Potion", 2);
        GameManager.Instance.AddItem("Quick Tonic", 1);
        

        for (int i = 0; i < itemButtons.Length; i++)
    {
        itemButtons[i].gameObject.SetActive(false);
    }

    for (int i = 0; i < GameManager.Instance.inventory.Count && i < itemButtons.Length; i++)
    {
        Item item = GameManager.Instance.inventory[i];

        itemButtons[i].gameObject.SetActive(true);
        itemButtonTexts[i].text = item.itemName + " x" + item.quantity;

        
    }
    Debug.Log("Loaded default test inventory (items).");
}
if (GameManager.Instance.weapons.Count == 0)
    {
        GameManager.Instance.AddWeapon("Sword", 5, 0.3f);
        GameManager.Instance.AddWeapon("Axe", 7, 0.1f);
        

        for (int i = 0; i < itemButtons.Length; i++)
    {
        itemButtons[i].gameObject.SetActive(false);
    }

    for (int i = 0; i < GameManager.Instance.weapons.Count && i < itemButtons.Length; i++)
    {
        Weapon weapon = GameManager.Instance.weapons[i];

        itemButtons[i].gameObject.SetActive(true);
        itemButtonTexts[i].text = weapon.weaponName;

        
    }
    Debug.Log("Loaded default test inventory (weapons).");
}
}
public void ShowItems()
    {
       
        itemPanel.SetActive(true);
        attackPanel.SetActive(false);
    }

    public void ShowAttack()
    {
        
        itemPanel.SetActive(false);
        attackPanel.SetActive(true);
    }
    void UpdateUI()
    {
        playerHPText.text = GameManager.Instance.playerHP.ToString();
        if(enemyHP<=0){
            enemyHP = 0;
        }
        enemyHPText.text = enemyHP.ToString();
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
{
    PlayerHealth.value = GameManager.Instance.playerHP;
    EnemyHealth.value = enemyHP;

}

    public void PlayerAttack()
    {
        if (state != BattleState.PlayerTurn) return;

        enemyHP -= playerDamage;
        battleText.text = "You attacked " + enemyName + " for " + playerDamage + " damage!";

        UpdateUI();

        if (enemyHP <= 0)
        {
            
            state = BattleState.Won;
            EndBattle();
            return;
        }

        state = BattleState.EnemyTurn;
        Invoke(nameof(EnemyTurn), 1f);
    }

    // public void UseItem()
    // {
    //     if (state != BattleState.PlayerTurn) return;

    //     if (potionCount <= 0)
    //     {
    //         battleText.text = "No potions left!";
    //         return;
    //     }

    //     potionCount--;
    //     playerHP += potionHeal;
    //     playerHP = Mathf.Min(playerHP, playerMaxHP);

    //     battleText.text = "Used potion! Restored " + potionHeal + " HP.";

    //     UpdateUI();

    //     state = BattleState.EnemyTurn;
    //     Invoke(nameof(EnemyTurn), 1f);
    // }

    public void CheckEnemy()
    {
        if (state != BattleState.PlayerTurn) return;

        battleText.text = enemyName + "\nHP: " + enemyHP +
                          "\nAttack: " + enemyDamage;

        state = BattleState.EnemyTurn;
        Invoke(nameof(EnemyTurn), 2f);
    }

    public void TryEscape()
    {
        if (state != BattleState.PlayerTurn) return;

        bool escaped = Random.value < 0.4f;

        if (escaped)
        {
            state = BattleState.Escaped;
            battleText.text = "You escaped successfully!";
            EndBattle();
        }
        else
        {
            battleText.text = "Escape failed!";
            state = BattleState.EnemyTurn;
            Invoke(nameof(EnemyTurn), 1f);
        }
    }

    void EnemyTurn()
    {
        if (state != BattleState.EnemyTurn) return;

        GameManager.Instance.playerHP -= enemyDamage;

        battleText.text = enemyName + " attacked for " + enemyDamage + " damage!";

        UpdateUI();

        if (GameManager.Instance.playerHP <= 0)
        {
            GameManager.Instance.playerHP = 0;
            state = BattleState.Lost;
            EndBattle();
            return;
        }

        state = BattleState.PlayerTurn;
        battleText.text += "\nYour turn!";
    }
    public void UsePotion()
{
    if (state != BattleState.PlayerTurn) return;

    bool success = GameManager.Instance.UseItem("Potion");

    if (!success)
    {
        battleText.text = "No potion available!";
        return;
    }

    GameManager.Instance.playerHP += 20;
    GameManager.Instance.playerHP = Mathf.Min(GameManager.Instance.playerHP, playerMaxHP);

    battleText.text = "Used Potion! Restored 20 HP.";

    UpdateUI();

    state = BattleState.EnemyTurn;
    //Debug.Log("Moving to enemy turn");
    Invoke(nameof(EnemyTurn), 1f);
}

public void UseWeapon(int index){
        if (state != BattleState.PlayerTurn) return;

    if(index >=GameManager.Instance.weapons.Count) return;
    Weapon weapon = GameManager.Instance.weapons[index];
    bool crit = Random.value < weapon.critChance;
    int damage = weapon.attack;
    if(crit){
        damage*=2;
    }
    enemyHP -= damage;
    battleText.text = "You attacked " + enemyName + " for " + damage + " damage!";

        UpdateUI();

        if (enemyHP <= 0)
        {
            state = BattleState.Won;
            EndBattle();
            return;
        }
        if(playerTurns>1){
            playerTurns--;
            battleText.text += "\nYour turn!";
        }
        else{
        state = BattleState.EnemyTurn;
        Invoke(nameof(EnemyTurn), 1f);
        }
}


public void UseQuickTonic()
{
    if (state != BattleState.PlayerTurn) return;

    bool success = GameManager.Instance.UseItem("Quick Tonic");

    if (!success)
    {
        battleText.text = "No quick tonic available!";
        return;
    }

    

    battleText.text = "Quick tonic skipped the enemy's turn!";
    
    
    state = BattleState.PlayerTurn;
    playerTurns = 2;
    battleText.text += "\nYour turn!";
    
    UpdateUI();
    

    

}


    void EndBattle()
    {
        switch (state)
        {
            case BattleState.Won:
                battleText.text = "Enemy defeated!";
                break;
            case BattleState.Lost:
                battleText.text = "You were defeated...";
                break;
            case BattleState.Escaped:
                battleText.text = "You escaped!";
                break;
        }

        // Add return to overworld here
    }
}
