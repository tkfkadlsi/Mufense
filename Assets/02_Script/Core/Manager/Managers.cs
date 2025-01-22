
using UnityEngine;

public class Managers : BaseInit
{
    private static Managers instance;   //Managers 클래스의 유일한 인스턴스

    public static Managers Instance     //에 접근하는 public 프로퍼티
    {
        get
        {
            if (instance == null)   //만약 instance가 null이면
                instance = FindAnyObjectByType<Managers>(); //현재 씬에서 하나 찾아오기.

            return instance;    //instance 반환
        }
    }

    #region 매니저 스크립트
    private GameManager _gameManager;
    private PoolManager _poolManager;
    private UIManager _uiManager;
    #endregion

    #region 매니저 접근용 프로퍼티
    public GameManager Game { get { return _gameManager; } }
    public PoolManager Pool { get { return _poolManager; } }
    public UIManager UI { get { return _uiManager; } }
    #endregion

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        Application.targetFrameRate = -1;

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _gameManager = GetComponent<GameManager>();
        _poolManager = GetComponent<PoolManager>();
        _uiManager = GetComponent<UIManager>();

        return true;
    }

}
