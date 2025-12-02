using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] Grid grid;
    private Dictionary<(int, int), Cell> dictGrid = new Dictionary<(int, int), Cell>();
    private Dictionary<(int, int), DynamicObj> dictDNM = new Dictionary<(int, int), DynamicObj>();
    [SerializeField] PlayerInput playerCtrl;
    public int moveCount;

    public void Initialize()
    {
        gameControl.I.InitMoveCount(moveCount);

        GenerateGrid();

        GenerateDynamicObj();

        if (playerCtrl == null)
            playerCtrl = GetComponentInChildren<PlayerInput>(); 
        playerCtrl.Initialize(grid, dictGrid, dictDNM);

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

    void GenerateDynamicObj()
    {
        DynamicObj[] dynamicObjs = GetComponentsInChildren<DynamicObj>();

        foreach (DynamicObj d in dynamicObjs)
        {
            d.Initialize(grid);
            dictDNM[(d.gridPos.x, d.gridPos.y)] = d;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }


}
