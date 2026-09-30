using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePos;

    [Header("발사 설정")]
    public float fireInterval = 0.2f; // 발사 간격 (초 단위)
    private bool isFiring = false;       // 현재 자동 발사 중인지 여부
    private Coroutine fireCoroutine;

    void Update()
    {
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ToggleFire();
        }
    }

    void ToggleFire()
    {
        isFiring = !isFiring; // 상태 반전 (true -> false, false -> true)

        if (isFiring)
        {
            // 자동 발사 시작
            fireCoroutine = StartCoroutine(FireRoutine());
        }
        else
        {
            // 자동 발사 중지
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
            }
        }
    }

    IEnumerator FireRoutine()
    {
        while (isFiring)
        {
            Fire();
            yield return new WaitForSeconds(fireInterval); // 설정한 간격만큼 대기
        }
    }

    void Fire()
    {
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}