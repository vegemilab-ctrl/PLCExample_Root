using UnityEngine;
using UnityEngine.Events;
using ActUtlType64Lib;

public class MXComponent : MonoBehaviour
{
    //MX Component 라이브러리 인터페이스
    private ActUtlType64 utltype;

    //읽어올 디바이스 주소0
    public string[] address0;
    public UnityEvent<short> onChangeValue0;
    //읽어올 디바이스 주소1
    public string[] address1;
    public UnityEvent<short> onChangeValue1;
    //읽어올 디바이스 주소2
    public string[] address2;
    public UnityEvent<short> onChangeValue2;

    void Start()
    {
        //시작하면 인터페이스를 생성해서
        utltype = new ActUtlType64();
        //PLC와 통신을 연결.
        int ret = utltype.Open();
        if(ret != 0)
        {
            Debug.Log($"0x{ret:x}");
        }
    }

    //어플리케이션이 종료될 때 호출되는 함수
    private void OnApplicationQuit()
    {
        if(utltype != null)
        {
            //PLC와 통신 연결 해제.
            int ret = utltype.Close();
            if(ret != 0)
            {
                Debug.Log($"0x{ret:x}");
            }
        }
    }
}
