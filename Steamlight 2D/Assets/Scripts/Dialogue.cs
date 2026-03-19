using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    bool value = false;
    [SerializeField] TextMeshProUGUI text;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            value = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            value = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            value = false;
        }
    }
    void Start()
    {
        
    }

  
    void Update()
    {
      
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (value == true)
        {
            {
                Debug.Log("hERE");
                text.text = "Hi";
            }
        }
    }
}
