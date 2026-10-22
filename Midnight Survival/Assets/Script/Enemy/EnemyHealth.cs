using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public float lifeTime = 20f;

    void Start()
    {
        // 일정 시간이 지나면 자동으로 파괴
        Destroy(gameObject, lifeTime);
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // 부딪힌 총알 오브젝트에서 Bullet 스크립트 컴포넌트를 가져옵니다.
            Bullet bullet = other.GetComponent<Bullet>();

            // 스크립트가 정상적으로 들어있다면 그 총알의 damage 값만큼 데미지를 줍니다.
            if (bullet != null)
            {
                TakeDamage((int)bullet.damage);
            }
            else
            {
                // 혹시 모를 예외 상황(태그는 Bullet인데 스크립트가 없는 경우)을 위한 기본 데미지
                TakeDamage(10);
            }

            // 총알 오브젝트 파괴
            Destroy(other.gameObject);
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
        // 적 오브젝트 파괴 (아이템 드롭 기능 삭제됨)
        Destroy(gameObject);
    }
}