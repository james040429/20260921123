using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePos;

    void Update()
    {
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Fire();
        }
    }

    void Fire()
    {
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}