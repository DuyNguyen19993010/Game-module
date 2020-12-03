using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public void RespawnPlayer(Vector2 _spawnPosition)
    {
        GameObject newPlayer = Instantiate((GameObject)Resources.Load("Prefab/Character/Player/Player", typeof(GameObject)));
        newPlayer.transform.position = _spawnPosition;
        newPlayer.SetActive(true);
        Debug.Log(newPlayer.transform.position);
    }
}
