using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] Grid grid;
    private Dictionary<(int, int), Cell> dictGrid = new Dictionary<(int, int), Cell>();
    [SerializeField] PlayerInput playerCtrl;
    public int moveCount;

    public void Initialize()
    {
        gameControl.I.InitMoveCount(moveCount);

        GenerateGrid();

        if (playerCtrl == null)
            playerCtrl = GetComponentInChildren<PlayerInput>(); 
        playerCtrl.Initialize(grid, dictGrid);
    }

    void GenerateGrid()
    {
        Cell[] childs = GetComponentsInChildren<Cell>();    

        foreach(Cell c in childs)
        {
            c.Initialize(grid);
            dictGrid[(c.gridPos.x, c.gridPos.y)] = c;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }


}
