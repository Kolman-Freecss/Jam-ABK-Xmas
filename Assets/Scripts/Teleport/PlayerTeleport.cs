using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleport
{
    [SerializeField] Transform player, destination;
    [SerializeField] GameObject playerPrefab;
    

    public PlayerTeleport(GameObject playerPrefab, Transform player, Transform destination)
    {
        this.playerPrefab = playerPrefab;
        this.player = player;   
        this.destination = destination;

    }
    public void TeleportPlayer()
    {
        playerPrefab.SetActive(false);
        player.position = destination.position;
        playerPrefab.SetActive(true);
    }


}
