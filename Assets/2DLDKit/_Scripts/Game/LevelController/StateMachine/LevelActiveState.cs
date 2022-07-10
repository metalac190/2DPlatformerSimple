using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActiveState : State
{
    private LevelFSM _stateMachine;

    private WinTrigger _winTrigger;
    private PlayerSpawner _playerSpawner;

    private PlayerCharacter _activePlayer;
    private GameSession _gameSession;
    private PlaytimeScreen _playtimeScreen;

    private float _elapsedTime;


    public LevelActiveState(LevelFSM stateMachine, LevelController levelController)
    {
        _stateMachine = stateMachine;

        _winTrigger = levelController.WinTrigger;
        _playerSpawner = levelController.PlayerSpawner;
        _gameSession = GameSession.Instance;
        _playtimeScreen = levelController.LevelHUD.PlaytimeScreen;

    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("LEVEL: Active");
        _winTrigger.Entered.AddListener(OnPlayerEnteredWin);
        _playerSpawner.PlayerRemoved += OnPlayerDied;

        // load elapsed time from data
        _elapsedTime = _gameSession.ElapsedTime;
    }

    public override void Exit()
    {
        base.Exit();

        _winTrigger.Entered.RemoveListener(OnPlayerEnteredWin);
        _playerSpawner.PlayerRemoved -= OnPlayerDied;

        // save elapsed time to data
        _gameSession.ElapsedTime = _elapsedTime;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        _elapsedTime += Time.deltaTime;
        _playtimeScreen.IncrementPlaytimeDisplay(_elapsedTime);
    }



    private void OnPlayerEnteredWin()
    {
        _stateMachine.ChangeState(_stateMachine.WinState);
    }

    private void OnPlayerDied(PlayerCharacter player)
    {
        Debug.Log("Player DIED!");
        
        _gameSession.DeathCount++;
        _stateMachine.ChangeState(_stateMachine.LoseState);
    }

    private void OnCancelPressed()
    {
        // reset level data. Make this clear to player in the future, and consider putting in menus
        _gameSession.ClearGameSession();
        LevelLoader.ReloadLevel();
    }
}
