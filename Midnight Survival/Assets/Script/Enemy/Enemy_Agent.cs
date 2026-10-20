using UnityEngine;

public class Enemy_Agent : MonoBehaviour,ICollide
{
    public void OnCollide( Player_Agent playerAgent )
    {
        playerAgent._Health.TakeDamage(10);
    }
}
