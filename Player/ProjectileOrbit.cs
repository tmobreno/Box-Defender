using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOrbit : MonoBehaviour
{
    GameObject player;
    private float timeToDestroy;
    private PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        timeToDestroy = Time.time + 2f;
        stats = FindObjectOfType<PlayerStats>();
        player = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + (transform.position - player.transform.position).normalized * 1f;
        transform.RotateAround(player.transform.position, new Vector3(0, 20, 0), Time.fixedDeltaTime * 5f);
        if (Time.time > timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().health -= stats.damage;
        }
    }
}
