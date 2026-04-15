using UnityEngine;

public class CoffeeMakerScript : MonoBehaviour
{

    GameObject plr;
    [SerializeField] float waitTime = 10;
    [SerializeField] public bool coffeeGiven;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && plr.GetComponent<Dialogue>().assignedTask == true)
        {
            this.gameObject.GetComponent<Animator>().SetBool("Wait", true);
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
        }

        if (waitTime <= 0)
        {
            this.gameObject.GetComponent<Animator>().SetBool("Wait", false);
            this.gameObject.GetComponent<Animator>().SetBool("Done", true);
        }
        if (coffeeGiven == true && plr.GetComponent<Dialogue>().tick2 == false)
        {
            plr.GetComponent<Animator>().SetBool("serving", true);
            plr.GetComponent<Animator>().SetBool("blue", true);
        }
    }
}
