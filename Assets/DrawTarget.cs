using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTarget : MonoBehaviour
{
    public GameObject currTarget;
    public float cellSize;

    private void Start()
    {
        currTarget.transform.localScale = Vector2.one * cellSize;
        Vector2 pos = RoundTransform(currTarget.transform.position, cellSize);
        currTarget.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D rayCast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (rayCast.collider != null)
            {
                Vector2 pos = RoundTransform(rayCast.point, cellSize);
                currTarget.transform.position = pos;
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
