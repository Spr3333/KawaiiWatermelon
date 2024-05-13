using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FruitManager))]
public class FruitUIManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Image nextFruitImage;
    private FruitManager fruitManager;
    // Start is called before the first frame update
    void Awake()
    {
        fruitManager = GetComponent<FruitManager>();
        FruitManager.OnUpdateNextFruitUI += UpdateNextFruitUI;
    }

    private void OnDestroy()
    {
        FruitManager.OnUpdateNextFruitUI -= UpdateNextFruitUI;

    }

    // Update is called once per frame
    void Update()
    {
        //nextFruitName.text = fruitManager.GetNextFruitName();
    }

    void UpdateNextFruitUI()
    {
        nextFruitImage.sprite = fruitManager.GetNextFruitImage();
    }
}
