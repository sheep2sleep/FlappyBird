using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoSingleton<Game>
{
    public Player player;
    public int currentLevelId = 1;

    private GAME_STATUS status;//��ǰ��Ϸ״̬-�ֶ�
    public GAME_STATUS Status//��ǰ��Ϸ״̬-����
    {
        get { return status; }
        set { status = value; UIManager.Instance.UpdateUI(); }
    }

    public int score;//��ǰ����-�ֶ�
    public int Score//��ǰ����-����
    {
        get { return score; }
        //���·���ʱ�Զ�����UI
        set { score = value; UIManager.Instance.UpdateScore(score); }
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
       

    }

    /// <summary>
    /// �����ʼ��Ϸ��ť
    /// </summary>
    public void StartGame()
    {
        player.Init();
        Status = GAME_STATUS.InGame;        
        UnitManager.Instance.Clear();
        player.Fly();

        LoadLevel();

        groundAmi.speed = 1;
    }

    /// <summary>
    /// ���عؿ�
    /// </summary>
    private void LoadLevel()
    {
        LevelManager.Instance.LoadLevel(currentLevelId);
        
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
            if (currentLevelId < LevelManager.Instance.levels.Count)
            {
                this.currentLevelId++;
                this.LoadLevel();
            }
            else
            {
                Status = GAME_STATUS.GameOver;
            }
            
        }
        else
        {
            this.Status = GAME_STATUS.GameOver;
        }
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
        Score = 0;
        groundAmi.speed = 1;
    }


}
