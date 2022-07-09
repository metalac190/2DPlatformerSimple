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
} 
