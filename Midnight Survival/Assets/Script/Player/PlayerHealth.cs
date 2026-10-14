using System;
using UnityEngine;
using UnityEngine.UI; // 🔥 추가: UI 컴포넌트(Slider) 제어를 위해 필수!

public class PlayerHealth : MonoBehaviour
{
    [Header("HP 시스템")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI 연동")]
    public Slider hpSlider; // 🔥 추가: 인스펙터창에서 슬라이더를 연결할 변수

    void Start()
    {
        currentHealth = maxHealth;

        // 🔥 추가: 게임 시작 시 체력바의 최대값과 현재값을 초기화합니다.
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            EnemyBullet Enemybullet = other.GetComponent<EnemyBullet>();

            if (Enemybullet != null)
            {
                TakeDamage((int)Enemybullet.damage);
            }
            else
            {
                TakeDamage(10);
            }

            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // 🔥 추가: 체력이 0 미만이나 maxHealth 초과가 되지 않도록 안전하게 제한합니다.
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // 🔥 추가: 체력이 깎일 때마다 체력바 UI의 value를 갱신합니다.
        if (hpSlider != null)
        {
            hpSlider.value = currentHealth;
        }

        Debug.Log($"체력: {currentHealth} (받은 데미지: {damage})");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("사망했습니다.");

        // 만약 오브젝트를 바로 파괴(Destroy)하면 사망 연출이나 UI 처리에 문제가 생길 수 있어, 
        // 일반적으로는 비활성화(SetActive)만 하거나 파괴 순서를 조절합니다.
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}