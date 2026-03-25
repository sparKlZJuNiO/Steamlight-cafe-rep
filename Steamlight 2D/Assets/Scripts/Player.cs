using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    GameObject plr;
    private Vector2 moveInput;
 
   [SerializeField] Rigidbody2D rb;
   [SerializeField] float speed = 0.05f;
  public bool isMoving;
   bool isPlaying = false;
    bool isPaused = false;
    [SerializeField] GameObject filter;
    GameObject[] NPCs;
   [SerializeField] string yourName;

    [Header("Animator")]
    Animator playerAnim;
   [SerializeField] Animator uiAnim;

    [Header("UI")]
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseButton2;
    [SerializeField] GameObject inputField;
    [SerializeField] GameObject tagBackground;
    [SerializeField] TextMeshPro tagText;
    bool triggered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() // Best for initializing variables before the game is loaded
    {
        plr = this.GetComponent<GameObject>();
        playerAnim = GetComponent<Animator>();
    }
    void Start() // Best for initializing variables
    {
        NPCs = GameObject.FindGameObjectsWithTag("NPC");
        filter.SetActive(true);
        inputField.SetActive(false);
    }

    // Update is called once per frame
    void Update() // Runs at frame-rate and is best for input systems or player movement
    {
        if (yourName.Length <= 12 && yourName.Length > 0)
        {
            isMoving = true;
            tagText.text = yourName;
            tagBackground.SetActive(true);
        }
        else
        {
            isMoving = false;
        }
    }

    private void FixedUpdate() // Runs in fixed intervals, good for rigidbodies and anything not frame-rate based
    {
        rb.position += new Vector2(moveInput.x, moveInput.y) * speed;

        if (moveInput.x > 0)
        {
           // Debug.Log("Right");
            if (-moveInput.y !< 0)
            {
                playerAnim.SetBool("sideways", false);
                playerAnim.SetBool("forward", false);
                playerAnim.SetBool("backwards", true);
            }
            else
            {
                playerAnim.SetBool("sideways", true);
                playerAnim.SetBool("forward", false);
                playerAnim.SetBool("backwards", false);
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        if (moveInput.x < 0)
        {
          //  Debug.Log("Left");
          if (-moveInput.y !< 0)
            {
                 playerAnim.SetBool("sideways", false);
                playerAnim.SetBool("forward", false);
                playerAnim.SetBool("backwards", true);
            }
          else
            {
                playerAnim.SetBool("sideways", true);
                this.GetComponent<SpriteRenderer>().flipX = false;
                playerAnim.SetBool("forward", false);
                playerAnim.SetBool("backwards", false);
            }
         
        }
        if (-moveInput.y > 0)
        {
           // Debug.Log("Up");
            playerAnim.SetBool("sideways", false);
            playerAnim.SetBool("forward", true);
            playerAnim.SetBool("backwards", false);
            this.GetComponent<SpriteRenderer>().flipX = false;
            tagBackground.GetComponent<SpriteRenderer>().enabled = true;
            tagText.text = yourName;
        }
        if (-moveInput.y < 0)
        {
            // Debug.Log("Below");
            playerAnim.SetBool("backwards", true);
            playerAnim.SetBool("forward", false);
            this.GetComponent<SpriteRenderer>().flipX = false;
            tagBackground.GetComponent<SpriteRenderer>().enabled = false;
            tagText.text = "";
        }
    }
    private void LateUpdate() // Runs after all updates and is best for post processing or camera movement
    {
        
    }

    public void PlayButton()
    {
        canvas.SetActive(false);
        isPlaying = true;
        inputField.SetActive(true);
    }

    public void OnSelect(string text)
    {
        triggered = true;
    }
        public void OnEndEdit(string text)
    {
        if (triggered == false)
        {
            return;
        }
        inputField.SetActive(false);
        Debug.Log(text);
        yourName = text;
    }

    public void PauseButton()
    {
        //Time.timeScale = 0;
        speed = 0;
       pauseButton.GetComponent<Image>().enabled = false;
      pauseButton2.GetComponent<Image>().enabled = true;
        uiAnim.SetBool("pause", true );
        pauseScreen.SetActive(true);
        isPaused = true;
        playerAnim.SetBool("sideways", false);
        playerAnim.SetBool("forward", false);
        playerAnim.SetBool("move", false);
        foreach (GameObject gameObject in NPCs)
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }
    public void unPauseButton()
    {
        //Time.timeScale = 1;
       pauseButton.GetComponent<Image>().enabled = true;
        pauseButton2.GetComponent<Image>().enabled = false;
        pauseScreen.SetActive(false);
        uiAnim.SetBool("pause", false);
        isPaused = false;
        speed = 0.05f;
        foreach (GameObject gameObject in NPCs)
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (isPlaying == true && !isPaused && isMoving == true)
        {
            moveInput = ctx.ReadValue<Vector2>();

            if (ctx.performed)
            {
                playerAnim.SetBool("move", true);
            }
            else
            {
                playerAnim.SetBool("move", false);
            }
        }
    }
}
