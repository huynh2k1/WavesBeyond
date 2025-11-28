using UnityEngine;

public class gameControl : MonoBehaviour
{
    public static gameControl I;
    [SerializeField] uiControl uiCtrl;
    [SerializeField] levelControl lvlCtrl;

    public State CurState;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        I = this;
    }

    private void OnEnable()
    {
        gameUI.OnClickHomeAction += Home;
        gameUI.OnClickReplayAction += ReplayGame;
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
        lvlCtrl.ReplayLevel();
    }

    public void Victory()
    {
        ChangeState(State.NONE);
        lvlCtrl.CheckIncreaseLevel();
        uiCtrl.Show(UIType.WIN);
    }

    public void Lose()
    {
        ChangeState(State.NONE);
        uiCtrl.Show(UIType.LOSE);
    }
}

public enum State
{
    NONE,
    PLAYING,    
}
