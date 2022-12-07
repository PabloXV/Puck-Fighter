using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour
{
    // Responsible for "pucks," namely their trajectory and physical movement.

    [SerializeField] private Transform indicator;
    [SerializeField] private SpriteRenderer icon;

    public PuckData pd;
    public GameManager.Team team;

    private Rigidbody2D rb;
    private float indicatorScale = 0.05f;
    private Vector2 premovedTrajectory;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = pd.mass;

        transform.localScale = new Vector3(transform.localScale.x * pd.size, transform.localScale.y * pd.size, 1f);
        icon.sprite = pd.icon;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Puck")
        {
            if (collision.collider.GetComponent<PuckController>().team != team)
            {
                if (TurnManager.instance.entities[TurnManager.instance.currentTurn] == this.gameObject)
                {
                    TurnManager.instance.entities.Remove(collision.collider.gameObject);
                    Destroy(collision.collider.gameObject);
                }
            }
        }
    }

    public void Launch(Vector2 trajectory)
    {
        premovedTrajectory = trajectory;
        StopCoroutine(nameof(WaitForTurn));
        StartCoroutine(nameof(WaitForTurn));
    }

    IEnumerator WaitForTurn()
    {
        while (true)
        {
            UpdateVelocityIndicator(premovedTrajectory);
            if (TurnManager.instance.entities[TurnManager.instance.currentTurn] == this.gameObject && rb.velocity.magnitude < 0.01)
            {
                UpdateVelocityIndicator(Vector2.zero);
                rb.velocity = premovedTrajectory * pd.speed;
                StartCoroutine(nameof(WaitForLaunchEnd));
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator WaitForLaunchEnd()
    {
        if (team == GameManager.Team.Player)
        {
            yield return new WaitUntil(() => rb.velocity.magnitude < 0.1);
        } else
        {
            yield return new WaitForSeconds(0.5f);
        }
        
        TurnManager.instance.EndTurn();
    }

    public void UpdateVelocityIndicator(Vector2 trajectory)
    {
        trajectory *= pd.speed;
        for (int i = 0; i < indicator.childCount; i++)
        {
            indicator.GetChild(i).transform.position = new Vector3(trajectory.x, trajectory.y, indicator.position.z) * i * indicatorScale + transform.position;
            float c = ((float)indicator.childCount - i) / indicator.childCount;
            indicator.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(c, c, c);
        }
    }
}
