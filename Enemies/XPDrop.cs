using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPDrop : MonoBehaviour
{
    private bool inRange;
    private Rigidbody rb;
    private PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange == true)
        {
            var pos = Vector3.MoveTowards(transform.position, GameObject.Find("Character").transform.position, 30f * Time.deltaTime);
            rb.MovePosition(pos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stats.xp += stats.xpGain;
            Destroy(gameObject);
        }
    }
}
