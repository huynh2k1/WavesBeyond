using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3Int gridPos;
    private Dictionary<(int, int), Cell> dictGrid;
    private Dictionary<(int, int), DynamicObj> dictDNM;
    private Dictionary<(int, int), Shark> dictSharks;

    [SerializeField] ParticleSystem _confetti;
    //None -> được đi
    //Dynamic _> di chuyển ô đó

    private void OnEnable()
    {
        gameUI.OnClickMoveLeftAction += MoveLeft;
        gameUI.OnClickMoveRightAction += MoveRight;
        gameUI.OnClickMoveUpAction += MoveUp;
        gameUI.OnClickMoveDownAction += MoveDown;
    }

    private void OnDestroy()
    {
        gameUI.OnClickMoveLeftAction -= MoveLeft;
        gameUI.OnClickMoveRightAction -= MoveRight;
        gameUI.OnClickMoveUpAction -= MoveUp;
        gameUI.OnClickMoveDownAction -= MoveDown;
    }


    public void Initialize(Grid grid, 
        Dictionary<(int, int), Cell> dict, 
        Dictionary<(int, int), DynamicObj> dictDynamic,
        Dictionary<(int, int), Shark> dictShark
       )
    {
        gridPos = grid.WorldToCell(transform.position);
        dictGrid = dict;         // giữ reference
        dictDNM = dictDynamic;   // giữ reference
        dictSharks = dictShark;  // giữ reference – không copy!!
    }


    public void MoveToPos(Vector3 pos, Action actionDone)
    {
        audioCtrl.I.PlaySoundByType(AudioType.MOVE);

        transform.DOKill();
        transform.DOMove(pos, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.position = pos;
            actionDone?.Invoke();
        });
    }

    void TryMove(Vector2Int direction)
    {
        if (gameControl.I.CurState != State.PLAYING)
            return;
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
        {
            return;
        }


        if (dictDNM.TryGetValue(targetKey, out DynamicObj dynamicObj) && dynamicObj != null)
        {
            Debug.Log("Dynamic try move");
            dynamicObj.TryMove(dictGrid, dictDNM, direction);
            return;
        }

        gameControl.I.UpdateMove();

        // EMPTY hoặc TARGET — player di chuyển vào
        Vector3 targetPos = cell.Position;

        MoveToPos(targetPos, () =>
        {
            gridPos = new Vector3Int(cell.gridPos.x, cell.gridPos.y, 0);
            // cập nhật vị trí grid
            if (cell.TypeCell == Cell.Type.TARGET)
            {
                _confetti.Play();
                gameControl.I.Victory();
                return;
            }

            if(gameControl.I.IsOutOfMove)
            {
                gameControl.I.Lose();
                return;
            }

            if(cell.TypeCell == Cell.Type.STARSEA)
            {
                gameControl.I.UpdateMove();
            }

            HandleSharkMove(transform.position, gridPos);
        });

    }

    void HandleSharkMove(Vector3 targetPos, Vector3Int gridPosition)
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
                dictSharks[key] = null;
                dictSharks[(gridPosition.x, gridPosition.y)] = shark;

                shark.gridPos = gridPosition;
                audioCtrl.I.PlaySoundByType(AudioType.SHARK);

                shark.MoveToPos(targetPos, () =>
                {
                    Destroy(gameObject);
                    gameControl.I.Lose();
                    return;
                });
            }
        }
    }

    public void MoveLeft()
    {
        TryMove(new Vector2Int(-1, 0));
    }

    public void MoveRight()
    {
        TryMove(new Vector2Int(+1, 0));
    }

    public void MoveUp()
    {
        TryMove(new Vector2Int(0, +1));
    }

    public void MoveDown()
    {
        TryMove(new Vector2Int(0, -1));
    }

}
