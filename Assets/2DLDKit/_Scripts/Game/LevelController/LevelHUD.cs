using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHUD : MonoBehaviour
{
    [SerializeField]
    private IntroScreen _introScreen;
    [SerializeField]
    private WinScreen _winScreen;
    [SerializeField]
    private PlaytimeScreen _playtimeScreen;
    [SerializeField]
    private PlayerHUD _playerHUD;

    public IntroScreen IntroScreen => _introScreen;
    public WinScreen WinScreen => _winScreen;
    public PlaytimeScreen PlaytimeScreen => _playtimeScreen;
    public PlayerHUD PlayerHUD => _playerHUD;

    public void DisableAllCanvases()
    {
        _introScreen.Hide();
        _winScreen.Hide();
        _playtimeScreen.Hide();
        _playerHUD.Hide();
    }
}
