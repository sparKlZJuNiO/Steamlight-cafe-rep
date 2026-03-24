using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public bool value = false;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject textObject;
   public GameObject[] NPCs;
    bool tick;
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
     
    }

  
    void Update()
    {
      foreach(GameObject gameObject in NPCs)
        {
           if (gameObject.GetComponent<CharacterMove>().npcValue == true)
            {
                tick = true;
            }
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (value == true && tick == true)
        {
            {
              //  Debug.Log("hERE");
                text.text = "Can I have a blue cappunchino?";
            }
        }
    }
}
