using UnityEngine;
using System.Collections;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;
    public int bombCount = 8;
    public float spawnDelay = 0.25f;   // seconds between each bomb
    public float areaX = 9f;
    public float areaZ = 9f;

    public LayerMask obstacleMask;     // walls, obstacles and other bombs
    public float checkRadius = 1f;     // free space needed around a bomb
    public float safeDistance = 4f;    // bombs stay this far from the player

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnBombs());
    }

    IEnumerator SpawnBombs()
    {
        for (int i = 0; i < bombCount; i++)
        {
            for (int tries = 0; tries < 50; tries++)
            {
                Vector3 pos = new Vector3(
                    Random.Range(-areaX, areaX),
                    0.5f,
                    Random.Range(-areaZ, areaZ)
                );

                bool farFromPlayer = Vector3.Distance(pos, player.position) > safeDistance;
                bool free = !Physics.CheckSphere(pos, checkRadius, obstacleMask);

                if (farFromPlayer && free)
                {
                    Instantiate(bombPrefab, pos, Quaternion.identity);
                    break;
                }
            }

            yield return new WaitForSeconds(spawnDelay); // wait, then next bomb
        }
    }
}