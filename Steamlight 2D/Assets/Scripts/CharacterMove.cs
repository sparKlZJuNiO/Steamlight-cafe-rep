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
   public bool npcValue = false;

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
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void FixedUpdate()
    {
        if (timer <= 1.1f)
        {
            rb.position = Vector2.MoveTowards(rb.position, pointA.transform.position, speed * Time.deltaTime);
            // position.z = -0.6f; 
            Debug.Log("Check");
            timer += Time.deltaTime;
        }
        else if (timer > 1.1f)
        {
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
        }
    }
}
