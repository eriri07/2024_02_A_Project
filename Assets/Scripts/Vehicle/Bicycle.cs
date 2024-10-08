using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicycle : Vehicle
{
    public override void Move()
    {
        base.Move();

        transform.Rotate(0, 10 * Time.deltaTime, 0);
    }

    public override void Horn()
    {
        Debug.Log("자전거 경적 : 딩딩 ");
    }

}
