using UnityEngine;

public class CoffeeMakerScript : MonoBehaviour
{

    GameObject plr;
    [SerializeField] float waitTime = 10;
    [SerializeField] public GameObject coffeeObject;
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
            coffeeObject.SetActive(true);
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
            coffeeObject.SetActive(true);
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
    }
}
