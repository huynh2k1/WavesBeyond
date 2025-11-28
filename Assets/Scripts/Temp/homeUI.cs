using System;
using UnityEngine;
using UnityEngine.UI;

public class homeUI : BaseUI
{
    public override UIType Type => UIType.HOME;

    [SerializeField] Button homeButton;
    [SerializeField] Button howtoplayButton;

    public static Action OnClickHomeAction;
    public static Action OnClickHowToPlayAction;

    private void Awake()
    {
        homeButton.onClick.AddListener(HomeClicked);
        howtoplayButton.onClick.AddListener(HowToPlayClicked);
    }

    public void HomeClicked()
    {
        OnClickHomeAction?.Invoke();
    }

    public void HowToPlayClicked()
    {
        OnClickHowToPlayAction?.Invoke();
    }
}
