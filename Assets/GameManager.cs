using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static int playerCash = 0;
    public int health = 100;
    public Text Healthtxt;
    public Text Cashtxt;
    private float scoreAddAmount = 10;
    [Header("Damage Screen")]
    public Color damageColor;
    public Image damageImage;
    float colorSmoothing = 6f;
    bool isTakingDamage = false;

    public bool gameover = false; // a boolean to indicate the game over state

    // GameManager instance
    private static GameManager instance = null;

    // This is a C# property - the code below isn't using it
    // as it is accessing the private static instance directly.
    // Use this property from other classes.
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        setHealthText(health);
        AddCashText(playerCash);
    }


    // Init the game manager
    void Awake()
    {
        if (instance)
        {
            Debug.Log("already an instance so destroying new one");
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
       if(isTakingDamage)
        {
            damageImage.color = damageColor;
        }
       else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSmoothing * Time.deltaTime);
        }
        isTakingDamage = false;
    }

    public void AddCashText(int playerCash)
    {
        Cashtxt.text = "Cash: " + playerCash + "$";
    }

    public void goalreached()
    {
        setGameOver();
    }

    // Is it game over
    public bool isGameOver()
    {
        return gameover;
    }

    // Set the game over state to true
    public void setGameOver()
    {
        gameover = true;
        //gameovertxt.text = "GAME OVER!";
    }


    public void setHealthText(int health)
    {
        Healthtxt.text = "Health: " + health;
    }

    // Something has attacked the player
    public void EnemyAttack()
    {
        health = health - 10;
        if (health < 0) health = 0;
        setHealthText(health);
        isTakingDamage = true;
        if (health <= 0)
        {
            setGameOver();
        }
    }

    public void AddCash()
    {
        playerCash = playerCash + 30;
        AddCashText(playerCash);
    }

    public void DeductCash()
    {
        if (playerCash >= 90)
        {
            playerCash = playerCash - 90;
            AddCashText(playerCash);
        }
    }

    public void AddHealth()
    {
        health += 10;
        setHealthText(health);
    }
}
