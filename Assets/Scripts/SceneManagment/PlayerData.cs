using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerData
{
	public static PlayerData instance = new PlayerData();

	public LinkedList<string> flags = new LinkedList<string>();
	

	public PlayerData() {
		if (instance == null) instance = this;
	}

	//Data
	public int transformationUUID = 0;
	public GameObject morphPlayerObj1 = null;
	public Sprite morphPlayerSprite1 = null;
	public GameObject morphPlayerObj2 = null;
	public Sprite morphPlayerSprite2 = null;

	public void ApplyPlayerData(GameObject player) {
		ItemOverlaySingelton.Instance.powerup1.PlayerObject = morphPlayerObj1;
		ItemOverlaySingelton.Instance.powerup2.PlayerObject = morphPlayerObj2;
		ItemOverlaySingelton.Instance.item1.sprite = morphPlayerSprite1;
		ItemOverlaySingelton.Instance.item2.sprite = morphPlayerSprite2;
	}

	public void SetFlag(string name)
	{
		name = name.ToLower();
		if (!flags.Contains(name))
		{
			flags.AddLast(name);
		}
	}

	/// <summary>
	/// Removes a flag out of the PlayerData flag field. Returns true if the flag existed.
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public bool RemoveFlag(string name)
    {
		name = name.ToLower();
		if (flags.Contains(name))
        {
			flags.Remove(name);
			return true;
        }
		return false;
    }

	public bool IsFlagSet(string name)
    {
		return flags.Contains(name.ToLower());
    }

	public void UpdateCurrentData() {
		this.transformationUUID = PlayerController.Instance.getUUID();
		flags = PlayerData.instance.flags;

		morphPlayerObj1 = ItemOverlaySingelton.Instance.powerup1.PlayerObject;
		morphPlayerObj2 = ItemOverlaySingelton.Instance.powerup2.PlayerObject;
		morphPlayerSprite1 = ItemOverlaySingelton.Instance.item1.sprite;
		morphPlayerSprite2 = ItemOverlaySingelton.Instance.item2.sprite;
	}
}
