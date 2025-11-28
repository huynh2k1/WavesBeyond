using System.Collections.Generic;
using UnityEngine;

public class BaseUICtrl : MonoBehaviour
{
    public BaseUI[] _arrUI;
    protected Dictionary<UIType, BaseUI> _uis = new Dictionary<UIType, BaseUI>();

    protected virtual void Awake()
    {
        foreach (var ui in _arrUI)
        {
            _uis[ui.Type] = ui;
        }
    }

    public void Show(UIType type)
    {
        if (!_uis.ContainsKey(type))
        {
            return;
        }
        _uis[type].Show();
    }

    public void Hide(UIType type)
    {
        if (!_uis.ContainsKey(type))
        {
            return;
        }
        _uis[type].Hide();
    }
}
