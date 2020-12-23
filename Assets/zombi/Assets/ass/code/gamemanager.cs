using UnityEngine;
using UnityEngine.UI; //for UI
using UnityEngine.SceneManagement; //for load the scene

public class gamemanager : MonoBehaviour
{
    public GameObject[] zombies;
    public bool isRising = false;
    public bool isFalling = false;
    public int riseSpeed = 1;
    public int fallSpeed = 1;
    private int activeZombieIndex = 0;

    private int ZombiesSmashed;

    private int LiveRemaining;
    private bool gameOver;

    public Image life01;
    public Image life02;
    public Image life03;
    public Text scoreText;
    public Button gameOverButton;
    public int scoreThreshold = 5;

    private Vector2 startPosition;
    void Start()
    {
        gameOver = false;
        ZombiesSmashed = 0;
        scoreText.text = ZombiesSmashed.ToString();
        LiveRemaining = 3;
        pickNewZombie();
    }

    void Update()
    {
        if (!gameOver)

            if (isRising)
            {
                // zombies are rising


                if (zombies[activeZombieIndex].transform.position.y - startPosition.y >= 3f)  // it is increese 3 tiles up & then automatically stop
                {
                    isRising = false;
                    isFalling = true;
                }
                else
                {
                    zombies[activeZombieIndex].transform.Translate(Vector2.up * Time.deltaTime * riseSpeed); // it indicate the speed pf zombie rising
                }
            }
            else if (isFalling)
            {
                // zombies are falling down
                if (zombies[activeZombieIndex].transform.position.y - startPosition.y <= 0f)
                {
                    //stop zombie
                    isRising = false;
                    isFalling = false;

                    LiveRemaining--;
                    UpdateLifeUI();


                }
                else
                {
                    zombies[activeZombieIndex].transform.Translate(Vector2.down * Time.deltaTime * riseSpeed); // it indicate the speed pf zombie falling
                }

            }
            else
            {
                zombies[activeZombieIndex].transform.position = startPosition;
                pickNewZombie();

            }
    }

    private void UpdateLifeUI()
    {
        if (LiveRemaining == 3) //for heart blink,
        {
            life01.gameObject.SetActive(true);
            life02.gameObject.SetActive(true);
            life03.gameObject.SetActive(true);
        }
        if (LiveRemaining == 2)
        {
            life01.gameObject.SetActive(true);
            life02.gameObject.SetActive(true);
            life03.gameObject.SetActive(false);
        }
        if (LiveRemaining == 1)
        {
            life01.gameObject.SetActive(true);
            life02.gameObject.SetActive(false);
            life03.gameObject.SetActive(false);
        }
        if (LiveRemaining == 0)
        {
            life01.gameObject.SetActive(false);
            life02.gameObject.SetActive(false);
            life03.gameObject.SetActive(false);
            // game over
            gameOver = true;
            gameOverButton.gameObject.SetActive(true);
        }

    }


    private void pickNewZombie()
    {
        isRising = true;
        isFalling = false;
        activeZombieIndex = UnityEngine.Random.Range(0, zombies.Length);  //its select the random zombies from the array of zombies
        startPosition = zombies[activeZombieIndex].transform.position;   //increase the value of y axis

    }
    public void KillZombie() //for kill the zombie function
    {
        scoreText.text = ZombiesSmashed.ToString();
        ZombiesSmashed++;
        IncreaseSpeed();
        Debug.Log(ZombiesSmashed);

        zombies[activeZombieIndex].transform.position = startPosition;
        pickNewZombie(); //randomly pick the zombies

    }

    private void IncreaseSpeed()
    {
        if(ZombiesSmashed >= scoreThreshold)
        {
            riseSpeed ++;
            scoreThreshold *= 2; //after the score increse 5 then automatically speed is increase 
        }
    }
    public void OnRestart()
    {
       // Debug.Log("time to restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // we can use index no in bracket
        // like this::  SceneManager.LoadScene(0); the number represent the scene index location
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(1); // it means that when we press main menu then it simply load the new scene
    }
                  
}
