using UnityEngine;

public class levelControl : MonoBehaviour
{
    [SerializeField] Level[] listLevel;
    Level _curLevel;
    public void InitLevel()
    {
        DestroyCurLevel();

        _curLevel = Instantiate(listLevel[CurLevel], transform);
    }

    public void ReplayLevel()
    {
        if(CurLevel > 0)
        {
            CurLevel--;
        }
        InitLevel();
    }

    public void CheckIncreaseLevel()
    {
        if(CurLevel < listLevel.Length - 1)
        {
            CurLevel++;
        }
        else
        {
            CurLevel = 0;
        }
    }

    public static int CurLevel
    {
        get => PlayerPrefs.GetInt("CurLevel", 0);
        set => PlayerPrefs.SetInt("CurLevel", value);
    }

    public void DestroyCurLevel()
    {
        if(_curLevel != null)
        {
            _curLevel.Destroy();    
            _curLevel = null;   
        }
    }
}
