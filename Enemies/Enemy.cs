using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health;
    private Transform target;
    private Rigidbody rb;

    private PlayerStats stats;
    public GameObject xpPiece;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Character").transform;
        stats = FindObjectOfType<PlayerStats>();
        health = Mathf.Max(30f * (stats.level / 5), 20f);
    }

    // Update is called once per frame
    void Update()
    {
        //var pos = Vector3.MoveTowards(transform.position, target.transform.position, 10f * Time.deltaTime);
        //rb.MovePosition(pos);

        agent.SetDestination(target.transform.position);

        if (health <= 0)
        {
            int i = Random.Range(2, 6);
            while (i > 0)
            {
                Instantiate(xpPiece, new Vector3(transform.position.x, transform.position.y + Random.Range(1,3), transform.position.z), transform.rotation);
                i--;
            }
            Destroy(gameObject);
        }
    }
}
