using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    [Header("스피드 설정")]
    public float scrollSpeed = 5f;        // 맵이 뒤로 흘러가는 속도

    [Header("땅 오브젝트 배열 (순서대로 넣으세요)")]
    public GameObject[] grounds;
    public float groundLength = 20f;      // 땅 오브젝트 '하나'의 정확한 Z축 길이

    private float reSpawnZ;               // 땅이 다시 앞으로 돌아갈 기준점 (한계선)
    private float totalLength;            // 전체 땅들의 총 길이

    void Start()
    {
        // 땅이 3개고 길이가 20이라면 총 길이는 60
        totalLength = groundLength * grounds.Length;

        // 화면 밖 뒤쪽으로 넘어가는 기준점 계산 (첫 번째 땅의 시작점보다 뒤)
        // 안전하게 땅 한 칸 크기만큼 뒤로 지정합니다.
        reSpawnZ = -groundLength;
    }

    void Update()
    {
        // 1. 모든 땅을 매 프레임마다 뒤로(Z축 마이너스 방향) 이동시킵니다.
        for (int i = 0; i < grounds.Length; i++)
        {
            grounds[i].transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime);

            // 2. 만약 어떤 땅이 플레이어 뒤쪽 한계선(reSpawnZ)보다 더 뒤로 넘어갔다면?
            if (grounds[i].transform.position.z <= reSpawnZ)
            {
                // 현재 위치에서 '전체 길이'만큼 앞(Z축 플러스 방향)으로 순간이동시킵니다.
                Vector3 newPos = grounds[i].transform.position;
                newPos.z += totalLength;
                grounds[i].transform.position = newPos;
            }
        }
    }
}