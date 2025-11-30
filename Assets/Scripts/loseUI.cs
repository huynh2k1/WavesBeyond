using System;
using UnityEngine;
using UnityEngine.UI;

public class loseUI : BasePopup
{
    public override UIType Type => UIType.LOSE;

    [SerializeField] Button _btnHome;
    [SerializeField] Button _btnReplay;

    public static Action OnClickHomeEvent;
    public static Action OnClickReplayEvent;

    protected override void Awake()
    {
        _btnHome.onClick.AddListener(HomeClicked);
        _btnReplay.onClick.AddListener(ReplayClicked);
    }

    void HomeClicked()
    {
        Hide(() =>
        {
            OnClickHomeEvent?.Invoke();
        });
    }

    void ReplayClicked()
    {
        Hide(() =>
        {
            OnClickReplayEvent?.Invoke();
        });
    }
}
