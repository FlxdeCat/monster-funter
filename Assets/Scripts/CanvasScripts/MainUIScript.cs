using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIScript : MonoBehaviour
{

    public static MainUIScript MainUIInstance;
    static Slider healthBar;
    static Slider staminaBar;
    static Slider RCooldownBar;
    static Slider FCooldownBar;
    static float RCooldownTime;
    static float FCooldownTime;
    public static float health;
    public static bool isDead;
    private InventoryController inventory;
    [SerializeField] private Image potion;
    [SerializeField] private Image meat;
    [SerializeField] private Image potion_side;
    [SerializeField] private Image meat_side;
    [SerializeField] private TMP_Text quantityTxt;
    [SerializeField] private TMP_Text itemNameTxt;
    
    void Awake()
    {
        if (MainUIInstance == null)
        {
            MainUIInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameObject.SetActive(true);
        health = 100f;
        isDead = false;
        inventory = InventoryController.inventoryInstance;
        healthBar = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        staminaBar = transform.GetChild(0).GetChild(1).GetComponent<Slider>();
        FCooldownBar = transform.GetChild(3).GetComponent<Slider>();
        RCooldownBar = transform.GetChild(4).GetComponent<Slider>();
        if (SceneManager.GetActiveScene().name == "MainMenu") gameObject.SetActive(false);
        else gameObject.SetActive(true);
        if (PlayerPrefs.GetString("chosenCharacter").Equals("Paladin"))
        {
            RCooldownBar.maxValue = 5;
            RCooldownBar.value = 5;
            RCooldownTime = 5f;
            FCooldownBar.maxValue = 3;
            FCooldownBar.value = 3;
            FCooldownTime = 3f;
        }
        else if (PlayerPrefs.GetString("chosenCharacter").Equals("Wizard"))
        {
            RCooldownBar.maxValue = 10;
            RCooldownBar.value = 10;
            RCooldownTime = 10f;
            FCooldownBar.maxValue = 5;
            FCooldownBar.value = 5;
            FCooldownTime = 5f;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))) staminaBar.value -= 0.1f * Time.deltaTime;
        else staminaBar.value += 0.05f * Time.deltaTime;
        RCooldownBar.value += 1 * Time.deltaTime;
        FCooldownBar.value += 1 * Time.deltaTime;
        healthBar.value = health / 100f;
        if(inventory.getChosenItem() == "Meat")
        {
            meat.enabled = true;
            meat_side.enabled = false;
            potion.enabled = false;
            potion_side.enabled = true;
            quantityTxt.text = inventory.getMeatQuantity().ToString();
            itemNameTxt.text = inventory.getChosenItem();
        }
        else
        {
            meat.enabled = false;
            meat_side.enabled = true;
            potion.enabled = true;
            potion_side.enabled = false;
            quantityTxt.text = inventory.getPotionQuantity().ToString();
            itemNameTxt.text = inventory.getChosenItem();
        }
        if (health <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(CharacterMovement.playerDies());
        }
    }

    public static bool hasStamina()
    {
        if (staminaBar.value > 0) return true;
        else return false;
    }
    public static bool hasRSkill()
    {
        if (RCooldownBar.value == RCooldownTime) return true;
        else return false;
    }
    public static bool hasFSkill()
    {
        if (FCooldownBar.value == FCooldownTime) return true;
        else return false;
    }
    public static void useRSkill()
    {
        RCooldownBar.value = 0;
    }
    public static void useFSkill()
    {
        FCooldownBar.value = 0;
    }

    public static void useMeat()
    {
        staminaBar.value = 1;
    }
    public static void usePotion()
    {
        health += 50f;
    }

    public static void playerDamaged(float damage)
    {
        health -= damage;
    }

    public void backToVillage()
    {
        Time.timeScale = 1;
        LoadingScreenManager.loadingScreenInstance.LoadNextScene("MainMap");
    }

    public void backToMainMenu()
    {
        Time.timeScale = 1;
        LoadingScreenManager.loadingScreenInstance.LoadNextScene("MainMenu");
    }

}
