using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3Int gridPos;
    private Dictionary<(int, int), Cell> dictGrid;
    private Dictionary<(int, int), DynamicObj> dictDNM;


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

    public void Initialize(Grid grid, Dictionary<(int, int), Cell> dict, Dictionary<(int, int), DynamicObj> dictDynamic)
    {
        gridPos = grid.WorldToCell(transform.position);
        dictGrid = new Dictionary<(int, int), Cell>(dict);
        dictDNM = new Dictionary<(int, int), DynamicObj>(dictDynamic);
    }


    public void MoveToPos(Vector3 pos, Action actionDone = default)
    {
        transform.position = pos;
        actionDone?.Invoke();
    }

    void TryMove(Vector2Int direction)
    {
        if (dictGrid == null)
        {
            Debug.LogError("PlayerInput.TryMove: dictGrid is null!");
            return;
        }

        if (dictDNM == null)
        {
            Debug.LogError("PlayerInput.TryMove: dictDNM is null! Did you call Initialize(...) ?");
            return;
        }
        (int, int) targetKey = (gridPos.x + direction.x, gridPos.y + direction.y);

        if (!dictGrid.TryGetValue(targetKey, out Cell cell) || cell == null)
        {
            return;
        }

        if (cell.TypeCell == Cell.Type.STATIC)
            return;


        if (dictDNM.TryGetValue(targetKey, out DynamicObj dynamicObj) && dynamicObj != null)
        {
            Debug.Log("Dynamic try move");
            dynamicObj.TryMove(dictGrid, dictDNM, direction);
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
        TryMove(new Vector2Int(-1, 0));
    }

    public void MoveRight(Dictionary<(int, int), Cell> dictGrid)
    {
        TryMove(new Vector2Int(+1, 0));
    }

    public void MoveUp(Dictionary<(int, int), Cell> dictGrid)
    {
        TryMove(new Vector2Int(0, +1));
    }

    public void MoveDown(Dictionary<(int, int), Cell> dictGrid)
    {
        TryMove(new Vector2Int(0, -1));
    }

}
