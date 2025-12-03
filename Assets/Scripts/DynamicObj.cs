using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObj : MonoBehaviour
{
    public Vector3Int gridPos;
    private Dictionary<(int, int), Shark> dictSharks;


    public void Initialize(Grid grid, Dictionary<(int, int), Shark> dictShark)
    {
        gridPos = grid.WorldToCell(transform.position);
        dictSharks = new Dictionary<(int, int), Shark>(dictShark);
    }

    public void MoveToPos(Vector3 pos, Action actionDone = default)
    {
        transform.position = pos;
        actionDone?.Invoke();
    }

    public void TryMove(Dictionary<(int, int), Cell> dictGrid, 
        Dictionary<(int, int), DynamicObj> dictDynamic,
        Vector2Int direction)
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
                return;
            }

            HandleSharkMove(transform.position, gridPos, () =>
            {
                dictDynamic[targetKey] = null;
            });
        });
    }

    void HandleSharkMove(Vector3 targetPos, Vector3Int gridPosition, Action actionDone = default)
    {
        // 4 hướng quanh player
        Vector2Int[] dirs = new Vector2Int[]
        {
        new Vector2Int(1,0),   // phải
        new Vector2Int(-1,0),  // trái
        new Vector2Int(0,1),   // lên
        new Vector2Int(0,-1),  // xuống
        };

        foreach (var dir in dirs)
        {
            (int, int) key = (gridPos.x + dir.x, gridPos.y + dir.y);

            if (dictSharks.TryGetValue(key, out Shark shark))
            {
                dictSharks[(shark.gridPos.x, shark.gridPos.y)] = null;
                dictSharks[(gridPosition.x, gridPosition.y)] = shark;

                shark.MoveToPos(targetPos, () =>
                {
                    actionDone?.Invoke();
                    Destroy(gameObject);
                    gameControl.I.Lose();
                });
            }
        }
    }
}
