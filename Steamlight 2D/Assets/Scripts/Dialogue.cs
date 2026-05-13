using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public bool value = false;
    public bool value2 = false;
    public bool value3 = false;
    public bool value4 = false;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] public TextMeshProUGUI text2;
    [SerializeField] public GameObject textObject;
    GameObject plr;
    public GameObject[] NPCs;
   public bool dialogueToggle;
   public bool tick;
   public bool tick2;
    public bool tick3;
    public bool tick4;
    public bool tick5;
    public bool tick6;
    public bool tick7;
    public bool dialogue2;
    public bool dialogue3;
    public bool dialogue4;
    [SerializeField] float wait = 1.6f;
    public bool assignedTask;
    public bool assignedTask2;
    public bool assignedTask3;
    GameObject coffeeMachine;
    [SerializeField] GameObject coffeeCup;
    [SerializeField] GameObject coffeeCupRed;

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
        }
        if (collision.CompareTag("Trigger2"))
        {
            value2 = true;
            textObject.SetActive(true);
        }
        if (collision.CompareTag("Trigger4") && tick6 == true)
        {
            value3 = true;
            textObject.SetActive(true);
        }
        if (collision.CompareTag("Trigger5") && tick7 == true)
        {
            value4 = true;
            textObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            value = true;
        }
        if (collision.CompareTag("Trigger2"))
        {
            value2 = true;
        }
        if (collision.CompareTag("Trigger4") && tick6 == true)
        {
            value3 = true;
        }
        if (collision.CompareTag("Trigger5") && tick7 == true)
        {
            value4 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            value = false;
            textObject.SetActive(false);
        }
        if (collision.CompareTag("Trigger2"))
        {
            value2 = false;
            textObject.SetActive(false);
        }
        if (collision.CompareTag("Trigger4") && tick6 == true)
        {
            value3 = false;
            textObject.SetActive(false);
        }
        if (collision.CompareTag("Trigger5") && tick7 == true)
        {
            value4 = false;
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
                if (wait <= 2f && tick == true && tick5 == false && tick6 == false)
                {
                    text2.text = "PRESS E TO CONTINUE DIALOGUE";
                   // Debug.Log("DialogueDetect");
                }
            }
            if (text2.GetComponent<TextMeshProUGUI>().IsActive() == true)
            {
                tick = true;
                dialogueToggle = true;
                wait -= Time.deltaTime;
                if (wait <= 2f && tick4 == true && tick5 == false && tick6 == false)
                {
                    text2.text = "PRESS E TO CONTINUE DIALOGUE";
                   
                }
            }
        }
        if (tick2 == true && text2.text == "PRESS E TO CONTINUE DIALOGUE" && dialogue2 == false)
        {
            tick3 = true;
            text2.text = "But, thank you for your order..";
            dialogueToggle = false;
            tick = false;
        }
        if (tick3 == true && text2.text == "But, thank you for your order.." && tick == false && value2 == true && dialogue2 == false)
        {
            tick5 = true;
            text2.text = plr.GetComponent<Player>().yourName + ": Are you okay manager?, Press E to Continue";
            dialogueToggle = false;
            dialogue2 = true;
            tick4 = false;
            
        }
        if (tick6 == true && value3 == true && dialogue3 == false)
        {
            text2.text = "Can I say something?, this is some of the best coffee that I have had in ages. I’m gonna leave a five star review.";
            tick4 = false;
            tick3 = false;
            tick2 = false;
            tick = false;
        }
        if (tick7 == true && value4 == true && dialogue4 == false)
        {
            text2.text = "Hey, I would like to file a complaint. About the service here. I had to wait for nearly an hour just for a coffee.";
            tick5 = false;
            tick4 = false;
            tick3 = false;
            tick2 = false;
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
        if (value2 == true && text2.text == plr.GetComponent<Player>().yourName + ": Are you okay manager?, Press E to Continue")
        {
            //  Debug.Log("hERE");
            wait = 5f;
            text2.text = "Manager: Erm...yes...yes..Just mad about these people...they always present nice. Press E to Continue";
            dialogueToggle = false;
            tick5 = true;
            coffeeMachine.GetComponent<CoffeeMakerScript>().coffeeGiven = false;
            assignedTask = false;
            dialogue2 = true;
        }
        if (value2 == true && text2.text == "Manager: Erm...yes...yes..Just mad about these people...they always present nice. Press E to Continue" && tick5 == true)
        {
            if (wait <= 2.3f)
            {
                text2.text = "Manager: get back to work..";
                tick6 = true;
                tick5 = false;
            }
        }
        if (value3 == true && text2.text == "Can I say something?, this is some of the best coffee that I have had in ages. I’m gonna leave a five star review." && tick6 == true)
        {
            dialogue3 = true;
            if (wait <= 2.3f)
            {
                text2.text = "Hey, can I make another order? Can I have red cappuccino to go please? Thank you.";
                assignedTask2 = true;
            }
        }

        if (value4 == true && text2.text == "Hey, I would like to file a complaint. About the service here. I had to wait for nearly an hour just for a coffee." && tick7 == true)
        {
            dialogue4 = true;
            if (wait <= 2.3f)
            {
                text2.text = "Hey I have a little thing that you could help me with?";
                assignedTask3 = true;
            }
        }
        if (coffeeMachine.GetComponent<CoffeeMakerScript>().coffeeGiven == true && coffeeMachine.GetComponent<CoffeeMakerScript>().coffeeGiven2 == false && value3 == false)
        {
            text2.text = "Woman went to the restroom huh..";
            value = false;
            tick = false;
            coffeeCup.SetActive(true);
            tick2 = true;
            plr.GetComponent<Animator>().SetBool("serving", false);
        }
        if (assignedTask2 == true && plr.GetComponent<Animator>().GetBool("serving") == true)
        {
            text2.text = plr.GetComponent<Player>().yourName + ": Waiter here for your starving stomach..";
            coffeeCupRed.SetActive(true);
            plr.GetComponent<Animator>().SetBool("serving", false);
            assignedTask2 = false;
        }
    }
}
