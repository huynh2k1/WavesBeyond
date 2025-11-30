using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3Int gridPos;
    private Dictionary<(int, int), Cell> dictGrid;

    //None -> được đi
    //Dynamic _> di chuyển ô đó

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft(dictGrid);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight(dictGrid);
        }else if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp(dictGrid);
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown(dictGrid);
        }
    }

    public void Initialize(Grid grid, Dictionary<(int, int), Cell> dict)
    {
        gridPos = grid.WorldToCell(transform.position);
        dictGrid = new Dictionary<(int, int), Cell>(dict);
    }


    public void MoveToPos(Vector3 pos, Action actionDone = default)
    {
        transform.position = pos;
        actionDone?.Invoke();
    }

    void TryMove(Dictionary<(int, int), Cell> dictGrid, Vector2Int direction)
    {
        (int, int) targetKey = (gridPos.x + direction.x, gridPos.y + direction.y);

        if (!dictGrid.TryGetValue(targetKey, out Cell cell) || cell == null)
        {
            return;
        }

        if (cell.TypeCell == Cell.Type.STATIC)
            return;


        if (cell.TypeCell == Cell.Type.DYNAMIC)
        {
            cell.TryMove(dictGrid,direction);
            return;
        }

        gameControl.I.UpdateMove();

        // EMPTY hoặc TARGET — player di chuyển vào
        Vector3 targetPos = cell.Position;
        gridPos = new Vector3Int(cell.gridPos.x, cell.gridPos.y, 0);

        MoveToPos(targetPos, () =>
        {
            if(cell.TypeCell == Cell.Type.STARSEA)
            {
                gameControl.I.UpdateMove();
            }
            // cập nhật vị trí grid
            if (cell.TypeCell == Cell.Type.TARGET)
            {
                gameControl.I.Victory();
                return;
            }

            if(gameControl.I.IsOutOfMove)
            {
                gameControl.I.Lose();
            }

        });

    }

    public void MoveLeft(Dictionary<(int, int), Cell> dictGrid)
    {
        TryMove(dictGrid, new Vector2Int(-1, 0));
    }

    public void MoveRight(Dictionary<(int, int), Cell> dictGrid)
    {
        TryMove(dictGrid, new Vector2Int(+1, 0));
    }

    public void MoveUp(Dictionary<(int, int), Cell> dictGrid)
    {
        TryMove(dictGrid, new Vector2Int(0, +1));
    }

    public void MoveDown(Dictionary<(int, int), Cell> dictGrid)
    {
        TryMove(dictGrid, new Vector2Int(0, -1));
    }

}
