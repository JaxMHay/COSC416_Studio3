using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;


    //increments score when coin collected
    public void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
