using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    // 유니티 에디터에서 설정할 수 있도록 구조체 생성
    [System.Serializable]
    public struct DropItem
    {
        public GameObject itemPrefab; // 떨어뜨릴 아이템 프리팹
        [Range(0f, 100f)]
        public float dropChance;      // 드롭 확률 (0% ~ 100%)
    }

    [Header("Item List")]
    public List<DropItem> dropItems = new List<DropItem>(); // 적이 줄 수 있는 아이템 리스트

    // 이 메서드는 적의 체력이 0이 되어 죽는 순간에 호출하면 됩니다.
    public void OnDeath()
    {
        DropItems();
        Destroy(gameObject); // 적 캐릭터 오브젝트 파괴
    }

    private void DropItems()
    {
        foreach (DropItem item in dropItems)
        {
            // 0부터 100 사이의 랜덤 숫자를 뽑아 확률 계산
            float randomValue = Random.Range(0f, 100f);

            if (randomValue <= item.dropChance)
            {
                // 아이템을 적의 현재 위치에 생성
                Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}