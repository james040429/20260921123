using UnityEngine;

public class Enemy_Agent : MonoBehaviour,ICollide
{
    [SerializeField] EnemyHealth _enemyhealth;
    public EnemyHealth _EnemyHealth => _enemyhealth;

    public void OnCollide( Player_Agent playerAgent )
    {
        playerAgent._Health.TakeDamage(10);
    }
}
