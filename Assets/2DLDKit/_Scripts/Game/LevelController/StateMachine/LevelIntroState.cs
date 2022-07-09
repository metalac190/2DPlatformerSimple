using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Note: The difference between 'setup' and 'intro', is setup will always get calledon level load.
/// Intro is only called if it's determined that this is the player's first experience with the level.
/// (cutscenes, initialization, etc.)
/// </summary>
public class LevelIntroState : State
{
    private LevelFSM _stateMachine;

    private PlayerSpawner _playerSpawner;
    private GameSession _gameSession;

    public LevelIntroState(LevelFSM stateMachine, LevelController controller)
    {
        _stateMachine = stateMachine;

        _playerSpawner = controller.PlayerSpawner;
        _gameSession = GameSession.Instance;
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("LEVEL: Intro");
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        // check if our cutscene is complete, if we have one. For now, just skip
        if (Input.GetButtonDown("Fire1"))
        {
            BeginLevel();
        }
        BeginLevel();
    }

    private void BeginLevel()
    {

        PlayerCharacter player = _playerSpawner.SpawnPlayer(_gameSession.SpawnLocation);
        _gameSession.LoadPlayerData(player);
        _stateMachine.ChangeState(_stateMachine.ActiveState);
    }
}
