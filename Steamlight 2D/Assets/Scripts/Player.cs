using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    GameObject plr;
    private Vector2 moveInput;
    Animator anim;
   [SerializeField] Rigidbody2D rb;
   [SerializeField] float speed = 0.05f;
   bool isMoving;
    bool isPlaying = false;

    [Header("UI")]
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseButton2;
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

        if (moveInput.x > 0)
        {
            Debug.Log("Right");
            anim.SetBool("sideways", true);
            anim.SetBool("forward", false);
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (moveInput.x < 0)
        {
            Debug.Log("Left");
            anim.SetBool("sideways", true);
            anim.SetBool("forward", false);
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (-moveInput.y > 0)
        {
            Debug.Log("Below");
            anim.SetBool("sideways", false);
            anim.SetBool("forward", true);
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private void LateUpdate() // Runs after all updates and is best for post processing or camera movement
    {
        
    }

    public void PlayButton()
    {
        canvas.SetActive(false);
        isPlaying = true;
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        pauseButton.GetComponent<Image>().enabled = false;
        pauseButton2.GetComponent<Image>().enabled = true;
        pauseScreen.SetActive(true);
    }
    public void unPauseButton()
    {
        Time.timeScale = 1;
        pauseButton.GetComponent<Image>().enabled = true;
        pauseButton2.GetComponent<Image>().enabled = false;
        pauseScreen.SetActive(false);
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
