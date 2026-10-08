using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject[] enemyPrefabs;    // 생성할 적 프리팹 배열
    [SerializeField] private float spawnInterval = 3.0f;    // 생성 주기 (초 단위)

    [Header("Spawn Area Settings (No Plane Required)")]
    // 🔥 spawnPlane 대신 스포너 가로/세로 범위를 직접 지정합니다.
    [SerializeField] private float spawnRangeX = 10.0f;     // X축 스폰 범위 (중심으로부터 가로 반경)
    [SerializeField] private float spawnRangeZ = 10.0f;     // Z축 스폰 범위 (중심으로부터 세로 반경)

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) return;

        // 1. 어떤 적을 생성할지 랜덤 선택
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemy = enemyPrefabs[randomEnemyIndex];

        // 2. 소환할 위치 계산 (스포너 위치 기준 범위 랜덤)
        Vector3 spawnPosition = GetRandomPosition();

        // 3. 프리팹 생성
        Instantiate(selectedEnemy, spawnPosition, selectedEnemy.transform.rotation);
    }

    // 🔥 스포너 오브젝트의 위치를 중심으로 랜덤 좌표를 계산하는 함수
    private Vector3 GetRandomPosition()
    {
        // 현재 스포너의 위치(transform.position)를 기준점으로 잡습니다.
        float minX = transform.position.x - spawnRangeX;
        float maxX = transform.position.x + spawnRangeX;
        float minZ = transform.position.z - spawnRangeZ;
        float maxZ = transform.position.z + spawnRangeZ;

        // 범위 안에서 랜덤한 X, Z 좌표 추출
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        // Y축 높이는 스포너의 현재 높이를 그대로 따라갑니다.
        float yPosition = transform.position.y;

        return new Vector3(randomX, yPosition, randomZ);
    }

    // 💡 유니티 에디터 화면(Scene 뷰)에서 스폰 범위를 눈으로 확인할 수 있게 해주는 편리한 기능입니다.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        // 스포너 위치에 지정된 크기만큼의 초록색 사각형 박스를 그려줍니다.
        Vector3 size = new Vector3(spawnRangeX * 2, 0.1f, spawnRangeZ * 2);
        Gizmos.DrawWireCube(transform.position, size);
    }
}