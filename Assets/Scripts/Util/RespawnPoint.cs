using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
	public static RespawnPoint lastRespawnPoint;
	public static PlayerData lastPlayerData = new PlayerData();


	public void OnTriggerEnter2D(Collider2D collision) {

		if (PlayerController.IsPlayer(collision.gameObject)) {
			lastRespawnPoint = this;
			lastPlayerData.UpdateCurrentData();
		}

	}

	public static void Respawn() {


		Exit.SpawnNewPlayer(lastRespawnPoint.transform.position, lastPlayerData);
	}
}
