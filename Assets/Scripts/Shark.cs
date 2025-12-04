using DG.Tweening;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public Vector3Int gridPos;

    public void Initialize(Grid grid)
    {
        gridPos = grid.WorldToCell(transform.position);

    }

    public void MoveToPos(Vector3 pos, System.Action actionDone = default)
    {
        transform.DOKill();
        transform.DOMove(pos, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.position = pos;
            actionDone?.Invoke();
        });
    }
}
