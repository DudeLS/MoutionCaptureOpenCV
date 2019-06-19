using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class HeadController : Controller
{
    /// <summary>
    /// Экземпляр
    /// </summary>
    public static HeadController Instance = null;

    /// <summary>
    /// Голова X
    /// </summary>
    public Slider HeadX;

    /// <summary>
    /// Текущий чекбокс
    /// </summary>
    public Toggle Toggle;

    ///// <summary>
    ///// Текущий чекбокс
    ///// </summary>
    //private Toggle _toggle;

    /// <summary>
    /// Менеджер цветоискателя
    /// </summary>
    private ColorFinderManager _colorFinderManager;

    /// <summary>
    /// Координаты
    /// </summary>
    private List<float> _xs;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (/*_toggle*/Toggle.isOn)
        {
            HeadX.Enable();
            _colorFinderManager = ColorFinderManager.Instance;
            if (!_colorFinderManager.Circles.Any())
            {
                return;
            }
            var point = _colorFinderManager.Circles.First();
            var width = _colorFinderManager.VideoCaptureManager.FrameWidth;            
            HeadX.value = point.Center.X / width;
        }
        else
        {
            HeadX.Disable();
        }
    }

    public override void Disable()
    {
        
    }

    public override void Enable()
    {
        
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Initialize()
    {
        //_toggle = GetComponent<Toggle>();
        _xs = new List<float>(5);
    }    
}
