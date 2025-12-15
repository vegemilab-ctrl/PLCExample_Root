using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CylinderController_Ex : MonoBehaviour
{
    // === 속도 및 이동 관련 변수 수정/추가 ===
    // 전진/후진 시 적용할 이동 속도 (Inspector에서 조정 가능)
    public float movementSpeed = 0.5f;

    public Vector3 forward = Vector3.forward;
    public Rigidbody rod;
    // public float power = 100f; // 더 이상 힘(Force)은 사용하지 않으므로 제거하거나 주석 처리합니다.

    public float maxExtensionDistance = 0.5f;
    private readonly float minExtensionDistance = 0.0f;

    private Vector3 initialPosition;
    // =====================================

    private void Awake()
    {
        rod = GetComponent<Rigidbody>();
        rod.useGravity = false;
        rod.constraints = RigidbodyConstraints.FreezeRotation;

        initialPosition = rod.position;
    }

    // FixedUpdate는 Rigidbody 물리 계산에 더 적합합니다.
    // 하지만 OpticalSensor에서 Update()를 사용하므로, 여기도 Update()를 사용하거나
    // 호출하는 쪽을 FixedUpdate()로 바꾸는 것이 좋습니다. (여기서는 기존 로직 유지)

    public void OnForward()
    {
        Vector3 worldForward = transform.TransformDirection(forward).normalized;

        // 목표 위치: 초기 위치에서 최대 전진 거리를 더한 위치
        Vector3 targetPosition = initialPosition + worldForward * maxExtensionDistance;

        // 현재 위치에서 목표 위치로 movementSpeed만큼 천천히 이동합니다.
        // Time.deltaTime을 곱하여 프레임 속도에 관계없이 일정한 속도를 유지합니다.
        Vector3 newPosition = Vector3.MoveTowards(rod.position, targetPosition, movementSpeed * Time.deltaTime);

        // 초기 위치로부터의 거리를 다시 계산하여 50cm를 초과하지 않도록 제한합니다.
        float currentExtension = Vector3.Dot(newPosition - initialPosition, worldForward);

        if (currentExtension <= maxExtensionDistance)
        {
            // Rigidbody.MovePosition을 사용하여 물리적으로 안전하게 위치를 이동합니다.
            rod.MovePosition(newPosition);
        }
    }

    public void OnBackward()
    {
        Vector3 worldForward = transform.TransformDirection(forward).normalized;

        // 목표 위치: 초기 위치 (initialPosition)
        Vector3 targetPosition = initialPosition;

        // 현재 위치에서 목표 위치(initialPosition)로 movementSpeed만큼 이동합니다.
        Vector3 newPosition = Vector3.MoveTowards(rod.position, targetPosition, movementSpeed * Time.deltaTime);

        // 초기 위치로부터의 거리를 다시 계산하여 0cm 미만으로 후진하지 않도록 제한합니다.
        float currentExtension = Vector3.Dot(newPosition - initialPosition, worldForward);

        if (currentExtension >= minExtensionDistance)
        {
            // Rigidbody.MovePosition을 사용하여 위치를 이동합니다.
            rod.MovePosition(newPosition);
        }
        else
        {
            // 0cm를 지나쳤으면 초기 위치에 고정합니다. (안정성 보장)
            rod.MovePosition(initialPosition);
        }
    }
}