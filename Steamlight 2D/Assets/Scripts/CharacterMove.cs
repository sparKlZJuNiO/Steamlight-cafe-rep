using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject pointA;
    [SerializeField] int speed;
    private Vector3 position;
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, pointA.transform.position, speed * Time.deltaTime);
        position.z = -0.6f;
    }
}
