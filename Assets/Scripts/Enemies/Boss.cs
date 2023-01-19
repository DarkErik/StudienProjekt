using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss Instance { get; private set; }

    [System.Serializable]
    public class PhaseData
    {
        public float timeToTraverseToPhase;
        public Vector3 position;
        public float shootSpeed;
        public float shootDelay;
        public float shotAngleDiffusion;
        public int shotAmount;
    }

    [SerializeField] private PhaseData[] phases;

    [SerializeField] private Animator anim;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private Transform shotPos;
    [SerializeField] private GameObject onDeathParticleSystem;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startPos = transform.position;
    }


    private int phase = 0;
    private bool traversing = true;
    private bool hurt = false;
    private float traverseTime = 0;
    private Vector3 startPos;
    private float shootDelay = 0;
    
    public void Update()
    {
        PhaseData current = phases[phase];

        if (traversing)
        {
            //transform.localRotation = Quaternion.LookRotation()
            traverseTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, current.position, traverseTime / current.timeToTraverseToPhase);

            if (traverseTime >= current.timeToTraverseToPhase)
            {
                traversing = false;
                shootDelay = 0;
            }
                
        } else
        {
            shootDelay += Time.deltaTime;
            if (shootDelay >= current.shootDelay && !hurt)
            {
                StartCoroutine(Shoot());

                shootDelay = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ContinuePhase();
        }
    }

    private IEnumerator Shoot()
    {
        anim.Play("Attack");
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < phases[phase].shotAmount; i++)
        {
            if (hurt) break;
            SpawnShot();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void SpawnShot()
    {
        Vector2 atan2 = -shotPos.position + PlayerController.Instance.transform.position;

        GameObject newShot = Instantiate(shotPrefab, shotPos.position, Quaternion.identity);
        newShot.GetComponent<Shot>()?.Init(phases[phase].shootSpeed, Mathf.Atan2(atan2.y, atan2.x) * Mathf.Rad2Deg + Random.Range(-phases[phase].shotAngleDiffusion / 2f, phases[phase].shotAngleDiffusion / 2f));
    }

    public void ContinuePhase()
    {
        hurt = true;
        StartCoroutine(_ContinuePhase());

    }

    private IEnumerator _ContinuePhase()
    {
        anim.Play("Hit");
        if (phase + 1 >= phases.Length)
        {
            onDeathParticleSystem.SetActive(true);
            onDeathParticleSystem.transform.SetParent(null, true);
            yield return new WaitForSeconds(3.5f);
            Destroy(this.gameObject);
        }
        else
        {
            yield return new WaitForSeconds(1.75f);
            startPos = transform.position;
            phase++;
            traverseTime = 0;
            traversing = true;
            hurt = false;
        }
    }
}
