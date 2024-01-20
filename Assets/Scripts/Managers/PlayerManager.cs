using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CharacterController2D player;

    public static PlayerManager instance;
    private void Awake()
    {
        //Singleton
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;

        DontDestroyOnLoad(instance);
    }



    public Transform GetPlayerTransform()
    {
        if(!player)
            return null;

        return player.transform;
    }


}
