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