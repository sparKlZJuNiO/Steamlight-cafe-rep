using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Xml;
using UnityEngine.Rendering;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using System.ComponentModel;

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
    GameObject[] autoMoveNPCs;
   [SerializeField] string yourName;
    [SerializeField] GameObject coffeeMachinePointB;
    [SerializeField] GameObject managerPointA;
    [SerializeField] GameObject waiterLinePointB;
    [SerializeField] GameObject arrowObject;
    [SerializeField] float offset;
    [SerializeField] GameObject coffeeMachine;
    [SerializeField] AudioSource audioSource;
    bool newPoint;
    bool value1;
    float timer = 12f;
    GameObject manager;

    [Header("Animator")]
    Animator playerAnim;
   [SerializeField] Animator uiAnim;

    [Header("UI")]
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject accessibilityCanvas;
    [SerializeField] GameObject optionsCanvas;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseButton2;
    [SerializeField] GameObject inputField;
    [SerializeField] GameObject tagBackground;
    [SerializeField] TextMeshPro tagText;
    [SerializeField] Image checkMark;
    [SerializeField] TextMeshProUGUI textPopup;
    [SerializeField] GameObject menuText;
    [SerializeField] GameObject menuBackground;
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Toggle togglePart;
    [SerializeField] Toggle vsyncToggle;
    [SerializeField] Volume volume;
    ColorAdjustments colorAdjustments;
    [SerializeField] GameObject checkpointUI;

    bool triggered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() // Best for initializing variables before the game is loaded
    {
        plr = GameObject.FindGameObjectWithTag("Player");
        playerAnim = GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Manager");
    }
    void Start() // Best for initializing variables
    {
        autoMoveNPCs = GameObject.FindGameObjectsWithTag("autoMoveNPC");
        filter.SetActive(true);
        inputField.SetActive(false);
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    public void RedGreen()
    {
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
            {

                colorAdjustments.hueShift.value = 64;
                colorAdjustments.hueShift.overrideState = true;
            }
    }

    public void BlueYellow()
    {
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {

            colorAdjustments.hueShift.value = -24;
            colorAdjustments.hueShift.overrideState = true;
        }
    }

    public void Off()
    {
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {

            colorAdjustments.hueShift.value = 0;
            colorAdjustments.hueShift.overrideState = true;
        }
    }

    public void SetVolume(float _value)
    {
        if (_value < 1)
        {
            _value = .001f;
        }
        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(soundSlider.value);   
    }

    public void RefreshSlider(float _value)
    {
        soundSlider.value = _value; 
    }

    public void MusicToggle()
    {
        if (togglePart.isOn == false)
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
    }
    public void VsyncToggle()
    {
        if (vsyncToggle.isOn == true)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
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
        if (value1 == false && isPlaying == true)
        {
            menuBackground.SetActive(true);
            menuText.GetComponent<TextMeshProUGUI>().text = "Hello, you must be the new employee. Welcome to Steamlight Cafe, I will be your manager. My job is to overlook employees like you to see how well you do your shift.";
            timer -= Time.deltaTime;
        }
        if (timer < 0 && yourName.Length <= 12 && yourName.Length < 1 && isPlaying == true)
        {
            value1 = true;
            menuText.GetComponent<TextMeshProUGUI>().text = "What's your name?";
            inputField.SetActive(true);
        }
        if (isPlaying == true && isMoving == true)
        {
                foreach (GameObject gameObject in autoMoveNPCs)
                {
                    gameObject.GetComponent<CharacterMove2>().autoMove = true;
                }

            if (this.GetComponent<Dialogue>().text2.text != "Can I have a blue cappunchino?" && newPoint == false)
            {
                float xDiff = waiterLinePointB.transform.position.x - arrowObject.transform.position.x;
                float yDiff = waiterLinePointB.transform.position.y - arrowObject.transform.position.y;

                float radians = Mathf.Atan2(yDiff, xDiff);
                float degrees = radians * Mathf.Rad2Deg;

                arrowObject.transform.rotation = Quaternion.Euler(0, 0, degrees + offset);
            }
            if (this.GetComponent<Dialogue>().text2.text == "Can I have a blue cappunchino?")
            {
                coffeeMachine.GetComponent<BoxCollider2D>().enabled = true;
            }

         
            if (this.GetComponent<Dialogue>().assignedTask == true && newPoint == false)
            {
                float xDiff2 = coffeeMachinePointB.transform.position.x - arrowObject.transform.position.x;
                float yDiff2 = coffeeMachinePointB.transform.position.y - arrowObject.transform.position.y;

                float radians2 = Mathf.Atan2(yDiff2, xDiff2);
                float degrees2 = radians2 * Mathf.Rad2Deg;

                arrowObject.transform.rotation = Quaternion.Euler(0, 0, degrees2 + offset);
            }

            if (coffeeMachine.GetComponent<Animator>().GetBool("Wait") == true)
            {
                checkMark.GetComponent<Image>().enabled = true;
            }

            if (plr.GetComponent<Dialogue>().tick2 == true && plr.GetComponent<Dialogue>().text2.text == "Thanks for the coffee")
            {
                plr.GetComponent<Animator>().SetBool("serving", false);
                checkpointUI.SetActive(true);
                float xDiff2 = managerPointA.transform.position.x - arrowObject.transform.position.x;
                float yDiff2 = managerPointA.transform.position.y - arrowObject.transform.position.y;

                float radians2 = Mathf.Atan2(yDiff2, xDiff2);
                float degrees2 = radians2 * Mathf.Rad2Deg;

                arrowObject.transform.rotation = Quaternion.Euler(0, 0, degrees2 + offset);
                newPoint = true;
            }
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
        menuBackground.SetActive(false);
        menuText.GetComponent<TextMeshProUGUI>().text = "";
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
        foreach (GameObject gameObject in plr.GetComponent<Dialogue>().NPCs)
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
        speed = 0.08f;
        foreach (GameObject gameObject in plr.GetComponent<Dialogue>().NPCs)
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }

    public void accessibilityButton()
    {
       accessibilityCanvas.SetActive(true);
    }

    public void OptionsMenu()
    {
        optionsCanvas.SetActive(true);
    }

    public void VolumeChange()
    {
       
    }

    public void backOptionsMenu()
    {
        optionsCanvas.SetActive(false);
    }
    public void backAccessiblityButton()
    {
        accessibilityCanvas.SetActive(false);
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
