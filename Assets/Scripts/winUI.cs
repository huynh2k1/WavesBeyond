using System;
using UnityEngine;
using UnityEngine.UI;

public class winUI : BasePopup
{
    public override UIType Type => UIType.WIN;

    [SerializeField] Button _btnHome;
    [SerializeField] Button _btnReplay;
    [SerializeField] Button _btnNext;

    public static Action OnClickHomeEvent;
    public static Action OnClickReplayEvent;
    public static Action OnClickNextEvent;

    protected override void Awake()
    {
        _btnHome.onClick.AddListener(HomeClicked);
        _btnNext.onClick.AddListener(NextClicked);
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

    void NextClicked()
    {
        Hide(() =>
        {
            OnClickNextEvent?.Invoke(); 
        });
    }
}
