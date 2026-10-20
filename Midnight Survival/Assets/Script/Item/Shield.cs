using UnityEngine;

public class Shield : MonoBehaviour,ICollide
{
    [Header("Item Life Time")]
    public float itemLifeTime = 5f; // 아이템 유지 시간 (5초)

    private void Start()
    {
        // 생성되자마자 itemLifeTime(5초) 후에 사라지도록 설정
        Destroy(gameObject, itemLifeTime);
    }

    public void OnCollide(Player_Agent playerAgent)
    {
        // 플레이어의 주요 로직을 담당하는 PlayerController 컴포넌트를 가져옴
        PlayerController player = playerAgent._PlayerCtrl;

        if (player != null)
        {
            player.ActivateShield(); // PlayerController의 보호막 활성화 함수 호출
            Destroy(gameObject);     // 아이템을 먹었으므로 필드에서 즉시 삭제
            Debug.Log("보호막 아이템을 정상적으로 획득했습니다.");
        }
        else
        {
            // 태그는 Player인데 스크립트가 없는 경우 예외 처리 로그
            Debug.LogError("부딪힌 오브젝트에 PlayerController 스크립트가 없습니다! 컴포넌트를 확인해주세요.");
        }
    }

}