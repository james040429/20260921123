using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;// 코루틴 사용을 위해 추가

public class PatternShooter : MonoBehaviour
{
    [Header("발사 설정")]
    public GameObject bulletPrefab;
    public Transform[] firePoints;
    public float fireInterval = 3.0f; // 3초마다 패턴 전환 발사

    [Header("패턴 1: 부채꼴 설정 (한 번에 발사)")]
    public int fanBulletCount = 9;
    public float fanAngle = 90f;

    [Header("패턴 2: 흩뿌리기(입자) 설정 (연사 발사)")]
    public int scatterBulletCount = 20;  // 총 발사할 입자 수
    public float scatterAngle = 60f;       // 발사 범위 각도
    public float angleVariance = 10f;      // 불규칙 편차
    public float scatterShootDelay = 0.05f; // 입자와 입자 사이의 발사 간격 (초)

    private float timer = 0f;
    private bool isFiringScatter = false; // 입자 연사 중에는 타이머 멈춤용

    void Update()
    {
        // 입자 연사 패턴이 진행 중일 때는 타이머가 가지 않도록 방지
        if (isFiringScatter) return;

        timer += Time.deltaTime;
        if (timer >= fireInterval)
        {
            timer = 0f;
            FireRandomPattern();
        }
    }

    void FireRandomPattern()
    {
        if (bulletPrefab == null) return;

        int patternIndex = Random.Range(0, 2);

        if (patternIndex == 0)
        {
            // 패턴 1: 정형화된 부채꼴 (한 번에 촤악)
            FireMultiplePointsImmediate();
        }
        else
        {
            // 패턴 2: 불꽃처럼 흩뿌리기 코루틴 시작 (다다다닥)
            StartCoroutine(FireScatterRoutine());
        }
    }

    // ----------------------------------------
    // [패턴 1] 기존 부채꼴 발사 로직 (즉시 발사)
    // ----------------------------------------
    void FireMultiplePointsImmediate()
    {
        Transform[] targets = (firePoints == null || firePoints.Length == 0) ? new Transform[] { transform } : firePoints;

        foreach (Transform spawnPoint in targets)
        {
            if (spawnPoint == null) continue;

            if (fanBulletCount <= 1)
            {
                Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                continue;
            }

            float startAngle = -fanAngle / 2f;
            float angleStep = fanAngle / (fanBulletCount - 1);

            for (int i = 0; i < fanBulletCount; i++)
            {
                float currentAngle = startAngle + (angleStep * i);
                Quaternion bulletRotation = spawnPoint.rotation * Quaternion.Euler(0, currentAngle, 0);
                Instantiate(bulletPrefab, spawnPoint.position, bulletRotation);
            }
        }
    }

    // ----------------------------------------
    // [패턴 2] 입자 흩뿌리기 로직 (시간 차 연사)
    // ----------------------------------------
    IEnumerator FireScatterRoutine()
    {
        isFiringScatter = true; // 연사 시작 안내

        Transform[] targets = (firePoints == null || firePoints.Length == 0) ? new Transform[] { transform } : firePoints;

        // 지정된 개수만큼 탄을 시간 차를 두고 발사
        for (int i = 0; i < scatterBulletCount; i++)
        {
            foreach (Transform spawnPoint in targets)
            {
                if (spawnPoint == null) continue;

                // 매 발사마다 랜덤한 각도 계산
                float randomBaseAngle = Random.Range(-scatterAngle / 2f, scatterAngle / 2f);
                float randomOffset = Random.Range(-angleVariance, angleVariance);
                float finalAngle = randomBaseAngle + randomOffset;

                Quaternion bulletRotation = spawnPoint.rotation * Quaternion.Euler(0, finalAngle, 0);
                Instantiate(bulletPrefab, spawnPoint.position, bulletRotation);
            }

            // scatterShootDelay(예: 0.05초)만큼 쉬었다가 다음 탄 발사
            yield return new WaitForSeconds(scatterShootDelay);
        }

        isFiringScatter = false; // 연사 종료
    }
}