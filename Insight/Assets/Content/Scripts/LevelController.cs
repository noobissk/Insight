using System.Collections.Generic;
using PlayerTurns;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private PlayerCMD _playerCMD;
    [SerializeField] private TurnController _turnController;
    [SerializeField] private Transform _dropZone;
    [SerializeField] private InGameMenu _inGameMenu;

    void Start()
    {
        _playerCMD = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCMD>();
    }

    public void StartLevel()
    {
        List<PlayerTurn> playerTurns = new List<PlayerTurn>();
        for (int i = 0; i < _dropZone.childCount; i++)
        {
            playerTurns.Add(_dropZone.GetChild(i).GetComponent<PlayerTurnHolder>().Turn);
        }

        _playerCMD.LoadCommands(playerTurns.ToArray());
        _turnController.StartTurns();

        playerTurns.Clear();

        if (_inGameMenu.isActionMenuOpen)
            _inGameMenu.ToggleActionMenu();
    }
}
