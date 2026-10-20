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
        // 땅의 총 길이 계산
        totalLength = groundLength * grounds.Length;

        // 기준점은 첫 번째 땅의 위치보다 딱 한 칸 뒤로 설정합니다.
        reSpawnZ = -groundLength;
    }

    void Update()
    {
        // 1. 모든 땅을 매 프레임마다 뒤로 이동시킵니다.
        for (int i = 0; i < grounds.Length; i++)
        {
            grounds[i].transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime);

            // 2. 땅이 한계선보다 뒤로 넘어갔을 때
            if (grounds[i].transform.position.z <= reSpawnZ)
            {
                Vector3 newPos = grounds[i].transform.position;

                // [핵심 보정] 한계선(reSpawnZ)을 얼마나 초과했는지 오차를 구합니다.
                float overshoot = reSpawnZ - newPos.z;

                // 단순히 totalLength를 더하는 것이 아니라, 
                // 정확한 리스폰 위치에서 오버슈트된 오차만큼을 빼주어 정밀하게 맞춥니다.
                newPos.z = reSpawnZ + totalLength - overshoot;

                grounds[i].transform.position = newPos;
            }
        }
    }
}