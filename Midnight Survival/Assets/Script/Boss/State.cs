using UnityEngine;

public abstract class State
{
    protected BossController owner;

    public State(BossController owner)
    {
        this.owner = owner;
    }

    public abstract void Enter();  // 상태에 진입할 때 1번 실행
    public abstract void Update(); // 상태 유지 중 매 프레임 실행
    public abstract void Exit();   // 상태에서 빠져나올 때 1번 실행
}