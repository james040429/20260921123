using UnityEngine;

public class BossIdleState : State
{
    private float idleTimer = 0f;
    private float duration = 3f; // 전체 대기 및 진입 시간 (3초)

    // 구간별 Z축 기준점 설정
    private float startZ = 500f;
    private float middleZ = 0f;
    private float targetZ = -300f;

    // 구간별 시간 배분 (총 3초)
    private float fastPhaseDuration = 1.0f; // 500에서 0까지 빛의 속도로 올 시간 (1초)
    // 남은 시간(2.0초)은 0에서 -250까지 천천히 이동하는 데 사용됩니다.

    public BossIdleState(BossController owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("보스: 대기 상태 진입 (Z: 500에서 초고속 출발)");
        idleTimer = 0f;

        // 진입 즉시 시작 위치(Z: 500)로 보스 배치
        Vector3 currentPos = owner.transform.position;
        owner.transform.position = new Vector3(currentPos.x, currentPos.y, startZ);
    }

    public override void Update()
    {
        idleTimer += Time.deltaTime;
        Vector3 currentPos = owner.transform.position;
        float currentZ = startZ;

        // 1구간: 게임 시작 후 1초 동안 (Z: 500 -> Z: 0) 빠르게 돌진
        if (idleTimer < fastPhaseDuration)
        {
            // 0초부터 1초까지의 비율 계산 (0.0 ~ 1.0)
            float t1 = Mathf.Clamp01(idleTimer / fastPhaseDuration);

            // 가속도를 붙이기 위해 t 값을 제곱(t * t)하여 처음엔 슈욱 빨라지게 연출
            float easeInT = t1 * t1;
            currentZ = Mathf.Lerp(startZ, middleZ, easeInT);
        }
        // 2구간: 1초 이후부터 3초까지 (Z: 0 -> Z: -250) 부드럽게 감속하며 안착
        else
        {
            // 1초부터 3초까지 흐른 시간 계산 (0.0 ~ 2.0초 사이)
            float elapsedInSlowPhase = idleTimer - fastPhaseDuration;
            float slowPhaseDuration = duration - fastPhaseDuration; // 2.0초

            // 2구간 내에서의 비율 계산 (0.0 ~ 1.0)
            float t2 = Mathf.Clamp01(elapsedInSlowPhase / slowPhaseDuration);

            // 도착지점에서 부드럽게 멈추도록 감속 곡선(SmoothStep) 적용
            float easeOutT = Mathf.SmoothStep(0f, 1f, t2);
            currentZ = Mathf.Lerp(middleZ, targetZ, easeOutT);
        }

        // 연산된 Z값을 보스 좌표에 실시간 반영
        owner.transform.position = new Vector3(currentPos.x, currentPos.y, currentZ);

        // 전체 지정 시간(3초)이 완료되면 공격 상태로 전환
        if (idleTimer >= duration)
        {
            owner.ChangeState(owner.AttackState);
        }
    }

    public override void Exit()
    {
        Debug.Log("보스: 등장 완료, 공격 상태 전환 (최종 Z: -300고정)");

        // 오차 방지를 위해 최종 목적지에 강제 고정
        Vector3 currentPos = owner.transform.position;
        owner.transform.position = new Vector3(currentPos.x, currentPos.y, targetZ);
    }
}