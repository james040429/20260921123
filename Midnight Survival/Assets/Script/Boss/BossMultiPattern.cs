using System.Collections;
using UnityEngine;

public class BossMultiPattern : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Pattern A: Fan Shot (부채꼴 발사)")]
    public int fanBulletCount = 5;
    public float fanAngle = 60f;
    public float fanBulletSpeed = 5f;
    public int fanBurstCount = 3;             // 부채꼴 연사 횟수
    public float fanDelayBetweenShots = 0.2f; // 부채꼴 연사 간격

    [Header("Pattern B: Particle Shot (입자 난사 발사)")]
    public int particleTotalCount = 30;       // 한 패턴 동안 발사할 총 입자 수
    public float particleDelay = 0.05f;       // 입자와 입자 사이의 아주 짧은 발사 간격
    public float particleMinSpeed = 3f;       // 최소 속도
    public float particleMaxSpeed = 8f;       // 최대 속도
    public float particleSpreadAngle = 45f;   // 입자가 퍼질 총 각도 범위 (예: 45도 안에서 무작위)

    [Header("Particle Size Settings (크기 다채롭게)")]
    public float minScale = 0.4f;             // 최소 크기 배율
    public float maxScale = 1.5f;             // 최대 크기 배율

    [Header("Global Timing")]
    public float delayBetweenPatterns = 3f;   // 패턴과 패턴 사이의 대기 시간 (3초)

    void Start()
    {
        // 보스가 생성되면 패턴 제어 코루틴 시작
        StartCoroutine(BossAttackMasterRoutine());
    }

    // 두 패턴을 번갈아가며 실행하는 메인 코루틴
    IEnumerator BossAttackMasterRoutine()
    {
        while (true)
        {
            // 1. 패턴 A (정돈된 부채꼴 발사) 실행
            yield return StartCoroutine(FanShotRoutine());
            yield return new WaitForSeconds(delayBetweenPatterns);

            // 2. 패턴 B (다채로운 크기의 입자 난사) 실행
            yield return StartCoroutine(ParticleShotRoutine());
            yield return new WaitForSeconds(delayBetweenPatterns);
        }
    }

    // [패턴 A 코루틴] 규칙적인 부채꼴 발사
    IEnumerator FanShotRoutine()
    {
        for (int i = 0; i < fanBurstCount; i++)
        {
            FireFanBullet();
            yield return new WaitForSeconds(fanDelayBetweenShots);
        }
    }

    // [패턴 B 코루틴] 다채로운 크기와 속도의 입자 난사
    IEnumerator ParticleShotRoutine()
    {
        for (int i = 0; i < particleTotalCount; i++)
        {
            // 1. 무작위 각도 계산 (firePoint 앞방향 기준 좌우 무작위 오차)
            float randomAngle = Random.Range(-particleSpreadAngle / 2f, particleSpreadAngle / 2f);
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, randomAngle, 0);

            // 2. 입자(총알) 생성
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);

            // 3. 크기를 다채롭게 조절 (Random.Range를 이용해 균등하게 무작위 크기 부여)
            float randomScale = Random.Range(minScale, maxScale);
            bullet.transform.localScale = Vector3.one * randomScale;

            // 4. 무작위 속도 부여
            float randomSpeed = Random.Range(particleMinSpeed, particleMaxSpeed);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = rotation * Vector3.forward * randomSpeed;
            }

            // 아주 짧은 대기시간을 두어 후두두둑 떨어지는(뿜어지는) 느낌 연출
            yield return new WaitForSeconds(particleDelay);
        }
    }

    // 부채꼴 발사 핵심 로직
    void FireFanBullet()
    {
        if (fanBulletCount <= 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // 기본 크기로 리셋 (패턴 B에서 프리팹 크기가 변형되는 것을 방지)
            bullet.transform.localScale = Vector3.one;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = firePoint.forward * fanBulletSpeed;
            return;
        }

        float startAngle = -fanAngle / 2f;
        float angleStep = fanAngle / (fanBulletCount - 1);

        for (int i = 0; i < fanBulletCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, currentAngle, 0);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);

            bullet.transform.localScale = Vector3.one; // 기본 크기 보장

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = rotation * Vector3.forward * fanBulletSpeed;
        }
    }
}