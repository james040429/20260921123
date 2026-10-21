
using UnityEngine;

public class Player_Agent : MonoBehaviour
{
    [SerializeField] PlayerHealth _health;
    public PlayerHealth _Health => _health;
    [SerializeField] Shield_Activer _shieldActiver;
    public Shield_Activer _Shield => _shieldActiver;



    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ICollide>(out ICollide iCollide ))
        {
            iCollide.OnCollide(this);
        }
    }
}
