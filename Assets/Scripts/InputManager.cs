using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput _playerInput;


    // Action map names
    private const string Player = "Player";
    private const string Menu = "Menu";


    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        // Subscribe to events
        GameManager.OnGameStart.AddListener(SwitchActionMapToPlayer);
        GameManager.OnGameOver.AddListener(SwitchActionMapToMenu);
    }


    private void OnDestroy()
    {
        // UnSubscribe from events
        GameManager.OnGameStart.RemoveListener(SwitchActionMapToPlayer);
        GameManager.OnGameOver.RemoveListener(SwitchActionMapToMenu);
    }


    private void SwitchActionMapToPlayer()
    {
        _playerInput.SwitchCurrentActionMap(Player);
    }


    private void SwitchActionMapToMenu()
    {
        _playerInput.SwitchCurrentActionMap(Menu);
    }
}
