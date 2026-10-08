using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f; // 총알 속도
    public float lifeTime = 5f; // 생존 시간 (3초 뒤 자동 파괴)
    public float damage = 13f;

    void Start()
    {
        // 일정 시간이 지나면 총알 오브젝트 파괴
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 매 프레임마다 후방(Z축)으로 이동
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}