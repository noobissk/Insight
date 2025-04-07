using System.Collections.Generic;
using PlayerTurns;
using UnityEngine;
using UnityEngine.SceneManagement;




public class LevelController : MonoBehaviour
{
    public static LevelController Singleton;
    private PlayerCMD _playerCMD;
    [SerializeField] private TurnController _turnController;
    [SerializeField] private GridController _gridController;
    [SerializeField] private Transform _dropZone;
    [SerializeField] private InGameMenu _inGameMenu;

    void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
    }
    void Start()
    {
        _playerCMD = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCMD>();
    }

    public void StartLevel()
    {
        if (_dropZone.childCount == 0)
            return;
        List<PlayerTurn> playerTurns = new List<PlayerTurn>();
        for (int i = 0; i < _dropZone.childCount; i++)
        {
            playerTurns.Add(_dropZone.GetChild(i).GetComponent<PlayerTurnHolder>().Turn);
        }

        _playerCMD.LoadCommands(playerTurns.ToArray());

        _turnController.maxTurns = playerTurns.Count;
        _turnController.StartTurns();

        playerTurns.Clear();

        if (_inGameMenu.isActionMenuOpen)
            _inGameMenu.ToggleActionMenu(false);
    }

    public enum GameOverType
    {
        Won,
        No_Turns,
        Hit_Enemy
    }
    public void GameOver(GameOverType i_type)
    {
        TurnController.Singleton.StopTurns();
        switch (i_type)
        {
            case GameOverType.Won:
                GameProgression.GameProgressHandler.SaveProgress(int.Parse(SceneManager.GetActiveScene().name.Replace("Level_", ""))+1);
                _inGameMenu.ShowGameOver();
                break;
            case GameOverType.No_Turns:
                _inGameMenu.ShowGameOver("-GAME OVER-<br>No turns left");
                break;
            case GameOverType.Hit_Enemy:
                _inGameMenu.ShowGameOver("-GAME OVER-<br>You hit an enemy");
                break;
            default:
                _inGameMenu.ShowGameOver();
                break;
        }
    }
}
