using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    /// <summary>
    /// Выключить
    /// </summary>
    void Disable();

    /// <summary>
    /// Включить
    /// </summary>
    void Enable();
}
