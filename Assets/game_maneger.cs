using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_maneger : MonoBehaviour
{
    public int score = 0;
    public int life = 4;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI LifeText;
    public GameObject enemyPrefab;
    public GameObject winPanel;
    public GameObject lostPanel;


    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        if (life <= 0)
            YouLost();

        if (score >= 150)
            YouWin();
    }

    public void LostOneLife()
    {
        life--;
        LifeText.text = "Life : " + life;


    }

    public void ADDOneLife()
    {
        life++;
        LifeText.text = "Life : " + life;


    }
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        if (score >= 150)
            YouWin();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score : " + score.ToString();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void YouLost()
    {
        lostPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void YouWin()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void AddGostKids(Transform gostTransform)
    {
        Vector3 startPosition = gostTransform.position;
       GameObject enemy =  Instantiate(enemyPrefab, startPosition + new Vector3(0, -1.5f, 0), Quaternion.identity);
        enemy.GetComponent<EnemyScript>().gameManager = this;

        GameObject enemy1 = Instantiate(enemyPrefab, startPosition + new Vector3(0, 1.5f, 0), Quaternion.identity);

        enemy1.GetComponent<EnemyScript>().gameManager = this;

    }
}
