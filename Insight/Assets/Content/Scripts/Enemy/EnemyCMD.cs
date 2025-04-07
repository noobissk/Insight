using MiguelTools;
using UnityEngine;

public class EnemyCMD : TurnUser
{
    private Grid_Base<Tile> _grid;
    public Vector2Int[] checkpoints = {};
    public int currentCheckpoint { get; private set; }

    void Start()
    {
        _grid = GridController.Singleton.grid;
        Initialize();
    }

    public override void Turn()
    {
        if (logicPosition == checkpoints[currentCheckpoint])
            currentCheckpoint = (currentCheckpoint + 1) % checkpoints.Length;

        if (Mathf.Abs(checkpoints[currentCheckpoint].x - logicPosition.x) > Mathf.Abs(checkpoints[currentCheckpoint].y - logicPosition.y))
            logicPosition.x += Mathf.Clamp(checkpoints[currentCheckpoint].x - logicPosition.x, -1, 1);
        else
            logicPosition.y += Mathf.Clamp(checkpoints[currentCheckpoint].y - logicPosition.y, -1, 1);
    }


    private void OnDrawGizmosSelected()
    {
        if (checkpoints == null || !Application.isPlaying)
            return;

        Gizmos.color = Color.cyan;

        for (int i = checkpoints.Length - currentCheckpoint-1; i > currentCheckpoint-1; i--)
        {
            if (i == currentCheckpoint)
                Gizmos.DrawLine(_grid.GetWorldPosition(logicPosition), _grid.GetWorldPosition(checkpoints[i]));
            else
                Gizmos.DrawLine(_grid.GetWorldPosition(checkpoints[i-1]), _grid.GetWorldPosition(checkpoints[i]));
        }
    }
}
