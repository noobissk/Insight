using System;
using MiguelTools;
using MyBox;
using UnityEngine;

[RequireComponent(typeof(TurnUser))]
public class TurnUserVisual : MonoBehaviour
{
    private Transform _visual;
    private TurnUser _turnUser;
    private Grid_Base<Tile> _grid;
    // private float _timer, _timePerTurn;

    void Start()
    {
        _turnUser = GetComponent<TurnUser>();
        _grid = GridController.Singleton.grid;
        _visual = _turnUser.visual;
        // _timePerTurn = TurnController.Singleton.timePerTurn;
    }

    void Update()
    {
        // if (_turnUser.visualPosition == _turnUser.logicPosition)
        // {
        //     _timer = 0;
        //     return;
        // }

        _visual.localScale = Vector2.Lerp(_visual.localScale, Vector2.one, Time.deltaTime * 20.0f);

        _turnUser.transform.position = Vector2.Lerp(_turnUser.transform.position, _grid.GetWorldPosition(_turnUser.logicPosition), Time.deltaTime * 20.0f);
        // _timer += Time.deltaTime;
    }
}