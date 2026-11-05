//using UnityEngine;
//using System.Collections;

//public class CoinSpawner : MonoBehaviour
//{
//    public GameObject coinPrefab;
//    public int coinCount = 20;
//    public float spawnDelay = 1.5f; // seconds between each coin
//    public float areaX = 9f;
//    public float areaZ = 9f;

//    void Start()
//    {
//        StartCoroutine(SpawnCoins());
//    }

//    IEnumerator SpawnCoins()
//    {
//        for (int i = 0; i < coinCount; i++)
//        {
//            Vector3 pos = new Vector3(
//                Random.Range(-areaX, areaX),
//                0.5f,
//                Random.Range(-areaZ, areaZ)
//            );

//            Instantiate(coinPrefab, pos, coinPrefab.transform.rotation);

//            yield return new WaitForSeconds(spawnDelay); // wait, then continue
//        }
//    }
//}
using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinCount = 20;
    public float spawnDelay = 1.5f;
    public float areaX = 9f;
    public float areaZ = 9f;

    public LayerMask obstacleMask;   // which layer counts as obstacle
    public float checkRadius = 0.7f; // how much free space a coin needs

    void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 pos = GetFreePosition();
            Instantiate(coinPrefab, pos, coinPrefab.transform.rotation);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    Vector3 GetFreePosition()
    {
        Vector3 pos = Vector3.zero;

        for (int tries = 0; tries < 30; tries++)
        {
            pos = new Vector3(
                Random.Range(-areaX, areaX),
                0.5f,
                Random.Range(-areaZ, areaZ)
            );

            // if nothing from the obstacle layer is here, this place is free
            if (!Physics.CheckSphere(pos, checkRadius, obstacleMask))
                return pos;
        }

        return pos; // after 30 tries, just use the last place
    }
}