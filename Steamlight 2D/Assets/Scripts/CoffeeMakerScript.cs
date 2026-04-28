using UnityEngine;

public class CoffeeMakerScript : MonoBehaviour
{

    GameObject plr;
    [SerializeField] float waitTime = 10;
    [SerializeField] public bool coffeeGiven;
    float color = 3f;
    [SerializeField] float stopValue;
    [SerializeField] GameObject NPC;
    [SerializeField] GameObject[] chairs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && plr.GetComponent<Dialogue>().assignedTask == true)
        {
            this.gameObject.GetComponent<Animator>().SetBool("Wait", true);
            NPC.SetActive(false);
            chairs[0].SetActive(false);
            chairs[1].SetActive(true);
        }

        if (waitTime < 1)
        {
            this.gameObject.GetComponent<Animator>().SetBool("Wait", false);
            this.gameObject.GetComponent<Animator>().SetBool("Done", false);
            waitTime = 10;
            plr.GetComponent<Dialogue>().assignedTask = false;
            coffeeGiven = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && plr.GetComponent<Dialogue>().assignedTask == true)
        {
            this.gameObject.GetComponent<Animator>().SetBool("Wait", true);
        }
        if (waitTime < 1)
        {
            this.gameObject.GetComponent<Animator>().SetBool("Wait", false);
            this.gameObject.GetComponent<Animator>().SetBool("Done", false);
            plr.GetComponent<Dialogue>().assignedTask = false;
            waitTime = 10;
            coffeeGiven = true;
        }
    }
    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<Animator>().GetBool("Wait") == true)
        {
            waitTime -= Time.deltaTime;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.980f, 0.0f); // Yellow
            if (waitTime < 9)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.8902f, 0.7490f); // White #FFE3BF
            }
            if (waitTime < 7)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.980f, 0.0f); // Yellow
            }
            if (waitTime < 5)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.8902f, 0.7490f); // White
            }
            if (waitTime < 4)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.980f, 0.0f); // Yellow
            }
            if (waitTime < 2)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.8902f, 0.7490f); // White
            }
            /*  switch (waitTime)
              {
                  case 9:
                      this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
                      break;
                 case 7:
                      this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.980f, 0.0f);
                      break;
                  case 5:
                      this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
                      break;
                  case 4:
                      this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.980f, 0.0f);
                      break;
              }*/
            if (this.gameObject.GetComponent<Animator>().GetBool("Wait") == true)
            {

            }
        }

        if (waitTime <= 0)
        {
            this.gameObject.GetComponent<Animator>().SetBool("Wait", false);
            this.gameObject.GetComponent<Animator>().SetBool("Done", true);

            for (color = 1.0f; color < 1.3; color++)
            {
                if (color < 1.3f && color > 1.0f)
                {
                    float newNum = (float)color;
                    this.gameObject.GetComponent<SpriteRenderer>().color -= new Color(color, color, color, 0) * Time.deltaTime; // White
                }
            }
        }
        if (coffeeGiven == true && plr.GetComponent<Dialogue>().tick2 == false)
        {
            plr.GetComponent<Animator>().SetBool("serving", true);
            plr.GetComponent<Animator>().SetBool("blue", true);
        }
    }
}
