using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerData
{
	public static PlayerData instance = new PlayerData();

	private LinkedList<string> flags = new LinkedList<string>();
	

	public PlayerData() {
		if (instance == null) instance = this;
	}

	//Data
	public int transformationUUID = 0;


	public void ApplyPlayerData(GameObject player) {

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
	}
}
