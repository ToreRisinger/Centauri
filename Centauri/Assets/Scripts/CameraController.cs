using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void LateUpdate()
    {
        if(GameManager.players.ContainsKey(Client.instance.myId))
        {
            Vector3 playerPosition = GameManager.players[Client.instance.myId].transform.position;
            this.transform.position = new Vector3(playerPosition.x, playerPosition.y, this.transform.position.z);
        }
    }
}
