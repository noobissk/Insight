using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid_Base<TGridObject>
{
    public Transform transform;
    private Vector3 position;
    private Vector2Int gridSize;
    private float gridScale;

    public bool useDebug;
    public delegate void Vector2PairEventDelegate(TGridObject old, Vector2Int position);
    public event Vector2PairEventDelegate OnGridUpdated;

    public TGridObject[,] grid;
    private TextMesh[,] grid_Debug;
    
    public Grid_Base(Transform _transform, Vector3 _position, Vector2Int _gridSize, float _gridScale, bool _useDebug, Func<TGridObject> CreateGridObject)
    {
        transform = _transform;
        position = _position;
        gridSize = _gridSize;
        gridScale = _gridScale;
        useDebug = _useDebug;

        grid = new TGridObject[gridSize.x, gridSize.y];
        grid_Debug = new TextMesh[gridSize.x, gridSize.y];


        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                grid[x, y] = CreateGridObject();
                grid_Debug[x, y] = CreateWorldTextObject($"{x}, {y}\n" + grid[x, y]?.ToString(), 40, transform, GetWorldPosition(x, y), Color.white);
            }
        }
        DebugUpdate();
    }
    public Grid_Base(Transform _transform, Vector3 _position, Vector2Int _gridSize, float _gridScale, bool _useDebug)
    {
        transform = _transform;
        position = _position;
        gridSize = _gridSize;
        gridScale = _gridScale;
        useDebug = _useDebug;

        grid = new TGridObject[gridSize.x, gridSize.y];
        grid_Debug = new TextMesh[gridSize.x, gridSize.y];


        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                grid_Debug[x, y] = CreateWorldTextObject($"{x}, {y}\n" + grid[x, y]?.ToString(), 40, transform, GetWorldPosition(x, y), Color.white);
            }
        }
        DebugUpdate();
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x - (gridSize.x - 1) * 0.5f, y - (gridSize.y - 1) * 0.5f, 0) * gridScale;
    }
    public Vector3 GetWorldPosition(Vector2Int xy)
    {
        return new Vector3(xy.x - (gridSize.x - 1) * 0.5f, xy.y - (gridSize.y - 1) * 0.5f, 0) * gridScale;
    }
    public void InvokeUpdateEvent(TGridObject old, Vector2Int position)
    {
        OnGridUpdated?.Invoke(old, position);
    }

    public Vector2Int GetGridPosition(Vector2 vector)
    {
        float x = position.x;
        float y = position.y;

        return new Vector2Int(Mathf.FloorToInt((vector.x + 0.5f - x) / gridScale + (gridSize.x - 1) * 0.5f), 
         Mathf.FloorToInt((vector.y + 0.5f - y) / gridScale + (gridSize.y - 1) * 0.5f));
    }

    #region GetValue
    public TGridObject GetGridValue(int x, int y)
    {
        if (x < gridSize.x && x >= 0 && y < gridSize.y && y >= 0)
        {
            return grid[x, y];
        }
        else
        {
            return default;
        }
    }
    public TGridObject GetGridValue(Vector2Int xy)
    {
        if (xy.x < gridSize.x && xy.x >= 0 && xy.y < gridSize.y && xy.y >= 0)
        {
            return grid[xy.x, xy.y];
        }
        else
        {
            return default;
        }
    }
    public TGridObject GetValue(Vector2 worldPos)
    {
        Vector2Int xy = GetGridPosition(worldPos);
        if (xy.x < gridSize.x && xy.x >= 0 && xy.y < gridSize.y && xy.y >= 0)
        {
            return grid[xy.x, xy.y];
        }
        else
        {
            return default;
        }
    }
    #endregion


    #region ChangeValue
    public void ChangeValue(int x, int y, TGridObject value)
    {
        if (x < gridSize.x && x >= 0 && y < gridSize.y && y >= 0)
        {
            TGridObject v = grid[x, y];
            grid[x, y] = value;
            OnGridUpdated?.Invoke(v, new Vector2Int(x, y));
        }
        DebugUpdate();
    }
    public void ChangeValue(Vector2Int xy, TGridObject value)
    {
        if (xy.x < gridSize.x && xy.x >= 0 && xy.y < gridSize.y && xy.y >= 0)
        {
            TGridObject v = grid[xy.x, xy.y];
            grid[xy.x, xy.y] = value;
            OnGridUpdated?.Invoke(v, xy);
        }
        DebugUpdate();
    }
    public void ChangeValue(Vector2 worldPos, TGridObject value)
    {
        Vector2Int xy = GetGridPosition(worldPos);
        if (xy.x < gridSize.x && xy.x >= 0 && xy.y < gridSize.y && xy.y >= 0)
        {
            TGridObject v = grid[xy.x, xy.y];
            grid[xy.x, xy.y] = value;
            OnGridUpdated?.Invoke(v, xy);
        }
        DebugUpdate();
    }
    #endregion

    public void DebugUpdate()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                grid_Debug[x, y].text = $"{x}, {y}\n" + grid[x, y]?.ToString();
                grid_Debug[x, y].gameObject.SetActive(useDebug);
            }
        }
    }
    public void DebugUpdate(string message, Vector2Int position)
    {
        grid_Debug[position.x, position.y].text = $"{position.x}, {position.y}\n" + message;
        grid_Debug[position.x, position.y].gameObject.SetActive(useDebug);
    }

    private TextMesh CreateWorldTextObject(string text, int fontSize, Transform parent, Vector3 localPosition, Color color)
    {
        GameObject _gameObject = new GameObject("Debug_Text", typeof(TextMesh));
        Transform _transform = _gameObject.transform;
        _transform.SetParent(parent);
        _transform.localPosition = localPosition;
        TextMesh textMesh = _gameObject.GetComponent<TextMesh>();
        textMesh.alignment = TextAlignment.Center;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.characterSize = gridScale * 0.075f;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.text = text;
        return textMesh;
    }
}