using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerData
{
	public static PlayerData instance = new PlayerData();


	

	public PlayerData() {
		if (instance == null) instance = this;
	}

	//Data
	public int transformationUUID = 0;


	public void ApplyPlayerData(GameObject player) {

	}

	public void UpdateCurrentData() {
		this.transformationUUID = PlayerController.Instance.getUUID();
	}
}
