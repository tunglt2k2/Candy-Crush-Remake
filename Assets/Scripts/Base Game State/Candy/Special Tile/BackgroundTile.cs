using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    public int hitPoints;
    
    private GoalManager goalManager;

    public virtual void Start()
    {
        goalManager = FindObjectOfType<GoalManager>();
    }
    public void UpdateTile()
    {
        if(hitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void UpdateGoal()
    {
        if (goalManager != null)
        {
            goalManager.CompareGoal(this.gameObject.tag);
            goalManager.UpdateGoals();
        }
    }

    public virtual void TakeDamage(int damage)
    {
        hitPoints -= damage;
        UpdateGoal();
        UpdateTile();
    }

    

}
