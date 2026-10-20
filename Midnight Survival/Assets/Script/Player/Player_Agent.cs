
using UnityEngine;

public class Player_Agent : MonoBehaviour
{
    [SerializeField] PlayerHealth _health;
    public PlayerHealth _Health => _health;
    [SerializeField] PlayerController _playerController;
    public PlayerController _PlayerCtrl => _playerController;
    [SerializeField] PlayerFire _fire;



    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ICollide>(out ICollide iCollide ))
        {
            iCollide.OnCollide(this);
        }
    }
}
