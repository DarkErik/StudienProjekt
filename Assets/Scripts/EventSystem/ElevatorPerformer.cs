using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPerformer : Performer
{
    [SerializeField] private Animator anim;
    [SerializeField] private SceneObj scene;
    [SerializeField] private ExitDir dir;
    [SerializeField] private Transform respawnPoint;

    private void Start()
    {
        if (Exit.exitSpawnName == "elev")
        {
            anim.Play("ElevatorClose");
            Exit.SpawnNewPlayer(respawnPoint.transform.position, PlayerData.instance);
            Time.timeScale = 1f;
        }
    }

    public override void OnTap(Trigger triggerData)
    {
        Time.timeScale = 0;
        StartCoroutine(Elevate());
    }
    
    private IEnumerator Elevate()
    {
        anim.Play("ElevatorOpen");
        yield return new WaitForSecondsRealtime(1.5f);
        PlayerData.instance.UpdateCurrentData();
        Exit.exitSpawnName = "elev";
        AudioManager.instance.Play("SceneTransition");
        SceneTransition.LoadScene(scene.name, dir);
    }

    protected override void OnUpdate()
    {
    }
}
