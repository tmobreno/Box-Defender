using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnPoint : MonoBehaviour
{
    private bool canAdd;
    private bool outline;
    public GameObject basicTower;
    public GameObject towerOutline;
    GameObject g;
    Vector3 towerPosition;

    private PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        towerPosition = new Vector3(transform.position.x, 1.6f, transform.position.z);
        stats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canAdd && Input.GetKeyDown(KeyCode.Space) && stats.turrets >= 1)
        {
            Instantiate(basicTower, towerPosition, transform.rotation);
            stats.turrets -= 1;
            Destroy(gameObject);
        }
        if (canAdd && outline && stats.turrets >= 1)
        {
            g = Instantiate(towerOutline, towerPosition, transform.rotation);
            outline = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canAdd = true;
            outline = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canAdd = false;
            Destroy(g);
        }
    }
}
