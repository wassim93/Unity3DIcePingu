using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { set; get; }

    private bool gameIsStarted = false;
    public bool IsDead { set; get; }

    public Animator gameCanvasanim;
    public Animator powerUpAnim;
    public Text scoreTxt, coinTxt, speedTxt, TapToStart, highScoreTxt, LifeScoreTxt;
    public Animator deathMenuAnim;
    public Text finalScoreTxt, finalCoinTxt;
    public Image logoImg;
    public Image timerPowerup { set; get; }

    private PlayerMovement movment;
    public int coinScore { set; get; }
    public float lifeScore { set; get; }
    public float score { set; get; }
    public float speedScore;
    private GameObject beforeStart;
    private AudioSource music;
    public PlayerMovement playerMovement { set; get; }
    public bool activePowerUp = false;
    public GameObject gameMenu;
    public GameObject achievmentMenu;



    private void Awake()
    {
        Instance = this;
        speedScore = 1;
        UpdateScores();
        movment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        
        beforeStart = GameObject.FindGameObjectWithTag("BeforeStart");
        highScoreTxt.text = PlayerPrefs.GetInt("highScore").ToString();
        gameMenu.SetActive(true);
        PlayGamesPlatform.Activate();
        LogIn();
        // advertisment 

        Advertisement.Initialize("1800265");
        // OnConnectionResponse(PlayGamesPlatform.Instance.localUser.authenticated);




    }

    public void UpdateScores()
    {
        scoreTxt.text = score.ToString("0");
        coinTxt.text = coinScore.ToString("0");
        speedTxt.text = speedScore.ToString("0.0");
        LifeScoreTxt.text = lifeScore.ToString("0");
    }

    public void UpdateSpeed(float speedAmount)
    {

        speedScore = speedAmount;
        UpdateScores();
    }

   
    // unity adds methods
    public void RequestRevive()
    {
        ShowOptions so = new ShowOptions();
        so.resultCallback = OnRestartButton;
        Advertisement.Show("rewardedVideo",so);

    }

    public void Revive(ShowResult sr) {

        if (sr == ShowResult.Finished)
        {
            IsDead = false;
            deathMenuAnim.SetTrigger("Alive");
            gameCanvasanim.SetTrigger("GameMenuShow");
           


        }
        else {

            //Awake();
        }

    }
  


    public void GetCoin()
    {
        coinScore ++;

        switch (coinScore)
        {
            case 2:
                UnlockAchievment(AchievmentLeader.collect_2coins);
                break;
            case 10:
                UnlockAchievment(AchievmentLeader.collect_10coins);
                break;
            case 20:
                UnlockAchievment(AchievmentLeader.collect_20coins);
                break;
                case 30:
                UnlockAchievment(AchievmentLeader.collect_30coins);
                break;
            case 50:
                UnlockAchievment(AchievmentLeader.collect_50coins);
                break;
            case 150:
                UnlockAchievment(AchievmentLeader.collect_150coins);
                break;
            default:
                break;
        }
        UpdateScores();
    }

    public void GetLife()
    {
        lifeScore += 1;
        UpdateScores();
    }
    // Use this for initialization
    void Start()
    {

        music = GetComponent<AudioSource>();
        //backMusic.Play ();


    }

    // Update is called once per frame
    void Update()
    {
        /*if (MobileInputs.Instance.Tap && !gameIsStarted) {
			gameIsStarted = true;
			movment.StartRunning ();
            FindObjectOfType<CameraMovement>().IsMoving = true;
            TapToStart.enabled = false;
            logoImg.enabled = false;
            highScoreTxt.enabled = false;
            achievmentMenu.SetActive(false);
            gameCanvasanim.SetTrigger("GameMenuShow");
            Destroy(beforeStart);
            music.Play();
           

		}*/
        if (gameIsStarted && !IsDead)
        {
            score += Time.time * 2;
            UpdateScores();


        }



    }


    public void onTap()
    {
        gameIsStarted = true;
        movment.StartRunning();
        FindObjectOfType<CameraMovement>().IsMoving = true;
        TapToStart.enabled = false;
        logoImg.enabled = false;
        highScoreTxt.enabled = false;
        achievmentMenu.SetActive(false);
        gameCanvasanim.SetTrigger("GameMenuShow");
        Destroy(beforeStart);
        music.Play();

    }

    public void OnRestartButton(ShowResult sr)
    {

        if (sr == ShowResult.Finished)
        {
            IsDead = false;
            deathMenuAnim.SetTrigger("Alive");
            gameCanvasanim.SetTrigger("GameMenuShow");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");




        }
    }


    public void OnDeath()
    {
        IsDead = true;
        finalScoreTxt.text = score.ToString("0");
        finalCoinTxt.text = coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Dead");
        gameCanvasanim.SetTrigger("GameMenuHide");
        music.Stop();

        reportScore((int)score);
        if (score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", (int)score);

        }
    }


    // google play 
    public void LogIn()
    {
       
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log("Login Sucess");
                    UnlockAchievment(AchievmentLeader.freshStart);

                }
                else
                {
                    Debug.LogWarning("Login failed");
                }
            });
        
    }





    private void OnConnectionResponse(bool authenticated)
    {
        if (authenticated)
        {
            UnlockAchievment(AchievmentLeader.freshStart);

            //connectedMenu.SetActive(true);
            //disconnectedMenu.SetActive(false);
            Debug.Log("connected");

        }
        else
        {
            //connectedMenu.SetActive(false);
            //disconnectedMenu.SetActive(true);

        }
    }


    public void OnLeaderBoardClick() {

        Social.ShowLeaderboardUI();
    }


    public void OnAchievementClick()
    {


        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();

        }
    }

    public void UnlockAchievment(string id)
    {

        Social.ReportProgress(id, 100.0f, (bool success) =>
        {
            Debug.Log("achivement unlock ");
        });
    }
    public void reportScore(int score)
    {
        Social.ReportScore(score, AchievmentLeader.highScore, (bool success) =>
        {
            Debug.Log("score high ");

        });

    }
   

	



}
