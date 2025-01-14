public class Managers : BaseInit
{
    private static Managers instance;   //Managers Ŭ������ ������ �ν��Ͻ�

    public static Managers Instance     //�� �����ϴ� public ������Ƽ
    {
        get
        {
            if (instance == null)   //���� instance�� null�̸�
                instance = FindAnyObjectByType<Managers>(); //���� ������ �ϳ� ã�ƿ���.

            return instance;    //instance ��ȯ
        }
    }

    #region �Ŵ��� ��ũ��Ʈ
    private GameManager _gameManager;
    private PoolManager _poolManager;
    #endregion

    #region �Ŵ��� ���ٿ� ������Ƽ
    public GameManager Game { get { return _gameManager; } }
    public PoolManager Pool { get { return _poolManager; } }
    #endregion

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        _gameManager = GetComponent<GameManager>();
        _poolManager = GetComponent<PoolManager>();

        return true;
    }

}
