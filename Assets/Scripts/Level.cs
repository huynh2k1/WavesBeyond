using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] Grid grid;
    private Dictionary<(int, int), Cell> dictGrid = new Dictionary<(int, int), Cell>();
    private Dictionary<(int, int), DynamicObj> dictDNM = new Dictionary<(int, int), DynamicObj>();
    private Dictionary<(int, int), Shark> dictShark = new Dictionary<(int, int), Shark>();
    [SerializeField] PlayerInput playerCtrl;
    public int moveCount;

    public void Initialize()
    {
        gameControl.I.InitMoveCount(moveCount);

        GenerateGrid();

        GenerateSharks();

        GenerateDynamicObj();


        if (playerCtrl == null)
            playerCtrl = GetComponentInChildren<PlayerInput>(); 
        playerCtrl.Initialize(grid, dictGrid, dictDNM, dictShark);

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
            d.Initialize(grid, dictShark);
            dictDNM[(d.gridPos.x, d.gridPos.y)] = d;
        }
    }

    void GenerateSharks()
    {
        Shark[] sharks = GetComponentsInChildren<Shark>();
        foreach (Shark s in sharks)
        {
            s.Initialize(grid);
            dictShark[(s.gridPos.x, s.gridPos.y)] = s;
        }

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }


}
