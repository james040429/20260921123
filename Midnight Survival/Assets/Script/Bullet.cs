using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // 총알 속도
    public float lifeTime = 3f; // 생존 시간 (3초 뒤 자동 파괴)

    void Start()
    {
        // 일정 시간이 지나면 총알 오브젝트 파괴
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 매 프레임마다 전방(Z축)으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}