using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField]
    private int _healAmount = 1;

    protected override void OnPickup(GameObject player)
    {
        Health health = player.GetComponent<Health>();
        if (health != null)
        {
            health.Heal(_healAmount);
        }
    }
}
