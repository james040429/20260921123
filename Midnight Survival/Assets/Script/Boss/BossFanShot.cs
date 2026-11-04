using UnityEngine;

public class BossFanShot : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;

    // [수정] 인스펙터에서 직관적으로 2개를 넣을 수 있도록 개별 변수로 선언합니다.
    public Transform firePoint1;
    public Transform firePoint2;

    public int bulletCount = 5;
    public float fanAngle = 60f;
    public float bulletSpeed = 5f;

    [Header("Time Settings")]
    public float attackCooldown = 2f; // 발사 간격 (FSM 상태가 참고하는 값)

    // FSM의 BossAttackState에서 호출할 핵심 메서드
    public void FireFanBullet()
    {
        // 1번 위치와 2번 위치에서 각각 부채꼴 탄막을 생성합니다.
        ShootFromPoint(firePoint1);
        ShootFromPoint(firePoint2);
    }

    // 단일 발사 포인트에서 부채꼴 연산을 처리하는 내부 헬퍼 메서드
    private void ShootFromPoint(Transform fp)
    {
        // 발사 포인트 오브젝트가 인스펙터에 지정되지 않았다면 건너뜁니다.
        if (fp == null || bulletPrefab == null) return;

        // 총알 개수가 1개 이하일 때는 부채꼴 계산 없이 정면으로 1발 발사
        if (bulletCount <= 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, fp.position, fp.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = fp.forward * bulletSpeed;
            return;
        }

        // 부채꼴 각도 분할 계산
        float startAngle = -fanAngle / 2f;
        float angleStep = fanAngle / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);

            // 발사 포인트의 현재 회전값 기준 Y축 회전 적용
            Quaternion rotation = fp.rotation * Quaternion.Euler(0, currentAngle, 0);
            GameObject bullet = Instantiate(bulletPrefab, fp.position, rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Unity 2023 / Unity 6의 최신 API 규격에 맞춘 물리 속도 적용
                rb.linearVelocity = rotation * Vector3.forward * bulletSpeed;
            }
        }
    }
}