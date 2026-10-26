using UnityEngine;

public class MapScroller : MonoBehaviour
{
    [Header("스피드 설정")]
    public float scrollSpeed = 5f;        // 맵이 뒤로 흘러가는 속도

    [Header("자식 땅 오브젝트 배열 (순서대로 넣으세요)")]
    public Transform[] grounds;
    public float groundLength = 20f;      // 땅 오브젝트 '하나'의 정확한 Z축 길이

    private int frontIndex = 0;           // 현재 가장 뒤에 있는(화면에서 먼저 사라질) 땅의 인덱스

    void Update()
    {
        // 1. 개별 땅이 아니라, '부모 오브젝트 전체'를 뒤로 이동시킵니다.
        // 자식들은 한 몸으로 묶여 움직이므로 절대 틈새가 벌어지지 않습니다.
        transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime);

        // 2. 가장 뒤에 처진 땅이 완전히 화면 밖(-groundLength 미만)으로 나갔는지 체크합니다.
        // 부모의 이동 좌표를 더한 '월드 좌표의 Z'값을 기준으로 검사합니다.
        if (grounds[frontIndex].position.z <= -groundLength)
        {
            // 가장 앞에 있는(가장 멀리 있는) 땅의 현재 Z 위치를 찾습니다.
            int backIndex = (frontIndex + grounds.Length - 1) % grounds.Length;
            float maxZ = grounds[backIndex].localPosition.z;

            // 3. 화면 밖으로 나간 땅의 '로컬 좌표'를 가장 앞쪽 땅의 바로 다음 칸으로 정밀 이동시킵니다.
            Vector3 newLocalPos = grounds[frontIndex].localPosition;
            newLocalPos.z = maxZ + groundLength;
            grounds[frontIndex].localPosition = newLocalPos;

            // 4. 기준 인덱스를 다음 땅으로 넘깁니다.
            frontIndex = (frontIndex + 1) % grounds.Length;
        }
    }
}