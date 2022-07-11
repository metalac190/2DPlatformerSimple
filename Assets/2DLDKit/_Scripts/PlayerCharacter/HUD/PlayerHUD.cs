using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : HUDScreen
{
    [SerializeField]
    private IconBar _healthBarGUI;
    [SerializeField]
    private Text _collectiblesTextUI;
    [SerializeField]
    private Text _keysTextGUI;

    private Health _health;
    private Inventory _inventory;
    private PlayerCharacter _playerCharacter;

    public void Initialize(PlayerCharacter playerCharacter)
    {
        // dependencies
        _playerCharacter = playerCharacter;
        _health = _playerCharacter.Health;
        _inventory = _playerCharacter.Inventory;
        // setup
        _healthBarGUI.CreateIcons(_health.Max);
        // this is dumb but layout groups need to wait a sec before they work
        Invoke(nameof(FillIcons), 0.1f);
        // events
        _health.HealthChanged += OnChangedHealth;
        _inventory.CollectiblesChanged += OnCollectiblesChanged;
        _inventory.KeysChanged += OnKeysChanged;
        // initial values
        _collectiblesTextUI.text = _inventory.Collectibles.ToString();
        _keysTextGUI.text = _inventory.Keys.ToString();
    }

    private void OnDestroy()
    {
        // only unsubscribe if we actually have a player character
        if(_playerCharacter != null)
        {
            _health.HealthChanged -= OnChangedHealth;
            _inventory.CollectiblesChanged -= OnCollectiblesChanged;
            _inventory.KeysChanged -= OnKeysChanged;
        }
    }

    private void OnChangedHealth(int newHealth)
    {
        _healthBarGUI.FillIcons(newHealth);
    }

    private void OnCollectiblesChanged(int newCollectibles)
    {
        _collectiblesTextUI.text = newCollectibles.ToString();
    }

    private void OnKeysChanged(int newKeysAmount)
    {
        _keysTextGUI.text = newKeysAmount.ToString();
    }

    private void FillIcons()
    {
        _healthBarGUI.FillIcons(_health.Current);
    }

}
