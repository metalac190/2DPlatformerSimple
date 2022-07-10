using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private LevelData _levelData;
    [SerializeField]
    private PlayerSpawner _playerSpawner;
    [SerializeField]
    private WinTrigger _winTrigger;
    [SerializeField]
    private LevelHUD _levelHUD;

    public LevelData LevelData => _levelData;
    public PlayerSpawner PlayerSpawner => _playerSpawner;
    public WinTrigger WinTrigger => _winTrigger;
    public LevelHUD LevelHUD => _levelHUD;
    private GameSession _gameSession;

    public PlayerCharacter ActivePlayerCharacter { get; private set; }

    private void Start()
    {
        _gameSession = GameSession.Instance;

        if (_gameSession.IsFirstAttempt)
        {
            Debug.Log("Initial Attempt");
            StartInitialAttempt();
        }
        else
        {
            Debug.Log("Continue Attempt");
            ContinuePreviousAttempt();
        }
    }

    private void StartInitialAttempt()
    {
        _gameSession.ClearGameSession();

        _gameSession.SpawnLocation = _playerSpawner.StartSpawnLocation.position;
        Debug.Log("Initial Spawn Location: " + _gameSession.SpawnLocation);
        ActivePlayerCharacter = _playerSpawner.SpawnPlayer(_gameSession.SpawnLocation);

        ActivePlayerCharacter.Health.Died.AddListener(OnPlayerDied);
    }

    private void ContinuePreviousAttempt()
    {
        ActivePlayerCharacter = _playerSpawner.SpawnPlayer(_gameSession.SpawnLocation);
        _gameSession.LoadPlayerData(ActivePlayerCharacter);

        ActivePlayerCharacter.Health.Died.AddListener(OnPlayerDied);
    }

    public void Win()
    {
        Debug.Log("You have win!");
    }

    public void Lose()
    {
        Debug.Log("Lose...");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OnPlayerDied()
    {
        Debug.Log("Player Died");
        _gameSession.DeathCount++;
        ActivePlayerCharacter.Health.Died.RemoveListener(OnPlayerDied);
        _playerSpawner.RemoveExistingPlayer(ActivePlayerCharacter);
    }
} 
