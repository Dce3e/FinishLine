using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] Vector2 goal;
    [SerializeField] Vector2 pos;
    [SerializeField] Vector2 speed;
    [SerializeField] Vector2 endSpeed;
    [SerializeField] float limitTime;

    [SerializeField] LineRenderer lineRenderer;

    int tick;
    List<Vector3> graph = new();

    // Start is called before the first frame update
    void Start()
    {
        graph.Add(pos);
    }

    private void FixedUpdate()
    {
        if (tick >= 300) return;
        tick++;
        pos += speed * Time.fixedDeltaTime;
        graph.Add(pos);
        lineRenderer.positionCount = graph.Count;
        lineRenderer.SetPositions(graph.ToArray());
    }
}
