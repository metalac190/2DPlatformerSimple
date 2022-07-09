using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportVolume : TriggerVolume
{
    [Header("Teleport Settings")]
    [SerializeField]
    private Transform _exitTransform;
    [SerializeField]
    private ParticleSystem _exitParticles;

    protected override void TriggerEntered(GameObject enteredObject)
    {
        Rigidbody2D rb = enteredObject.GetComponent<Rigidbody2D>();

        if(rb != null)
        {
            if(_exitParticles != null)
            {
                ParticleSystem exitParticles = Instantiate
                    (_exitParticles, _exitTransform.position, Quaternion.identity);
                exitParticles.Play();
            }

            rb.MovePosition(_exitTransform.position);
        }
    }
}
