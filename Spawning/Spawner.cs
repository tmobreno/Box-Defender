using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public float spawnTimer;
    public float platformTimer;
    public GameObject enemy;
    public GameObject[] spawnPositions;
    public GameObject platform;
    public GameObject towerSpawnPoint;

    private PlayerStats stats;
    private NavMeshSurface surface;
    private bool expanded;

    // Start is called before the first frame update
    void Start()
    {
        expanded = false;
        spawnTimer = Time.time + 5f;
        platformTimer = Time.time + 30f;
        stats = FindObjectOfType<PlayerStats>();
        surface = GameObject.Find("NavMesh").GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTimer)
        {
            int rand = Random.Range(0, 4);
            Instantiate(enemy, spawnPositions[rand].transform.position, transform.rotation);
            spawnTimer = Time.time + Mathf.Min((5f / (stats.level / 3)), 5f);
        }
        if (Time.time > platformTimer && !expanded)
        {
            while (!expanded)
            {
                int rand = Random.Range(0, 4);
                Vector3 newPlat = new Vector3(0, 0, 0);
                Vector3 spawn1 = new Vector3(0, 0, 0);
                Vector3 spawn2 = new Vector3(0, 0, 0);
                // right
                if (rand == 0)
                {
                    newPlat = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
                    spawn1 = new Vector3(newPlat.x + 5, newPlat.y, newPlat.z - 5);
                    spawn2 = new Vector3(newPlat.x + 5, newPlat.y, newPlat.z + 5);
                }
                // left
                if (rand == 1)
                {
                    newPlat = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
                    spawn1 = new Vector3(newPlat.x - 5, newPlat.y, newPlat.z - 5);
                    spawn2 = new Vector3(newPlat.x - 5, newPlat.y, newPlat.z + 5);
                }
                // up
                if (rand == 2)
                {
                    newPlat = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
                    spawn1 = new Vector3(newPlat.x + 5, newPlat.y, newPlat.z + 5);
                    spawn2 = new Vector3(newPlat.x - 5, newPlat.y, newPlat.z + 5);
                }
                // down
                if (rand == 3)
                {
                    newPlat = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
                    spawn1 = new Vector3(newPlat.x + 5, newPlat.y, newPlat.z - 5);
                    spawn2 = new Vector3(newPlat.x - 5, newPlat.y, newPlat.z - 5);
                }
                float radius = 2f;

                if (Physics.CheckSphere(newPlat, radius))
                {
                    rand = Random.Range(0, 4);
                }
                else
                {
                    Instantiate(towerSpawnPoint, spawn1, transform.rotation);
                    Instantiate(towerSpawnPoint, spawn2, transform.rotation);
                    Instantiate(platform, newPlat, transform.rotation);
                    surface.BuildNavMesh();
                    expanded = true;
                }
            }
        }
    }
}
