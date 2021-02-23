using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerData : MonoBehaviour
{
   private List<PlayerConfiguration> playerConfigs;
   public static PlayerData Instance { get; private set; }


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("singleton");
        }

        else

        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {

        pi.transform.SetParent(transform);

        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));

        }
    }




}



public class PlayerConfiguration
{
    public PlayerInput Input { get; private set; }
    public int PlayerIndex { get; private set; }
    public bool isReady { get; set; }
    public Material playerMaterial { get; set; }

    public PlayerConfiguration(PlayerInput pi)
    {

        PlayerIndex = pi.playerIndex;
        Input = pi;
    }

    

}