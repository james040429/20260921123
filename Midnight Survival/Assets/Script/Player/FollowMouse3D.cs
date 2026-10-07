using UnityEngine;
// 1. 새로운 인풋 시스템 네임스페이스를 추가합니다.
using UnityEngine.InputSystem;

public class FollowMouse3D : MonoBehaviour
{
    private Camera mainCamera;
    private Plane groundPlane;

    public Vector3 _offset;

    void Start()
    {
        mainCamera = Camera.main;
        // y값이 0인 평면 설정
        groundPlane = new Plane(Vector3.up, _offset);
    }

    void Update()
    {
        // 2. 현재 활성화된 마우스가 없으면 코드 실행을 건너뜁니다.
        if (Mouse.current == null) return;

        // 3. 새 인풋 시스템 방식으로 마우스 화면 좌표를 가져옵니다.
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // 마우스 포인터 위치를 향하는 레이 생성
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        float rayDistance;

        // 레이와 평면이 교차하는 지점 계산
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 targetPoint = ray.GetPoint(rayDistance);
            // 오브젝트 위치 갱신
            Vector3 pos = targetPoint;

            float posX = Mathf.Clamp(pos.x, -46f, 46f);
            float posZ = Mathf.Clamp(pos.z, -22, 22f);

            pos.x = posX;
            pos.z = posZ;



            transform.position = pos;







        }
    }
}