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

    public Vector3 Position => transform.position;

    public enum Type
    {
        NONE,
        STATIC, //chướng ngại không thể di chuyển
        TARGET,
        STARSEA
    }
}
