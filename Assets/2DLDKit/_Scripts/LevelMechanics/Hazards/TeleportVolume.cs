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
    [SerializeField]
    private AudioClip _teleportSound;

    protected override void TriggerEntered(GameObject enteredObject)
    {
        if (_exitParticles != null)
            Instantiate(_exitParticles, _exitTransform.position, Quaternion.identity);
        if (_teleportSound != null)
            AudioHelper.PlayClip2D(_teleportSound, 1);

        enteredObject.transform.position = _exitTransform.position;
    }
}
