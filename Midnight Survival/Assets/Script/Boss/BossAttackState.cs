using UnityEngine;

public class BossAttackState : State
{
    private float lastAttackTime = 0f;

    public BossAttackState(BossController owner) : base(owner) { }

    public override void Enter()
    {
        Debug.Log("보스: 공격 상태 진입");

        // 예외 처리: 만약 컴포넌트가 없으면 동작하지 않음
        if (owner.fanShot == null) return;

        // 진입하자마자 첫 발사를 하기 위해 타이머 세팅
        lastAttackTime = Time.time - owner.fanShot.attackCooldown;
    }

    public override void Update()
    {
        // [안전장치] 방어 코드를 추가하여 널 참조 에러(NullReferenceException)를 완전히 방지합니다.
        if (owner.fanShot == null)
        {
            Debug.LogError("[FSM 에러] BossAttackState에서 BossFanShot 컴포넌트를 참조할 수 없습니다. 대기 상태로 강제 전환합니다.");
            owner.ChangeState(owner.IdleState);
            return;
        }

        // 쿨타임 체크 후 발사
        if (Time.time >= lastAttackTime + owner.fanShot.attackCooldown)
        {
            owner.fanShot.FireFanBullet(); // 공격 스크립트 발동
            lastAttackTime = Time.time;
        }
    }

    public override void Exit()
    {
        Debug.Log("보스: 공격 상태 종료");
    }
}