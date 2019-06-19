using UnityEngine.UI;

/// <summary>
/// Расширения для <see cref="Slider"/>
/// </summary>
public static class SliderExtensions
{
    /// <summary>
    /// Выключить <see cref="Slider"/>
    /// </summary>
    /// <param name="slider"><see cref="Slider"/></param>
    /// <param name="value">Значение</param>
    public static void Disable(this Slider slider, float value = Constants.SliderDefaultValue)
    {
        slider.interactable = false;
        slider.value = value;
    }

    /// <summary>
    /// Включить <see cref="Slider"/>
    /// </summary>
    /// <param name="slider"><see cref="Slider"/></param>
    /// <param name="value">Значение</param>
    public static void Enable(this Slider slider, float value = Constants.SliderDefaultValue)
    {
        if (!slider.interactable)
        {
            slider.value = value;
            slider.interactable = true;
        }        
    }
}
