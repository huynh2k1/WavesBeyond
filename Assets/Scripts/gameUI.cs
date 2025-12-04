using System;
using UnityEngine;
using UnityEngine.UI;

public class gameUI : BaseUI
{
    public override UIType Type => UIType.GAME;

    [SerializeField] Button homeButton;
    [SerializeField] Button replayButton;

    public static Action OnClickHomeAction;
    public static Action OnClickReplayAction;

    [SerializeField] Button _btnMoveLeft;
    [SerializeField] Button _btnMoveRight;
    [SerializeField] Button _btnMoveUp;
    [SerializeField] Button _btnMoveDown;

    public static Action OnClickMoveLeftAction;
    public static Action OnClickMoveRightAction;
    public static Action OnClickMoveUpAction;
    public static Action OnClickMoveDownAction;

    private void Awake()
    {
        homeButton.onClick.AddListener(HomeClicked);
        replayButton.onClick.AddListener(ReplayClicked);

        _btnMoveLeft.onClick.AddListener(OnClickMoveLeft);
        _btnMoveRight.onClick.AddListener(OnClickMoveRight);
        _btnMoveUp.onClick.AddListener(OnClickMoveUp);
        _btnMoveDown.onClick.AddListener(OnClickMoveDown);
    }

    void HomeClicked()
    {
        OnClickHomeAction?.Invoke();
    }

    void ReplayClicked()
    {
        OnClickReplayAction?.Invoke();
    }

    void OnClickMoveLeft()
    {
        OnClickMoveLeftAction?.Invoke();
    }

    void OnClickMoveRight()
    {
        OnClickMoveRightAction?.Invoke();
    }

    void OnClickMoveUp()
    {
        OnClickMoveUpAction?.Invoke();
    }

    void OnClickMoveDown()
    {
        OnClickMoveDownAction?.Invoke();
    }
}
