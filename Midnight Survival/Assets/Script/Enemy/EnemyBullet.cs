using UnityEngine;

public class EnemyBullet : MonoBehaviour, ICollide
{
    public float speed = 20f;   // 총알 속도
    public float damage = 15f;

    private Camera mainCamera;

    void Awake()
    {
        // 씬에서 태그가 "MainCamera"인 카메라를 미리 찾아둡니다.
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 매 프레임마다 후방(Z축)으로 이동
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // 메인 카메라가 존재할 때만 화면 밖 체크를 수행합니다.
        if (mainCamera != null)
        {
            CheckOutOfBounds();
        }
    }

    void CheckOutOfBounds()
    {
        // 총알의 세계 좌표(World Position)를 카메라의 뷰포트 좌표(0~1 사이 값)로 변환합니다.
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);

        // 카메라 뒤쪽에 있거나(z <= 0), 가로(x) 또는 세로(y) 화면 범위를 벗어났는지 확인합니다.
        if (viewPos.z <= 0f || viewPos.x < 0f || viewPos.x > 1f || viewPos.y < 0f || viewPos.y > 1f)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollide(Player_Agent playerAgent)
    {
        if (playerAgent._Health != null)
        {
            playerAgent._Health.TakeDamage(10); // PlayerHealth 10 감소
        }

        // 부딪힌 후 총알 파괴
        Destroy(gameObject);
    }
}