using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public bool value = false;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] public TextMeshProUGUI text2;
    [SerializeField] GameObject textObject;
    GameObject plr;
    public GameObject[] NPCs;
    bool dialogueToggle;
    bool tick;
    float wait;
    public bool assignedTask;

     
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
    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
    }

  
    void Update()
    {
 
        foreach (GameObject gameObject in NPCs)
        {
           if (gameObject.GetComponent<CharacterMove>().npcValue == true)
            {
                tick = true;
                dialogueToggle = true;
                wait += Time.deltaTime;
                if (wait > 1.2f && tick == true)
                {
                    text2.text = "PRESS E TO CONTINUE DIALOGUE";
                }
            }
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (value == true && tick == true)
        {
            {
                //  Debug.Log("hERE");
                wait = 0;
                text2.text = "Can I have a blue cappunchino?";
                dialogueToggle = false;
                tick = false;
                assignedTask = true;
            }
        }
    }
}
