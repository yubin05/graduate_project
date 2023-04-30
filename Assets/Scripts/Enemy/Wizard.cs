using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    SpriteRenderer[] renders;

    private bool canTeleport = true;
    public float teleportCoolTime;

    [SerializeField]
    public GameObject inital_teleport_point;

    protected override void Awake()
    {
        // Wizard's each body's part get
        renders = transform.Find("body").GetComponentsInChildren<SpriteRenderer>();

        Instantiate(inital_teleport_point, new Vector2(transform.position.x, transform.position.y + 0.25f), transform.rotation,
            transform.parent.Find("Teleport_Points"));

        base.Awake();
        isRightDefaultValue = false;
    }

    // this method called by Wizard_DetectTeleportPoint.cs
    public void Teleport(List<Transform> teleport_points)
    {
        //teleport_points = transform.Find("Detect_Teleport_Point").GetComponent<Wizard_DetectTeleportPoint>().Get_Teleport_Points();
        if (!canTeleport || teleport_points.Count <= 1) { return; }     // if telepoint_point don't exist, return function

        // teleport cooltime
        canTeleport = false;
        StartCoroutine(CoolTimeTeleport());

        // teleport signal
        foreach (SpriteRenderer render in renders) { render.color = Color.black; }
        gameObject.layer = 9;   // enemydead

        // actually teleport method
        StartCoroutine(MovePosition(teleport_points));
    }

    IEnumerator CoolTimeTeleport()
    {
        yield return new WaitForSeconds(teleportCoolTime);
        canTeleport = true;
    }

    IEnumerator MovePosition(List<Transform> teleport_points)
    {
        yield return new WaitForSeconds(0.5f);

        int random_index = Random.Range(0, teleport_points.Count);
        transform.position = new Vector2(teleport_points[random_index].position.x, teleport_points[random_index].position.y - 0.25f);

        // Finish Teleport Signal
        StartCoroutine(FinishTeleportSignal());
    }

    IEnumerator FinishTeleportSignal()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (SpriteRenderer render in renders) { render.color = Color.white; }
        gameObject.layer = 8;   // enemydead
    }

    public override void Move()
    {
        // don't move left and right

        // prevent collider bug
        transform.position = new Vector2(transform.position.x - 0.01f, transform.position.y);
        transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
    }

    protected override void DestroyEnemyObject()
    {
        // explosion signal
        foreach (SpriteRenderer render in renders) { render.color = new Color(1, 1, 1, 0.4f); }
        //explosion signal sound at this line

        StartCoroutine(StartExplosion());
        StartCoroutine(DelayDestroy());
    }

    IEnumerator StartExplosion()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Find("Explosion").gameObject.SetActive(true);
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
