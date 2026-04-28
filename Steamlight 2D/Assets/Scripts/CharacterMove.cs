using System.Threading;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject pointA;
    [SerializeField] float speed;
    private Vector3 position;
    [SerializeField] Rigidbody2D rb;
   [SerializeField] float timer;
    Animator anim;
   public bool npcValue = false;
    GameObject plr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger2"))
        {
            npcValue = true;
        }
    }
    void Start()
    {
        position = rb.position;
        plr = GameObject.FindGameObjectWithTag("Player");
        rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      if (plr.GetComponent<Dialogue>().value == false)
        {
            rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }
      if (plr.GetComponent<Dialogue>().text2.text == "Thanks for the coffee")
        {
            
        }
    }

    private void FixedUpdate()
    {
        if (timer <= 1.1f && plr.GetComponent<Dialogue>().value == true)
        {

            // position.z = -0.6f; 
        
        }
        else if (timer > 1.1f)
        {
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
            rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
            anim.SetBool("walking", false);
        }
    }
}
