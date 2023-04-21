using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesList : MonoBehaviour
{
    public Image xpBar;
    public PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        xpBar.fillAmount = stats.xp / stats.xpToLevel;
    }

    public void WhichEffect(string n)
    {
        Debug.Log(n);
        switch(n)
        {
            case "DamageUp(Clone)":
                stats.damage *= 1.25f;
                break;
            case "SpeedUp(Clone)":
                stats.speed *= 1.5f;
                break;
            case "ProjectileUp(Clone)":
                stats.attackTimer /= 1.5f;
                stats.projectileSpeed *= 1.5f;
                break;
            case "XPGainUp(Clone)":
                stats.xpGain *= 1.5f;
                break;
            default:
                break;
        }
        stats.DestroyPowerups();
        Time.timeScale = 1f;
    }

}
