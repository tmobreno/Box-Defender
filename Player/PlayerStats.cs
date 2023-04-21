using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float damage, speed, projectileSpeed, attackTimer, hitRadius;

    public float xp, xpToLevel, xpGain, level, score, turrets;

    public bool gun, spear, orbit;

    public GameObject[] upgrades;
    public Canvas upgradeCanvas;
    private GameObject R;
    private GameObject L;

    // Start is called before the first frame update
    void Start()
    {
        level = 1f;
        xpToLevel = 20f; 
        xp = 0f;
        xpGain = 2f;
        turrets = 0;

        speed = 5f;
        projectileSpeed = 20f;
        attackTimer = 1f;
        damage = 10f;
        hitRadius = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (xp >= xpToLevel)
        {
            Time.timeScale = 0f;

            int left = 0;
            int right = 0;
            while (left == right)
            {
                left = Random.Range(0, upgrades.Length);
                right = Random.Range(0, upgrades.Length);
            }

            L = Instantiate(upgrades[left], new Vector3(-200, 0, 0), transform.rotation);
            R = Instantiate(upgrades[right], new Vector3(200, 0, 0), transform.rotation);
            L.transform.SetParent(upgradeCanvas.transform, false);
            R.transform.SetParent(upgradeCanvas.transform, false);

            turrets += 1;
            level += 1;
            xp = 0;
            xpToLevel *= 1.5f;
        }
    }

    public void DestroyPowerups()
    {
        Destroy(L);
        Destroy(R);
    }
}
