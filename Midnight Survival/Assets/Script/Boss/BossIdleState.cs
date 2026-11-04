using UnityEngine;

public class BossIdleState : State
{
    private float idleTimer = 0f;
    private float duration = 3f; // 3초 동안 대기 상태 유지

    // 생성자
    public BossIdleState(BossController owner) : base(owner) { }

    // 상태 진입 시 호출
    public override void Enter()
    {
        Debug.Log("보스: 대기 상태 진입");
        idleTimer = 0f;
    }

    // 상태 유지 중 매 프레임 호출 (에러가 발생한 핵심 부분입니다!)
    public override void Update()
    {
        idleTimer += Time.deltaTime;

        // 3초가 지나면 공격 상태(AttackState)로 전환합니다.
        if (idleTimer >= duration)
        {
            owner.ChangeState(owner.AttackState);
        }
    }

    // 상태에서 빠져나올 때 호출
    public override void Exit()
    {
        Debug.Log("보스: 대기 상태 종료");
    }
}