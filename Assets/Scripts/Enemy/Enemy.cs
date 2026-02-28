using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class Enemy : MonoBehaviour
{
    [Inject] private GameManager gameManager;

    private List<Enemy> enemyComponents = new List<Enemy>();

    protected virtual void Awake()
    {
        gameManager.OnPaused += Deactivate;
        gameManager.OnResumed += Activate;

        Enemy[] allComponents = GetComponents<Enemy>();

        foreach (Enemy component in allComponents)
        {
            if (component != this)
            {
                enemyComponents.Add(component);
            }
        }
    }

    protected void Deactivate()
    {
        foreach (Enemy component in enemyComponents)
        {
            if (component != null)
                component.enabled = false;
        }
    }

    protected void Activate()
    {
        foreach (Enemy component in enemyComponents)
        {
            if (component != null)
                component.enabled = true;
        }
    }
    private void OnDisable()
    {
        gameManager.OnPaused -= Deactivate;
        gameManager.OnResumed -= Activate;
    }

}