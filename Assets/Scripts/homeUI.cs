using System;
using UnityEngine;
using UnityEngine.UI;

public class homeUI : BaseUI
{
    public override UIType Type => UIType.HOME;

    [SerializeField] Button playButton;
    [SerializeField] Button howtoplayButton;

    public static Action OnClickPlayAction;
    public static Action OnClickHowToPlayAction;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayClicked);
        howtoplayButton.onClick.AddListener(HowToPlayClicked);
    }

    public void PlayClicked()
    {
        OnClickPlayAction?.Invoke();
    }

    public void HowToPlayClicked()
    {
        OnClickHowToPlayAction?.Invoke();
    }
}
