using System.Collections;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bulletPrefab; // 적이 발사할 총알 프리팹
    public Transform firePos;       // 발사 위치

    [Header("발사 설정")]
    public float fireInterval = 2.0f; // 발사 간격 (2초)

    void Start()
    {
        // 게임이 시작되면 자동으로 발사 코루틴을 실행합니다.
        StartCoroutine(AutoFireRoutine());
    }

    IEnumerator AutoFireRoutine()
    {
        // 적이 파괴되거나 스크립트가 꺼지기 전까지 무한 반복합니다.
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(fireInterval); // 설정한 간격(2초)만큼 대기
        }
    }

    void Fire()
    {
        if (bulletPrefab != null && firePos != null)
        {
            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        }
    }
}