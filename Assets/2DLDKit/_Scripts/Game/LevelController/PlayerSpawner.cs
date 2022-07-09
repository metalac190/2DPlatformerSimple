using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSpawner : MonoBehaviour
{
    public event Action<PlayerCharacter> PlayerSpawned;
    public event Action<PlayerCharacter> PlayerRemoved;

    [SerializeField]
    private LevelController _levelController;

    [Header("Player Spawning")]

    [SerializeField]
    private Transform _startSpawnLocation;
    [SerializeField]
    private float _respawnDelay = 1.5f;
    [SerializeField]
    private PlayerCharacter _playerPrefab;

    private PlayerCharacter _player;

    public float RespawnDelay => _respawnDelay;
    public PlayerCharacter ActivePlayer => _player;
    public Transform StartSpawnLocation => _startSpawnLocation;

    /// <summary>
    /// Spawn a new player at start position
    /// </summary>
    public PlayerCharacter SpawnPlayer(Vector3 spawnPosition)
    {
        //Debug.Log("Spawn Player");
        // if there's already a player, remove it
        if(_player != null)
        {
            RemoveExistingPlayer();
        }

        _player = Instantiate(_playerPrefab, spawnPosition, Quaternion.identity);
        //TODO look into a way to pass this information before instantiating (it calls awake before initialize)

        _player.Health.Died.AddListener(OnPlayerDied);

        PlayerSpawned?.Invoke(_player);

        //TODO set the camera to begin following the player

        return _player;
    }

    public void RemoveExistingPlayer()
    {
        PlayerRemoved?.Invoke(_player);
        Destroy(_player.gameObject);
    }

    private void OnPlayerDied()
    {
        _player.Health.Died.RemoveListener(OnPlayerDied);
        RemoveExistingPlayer();
    }

}
