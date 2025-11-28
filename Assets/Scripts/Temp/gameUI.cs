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

    private void Awake()
    {
        homeButton.onClick.AddListener(HomeClicked);
        replayButton.onClick.AddListener(ReplayClicked);
    }

    void HomeClicked()
    {
        OnClickHomeAction?.Invoke();
    }

    void ReplayClicked()
    {
        OnClickReplayAction?.Invoke();
    }
}
