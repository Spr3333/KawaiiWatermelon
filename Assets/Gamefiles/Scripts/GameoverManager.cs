using UnityEngine;

public class GameoverManager : MonoBehaviour
{

    [Header("Refrences")]
    [SerializeField] private GameObject deadLine;
    [SerializeField] private Transform fruitParent;


    [Header("Settings")]
    [SerializeField] private float gameOverThreshold;
    private float timer;
    private bool timerOn;
    private bool isGameOver;

    void Update()
    {
        if (!isGameOver)
            ManageGameOver();


    }

    private void ManageGameOver()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;

            if (!IsFruitAboveLine())
                StopTimer();


            if (timer > gameOverThreshold)
                GameOver();
        }
        else
        {
            if (IsFruitAboveLine())
                StartTimer();
        }
    }

    private bool IsFruitAboveLine()
    {
        for (int i = 0; i < fruitParent.childCount; i++)
        {
            Fruit fruit = fruitParent.GetChild(i).GetComponent<Fruit>();

            if (!fruit.HasCollided())
                continue;

            if (IsFruitAboveLine(fruitParent.GetChild(i)))
                return true;
        }
        return false;
    }

    private bool IsFruitAboveLine(Transform fruit)
    {
        float fruitHeight = fruit.localScale.y / 2;
        if (fruit.position.y + fruitHeight > deadLine.transform.position.y)
            return true;

        return false;
    }

    private void StartTimer()
    {
        timer = 0;
        timerOn = true;
    }

    private void StopTimer()
    {
        timerOn = false;
    }

    private void GameOver()
    {
        GameManager.Instance.SetGameoverState();
        isGameOver = true;
    }
}
