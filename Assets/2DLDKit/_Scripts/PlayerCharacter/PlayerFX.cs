using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Health _health;

    [Header("Player FX")]
    [SerializeField] private ParticleSystem _deathParticlePrefab;
    [SerializeField] private AudioClip _deathSound;


    private void OnEnable()
    {
        _health.Died.AddListener(OnDied);
    }

    private void OnDisable()
    {
        _health.Died.RemoveListener(OnDied);
    }

    public void OnDied()
    {
        if(_deathParticlePrefab != null)
            Instantiate(_deathParticlePrefab, transform.position, Quaternion.identity);
        if (_deathSound != null)
            AudioHelper.PlayClip2D(_deathSound, 1);
    }
}
