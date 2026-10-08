using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // [최초 1회 실행] 현재 적 위치에서 가장 가까운 NavMesh 바닥을 찾습니다.
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 5.0f, NavMesh.AllAreas))
        {
            // 찾은 바닥 위치로 에이전트를 강제 이동(안착)시킵니다.
            agent.Warp(hit.position);
        }
        else
        {
            Debug.LogError($"{gameObject.name} 주변에 NavMesh 바닥을 찾을 수 없습니다! Bake 상태를 확인하세요.");
        }

        // 💡 추가 팁: 하이어라키(Hierarchy)에 있는 플레이어를 자동으로 찾도록 설정
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
    }

    void Update()
    {
        // 에이전트가 활성화되어 있고, 바닥(NavMesh) 위에 정상적으로 있을 때만 목적지 설정
        if (player != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
        }
    }
}