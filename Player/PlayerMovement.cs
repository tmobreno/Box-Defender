using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 inputMovement;
    float moveSpeed;
    private float attackTimer;

    private PlayerStats stats;

    public GameObject projectile, spearProjectile, orbitProjectile;
    public Collider sphere;

    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
        stats.gun = true;
        stats.spear = false;
        stats.orbit = false;
    }

    // Update is called once per frame
    void Update()
    {
        inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(inputMovement * Time.deltaTime * stats.speed, Space.World);

        if (Time.time > attackTimer)
        {
            if(DetectNearbyEnemies() && stats.gun)
            {
                Instantiate(projectile, transform.position, transform.rotation);
            }
            if (DetectNearbyEnemies() && stats.spear)
            {
                Instantiate(spearProjectile, transform.position, transform.rotation);
            }
            if (stats.orbit)
            {
                Instantiate(orbitProjectile, transform.position, transform.rotation);
            }
            attackTimer = Time.time + stats.attackTimer;
        }
    }

    private bool DetectNearbyEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, stats.hitRadius);
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
