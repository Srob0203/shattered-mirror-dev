using UnityEngine;
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

    public BattleState state;

    [Header("Player Stats")]
    public int playerHP = 50;
    public int playerMaxHP = 50;
    public int playerDamage = 10;
    public int potionCount = 2;
    public int potionHeal = 20;

    [Header("Enemy Stats")]
    public string enemyName = "Dragon";
    public int enemyHP = 30;
    public int enemyDamage = 6;

    [Header("UI")]
    public TMP_Text battleText;
    public TMP_Text playerHPText;
    public TMP_Text enemyHPText;

    void Start()
    {
        StartBattle();
    }

    void StartBattle()
    {
        state = BattleState.Start;

        battleText.text = "A wild " + enemyName + " appeared!";

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

    void UpdateUI()
    {
        playerHPText.text = "Player HP: " + playerHP;
        enemyHPText.text = enemyName + " HP: " + enemyHP;
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

    public void UseItem()
    {
        if (state != BattleState.PlayerTurn) return;

        if (potionCount <= 0)
        {
            battleText.text = "No potions left!";
            return;
        }

        potionCount--;
        playerHP += potionHeal;
        playerHP = Mathf.Min(playerHP, playerMaxHP);

        battleText.text = "Used potion! Restored " + potionHeal + " HP.";

        UpdateUI();

        state = BattleState.EnemyTurn;
        Invoke(nameof(EnemyTurn), 1f);
    }

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

        playerHP -= enemyDamage;

        battleText.text = enemyName + " attacked for " + enemyDamage + " damage!";

        UpdateUI();

        if (playerHP <= 0)
        {
            state = BattleState.Lost;
            EndBattle();
            return;
        }

        state = BattleState.PlayerTurn;
        battleText.text += "\nYour turn!";
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
