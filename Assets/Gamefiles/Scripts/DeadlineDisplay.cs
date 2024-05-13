using System.Collections;
using UnityEngine;

public class DeadlineDisplay : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject deadLine;
    [SerializeField] private Transform fruitParent;

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;

    }

    private void GameStateChangedCallback(GameState state)
    {
        if (state == GameState.Game)
            StartCheckingForNEarbyFruits();
        else
            StopCheckingForNearbyFruits();
    }

    private void StartCheckingForNEarbyFruits()
    {
        StartCoroutine(CheckForNearbyFruits());
    }

    private void StopCheckingForNearbyFruits()
    {
        StopCoroutine(CheckForNearbyFruits());
        HideDeadline();
    }

    IEnumerator CheckForNearbyFruits()
    {
        while (true)
        {
            bool foundNearbyFruit = false;
            for (int i = 0; i < fruitParent.childCount - 1; i++)
            {
                if (!fruitParent.GetChild(i).GetComponent<Fruit>().HasCollided())
                    continue;

                float distance = Mathf.Abs(fruitParent.GetChild(i).position.y - deadLine.transform.position.y);
                if (distance < 1)
                {
                    ShowDeadline();
                    foundNearbyFruit = true;
                    break;
                }
            }
            if (!foundNearbyFruit)
                HideDeadline();

            yield return new WaitForSeconds(1);
        }
    }

    private void ShowDeadline() => deadLine.SetActive(true);

    private void HideDeadline() => deadLine.SetActive(false);
}
