using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            // 🔥 수정: 부딪힌 총알 오브젝트에서 Bullet 스크립트 컴포넌트를 가져옵니다.
            EnemyBullet Enemybullet = other.GetComponent<EnemyBullet>();

            // 스크립트가 정상적으로 들어있다면 그 총알의 damage 값만큼 데미지를 줍니다.
            if (Enemybullet != null)
            {
                TakeDamage((int)Enemybullet.damage);
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
        Debug.Log($"체력: {currentHealth} (받은 데미지: {damage})");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("사망했습니다.");
        Destroy(gameObject);
    }
}