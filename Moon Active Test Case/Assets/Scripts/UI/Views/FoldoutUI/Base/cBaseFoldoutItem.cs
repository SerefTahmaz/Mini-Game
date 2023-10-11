using UnityEngine;

public abstract class cBaseFoldoutItem : MonoBehaviour
{
    public abstract void Refresh();
    public abstract void Activate(float duration);
    public abstract void Deactivate(float duration);
}