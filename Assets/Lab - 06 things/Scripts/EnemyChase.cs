using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float force = 5f;       // higher = faster enemy
    public float startDelay = 2f;  // seconds before the enemy starts chasing

    Rigidbody rb;
    Transform player;
    float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer < startDelay) return;

        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        rb.AddForce(dir.normalized * force);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.EndGame("You Got Caught!");
        }
    }
}