using UnityEngine;

namespace PlayerTurns
{
    public class PlayerTurnHolder_MoveUp : PlayerTurnHolder
    {
        public override void Turn(PlayerCMD i_playerCMD, GridController i_grid)
        {
            // Vector3Int v = i_grid.WorldToCell(i_playerCMD.transform.position);

            i_playerCMD.logicPosition.y++;
        }
    }
}
