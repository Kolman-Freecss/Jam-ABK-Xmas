using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] Transform player, destination;
    [SerializeField] GameObject playerPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPrefab.SetActive(false);
            player.position = destination.position;
            playerPrefab.SetActive(true);


        }
    }


}
