using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SavePoint : TriggerVolume
{
    [SerializeField]
    private Transform _newSpawnPoint;

    private GameSession _gameSession;

    protected override void Awake()
    {
        base.Awake();

        _gameSession = GameSession.Instance;
    }

    protected override void TriggerEntered(GameObject enteredObject)
    {
        Debug.Log("Set new spawn point");
        // if we're not in the layer, return
        PlayerCharacter player = enteredObject.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            _gameSession.SavePlayerData(transform.position, player);
        }
    }
}
