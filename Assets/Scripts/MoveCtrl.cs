using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveCtrl : MonoBehaviour
{
    [SerializeField] Text _txtMove;
    int _moveCount;

    public void InitCount(int count)
    {
        _moveCount = count;
        UpdateTextMove();   
    }

    public void UpdateMove()
    {
        if (_moveCount <= 0)
            return;
        _moveCount--;
        UpdateTextMove();
    }


    public bool IsOutOfMove()
    {
        if (_moveCount <= 0)
            return true;
        return false;
    }
    void UpdateTextMove()
    {
        _txtMove.text = _moveCount.ToString();  
    }
}
