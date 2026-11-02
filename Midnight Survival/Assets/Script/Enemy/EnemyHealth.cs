using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    private Camera mainCamera;

    void Awake()
    {
        // 씬에서 태그가 "MainCamera"인 카메라를 미리 찾아둡니다.
        mainCamera = Camera.main;
    }

    public int maxHealth = 100;
    private int currentHealth;
    public float lifeTime = 60f;

    void Start()
    {
        // 일정 시간이 지나면 자동으로 파괴
        Destroy(gameObject, lifeTime);
        currentHealth = maxHealth;
    }
    private void Update()
    {
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"적 체력: {currentHealth} (받은 데미지: {damage})");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddKill();
        }
        Debug.Log("적이 사망했습니다.");

        EnemyDropItem dropSystem = GetComponent<EnemyDropItem>();
        if (dropSystem != null)
        {
            dropSystem.DropShieldItem();
        }

        // 2. 그 뒤에 적 오브젝트 삭제
        Destroy(gameObject);
    }
}