using System.Threading;
using UnityEngine;

public class CharacterMove2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject pointA;
    [SerializeField] float speed;
    private Vector3 position;
    [SerializeField] Rigidbody2D rb;
   [SerializeField] float timer;
    Animator anim;
    GameObject plr;
   public bool autoMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
    }

    private void Awake()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        position = rb.position;
        rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (autoMove == false)
        {
            rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void FixedUpdate()
    {
        // Automove 
        if (timer <= 1.1f && autoMove == true)
        {
            rb.position = Vector2.MoveTowards(rb.position, pointA.transform.position, speed * Time.deltaTime);
            anim.SetBool("walking", true);
            // position.z = -0.6f; 
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;
            // Debug.Log("Check");
            timer += Time.deltaTime;
        }
        else if (timer > 1.1f)
        {
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
            rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
            anim.SetBool("walking", false);
            autoMove = false;
        }
    }
}
