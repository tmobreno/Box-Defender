using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : MonoBehaviour
{
    private float attackTimer;
    public float attackSpeed;
    public GameObject projectile;
    PlayerStats stats;
    public float towerRadius;

    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
        attackSpeed = 3f;
        towerRadius = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > attackTimer && DetectNearbyEnemies())
        {
            Instantiate(projectile, transform.position, transform.rotation);
            attackTimer = Time.time + attackSpeed;
        }
    }

    private bool DetectNearbyEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, towerRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Ray ray = new Ray(transform.position, hitColliders[i].transform.position - transform.position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Enemy damageableEnemy = hit.collider.GetComponent<Enemy>();

                if (damageableEnemy)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
