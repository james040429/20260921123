using UnityEngine;
using System.Collections;

public class Shield_Activer : MonoBehaviour
{
    [Header("Shield Settings")]
    public bool hasShield = false;         // 현재 보호막 보유 여부
    public float shieldDuration = 5f;      // 보호막 지속 시간 (5초)
    public GameObject shieldVisualPrefab;  // 플레이어 자식으로 붙어있는 구체(Sphere) 보호막 오브젝트
    GameObject _shieldInst;
    private Coroutine shieldTimerCoroutine;

    [SerializeField] PlayerHealth _playerHealth;

    private void Start()
    {  
        
    }

    // Shield 아이템을 먹었을 때 호출되는 함수
    public void ActivateShield()
    {
        hasShield = true;

        if (_shieldInst == null)
        {
            _shieldInst = Instantiate(shieldVisualPrefab);
            _shieldInst.transform.SetParent(transform);
            _shieldInst.transform.localPosition = Vector3.zero;
            Destroy_Eventer destroyer = _shieldInst.GetComponent<Destroy_Eventer>();
            destroyer._onDestroy.AddListener(DeactivateShield);
        }

        // 이미 돌고 있는 타이머가 있다면 초기화 (시간 연장)
        if (shieldTimerCoroutine != null)
        {
            StopCoroutine(shieldTimerCoroutine);
        }

        shieldTimerCoroutine = StartCoroutine(ShieldTimer());
        Debug.Log("보호막 활성화! 5초 동안 적의 총알을 방어합니다.");
    }

    private IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(shieldDuration);
        DeactivateShield();
    }

    public void DeactivateShield()
    {
        hasShield = false;

        if (_shieldInst != null)
        {
            Destroy(_shieldInst);
        }

        if (shieldTimerCoroutine != null)
        {
            StopCoroutine(shieldTimerCoroutine);
            shieldTimerCoroutine = null;
        }
        Debug.Log("보호막이 소멸했습니다. 이제 데미지를 받습니다!");
    }

    // 플레이어 본체가 데미지를 입는 함수
    public void TakeDamage(int damage)
    {
        // 코드가 안전하게 이중 잠금 역할을 합니다. 
        // 보호막이 켜져 있다면 본체는 절대 데미지를 받지 않습니다.
        if (hasShield) return;

        // 체력 감소 로직 (예시)
        Debug.Log($"플레이어가 {damage}의 데미지를 입었습니다.");
    }
}