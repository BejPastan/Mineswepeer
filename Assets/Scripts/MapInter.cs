using UnityEngine;
using UnityEngine.Events;

public class MapInter : MonoBehaviour
{
    public UnityEvent onMouseClick;

    private void OnMouseDown()
    {
        onMouseClick.Invoke();
    }
}
