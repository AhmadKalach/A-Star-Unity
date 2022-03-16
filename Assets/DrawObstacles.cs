using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawObstacles : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float cellSize;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D rayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (rayCast.collider != null && !rayCast.collider.gameObject.CompareTag("Obstacle"))
            {
                Vector2 pos = RoundTransform(rayCast.point, cellSize);
                GameObject obj = Instantiate(obstaclePrefab, pos, Quaternion.identity);
                obj.transform.localScale = Vector2.one * cellSize;
            }
        }

        if (Input.GetMouseButton(1))
        {
            RaycastHit2D rayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (rayCast.collider != null && rayCast.collider.gameObject.CompareTag("Obstacle"))
            {
                Destroy(rayCast.collider.gameObject);
            }
        }
    }

    private Vector2 RoundTransform(Vector2 v, float snapValue)
    {
        return new Vector2
        (
            snapValue * Mathf.Round(v.x / snapValue),
            snapValue * Mathf.Round(v.y / snapValue)
        );
    }
}
