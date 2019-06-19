using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EyesController : Controller
{
    /// <summary>
    /// Экземпляр
    /// </summary>
    public static EyesController Instance = null;

    /// <summary>
    /// Глаза X
    /// </summary>
    public Slider EyasX;

    /// <summary>
    /// Глаза Y
    /// </summary>
    public Slider EyasY;

    /// <summary>
    /// Текущий чекбокс
    /// </summary>
    public Toggle Toggle;

    /// <summary>
    /// Менеджер цветоискателя
    /// </summary>
    private ColorFinderManager _colorFinderManager;

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
        //DontDestroyOnLoad(gameObject);
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Toggle.isOn)
        {
            EyasX.Enable();
            EyasY.Enable();
            _colorFinderManager = ColorFinderManager.Instance;
            if (!_colorFinderManager.Circles.Any())
            {
                return;
            }
            var point = _colorFinderManager.Circles.First();
            var width = _colorFinderManager.VideoCaptureManager.FrameWidth;
            var height = _colorFinderManager.VideoCaptureManager.FrameHeight;
            EyasX.value = point.Center.X / width;
            EyasY.value = point.Center.Y / height;
        }
        else
        {
            EyasX.Disable();
            EyasY.Disable();
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

    }    
}
