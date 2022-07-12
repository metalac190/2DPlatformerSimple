using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerVolume : MonoBehaviour
{
    [SerializeField][Tooltip("Objects in these layers will activate" +
        "this trigger")]
    private LayerMask _layersDetected = -1;

    protected Collider2D Collider = null;

    protected virtual void Awake()
    {
        // ensure it's marked as trigger
        Collider = GetComponent<Collider2D>();
        Collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // if we're not in the layer, return
        if (!PhysicsHelper.IsInLayerMask(otherCollider.gameObject, _layersDetected)) { return; }

        TriggerEntered(otherCollider.gameObject);
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        // if we're not in the layer, return
        if (!PhysicsHelper.IsInLayerMask(otherCollider.gameObject, _layersDetected)) { return; }

        TriggerExited(otherCollider.gameObject);
    }

    protected virtual void TriggerEntered(GameObject objectEntered)
    {

    }

    protected virtual void TriggerExited(GameObject objectEntered)
    {

    }
}
