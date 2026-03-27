using UnityEngine;

public class ManagerMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject pointB;
    [SerializeField] float speed;
    private Vector3 position;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float timer;
    public bool npcValue = false;
    Animator anim;
    GameObject plr;

    void Start()
    {
        position = rb.position;
        plr = GameObject.FindGameObjectWithTag("Player");
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (plr.GetComponent<Player>().isMoving == false)
        {
            rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void FixedUpdate()
    {
        if (timer <= 2.3f && plr.GetComponent<Player>().isMoving == true)
        {
            rb.position = Vector2.MoveTowards(rb.position, pointB.transform.position, speed * Time.deltaTime);
            // position.z = -0.6f; 
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;
            // Debug.Log("Check");
            timer += Time.deltaTime;
            anim.SetBool("walking", true);
        }
        else if (timer >= 2.3f)
        {
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
            rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
            rb.position = new Vector3(-12.0699997f, -15.6099997f, -3.4000001f);
            anim.SetBool("walking", false);
        }
    }
}
