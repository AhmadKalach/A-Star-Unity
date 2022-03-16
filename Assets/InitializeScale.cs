using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeScale : MonoBehaviour
{
    public GameObject currTarget;
    public float cellSize;

    private void Start()
    {
        currTarget.transform.localScale = Vector2.one * cellSize;
        Vector2 pos = RoundTransform(currTarget.transform.position, cellSize);
        currTarget.transform.position = pos;
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
