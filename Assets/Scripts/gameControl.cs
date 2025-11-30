using UnityEngine;

public class gameControl : MonoBehaviour
{
    public static gameControl I;
    [SerializeField] uiControl uiCtrl;
    [SerializeField] levelControl lvlCtrl;
    [SerializeField] MoveCtrl moveCtrl;

    public State CurState;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        I = this;
    }

    private void Start()
    {
        Home();
    }

    private void OnEnable()
    {
        homeUI.OnClickPlayAction += StartGame;

        gameUI.OnClickHomeAction += Home;
        gameUI.OnClickReplayAction += StartGame;

        winUI.OnClickNextEvent += NextGame;
        winUI.OnClickHomeEvent += Home;
        winUI.OnClickReplayEvent += ReplayGame;

        loseUI.OnClickHomeEvent += Home;
        loseUI.OnClickReplayEvent += StartGame;
    }

    public void ChangeState(State newState)
    {
        CurState = newState;
    }

    public void Home()
    {
        ChangeState(State.NONE);
        uiCtrl.ActiveHome();
    }

    public void StartGame()
    {
        ChangeState(State.PLAYING);
        uiCtrl.ActiveGame();
        lvlCtrl.InitLevel();
    }

    public void ReplayGame()
    {
        ChangeState(State.PLAYING);
        uiCtrl.ActiveGame();
        lvlCtrl.InitLevel();
    }

    public void NextGame()
    {
        ChangeState(State.PLAYING);
        uiCtrl.ActiveGame();
        lvlCtrl.InitLevel();
    }

    public void Victory()
    {
        Debug.Log("Win");
        ChangeState(State.NONE);
        lvlCtrl.CheckIncreaseLevel();
        uiCtrl.Show(UIType.WIN);
    }

    public void Lose()
    {
        ChangeState(State.NONE);
        uiCtrl.Show(UIType.LOSE);
    }

    public void InitMoveCount(int count)
    {
        moveCtrl.InitCount(count);
    }

    public void UpdateMove()
    {
        moveCtrl.UpdateMove();
    }

    public bool IsOutOfMove => moveCtrl.IsOutOfMove();
}

public enum State
{
    NONE,
    PLAYING,    
}
