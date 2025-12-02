using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObj : MonoBehaviour
{
    public Vector3Int gridPos;

    public void Initialize(Grid grid)
    {
        gridPos = grid.WorldToCell(transform.position);

    }

    public void MoveToPos(Vector3 pos, Action actionDone = default)
    {
        transform.position = pos;
        actionDone?.Invoke();
    }

    public void TryMove(Dictionary<(int, int), Cell> dictGrid, Dictionary<(int, int), DynamicObj> dictDynamic, Vector2Int direction)
    {
        (int, int) oldKey = (gridPos.x, gridPos.y);
        (int, int) targetKey = (gridPos.x + direction.x, gridPos.y + direction.y);

        // Không tồn tại cell
        if (!dictGrid.TryGetValue(targetKey, out Cell cell) || cell == null)
            return;

        // Ô kế bên có Dynamic hoặc Target → không di chuyển
        if (cell.TypeCell == Cell.Type.TARGET)
            return;

        // Update số lượt di chuyển
        gameControl.I.UpdateMove();

        // --- Cập nhật lại grid ---
        dictDynamic[oldKey] = null;
        dictDynamic[targetKey] = this;


        // Di chuyển object
        Vector3 targetPos = cell.Position;
        gridPos = new Vector3Int(cell.gridPos.x, cell.gridPos.y, 0);

        MoveToPos(targetPos, () =>
        {
            if (gameControl.I.IsOutOfMove)
            {
                gameControl.I.Lose();
            }
        });
    }
}
