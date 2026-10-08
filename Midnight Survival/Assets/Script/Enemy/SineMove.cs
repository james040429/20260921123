using UnityEngine;

public class SineMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float horizontalSpeed = 4.0f; // 좌우 이동 속도
    [SerializeField] private float backwardSpeed = 3.0f;   // 뒤로 나아가는 속도

    [Header("Plane Boundaries")]
    // Plane의 좌우 한계값 (이 범위를 벗어나면 반대로 튕깁니다)
    [SerializeField] private float minX = -46.0f;
    [SerializeField] private float maxX = 46.0f;

    private bool isMovingRight = true; // 현재 오른쪽으로 이동 중인지 여부

    void Start()
    {
        // 시작할 때 무작위 방향으로 출발하고 싶다면 아래 주석을 해제하세요.
        // isMovingRight = (Random.value > 0.5f);
    }

    void Update()
    {
        // 1. 뒤로 계속 나아가는 이동 처리 (Z축)
        float currentZ = transform.position.z - (backwardSpeed * Time.deltaTime);

        // 2. 현재 방향에 따른 좌우 이동 처리 (X축)
        float currentX = transform.position.x;

        if (isMovingRight)
        {
            currentX += horizontalSpeed * Time.deltaTime;

            // 오른쪽 경계(maxX)에 도달하거나 넘어서면 방향을 왼쪽으로 바꿈
            if (currentX >= maxX)
            {
                currentX = maxX; // 경계에 고정
                isMovingRight = false; // 방향 전환
            }
        }
        else
        {
            currentX -= horizontalSpeed * Time.deltaTime;

            // 왼쪽 경계(minX)에 도달하거나 넘어서면 방향을 오른쪽으로 바꿈
            if (currentX <= minX)
            {
                currentX = minX; // 경계에 고정
                isMovingRight = true; // 방향 전환
            }
        }

        // 3. 최종 계산된 X, Z 좌표를 오브젝트 위치에 적용 (Y축은 유지)
        transform.position = new Vector3(currentX, transform.position.y, currentZ);
    }
}