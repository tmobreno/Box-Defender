using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeProperties : MonoBehaviour
{
    public UpgradesList upgrades;

    // Start is called before the first frame update
    void Start()
    {
        upgrades = FindObjectOfType<UpgradesList>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Chosen()
    {
        upgrades.WhichEffect(gameObject.name);
    }
}
