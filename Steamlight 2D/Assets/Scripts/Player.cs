using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    GameObject plr;
    private Vector2 moveInput;
    Animator anim;
   [SerializeField] Rigidbody2D rb;
   [SerializeField] float speed = 0.05f;
    bool isMoving;
    [SerializeField] GameObject canvas;
    bool isPlaying = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() // Best for initializing variables before the game is loaded
    {
        plr = this.GetComponent<GameObject>();
        anim = GetComponent<Animator>();
    }
    void Start() // Best for initializing variables
    {
    }

    // Update is called once per frame
    void Update() // Runs at frame-rate and is best for input systems or player movement
    {

    }

    private void FixedUpdate() // Runs in fixed intervals, good for rigidbodies and anything not frame-rate based
    {
        rb.position += new Vector2(moveInput.x, moveInput.y) * speed;    
    }
    private void LateUpdate() // Runs after all updates and is best for post processing or camera movement
    {
        
    }

    public void PlayButton()
    {
        canvas.SetActive(false);
        isPlaying = true;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (isPlaying == true)
        {
            moveInput = ctx.ReadValue<Vector2>();

            if (ctx.performed)
            {
                anim.SetBool("move", true);
            }
            else
            {
                anim.SetBool("move", false);
            }
        }
    }
}
