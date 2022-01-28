using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region => ===== Static =====

    public static PlayerController[] ActivePlayers = new PlayerController[2];

    public static Action<Players> AtkActionEvent;

    #endregion

    #region => ===== Data =====

    [SerializeField]
    private KeyCode _playerKeyCode;
    public KeyCode PlayerKeyCode => _playerKeyCode;

    [SerializeField]
    private Players _playerType;
    public Players PlayerType => _playerType;

    #endregion

    #region => ===== Update =====

    private void OnEnable()
    {
        ActivePlayers[(int)PlayerType] = this;
    }

    private void OnDisable()
    {
        ActivePlayers[(int)PlayerType] = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_playerKeyCode)) PlayerAtk();
    }

    #endregion

    #region => ===== Player Effect =====

    private void PlayerAtk()
    {
        if (_playerType == Players.Yellow)
        {
            YellowAtk();
        }
        else
        {
            PurpleAtk();
        }
    }

    private void YellowAtk()
    {
        if (AtkActionEvent != null) AtkActionEvent(_playerType);
    }

    private void PurpleAtk()
    {
        if (AtkActionEvent != null) AtkActionEvent(_playerType);

    }

    #endregion
}

public enum Players
{
    Yellow,
    Purple
}