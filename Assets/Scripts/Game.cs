using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
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

    public TextMeshProUGUI tmpCur;
    public TextMeshProUGUI tmpFin;
    public TextMeshProUGUI tmpBest;

    public int score;//��ǰ����-�ֶ�
    public int Score//��ǰ����-����
    {
        get { return score; }
        //���·���ʱ�Զ�����UI
        set { score = value; tmpCur.text = score.ToString(); tmpFin.text = score.ToString(); }
    }   

    public PipelineManager pipelineManager;
    public UnitManager unitManager;

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
        player.OnScore = Player_OnScore;//���¼�
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �����ʼ��Ϸ��ť
    /// </summary>
    public void StartGame()
    {
        Status = GAME_STATUS.InGame;
        //Debug.LogFormat("Status: {0}", Status);

        pipelineManager.StartRun();
        unitManager.Begin();
        player.Fly();

        groundAmi.speed = 1;
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
    private void Player_OnDeath()
    {
        Status = GAME_STATUS.GameOver;
        pipelineManager.Stop();
        unitManager.Stop();
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
        pipelineManager.Restart();
        player.Restart();
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
