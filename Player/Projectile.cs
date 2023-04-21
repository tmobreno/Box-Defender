using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private Vector3 targetLocation;
    private Rigidbody rb;
    private PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
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
        targetLocation = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            var pos = Vector3.MoveTowards(transform.position, target.transform.position, stats.projectileSpeed * Time.deltaTime);
            rb.MovePosition(pos);
        }
        if (target == null)
        {
            var pos = Vector3.MoveTowards(transform.position, targetLocation, stats.projectileSpeed * Time.deltaTime);
            rb.MovePosition(pos);
            StartCoroutine(EventualDestroy());
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
            Destroy(gameObject);
        }
    }

    IEnumerator EventualDestroy()
    {
        float stay = Random.Range(1f, 2f);
        yield return new WaitForSeconds(stay);
        Destroy(gameObject);
    }
}
