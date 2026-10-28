using UnityEngine;
using UnityEngine.Events;

public class Destroy_Eventer : MonoBehaviour
{
    public UnityEvent _onDestroy;
    public LayerMask targetLayerMask;

    private void OnDestroy()
    {
        _onDestroy?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((targetLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
