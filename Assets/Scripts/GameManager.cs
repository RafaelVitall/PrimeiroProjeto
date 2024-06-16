using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //DECLARAÇÕES
    public BallController ballController;
    public EnemyPaddleController enemyPaddleController;
    [Space(10)]

    public Transform playerPaddle; 
    public Transform enemyPaddle;
    [Space(10)]

    public int playerScore = 0;
    public int enemyScore = 0;
    public Text textPointsPlayer;
    public Text textPointsEnemy;
    [Space(10)]

    public GameObject screenEndGame;
    public int winPoints;
    [Space(10)]

    public TextMeshProUGUI textEndGame;
    public Text placar;

    void Start() 
    { 
        ResetGame();
    } 

    public void ResetGame() 
    {    
        // Define as posições iniciais das raquetes
        screenEndGame.SetActive(false);
        playerPaddle.position = new Vector3(-7f, 0f, 0f);
        enemyPaddle.position = new Vector3(7f, 0f, 0f);

        ballController.ResetBall();

        playerScore = 0;
        enemyScore = 0;
        textPointsEnemy.text = enemyScore.ToString();
        textPointsPlayer.text = playerScore.ToString();
        enemyPaddleController.speed = enemyPaddleController.initialspeed;

    }

    public void ScorePlayer()
    {
        playerScore++;
        textPointsPlayer.text = playerScore.ToString();
        enemyPaddleController.speed *= enemyPaddleController.speedUp;
        CheckWin();
    }
    public void ScoreEnemy()
    {
        enemyScore++;
        textPointsEnemy.text = enemyScore.ToString();
        CheckWin();
    }

    public void CheckWin()
    {
        if (enemyScore >= winPoints || playerScore >= winPoints)
        {
            //ResetGame();
            EndGame();
        }
    }

    public void EndGame()
    {
        ballController.EndGameBall();
        screenEndGame.SetActive(true);
        string winner = SaveController.Instance.GetName(playerScore > enemyScore);
        textEndGame.text = "Vitória " + winner;
        SaveController.Instance.SaveWinner(winner);

        placar.text = enemyScore + " - " + playerScore.ToString();

        Invoke("LoadMenu", 2f);
    }
    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
