using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public bool value = false;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] public TextMeshProUGUI text2;
    [SerializeField] GameObject textObject;
    GameObject plr;
    public GameObject[] NPCs;
   public bool dialogueToggle;
    bool tick;
   public bool tick2;
    public bool tick3;
    [SerializeField] float wait = 1.6f;
    public bool assignedTask;
    GameObject coffeeMachine;
    [SerializeField] GameObject coffeeCup;

    private void Awake() // Before initialization, good when the game is not loaded in
    {
        
    }

    void Start() // Initializating variables, good when the game is loaded
    {
        plr = GameObject.FindGameObjectWithTag("Player");
        coffeeMachine = GameObject.FindGameObjectWithTag("CoffeeMaker");
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            value = true;
            textObject.SetActive(true);
            text2.text = plr.GetComponent<Player>().yourName + ": Hello ma’am what can I do for you…";
        }
      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            value = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            value = false;
            textObject.SetActive(false);
        }
    }

    void Update()
    {
        foreach (GameObject gameObject in NPCs)
        {
            if (text2.GetComponent<TextMeshProUGUI>().IsActive() == true)
            { 
                tick = true;
                dialogueToggle = true;
                wait -= Time.deltaTime;
                if (wait <= 2f && tick == true)
                {
                    text2.text = "PRESS E TO CONTINUE DIALOGUE";
                }
            }
        }
        if (tick2 == true && text2.text == "PRESS E TO CONTINUE DIALOGUE")
        {
            tick3 = true;
            text2.text = "But, thank you for your order..";
            dialogueToggle = false;
            tick = false;
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (value == true && tick == true && tick2 == false)
        {
                //  Debug.Log("hERE");
                wait = 5f;
                text2.text = "Can I have a blue hot chocolate?";
                dialogueToggle = false;
                tick = false;
                assignedTask = true;
        }
        if (coffeeMachine.GetComponent<CoffeeMakerScript>().coffeeGiven == true)
        {
            text2.text = "Woman went to the restroom huh..";
            value = false;
            tick = false;
            coffeeCup.SetActive(true);
            tick2 = true;
            plr.GetComponent<Animator>().SetBool("serving", false);
        }
    }
}
