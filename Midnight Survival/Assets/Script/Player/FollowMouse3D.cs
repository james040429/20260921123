using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse3D : MonoBehaviour
{
    private Camera mainCamera;
    private Plane groundPlane;
    private Rigidbody rb; // 리지드바디 캐싱
    private Vector3 currentVelocity;

    [Header("오브젝트 높이 및 오프셋 설정")]
    public Vector3 offset;

    [Header("움직임 속도 계수 (낮을수록 타겟에 빨리 도달)")]
    [Range(0.01f, 1f)]
    public float smoothTime = 0.2f;

    [Header("이동 제한 범위 (Min / Max)")]
    [SerializeField] private float minX = -85f;
    [SerializeField] private float maxX = 85f;
    [SerializeField] private float minZ = -26f;
    [SerializeField] private float maxZ = 120f;

    // FixedUpdate에서 사용하기 위해 마우스 목표 지점을 저장하는 변수
    private Vector3 targetPosition;
    private bool hasValidTarget = false;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("씬에 'MainCamera' 태그가 지정된 카메라가 없습니다!");
            enabled = false;
            return;
        }

        // 1. 리지드바디 컴포넌트 가져오기 및 예외 처리
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("플레이어에 Rigidbody 컴포넌트가 없습니다! 추가해주세요.");
            enabled = false;
            return;
        }

        // 2. 물리 연산으로 가라앉거나 회전이 뒤틀리는 현상 방지
        rb.useGravity = false; // 마우스 추적 중에는 중력 일시 해제 (필요시 꺼두기)
        rb.constraints = RigidbodyConstraints.FreezeRotation; // 마우스 이동 시 멋대로 회전하는 것 방지

        groundPlane = new Plane(Vector3.up, offset);
        targetPosition = transform.position;
    }

    void Update()
    {
        // 인풋 시스템의 마우스 입력은 프레임 기반(Update)으로 안전하게 받아옵니다.
        if (Mouse.current == null) return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (groundPlane.Raycast(ray, out float rayDistance))
        {
            Vector3 targetPoint = ray.GetPoint(rayDistance);

            float clampedX = Mathf.Clamp(targetPoint.x, minX, maxX);
            float clampedZ = Mathf.Clamp(targetPoint.z, minZ, maxZ);

            // 목표 좌표 설정 (Y값은 바닥으로 가라앉지 않게 고정하거나 평면 높이 적용)
            targetPosition = new Vector3(clampedX, targetPoint.y, clampedZ);
            hasValidTarget = true;
        }
    }

    void FixedUpdate()
    {
        if (!hasValidTarget) return;

        // 3. transform.position 대신 Rigidbody를 부드럽게 물리적으로 이동시킵니다.
        // 이 방식이 덜덜거리는 떨림(Jittering)을 완벽하게 잡아줍니다.
        Vector3 nextPosition = Vector3.SmoothDamp(
            rb.position,
            targetPosition,
            ref currentVelocity,
            smoothTime,
            Mathf.Infinity,
            Time.fixedDeltaTime // FixedUpdate 내부이므로 fixedDeltaTime 기준 계산
        );

        rb.MovePosition(nextPosition);
    }
}