using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
public class TriggerVolume : MonoBehaviour
{
    [SerializeField][Tooltip("If true, this trigger will only fire once. " +
        "If false it can be triggered each time the volume is entered")]
    private bool _oneShot = true;
    [SerializeField][Tooltip("Only this specific object will activate this trigger")]
    private GameObject _specificTriggerObject = null;
    [SerializeField][Tooltip("Objects in these layers will activate" +
        "this trigger")]
    private LayerMask _layersDetected = -1;

    public UnityEvent Entered;
    public UnityEvent Exited;

    private Collider2D _collider = null;
    private bool _alreadyEntered = false;

    [Header("Gizmo Settings")]
    [SerializeField]
    private bool _displayGizmos = false;
    [SerializeField]
    private bool _showOnlyWhileSelected = true;
    [SerializeField]
    private Color _gizmoColor = Color.green;

    protected virtual void Awake()
    {
        // ensure it's marked as trigger
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // if it's a oneshot and we've already fired it, ignore
        if(_oneShot && _alreadyEntered) { return; }
        // if we have a specified object and this is not that object, ignore
        if(_specificTriggerObject != null
            && otherCollider.gameObject != _specificTriggerObject) { return; }
        // if we're not in the layer, return
        if (!PhysicsHelper.IsInLayerMask(otherCollider.gameObject, _layersDetected)) { return; }

        Entered.Invoke();

        TriggerEntered(otherCollider.gameObject);
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        // if we're not in the layer, return
        if (!PhysicsHelper.IsInLayerMask(otherCollider.gameObject, _layersDetected)) { return; }

        TriggerExited(otherCollider.gameObject);
    }

    public void ResetTrigger()
    {
        _alreadyEntered = false;
    }

    protected virtual void TriggerEntered(GameObject objectEntered)
    {

    }

    protected virtual void TriggerExited(GameObject objectEntered)
    {

    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        if (_displayGizmos && _showOnlyWhileSelected == false)
        {
            DrawTriggerBounds();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(_displayGizmos && _showOnlyWhileSelected)
        {
            DrawTriggerBounds();
        }
    }

    private void DrawTriggerBounds()
    {
        // if we don't have a collider, attempt to find it
        if (_collider == null)
        {
            _collider = GetComponent<Collider2D>();
            // if we still don't have it, it's an error. Disable
            if (_collider == null)
            {
                Debug.LogWarning("No collider on " + gameObject.name + ". Disabling");
                gameObject.SetActive(false);
            }
        }
        // set the color and draw it
        Gizmos.color = _gizmoColor;
        Vector3 position = new Vector3(transform.position.x + _collider.offset.x,
            transform.position.y + _collider.offset.y, 0);
        Gizmos.DrawCube(position, _collider.bounds.size);
    }
    #endregion

}
