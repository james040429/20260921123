using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    [Header("이동 속도 설정")]
    [SerializeField] private float forwardSpeed = 20.0f; // Z축 전진 속도

    [Header("웨이브(Sin) 설정")]
    [SerializeField] private float waveSpeed = 10.0f;     // 좌우로 움직이는 속도 (주기)
    [SerializeField] private float waveWidth = 5.0f;     // 좌우로 움직이는 폭 (진폭)
    [SerializeField] private bool useYAxisForWave = false; // 체크하면 좌우가 아닌 위아래(Y축)로 웨이브

    private float elapsedTime = 0f;

    void Update()
    {
        // 1. 시간 경과 기록
        elapsedTime += Time.deltaTime;

        // 2. 기본 Z축 전진 거리 계산
        float moveZ = -forwardSpeed * Time.deltaTime;

        // 3. Mathf.Sin을 이용한 웨이브(물결) 변화량 계산
        // 시간에 따라 -1 ~ 1 사이를 반복하는 값에 폭(waveWidth)을 곱합니다.
        float wave = Mathf.Sin(elapsedTime * waveSpeed) * waveWidth;

        // 4. 웨이브 방향 분기 (X축 좌우 혹은 Y축 위아래)
        if (!useYAxisForWave)
        {
            // X축으로 흔들리며 Z축으로 전진 (월드 좌표계 기준)
            transform.Translate(new Vector3(wave * Time.deltaTime * waveSpeed, 0, moveZ), Space.World);
        }
        else
        {
            // Y축으로 흔들리며 Z축으로 전진
            transform.Translate(new Vector3(0, wave * Time.deltaTime * waveSpeed, moveZ), Space.World);
        }
    }
}