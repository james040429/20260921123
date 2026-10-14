using UnityEngine;
using TMPro; // 🔥 TMPro(TextMeshPro) 컴포넌트 사용을 위해 필수!

public class GameManager : MonoBehaviour
{
    // 어디서나 GameManager에 접근할 수 있도록 싱글톤(Singleton) 구현
    public static GameManager Instance { get; private set; }

    [Header("UI 연동")]
    public TextMeshProUGUI killCountText; // 에디터에서 KillCountText 오브젝트를 연결할 변수

    private int killCount = 0; // 실제 처치 수 카운트 변수

    void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 게임 시작 시 UI 텍스트 초기화
        UpdateKillCountUI();
    }

    // 적이 죽었을 때 외부(Enemy 스크립트 등)에서 호출할 함수
    public void AddKill()
    {
        killCount++;
        UpdateKillCountUI();
    }

    // UI 텍스트를 현재 처치 수로 갱신하는 함수
    void UpdateKillCountUI()
    {
        if (killCountText != null)
        {
            killCountText.text = $"Kills: {killCount}";
        }
    }
}