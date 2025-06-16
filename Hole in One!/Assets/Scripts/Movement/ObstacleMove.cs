using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;
    public float offset = 0f; // Unique for each obstacle
    private PlayerMovement playerMovement;

    private Vector3 startPos;

    void Start()
    {
        //playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        startPos = transform.position;
    }

    void Update()
    {
        float movement = Mathf.Sin(Time.time * speed + offset) * distance;
        transform.position = new Vector3(startPos.x + movement, startPos.y, startPos.z);
    }
    /*
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement.Respawn();
        }
    }
    */
}