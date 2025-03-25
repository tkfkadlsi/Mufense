using UnityEngine;

public abstract class BaseInit : MonoBehaviour
{
    private bool _initStart = false;    //init을 한 적이 있는지 저장하는 변수
    private bool _releaseComplete = false;  //release를 한 적이 있는지 저장하는 변수

    /// <summary>
    /// Awake에서 Init()실행.
    /// 
    /// 모든 Init()함수는
    /// 
    /// if(base.Init() == false)
    ///     return false;
    ///     
    /// 코드
    /// 
    /// return true;
    /// 
    /// 의 형식으로 사용해야 한다. 부모부터 Init이 시작됨.
    /// </summary>
    private void Awake()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_initStart == false) //만약 한번도 Init을 실행한 적이 없으면
        {
            _initStart = true;  //변수에 저장하기
            Debug.Log($"[사람이냐] : {gameObject.name} Init");  //로그 띄우기
        }

        return _initStart;  //true 반환하기
    }

    /// <summary>
    /// OnEnable에서 Setting()실행.
    /// 
    /// 모든 Setting()함수는
    /// 
    /// base.Setting()
    /// 
    /// 코드
    /// 
    /// 의 형식으로 사용해야 한다. 부모부터 Setting이 시작됨.
    /// </summary>
    private void OnEnable()
    {
        Setting();
    }

    protected virtual void Setting()
    {
        Debug.Log($"[사람이냐] : {gameObject.name} Setting");
    }

    /// <summary>
    /// OnDisable에서 Release()실행.
    /// 
    /// 모든 Release()함수는
    /// 
    /// 코드
    /// base.Release()
    /// 
    /// 의 형식으로 사용해야 한다. 자식부터 Release가 시작됨.
    /// </summary>
    private void OnDisable()
    {
        Release();
    }

    protected virtual void Release()
    {
        Debug.Log($"[사람이냐] : {gameObject.name} Release");
    }
}
