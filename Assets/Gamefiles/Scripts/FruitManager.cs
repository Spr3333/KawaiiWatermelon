using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

public class FruitManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Fruit[] fruitPrefabs;
    [SerializeField] private Fruit[] fruitToSpawn;
    [SerializeField] private LineRenderer fruitDropLine;
    [SerializeField] private Transform fruitParent;
    [SerializeField] private float fruitSpawnYPos = 3.5f;
    [SerializeField] private bool enableGizmos;
    [SerializeField] private float spawnDelay;


    [Header("Settings")]
    private Fruit currentFruit;
    private bool canSpwan;
    private bool isControlling;
    private int nextFruitIndex;


    [Header("Action")]
    public static Action OnUpdateNextFruitUI;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        MergeManager.OnMergeProcess += MergeProcessCallBack;
    }

    private void OnDestroy()
    {
        MergeManager.OnMergeProcess -= MergeProcessCallBack;

    }

    void Start()
    {
        SetNextFruitIndex();
        HideLine();
        canSpwan = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGameState())
            return;

        if (canSpwan)
            ManagePlayerInput();
    }

    void ManagePlayerInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                MouseDownCallBack();
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (isControlling)
                    MouseDragCallBack();
                else
                    MouseDownCallBack();
            }
            else if (touch.phase == TouchPhase.Ended && isControlling)
                MouseUpCallBack();
        }
    }

    void MouseDownCallBack()
    {
        DisplayLine();
        GuideLineAtClickePos();
        SpawnFruit();

        isControlling = true;
    }

    void MouseDragCallBack()
    {
        GuideLineAtClickePos();
        currentFruit.MoveTo(GetSpawnPosition());
    }

    void MouseUpCallBack()
    {
        HideLine();

        if (currentFruit != null)
            currentFruit.EnablePhysics();

        isControlling = false;
        canSpwan = false;
        StartDelayTimer();
    }

    void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();
        spawnPosition.y = fruitSpawnYPos;
        currentFruit = Instantiate(fruitToSpawn[nextFruitIndex], spawnPosition, Quaternion.identity, fruitParent);
        currentFruit.name = "Fruit" + Random.Range(0, 1000);

        SetNextFruitIndex();
    }

    private void SetNextFruitIndex()
    {
        nextFruitIndex = Random.Range(0, fruitToSpawn.Length);
        OnUpdateNextFruitUI?.Invoke();
    }

    public string GetNextFruitName()
    {
        return fruitToSpawn[nextFruitIndex].name;
    }

    public Sprite GetNextFruitImage()
    {
        return fruitToSpawn[nextFruitIndex].GetFruitSprite();
    }

    Vector2 GetClickedWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 clickedWorldPos = GetClickedWorldPos();
        clickedWorldPos.y = fruitSpawnYPos;
        return clickedWorldPos;
    }

    void GuideLineAtClickePos()
    {
        fruitDropLine.SetPosition(0, GetSpawnPosition());
        fruitDropLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15);
    }

    void HideLine()
    {
        fruitDropLine.enabled = false;
    }

    void DisplayLine()
    {
        fruitDropLine.enabled = true;
    }

    void StartDelayTimer()
    {
        Invoke("StopDelayTimer", spawnDelay);
    }

    void StopDelayTimer()
    {
        canSpwan = true;
    }

    private void MergeProcessCallBack(FruitType type, Vector2 sapwnPos)
    {
        for (int i = 0; i < fruitPrefabs.Length; i++)
        {
            if (fruitPrefabs[i].GetFruitType() == type)
            {
                SpawnMergeFruit(fruitPrefabs[i], sapwnPos);
                break;
            }
        }
    }

    private void SpawnMergeFruit(Fruit fruit, Vector2 sapwnPos)
    {
        Fruit fruitInstance = Instantiate(fruit, sapwnPos, Quaternion.identity, fruitParent);
        fruitInstance.EnablePhysics();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizmos)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-50, fruitSpawnYPos, 0), new Vector3(50, fruitSpawnYPos, 0));
    }
#endif
}
