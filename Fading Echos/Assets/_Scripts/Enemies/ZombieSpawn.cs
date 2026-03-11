using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [Header("Zombie Vars")]
    public GameObject zombiePrefab;
    public Transform zombieSpawnPos;
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
        Instantiate(zombiePrefab, zombieSpawnPos.position, zombieSpawnPos.rotation);
    }
}
