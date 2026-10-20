using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    [Header("Shield Drop Settings")]
    public GameObject shieldItemPrefab; // 📌 유니티 에디터에서 Shield 아이템 프리팹을 연결해주세요.

    [Range(0f, 100f)]
    public float dropProbability = 100f; // 📌 드롭 확률 (기본 10%)

    // 외부(EnemyHealth)에서 적이 죽었을 때 호출할 드롭 함수
    public void DropShieldItem()
    {
        // 0.0부터 100.0 사이의 무작위 값 추출
        float randomValue = Random.Range(0f, 100f);

        // 뽑은 값이 설정한 확률보다 작거나 같으면 획득 성공
        if (randomValue <= dropProbability)
        {
            if (shieldItemPrefab != null)
            {
                // 적이 죽은 현재 위치(transform.position)에 보호막 아이템 생성
                Instantiate(shieldItemPrefab, transform.position, Quaternion.identity);
                Debug.Log($"{dropProbability}% 확률 성공! 보호막 아이템이 드롭되었습니다.");
            }
            else
            {
                Debug.LogWarning("Shield Item Prefab이 인스펙터 창에 연결되지 않았습니다!");
            }
        }
    }
}