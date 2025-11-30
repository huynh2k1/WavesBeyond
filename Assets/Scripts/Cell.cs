using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour
{
    public Vector3Int gridPos;
    public Type TypeCell;
    public GameObject _decor;

    public void Initialize(Grid grid)
    {
        gridPos = grid.WorldToCell(transform.position);
        name = $"{TypeCell} ( {gridPos.x}, {gridPos.y})";
    }

    public void TryMove(Dictionary<(int, int), Cell> dictGrid, Vector2Int direction)
    {
        (int, int) targetKey = (gridPos.x + direction.x, gridPos.y + direction.y);

        if (!dictGrid.TryGetValue(targetKey, out Cell cell) || cell == null)
        {
            return;
        }

        if (cell.TypeCell == Cell.Type.DYNAMIC || cell.TypeCell == Cell.Type.STATIC || cell.TypeCell == Cell.Type.TARGET)
            return;


        gameControl.I.UpdateMove();
        SwapCellPosition(this, cell, dictGrid);

    }


    void SwapCellPosition(Cell a, Cell b, Dictionary<(int, int), Cell> dictGrid)
    {
        // Remove key cũ
        dictGrid.Remove((a.gridPos.x, a.gridPos.y));
        dictGrid.Remove((b.gridPos.x, b.gridPos.y));

        // Hoán đổi gridPos
        (a.gridPos, b.gridPos) = (b.gridPos, a.gridPos);

        // Update lại dict
        dictGrid[(a.gridPos.x, a.gridPos.y)] = a;
        dictGrid[(b.gridPos.x, b.gridPos.y)] = b;

        // Swap type
        //(a.TypeCell, b.TypeCell) = (b.TypeCell, a.TypeCell);

        // Move transform
        Vector3 posA = a.Position;
        a.MoveToPos(b.Position);
        b.MoveToPos(posA);

    }

    public void MoveToPos(Vector3 pos)
    {
        transform.position = pos;
    }

    public Vector3 Position => transform.position;

    public enum Type
    {
        NONE,
        STATIC, //chướng ngại không thể di chuyển
        DYNAMIC,
        TARGET,
        STARSEA
    }
}
