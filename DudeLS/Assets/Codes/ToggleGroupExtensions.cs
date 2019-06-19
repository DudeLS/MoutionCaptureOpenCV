using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public static class ToggleGroupExtensions
{
    public static void DisableChilds(this ToggleGroup toggleGroup)
    {
        var childs = toggleGroup.GetComponentsInChildren<Toggle>();
        foreach(var toggle in childs)
        {
            toggle.interactable = false;
            toggle.isOn = false;
        }
    }

    public static void EnableChilds(this ToggleGroup toggleGroup)
    {
        var childs = toggleGroup.GetComponentsInChildren<Toggle>();
        foreach (var toggle in childs)
        {
            toggle.interactable = true;
        }
    }
}
