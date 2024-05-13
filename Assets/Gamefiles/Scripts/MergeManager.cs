using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{


    [Header("Action")]
    public static Action<FruitType, Vector2> OnMergeProcess;
    Fruit lastSender;

    // Start is called before the first frame update
    void Awake()
    {
        Fruit.OnFruitCollision += FruitCollisionCallback;
    }

    private void OnDestroy()
    {
        Fruit.OnFruitCollision -= FruitCollisionCallback;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FruitCollisionCallback(Fruit sender, Fruit otherFruit)
    {
        if (lastSender != null)
            return;

        lastSender = sender;
        ProcessMerge(sender, otherFruit);
        Debug.Log("Fruit Collided" + sender.name);
    }

    private void ProcessMerge(Fruit sender, Fruit otherFruit)
    {
        FruitType mergeFruitType = sender.GetFruitType();
        mergeFruitType += 1;

        Vector2 spawnPos = (sender.transform.position + otherFruit.transform.position) / 2;

        sender.Die();
        otherFruit.Die();

        StartCoroutine(ResetLastSender());

        OnMergeProcess?.Invoke(mergeFruitType, spawnPos);

    }

    IEnumerator ResetLastSender()
    {
        yield return new WaitForEndOfFrame();
        lastSender = null;
    }
}
