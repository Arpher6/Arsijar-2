using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private void Start()
    {
        Vector2 position = new Vector2(Random.Range(minX, maxX),Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(PlayerPrefab.name,position, Quaternion.identity);
    }
}
