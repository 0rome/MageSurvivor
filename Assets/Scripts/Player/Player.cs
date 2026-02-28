using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    public PlayerType type;
    
    private int playerLevel = 1;
    public int PlayerLevel => playerLevel;

    [Inject] private GameManager gameManager;

    private List<Player> playerComponents = new List<Player>();

    protected virtual void Awake()
    {
        gameManager.OnPaused += Deactivate;
        gameManager.OnResumed += Activate;

        MonoBehaviour[] allComponents = GetComponents<MonoBehaviour>();
    }

    public void AddLevel()
    {
        playerLevel++;
    }

    protected void Deactivate()
    {
        foreach (MonoBehaviour component in playerComponents)
        {
            if (component != null)
                component.enabled = false;
        }
    }

    protected void Activate()
    {
        foreach (MonoBehaviour component in playerComponents)
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
