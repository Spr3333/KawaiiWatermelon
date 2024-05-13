using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    [Header("Actions")]
    public static Action<Fruit, Fruit> OnFruitCollision;
    [SerializeField] private FruitType fruitType; 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem mergeParticle;


    [Header("Settings")]
    private bool hasCollided;
    private bool canBeMerged;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("AllowMerge", .5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnablePhysics()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
    }

    public void MoveTo(Vector2 targetPos)
    {
        transform.position = targetPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ManageCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ManageCollision(collision);
    }

    private void ManageCollision(Collision2D collision)
    {
        hasCollided = true;

        if (!canBeMerged)
            return;

        if (collision.collider.TryGetComponent(out Fruit otherFruit))
        {

            if (otherFruit.GetFruitType() != fruitType)
                return;

            if (!otherFruit.CanBeMerged())
                return;

            OnFruitCollision?.Invoke(this, otherFruit);
        }
    }

    public FruitType GetFruitType()
    {
        return fruitType;
    }

    public Sprite GetFruitSprite()
    {
        return spriteRenderer.sprite;
    }

    public bool HasCollided()
    {
        return hasCollided;
    }

    public bool CanBeMerged()
    {
        return canBeMerged;
    }

    private void AllowMerge()
    {
        canBeMerged = true;
    }

    public void Die()
    {
        if (mergeParticle != null)
        {
            mergeParticle.transform.SetParent(null);
            mergeParticle.Play();
        }

        Destroy(gameObject);
    }
}
