using DG.Tweening;
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
        dictSharks = dictShark;
    }

    public void MoveToPos(Vector3 pos, Action actionDone = default)
    {
        audioCtrl.I.PlaySoundByType(AudioType.MOVE);

        transform.DOKill();
        transform.DOMove(pos, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.position = pos;
            actionDone?.Invoke();
        });
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
        if ((cell.TypeCell == Cell.Type.STATIC) || cell.TypeCell == Cell.Type.TARGET)
            return;
        if (dictDynamic.TryGetValue(targetKey, out DynamicObj dynamicObj) && dynamicObj != null)
        {
            return;
        }
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

            HandleSharkMove(dictDynamic, transform.position, gridPos);
        });
    }

    void HandleSharkMove(Dictionary<(int, int), DynamicObj> dictDynamic, Vector3 targetPos, Vector3Int gridPosition, Action actionDone = default)
    {
        Vector2Int[] dirs = new Vector2Int[]
        {
            new Vector2Int( 1, 0),   // phải
            new Vector2Int(-1, 0),   // trái
            new Vector2Int( 0, 1),   // lên
            new Vector2Int( 0,-1),   // xuống

            new Vector2Int( 1, 1),   // chéo phải lên
            new Vector2Int( 1,-1),   // chéo phải xuống
            new Vector2Int(-1, 1),   // chéo trái lên
            new Vector2Int(-1,-1),   // chéo trái xuống
        };

        foreach (var dir in dirs)
        {
            (int, int) key = (gridPos.x + dir.x, gridPos.y + dir.y);

            if (dictSharks.TryGetValue(key, out Shark shark))
            {
                dictSharks[(shark.gridPos.x, shark.gridPos.y)] = null;
                dictSharks[(gridPosition.x, gridPosition.y)] = shark;

                shark.gridPos = gridPosition;

                audioCtrl.I.PlaySoundByType(AudioType.DYNAMIC);
                shark.MoveToPos(targetPos, () =>
                {
                    dictDynamic[(gridPos.x, gridPos.y)] = null;


                    actionDone?.Invoke();
                    Destroy(gameObject);
                });
            }
            else
            {
                actionDone?.Invoke();
            }
        }
    }
}
