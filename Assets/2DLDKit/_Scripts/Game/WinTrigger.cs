using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WinTrigger : TriggerVolume
{
    [Header("Win Settings")]
    [SerializeField]
    private AudioClip _winSound;
    [SerializeField]
    private ParticleSystem _winParticlePrefab;

    protected override void TriggerEntered(GameObject enteredObject)
    {
        PlayerCharacter player = enteredObject.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            Debug.Log("WIN");
            if (_winSound != null)
                AudioHelper.PlayClip2D(_winSound, 1);
            if (_winParticlePrefab != null)
                Instantiate(_winParticlePrefab, transform.position, Quaternion.identity);
        }
    }
}
