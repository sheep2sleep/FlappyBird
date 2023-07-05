using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoSingleton<Game>
{
    public enum GAME_STATUS
    {
        Ready,
        InGame,
        GameOver
    }

    private GAME_STATUS status;//��ǰ��Ϸ״̬-�ֶ�
    public GAME_STATUS Status//��ǰ��Ϸ״̬-����
    {
        get { return status; }
        set { status = value; UpdataUI(); }
    }

    public GameObject panelReady;
    public GameObject panelInGame;
    public GameObject panelGameOver;
    public Player player;
    public int currentLevelId = 1;

    public TextMeshProUGUI tmpCur;
    public TextMeshProUGUI tmpFin;
    public TextMeshProUGUI tmpBest;
    public TextMeshProUGUI uiLife;
    public TextMeshProUGUI uiLevelName;
    public Slider hpBar;

    public int score;//��ǰ����-�ֶ�
    public int Score//��ǰ����-����
    {
        get { return score; }
        //���·���ʱ�Զ�����UI
        set { score = value; tmpCur.text = score.ToString(); tmpFin.text = score.ToString(); }
    }   

    public Animator groundAmi;

    private void Awake()
    {
        //#if UNITY_ANDROID && !UNITY_EDITOR
        //Application.targetFrameRate = 60;
        //#else 
        //Application.targetFrameRate = -1;
        //#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        //�޸�֡��Ϊ60֡
        Application.targetFrameRate = 60;
        Status = GAME_STATUS.Ready;
        player.OnDeath += Player_OnDeath;//��ί�к���

        
    }

    // Update is called once per frame
    void Update()
    {
        //Ѫ����ֵ����
        if(Status == GAME_STATUS.InGame)
        {
            hpBar.value = Mathf.Lerp(hpBar.value, player.HP, 0.1f);
        }
        if(player != null){
            uiLife.text = player.life.ToString();
        }

    }

    /// <summary>
    /// �����ʼ��Ϸ��ť
    /// </summary>
    public void StartGame()
    {
        Status = GAME_STATUS.InGame;
        
        UnitManager.Instance.Clear();
        player.Fly();
        hpBar.value = player.HP;

        LoadLevel();

        groundAmi.speed = 1;
    }

    /// <summary>
    /// ���عؿ�
    /// </summary>
    private void LoadLevel()
    {
        LevelManager.Instance.LoadLevel(currentLevelId);
        uiLevelName.text = string.Format("LEVEL {0} {1}", LevelManager.Instance.level.LevelID.ToString(), LevelManager.Instance.level.Name.ToString());
        LevelManager.Instance.level.OnLevelEnd = OnLevelEnd;
    }

    /// <summary>
    /// ΪUnityEvent��ִ���¼�
    /// </summary>
    /// <param name="result"></param>
    private void OnLevelEnd(Level.LEVEL_RESULT result)
    {
        if (result == Level.LEVEL_RESULT.SUCCESS)
        {
            this.currentLevelId++;
            this.LoadLevel();
        }
        else
        {
            this.Status = GAME_STATUS.GameOver;
        }
    }

    /// <summary>
    /// ���ݵ�ǰstatus����UI
    /// </summary>
    public void UpdataUI()
    {
        panelReady.SetActive(Status == GAME_STATUS.Ready);
        panelInGame.SetActive(Status == GAME_STATUS.InGame);
        panelGameOver.SetActive(Status == GAME_STATUS.GameOver);
    }

    /// <summary>
    /// �������ί��ִ��
    /// </summary>
    private void Player_OnDeath(Unit sender)
    {
        if(player.life <= 0)
        {
            Status = GAME_STATUS.GameOver;
            UnitManager.Instance.Clear();
        }
        else
        {
            //�������������Ϊ0ֱ�Ӹ���
            player.Rebirth();
        }
        
        UnitManager.Instance.Clear();
        UpdateScore();
        groundAmi.speed = 0;
    }

    /// <summary>
    /// ��һ����¼�ִ��
    /// </summary>
    private void Player_OnScore(int score)
    {
        Score += score;
    }

    /// <summary>
    /// �ص�������
    /// </summary>
    public void Restart()
    {
        Status = GAME_STATUS.Ready;
        //pipelineManager.Restart();
        player.Init();
        hpBar.value = player.HP;
        Score = 0;
        groundAmi.speed = 1;
    }

    public void UpdateScore()
    {
        int bestScore = 0;
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
        }
        bestScore = Math.Max(bestScore, Score);
        PlayerPrefs.SetInt("BestScore", bestScore);
        tmpBest.text = bestScore.ToString();
    }
}
