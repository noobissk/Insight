using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerTurns;

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
        foreach (TurnUser turnUser in turnUsers)
        {
            // if (turnUser.logicPosition != turnUser.visualPosition)
                // turnUser.visualPosition = turnUser.logicPosition;
            turnUser.Turn();
        }
        // Debug.Log("Current turn: " + turnID);

        yield return new WaitForSeconds(timePerTurn);
        
        turnID++;
        if (turnID != maxTurns)
            StartTurns();
    }

#endregion
}
