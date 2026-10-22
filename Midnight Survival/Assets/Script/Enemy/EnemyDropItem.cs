using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    [Header("Shield Drop Settings")]
    [Tooltip("유니티 에디터에서 생성할 Shield 아이템 프리팹을 연결해주세요.")]
    public GameObject shieldItemPrefab;

    [Range(0f, 100f)]
    [Tooltip("아이템이 드롭될 확률입니다. (0 = 드롭 안 됨, 100 = 무조건 드롭)")]
    public float dropProbability = 10f; // 📌 주석에 맞춰 기본값을 10%로 수정했습니다.

    /// <summary>
    /// 외부(EnemyHealth 등)에서 적이 죽었을 때 호출하는 드롭 함수
    /// </summary>
    public void DropShieldItem()
    {
        // 프리팹이 연결되지 않았다면 확률 계산을 하기 전에 미리 경고를 띄우고 리턴합니다.
        if (shieldItemPrefab == null)
        {
            Debug.LogWarning($"[{gameObject.name}] Shield Item Prefab이 인스펙터 창에 연결되지 않았습니다!");
            return;
        }

        // 0.0초과 100.0이하 사이의 무작위 값 추출
        float randomValue = Random.Range(0.0001f, 100f);

        // 뽑은 값이 설정한 확률보다 작거나 같으면 획득 성공
        if (randomValue <= dropProbability)
        {
            // 적이 죽은 현재 위치(transform.position)에 보호막 아이템 생성
            Instantiate(shieldItemPrefab, transform.position, Quaternion.identity);
            Debug.Log($"{gameObject.name}이(가) {dropProbability}% 확률을 뚫고 보호막 아이템을 드롭했습니다!");
        }
        else
        {
            Debug.Log($"{gameObject.name} 드롭 실패 (확률: {dropProbability}%, 뽑은 값: {randomValue:F2})");
        }
    }
}