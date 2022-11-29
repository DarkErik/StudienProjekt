using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public static PlayerMovement CurrentMovement { get; private set; }

	private Rigidbody2D _rb2D;
    protected virtual void Awake()
    {
        CurrentMovement = this;
		_rb2D = GetComponent<Rigidbody2D>();
    }


	public static void Shutdown()
	{
		_Shutdown();
	}

	private static void _Shutdown()
	{
		if (CurrentMovement == null) return;
		CurrentMovement.enabled = false;
		CurrentMovement._rb2D.velocity = new Vector2(0, CurrentMovement._rb2D.velocity.y);

	}

	public static void WakeUp()
	{
		if (CurrentMovement == null) return;
		CurrentMovement.enabled = true;
	}

	public static void Shutdown(float time)
	{
		CurrentMovement.StartCoroutine(EnablePlayerControllerAfterTime(time));
		_Shutdown();
	}


	private static IEnumerator EnablePlayerControllerAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
		CurrentMovement.enabled = true;
	}
}
