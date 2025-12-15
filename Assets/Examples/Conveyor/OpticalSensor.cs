using UnityEditor;
using UnityEngine;

public class OpticalSensor : MonoBehaviour
{
    // === 새로 추가된 부분: 실린더 컨트롤러 참조 ===
    public CylinderController_Ex targetCylinder;
    // ============================================

    public enum DetectAxis
    {
        None = 0,
        XAxis,
        XAxisMinus,
        YAxis,
        YAxisMinus,
        ZAxis,
        ZAxisMinus,
        Max
    }

    // 검출할 게임오브젝트의 레이어들
    public LayerMask detectLayer;
    // 검출 가능 거리
    public float detectableDistance = 0.3f;
    // 센서 방향
    public DetectAxis axis = DetectAxis.ZAxis;
    // 검출할 수 있는 종류
    public string detectableTag = string.Empty;

    // 검출 여부
    private bool _hasDetected = false;
    // 검출 위치
    private Vector3 _detectedPoint;

    private void Update()
    {
        // 1. 센서 방향 설정 (기존 코드와 동일)
        Vector3 direction = axis switch
        {
            DetectAxis.XAxis => transform.right,
            DetectAxis.XAxisMinus => -transform.right,
            DetectAxis.YAxis => transform.up,
            DetectAxis.YAxisMinus => -transform.up,
            DetectAxis.ZAxis => transform.forward,
            DetectAxis.ZAxisMinus => -transform.forward,
            _ => Vector3.zero
        };

        if (direction == Vector3.zero)
            return;

        // 2. Raycast를 이용한 물체 감지 (기존 코드와 동일)
        Ray ray = new Ray(transform.position, direction);
        bool isHit = Physics.Raycast(ray, out RaycastHit hit, detectableDistance, detectLayer);

        bool currentDetected = false;

        if (isHit)
        {
            // 태그 검사 (기존 코드와 동일)
            if (!string.IsNullOrEmpty(detectableTag) && hit.transform.gameObject.tag != detectableTag)
            {
                currentDetected = false;
            }
            else
            {
                currentDetected = true;
                _detectedPoint = hit.point;
            }
        }
        else
        {
            currentDetected = false;
        }

        if (targetCylinder != null)
        {
            if (currentDetected)
            {
                // 물체가 감지되었을 때: 실린더 로드를 전진시킵니다.
                targetCylinder.OnForward();
            }
            else
            {
                // 물체가 감지되지 않았을 때: 실린더 로드를 후진시킵니다. (추가된 부분)
                targetCylinder.OnBackward();
            }
        }

        _hasDetected = currentDetected; // Gizmos를 위한 상태 업데이트
    }

    // OnDrawGizmos 함수는 기존과 동일하게 유지됩니다.
    private void OnDrawGizmos()
    {
        // ... (생략)
        // Gizmos 설정 코드는 변경 없이 그대로 사용하시면 됩니다.
        // ...
    }
}