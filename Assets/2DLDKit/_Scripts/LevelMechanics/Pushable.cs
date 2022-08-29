using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// It's important that this script is NOT put on something tagged in the
/// ground layer, otherwise the player will not be detected.
/// As a workaround we can put this on a child object not in the 'Ground' layer
/// so the parent can still be used as walkable ground
/// </summary>
public class Pushable : MonoBehaviour
{
    [SerializeField] private float _pushAcceleration = 100;
    [SerializeField] private float _maxXVelocity = 100;
    [SerializeField] private Rigidbody2D _rb;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector2 pushDirection = transform.position - other.transform.position;
            Debug.Log("Push Direction: " + pushDirection);
            // only keep x movement, can't push 'up', only 'side to side'
            pushDirection = new Vector2(pushDirection.x, 0);
            pushDirection.Normalize();
            // apply force
            Debug.Log("Adding force..." + pushDirection * _pushAcceleration);
            _rb.AddForce(pushDirection * _pushAcceleration);
            // if we're pushing too fast, clamp it
            float xClampedVelocity = Mathf.Clamp(_rb.velocity.x, -_maxXVelocity, _maxXVelocity);
            _rb.velocity = new Vector2(xClampedVelocity, 0);
            Debug.Log("Velocity..." + _rb.velocity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _rb.velocity = Vector2.zero;
    }
}
