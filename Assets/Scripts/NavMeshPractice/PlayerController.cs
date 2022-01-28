using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController[] ActivePlayers = new PlayerController[2];


    [SerializeField]
    private Players _playerType;
    public Players PlayerType => _playerType;

    private void OnEnable()
    {
        ActivePlayers[(int)PlayerType] = this;
    }

    private void OnDisable()
    {
        ActivePlayers[(int)PlayerType] = null;
    }

}

public enum Players
{
    Yellow,
    Purple
}