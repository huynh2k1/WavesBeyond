using UnityEngine;

public class uiControl : BaseUICtrl
{
    private void OnEnable()
    {
        homeUI.OnClickPlayAction += ActiveHome;
        homeUI.OnClickHowToPlayAction += HowToPlay;
    }

    private void OnDestroy()
    {
        homeUI.OnClickPlayAction -= ActiveHome;
        homeUI.OnClickHowToPlayAction -= HowToPlay;
    }

    public void ActiveHome()
    {
        Show(UIType.HOME);
        Hide(UIType.GAME);
    }

    public void ActiveGame()
    {
        Show(UIType.GAME);
        Hide(UIType.HOME);
    }

    public void HowToPlay()
    {
        Show(UIType.HOWTOPLAY);
    }
}
