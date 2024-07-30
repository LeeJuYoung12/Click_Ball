using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    static GameManager Instance_s;
    public static GameManager Instance { get { Init(); return Instance_s; } }

    static InputManager _Input = new InputManager();
    static ResourceManager _Resource = new ResourceManager();
    static BallManager _Ball = new BallManager();
        
    public static InputManager Input_Manager { get { return _Input; } }
    public static BallManager Ball_Manager { get { return _Ball; } }
    public static ResourceManager Resource_Manager { get { return _Resource; } }
        
    
    public static bool isStart, isLevel1, isLevel2, isLevel3;
    public static int score;
    private TMP_Text scoreText, timerText, maxScoreText;

    private GameObject ending;

    // Start is called before the first frame update
    void Start()
    {
        SetResolution();
        Init();
        //Ball_Manager.GetBall();
        //Time_Manager.CountTime();
    }

    // Update is called once per frame
    void Update()
    {
        _Input.OnUpdate();

        if (isStart)
        {
            Score();
            CountTime();
            Ball_Manager.GetBall();
            
            isStart = false;
        }
        else if (isLevel2)
        {
            Ball_Manager.MoveBall();
        }
        else if (isLevel3)
        {

            Ball_Manager.MoveBall();            
        }


    }
    public void SetResolution()
    {
        int setWidth = 1920; // 화면 너비
        int setHeight = 1080; // 화면 높이

        //해상도를 설정값에 따라 변경
        //3번째 파라미터는 풀스크린 모드를 설정 > true : 풀스크린, false : 창모드
        Screen.SetResolution(setWidth, setHeight, true);
    }
    
    void LateUpdate()
    {
        if(scoreText != null) scoreText.text = score.ToString();
    }
    
    //싱글톤 생성
    static void Init()
    {
        if (Instance_s == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if (go == null)
            {
                go = new GameObject { name = "GameManager" };
                go.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(go);
            Instance_s = go.GetComponent<GameManager>();
        }
    }

    public void Score()
    {        
        GameObject canvas = GameObject.Find("Canvas");
        scoreText = GameManager.Resource_Manager.Instantiate("ScoreText", canvas.transform).GetComponent<TMP_Text>();
        
    }

    public void CountTime()
    {
        GameObject canvas = GameObject.Find("Canvas");
        timerText = GameManager.Resource_Manager.Instantiate("TimeText", canvas.transform).GetComponent<TMP_Text>();

        StartCoroutine(Timer(20.0f, timerText));
    }

    IEnumerator Timer(float time, TMP_Text timerText)
    {
        //시작시간보여주기
        timerText.text = ((int)time).ToString();
        yield return new WaitForSeconds(1f);

        while (time > 1)
        {
            time -= Time.deltaTime;
            timerText.text = ((int)time).ToString();

            yield return null;

        }
        
        GameOver();       
    }


    public void GameOver()
    {
        ending = GameObject.Find("Ending");
        ending.transform.GetChild(0).gameObject.SetActive(true);

        if (maxScoreText==null)
            maxScoreText = GameObject.Find("MaxScore").GetComponent<TMP_Text>();

        if(score>=PlayerPrefs.GetInt("MaxScore", 0)) 
            PlayerPrefs.SetInt("MaxScore", score);

        maxScoreText.text = PlayerPrefs.GetInt("MaxScore", 0).ToString();

        isLevel1 = false;
        isLevel2 = false;
        isLevel3 = false;

        Destroy(scoreText);
        Destroy(timerText);
        Destroy(GameManager.Ball_Manager.BallGroup);
    }

    //설정값 초기화
    public void Reset()
    {

        score = 0;
    
        StartCoroutine("ResetCoroutine");
    }

    IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        SceneManager.LoadScene("TitleScene");
    }
}
