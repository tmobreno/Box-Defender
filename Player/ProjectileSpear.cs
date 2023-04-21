using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpear : MonoBehaviour
{
    private Transform target;
    private Rigidbody rb;
    private PlayerStats stats;

    private float timeToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        timeToDestroy = Time.time + 1f;

        rb = GetComponent<Rigidbody>();
        stats = FindObjectOfType<PlayerStats>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            Destroy(gameObject);
        }
        if (enemies.Length > 0)
        {
            target = GetClosestEnemy(enemies);
        }
        if (target == null)
        {
            Destroy(gameObject);
        }
        Vector3 targetDirection = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(targetDirection);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position += transform.forward * Time.deltaTime * stats.projectileSpeed;
        }
        if (Time.time > timeToDestroy)
        {
            Destroy(gameObject);
        }
    }

    Transform GetClosestEnemy(GameObject[] enemies)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin.transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().health -= stats.damage;
        }
    }
}
