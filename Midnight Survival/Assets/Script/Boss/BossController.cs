using UnityEngine;

public class BossController : MonoBehaviour
{
    private State currentState;

    // 공격 스크립트를 참조할 변수
    [HideInInspector] public BossFanShot fanShot;

    // 상태 객체들
    public BossIdleState IdleState { get; private set; }
    public BossAttackState AttackState { get; private set; }

    void Awake()
    {
        // [핵심] 같은 오브젝트에 붙은 BossFanShot 컴포넌트를 정확히 가져옵니다.
        fanShot = GetComponent<BossFanShot>();

        // 만약 찾지 못했다면 인스펙터나 컴포넌트 부착 상태를 강제로 확인하도록 경고를 띄웁니다.
        if (fanShot == null)
        {
            Debug.LogError($"[FSM 에러] {gameObject.name} 오브젝트에 'BossFanShot' 스크립트가 부착되어 있지 않습니다! 인스펙터 창에서 스크립트를 추가해 주세요.");
            Debug.Break(); // 유니티 에디터를 일시정지 시켜 추적을 돕습니다.
        }

        // 상태 인스턴스 생성 (반드시 컴포넌트를 먼저 찾은 뒤에 생성해야 안전합니다)
        IdleState = new BossIdleState(this);
        AttackState = new BossAttackState(this);
    }

    void Start()
    {
        // 시작 상태 지정
        ChangeState(IdleState);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public void ChangeState(State newState)
    {
        if (currentState == newState) return;

        if (currentState != null) currentState.Exit();
        currentState = newState;
        if (currentState != null) currentState.Enter();
    }
}