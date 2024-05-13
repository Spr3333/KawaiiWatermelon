using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergePushEffect : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private float pushRadius;
    [SerializeField] private Vector2 minMaxPushMagnitude;
    [SerializeField] private float pushMagnitude;
    private Vector2 pushPosition;


    [Header("Settings")]
    [SerializeField] private bool enableGizoms;

    private void Awake()
    {
        MergeManager.OnMergeProcess += MergePushCallback;
        SettingsManager.OnMergeForceChange += MergeForceChangedCallback;
    }

    private void OnDestroy()
    {
        MergeManager.OnMergeProcess -= MergePushCallback;
        SettingsManager.OnMergeForceChange -= MergeForceChangedCallback;
    }    

    private void MergePushCallback(FruitType type, Vector2 spawnPos)
    {
        pushPosition = spawnPos;

        Collider2D[] collision =  Physics2D.OverlapCircleAll(pushPosition, pushRadius);

        foreach (var collider in collision)
        {
            if(collider.TryGetComponent(out Fruit fruit))
            {
                Vector2 force = ((Vector2)fruit.transform.position - spawnPos).normalized;
                force *= pushMagnitude;
                fruit.GetComponent<Rigidbody2D>().AddForce(force);
            }
        }
    }

    private void MergeForceChangedCallback(float value)
    {
        pushMagnitude =  Mathf.Lerp(minMaxPushMagnitude.x, minMaxPushMagnitude.y, value);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizoms)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pushPosition, pushRadius);
    }
#endif
}
