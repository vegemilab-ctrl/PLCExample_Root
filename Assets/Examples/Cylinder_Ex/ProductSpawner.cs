using UnityEngine;
using System.Collections; // IEnumerator를 사용하기 위해 추가해야 합니다.

public class ProductSpawner : MonoBehaviour
{
    // 생산할 제품 오브젝트 (Unity Inspector에서 할당)
    public GameObject product;

    // 생산 위치 (Unity Inspector에서 할당)
    public Transform spawnPosition;

    // 생성 간격 (1초)
    private readonly float spawnInterval = 2.0f;

    // 게임 시작 시 한 번 호출됩니다.
    void Start()
    {
        // 큐브 생성 루틴을 시작합니다.
        StartCoroutine(SpawnProductRoutine());
    }

    // 인풋에 연결된 Product액션은 자동 생성 기능으로 대체되므로 제거하거나 주석 처리했습니다.
    // public void OnProduct()
    // {
    //     Instantiate(product, spawnPosition.position, spawnPosition.rotation);
    // }

    /// <summary>
    /// 1초마다 큐브를 생성하는 코루틴입니다.
    /// </summary>
    IEnumerator SpawnProductRoutine()
    {
        // 무한 루프를 돌면서 큐브를 생성합니다.
        while (true)
        {
            // 큐브 생성 함수 호출
            SpawnProduct();

            // 지정된 시간(1.0초)만큼 기다립니다.
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /// <summary>
    /// 실제 큐브를 생성하는 함수
    /// </summary>
    void SpawnProduct()
    {
        // 제품(product)을 지정된 위치(spawnPosition)에 생성합니다.
        Instantiate(product, spawnPosition.position, spawnPosition.rotation);
    }
}