using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerTurns;
using MiguelTools;

public class TurnController : MonoBehaviour
{
    public static TurnController Singleton;
    public List<TurnUser> turnUsers = new List<TurnUser>();
    [SerializeField] public float timePerTurn = 0.5f;
    // [SerializeField] Transform _playerTurnsParent;
    // private PlayerTurn[] _playerTurns;
    [HideInInspector] public int maxTurns;
    public int turnID { get; private set; }

#region Init
    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
    }
#endregion
    void OnDestroy()
    {
        turnUsers.Clear();
    }

#region Register / Unregister Turn users
    public void RegisterTurnUser(TurnUser i_turnUser)
    {
        turnUsers.Add(i_turnUser);
        i_turnUser.logicPosition = GridController.Singleton.grid.GetGridPosition(i_turnUser.transform.position);
        i_turnUser.tileType = GridController.Singleton.grid.GetGridValue(i_turnUser.logicPosition).type;
    }
    public void UnRegisterTurnUser(TurnUser i_turnUser)
    {
        turnUsers.Remove(i_turnUser);
    }
#endregion
#region Turns
    public void StartTurns()
    {
        StartCoroutine(Turn_Routine());
    }
    public void StopTurns()
    {
        StopCoroutine(Turn_Routine());
        turnID = 0;
    }

    private IEnumerator Turn_Routine()
    {
        Grid_Base<MiguelTools.Tile> grid = GridController.Singleton.grid;
        foreach (TurnUser turnUser in turnUsers)
        {
            turnUser.Turn();

            if (turnUser.oldPosition == turnUser.logicPosition)
                continue;

            grid.GetGridValue(turnUser.oldPosition).type = TileType.Empty;

            if (turnUser.tileType == TileType.Player && grid.GetGridValue(turnUser.logicPosition).type == TileType.Goal) // WIN
            {
                yield return new WaitForSeconds(timePerTurn);
                turnUser.oldPosition = turnUser.logicPosition;
                LevelController.Singleton.GameOver(LevelController.GameOverType.Won);
                yield break;
            }
            if (turnUser.tileType == TileType.Player && grid.GetGridValue(turnUser.logicPosition).type == TileType.Enemy) // Lose 1
            {
                yield return new WaitForSeconds(timePerTurn);
                turnUser.oldPosition = turnUser.logicPosition;
                LevelController.Singleton.GameOver(LevelController.GameOverType.Hit_Enemy);
                yield break;
            }
            if (turnUser.tileType == TileType.Enemy && grid.GetGridValue(turnUser.logicPosition).type == TileType.Player) // Lose 2
            {
                yield return new WaitForSeconds(timePerTurn);
                turnUser.oldPosition = turnUser.logicPosition;
                LevelController.Singleton.GameOver(LevelController.GameOverType.Hit_Enemy);
                yield break;
            }

            grid.GetGridValue(turnUser.oldPosition).type = TileType.Empty;
            grid.GetGridValue(turnUser.logicPosition).type = turnUser.tileType;

            grid.DebugUpdate();

            turnUser.oldPosition = turnUser.logicPosition;
        }

        yield return new WaitForSeconds(timePerTurn);
        
        turnID++;
        if (turnID != maxTurns)
            StartTurns();
        else
            LevelController.Singleton.GameOver(LevelController.GameOverType.No_Turns);

    }

#endregion
}
