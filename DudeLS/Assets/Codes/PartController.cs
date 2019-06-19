using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PartController : MonoBehaviour
{
    /// <summary>
    /// Чекбоксы частей
    /// </summary>
    public ToggleGroup PartToggleGroup;

    ///// <summary>
    ///// Контроллер головы
    ///// </summary>
    //public HeadController HeadController;

    ///// <summary>
    ///// Контроллер для рта
    ///// </summary>
    //public MouthController MouthController;

    ///// <summary>
    ///// Контроллер глаз
    ///// </summary>
    //public EyesController EyesController;

    private IController _controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //var controllers = GetControllers();
        //controllers.ForEach(c => c.Disable());

        //_controller = GetActiveController();
        //if(_controller == null)
        //{
        //    return;
        //}
        //_controller.Enable();
    }

    private Toggle GetActiveToggle()
    {

        return PartToggleGroup != null ? PartToggleGroup.ActiveToggles().FirstOrDefault() : null;
    }

    private List<IController> GetControllers()
    {
        var toggles = PartToggleGroup.GetComponentsInChildren<Toggle>();
        var controllers = new List<IController>();
        foreach(var toggle in toggles)
        {
            var controller = toggle.GetComponent<IController>();
            if(controller != null)
            {
                controllers.Add(controller);
            }
        }
        return controllers;
    }

    private IController GetActiveController()
    {
        var toggle = GetActiveToggle();
        if (toggle == null)
        {
            return null;
        }
        return toggle.GetComponent<IController>();        
    }
}
