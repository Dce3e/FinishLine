using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] Vector2 goal;
    [SerializeField] Vector2 pos;
    [SerializeField] Vector2 speed;
    [SerializeField] Vector2 endSpeed;
    [SerializeField] float limitTime;
    [SerializeField] float limitTimeY;

    [SerializeField] LineRenderer lineRenderer;

    int tick;
    List<Vector3> graph = new();

    FinishLineDisplayHandler handler;

    enum EType { Pos, TickSpeedX, TickSpeedY }
    [SerializeField] EType showType;

    [SerializeField] int ticklimit;

    // Start is called before the first frame update
    void Start()
    {
        handler = new FinishLineDisplayHandler(goal.x - pos.x, limitTime, endSpeed.x);
        switch (showType)
        {
            case EType.Pos:
                graph.Add(new Vector2(pos.x, pos.y));
                break;
            case EType.TickSpeedX:
                graph.Add(new Vector2(tick, speed.x));
                break;
            case EType.TickSpeedY:
                graph.Add(new Vector2(tick, speed.y));
                break;
        }
    }

    public const float fixedTime = 0.08f;

    float startSpeed;
    float startSpeedY;

    [SerializeField] float defaultA;

    public float a;
    public float b;
    public float c;
    public float d;

    private void Update()
    {
        //if (pos.x > goal.x) return;

        if (tick >= ticklimit) return;
        tick++;

        handler.Calculate(tick, ref speed.x, goal.x - pos.x, endSpeed.x, false);

        //if (tick == 1) startSpeed = speed.x;

        //var avgSpeed = (startSpeed + endSpeed.x) * 0.5f;
        //var du = (goal.x + startSpeed * fixedTime) / avgSpeed;

        ////if (pos.x < goal.x)
        ////{
        //if (du - fixedTime > limitTime)
        //{
        //    // ((s + m) / 2 + (e + m) / 2)

        //    var test = limitTime + fixedTime;

        //    var mid = (((goal.x + startSpeed * fixedTime) / test * 2 * 2) - startSpeed - endSpeed.x) * 0.5f;

        //    if (tick * fixedTime < test * 0.5f)
        //    {
        //        var t = tick / (test * 0.5f / fixedTime);

        //        speed.x = Mathf.Lerp(startSpeed, mid, t);

        //        a += speed.x * fixedTime;
        //    }
        //    else
        //    {
        //        var t = tick / (test * 0.5f / fixedTime);
        //        speed.x = Mathf.Lerp(mid, endSpeed.x, t - 1);
        //        b += speed.x * fixedTime;
        //    }

        //    Debug.Log($"{tick} = {speed.x})");
        //}
        //else
        //{
        //    speed.x = Mathf.Lerp(startSpeed, endSpeed.x, tick / (du / fixedTime));

        //    Debug.Log($"{tick} / ({du}/fixedTime = {speed.x})");
        //    d += speed.x * fixedTime;
        //}

        pos.x += speed.x * fixedTime;
        ////}
        //if (tick == 1) startSpeedY = speed.y;

        //avgSpeed = (limitTimeY * defaultA + startSpeedY + startSpeedY) * 0.5f;
        //du = (goal.y + startSpeedY * fixedTime) / avgSpeed;

        ////if (pos.y > 0f)
        ////{
        //if (du - fixedTime > limitTimeY || du <= 0)
        //{
        //    var ev = goal.y * 2 / limitTimeY - startSpeedY;
        //    speed.y = Mathf.Lerp(startSpeedY, ev, tick / (limitTimeY / fixedTime));
        //    c += speed.y * fixedTime;
        //}
        //else
        //{
        //    speed.y = startSpeedY + tick * fixedTime * defaultA;
        //}

        //float speedY = -speed.y;
        //float posY = -pos.y;

        //handler.Calculate(tick, ref speedY, 0f, posY, goal.y - posY, endSpeed.y);

        //speed.y = -speedY;
        //pos.y = -posY;

        pos.y += speed.y * fixedTime;
        //}
        switch (showType)
        {
            case EType.Pos:
                graph.Add(new Vector2(pos.x, pos.y));
                break;
            case EType.TickSpeedX:
                graph.Add(new Vector2(tick, speed.x));
                break;
            case EType.TickSpeedY:
                graph.Add(new Vector2(tick, speed.y));
                break;
        }
        lineRenderer.positionCount = graph.Count;
        lineRenderer.SetPositions(graph.ToArray());
    }
}
