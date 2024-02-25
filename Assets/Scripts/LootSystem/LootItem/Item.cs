using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class Item : MonoBehaviour, ICollectible
{
    public static event Action OnCoinCollected;
    public int idItem;
    Rigidbody2D rb;
    bool hasTarget;
    Vector3 targetPosition;
    float moveSpeed = 5f;
    public void Collect()
    {
        if (idItem == 1)
        {
            Debug.Log("Collected Power");
            Destroy(gameObject);
            ExperienceManager.Instance.AddExperience(Random.Range(70, 130), 1);
            OnCoinCollected?.Invoke();
        }
        if (idItem == 2)
        {
            Debug.Log("CollectedSpeed");
            Destroy(gameObject);
            ExperienceManager.Instance.AddExperience(Random.Range(70, 130), 2);
            OnCoinCollected?.Invoke();
        }
        if (idItem == 3)
        {
            Debug.Log("CollectedRange");
            Destroy(gameObject);
            ExperienceManager.Instance.AddExperience(Random.Range(70, 130), 3);
            OnCoinCollected?.Invoke();
        }
        if (idItem == 4)
        {
            Debug.Log("CollectedRange");
            Destroy(gameObject);
            ExperienceManager.Instance.AddExperience(1, 4);
            OnCoinCollected?.Invoke();
        }

    }
    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
        }
    }
    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
