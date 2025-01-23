using UnityEngine;

public abstract class BaseInit : MonoBehaviour
{
    private bool _initStart = false;    //init�� �� ���� �ִ��� �����ϴ� ����
    private bool _releaseComplete = false;  //release�� �� ���� �ִ��� �����ϴ� ����


    /// <summary>
    /// Awake���� Init()����.
    /// 
    /// ��� Init()�Լ���
    /// 
    /// if(base.Init() == false)
    ///     return false;
    ///     
    /// �ڵ�
    /// 
    /// return true;
    /// 
    /// �� �������� ����ؾ� �Ѵ�. �θ���� Init�� ���۵�.
    /// </summary>
    private void Awake()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_initStart == false) //���� �ѹ��� Init�� ������ ���� ������
        {
            _initStart = true;  //������ �����ϱ�
            Debug.Log($"[����̳�] : {gameObject.name} Init");  //�α� ����
        }

        return _initStart;  //true ��ȯ�ϱ�
    }

    /// <summary>
    /// OnDestroy���� Release()����.
    /// 
    /// ��� Release()�Լ���
    /// 
    /// �ڵ�
    /// base.Release()
    /// 
    /// �� �������� ����ؾ� �Ѵ�. �ڽĺ��� Release�� ���۵�.
    /// </summary>
    private void OnDestroy()
    {
        Release();
    }

    protected virtual void Release()
    {
        Debug.Log($"[����̳�] : {gameObject.name} Release");
    }
}
