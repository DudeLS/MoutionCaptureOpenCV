using UnityEngine;

public abstract class Controller : MonoBehaviour, IController
{
    public abstract void Disable();

    public abstract void Enable();
}
