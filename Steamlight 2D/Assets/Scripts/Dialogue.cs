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
   [SerializeField] float wait = 5;
    public bool assignedTask;
    GameObject coffeeMachine;

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
            if (text2.enabled == true)
            { 
                tick = true;
                dialogueToggle = true;
                wait -= Time.deltaTime;
                if (wait < 1f && tick == true)
                {
                    text2.text = "PRESS E TO CONTINUE DIALOGUE";
                    wait = 5f;
                }
            }
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (value == true && tick == true && tick2 == false)
        {
                //  Debug.Log("hERE");
                wait = 0;
                text2.text = "Can I have a blue cappunchino?";
                dialogueToggle = false;
                tick = false;
                assignedTask = true;
        }
        if (coffeeMachine.GetComponent<CoffeeMakerScript>().coffeeGiven == true)
        {
            text2.text = "Thanks for the coffee";
            value = false;
            tick = false;
            tick2 = true;
            plr.GetComponent<Animator>().SetBool("serving", false);
        }
    }
}
