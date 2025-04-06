using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerTurns;

public class TurnController : MonoBehaviour
{
    public static TurnController Singleton;
    private List<TurnUser> _turnUsers = new List<TurnUser>();
    [SerializeField] private float _timePerTurn = 0.5f;
    // [SerializeField] Transform _playerTurnsParent;
    // private PlayerTurn[] _playerTurns;
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
        _turnUsers.Clear();
    }

#region Register / Unregister Turn users
    public void RegisterTurnUser(TurnUser i_turnUser)
    {
        _turnUsers.Add(i_turnUser);
    }
    public void UnRegisterTurnUser(TurnUser i_turnUser)
    {
        _turnUsers.Remove(i_turnUser);
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
        foreach (TurnUser turnUser in _turnUsers)
        {
            turnUser.Turn();
        }

        yield return new WaitForSeconds(_timePerTurn);
        
        turnID++;
        StartTurns();
    }
#endregion
}
