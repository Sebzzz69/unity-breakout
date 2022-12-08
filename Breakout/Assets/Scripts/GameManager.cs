using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.IK;

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }

    public int level = 1;

    public int score;

    public int lives = 3;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;

        Loadlevel(1);
    }

    private void Loadlevel(int level)
    {
        this.level = level;

        SceneManager.LoadScene($"Level{level}");
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
    }

    private void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");

        NewGame();
    }

    public void Miss()
    {
        this.lives--;

        if (lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }
    public void Hit(Brick brick)
    {
        if (brick.health < 1)
        {
            this.score += brick.destroyPoints;
            return;
        }
        this.score += brick.hitPoints;
    }

}
