using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [Header("Zombie Vars")]
    public GameObject zombiePrefab;
    public Transform[] zombieSpawnPos;
    private float repeatCycle = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            Destroy(gameObject, 10f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        if (zombieSpawnPos.Length == 0)
        {
            Debug.LogWarning("No spawn positions assigned!");
            return;
        }

        int randomIndex = Random.Range(0, zombieSpawnPos.Length);
        Transform selectedSpawnPos = zombieSpawnPos[randomIndex];

        Instantiate(zombiePrefab, selectedSpawnPos.position, selectedSpawnPos.rotation);
    }
}
