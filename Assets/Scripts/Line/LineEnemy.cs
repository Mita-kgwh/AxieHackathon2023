using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineEnemy : MonoBehaviour {

    public ComradeType leaderType;
    public List<ComradeType> follower;
    public float speed;
    public int distance;
    public float startDistance;

    [HideInInspector]
    public EnemyBaseBody leader;
    [HideInInspector]
    public List<PathRecorder> recorder;

    protected List<GameObject> bodies = new List<GameObject>();

    protected virtual void Start()
    {
        CreateLeader();
        CreateFollower();
    }

    protected virtual void CreateLeader()
    {
        GameObject go = (GameObject)Instantiate(ComradeManager.Instance.GetObjectByType(leaderType), transform);
        leader = go.GetComponent<EnemyBaseBody>();

        if (leader != null) // Nếu EnemyBaseBody tồn tại
        {
            // Tạo recorder để lưu vị trí
            recorder = new List<PathRecorder>();

            // Gán recorder của linePlayer cho leader
            leader.recorder = recorder;

            // Cho thằng này làm leader
            leader.leader = true;

            // gán this vào linePlayer của leader dùng để check đụng hàng
            //leader.linePlayer = ;
            leader.gameObject.AddComponent<LeaderTrigger>();

            leader.SetSpeed(speed);
        }
        bodies.Add(go);
    }

    protected virtual void CreateFollower() //ERR
    {
        Vector3 pos = leader.transform.position;
        pos.y += startDistance * follower.Count * distance;

        // Khởi tạo vị trí ban đầu của recorder
        for (int i = 0; i <= follower.Count * distance; i++)
        {
            recorder.Add(new PathRecorder(pos, leader.dir));
            pos.y -= startDistance;
        }

        // Thêm từng player vào
        for (int i = 0; i < follower.Count; i++)
        {
            AddBody(follower[i], bodies.Count);
        }
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnTurnDown();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnTurnUp();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnTurnLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnTurnRight();
        }
        //else if (Input.GetKeyDown(KeyCode.A))
        //{
        //    if (distance * (bodies.Count) < recorder.Count)
        //        AddBody(ComradeType.PANDA, bodies.Count);
        //}
    }

    public virtual void OnTurnLeft()
    {
        leader.Turn(Direction.LEFT);
    }

    public virtual void OnTurnRight()
    {
        leader.Turn(Direction.RIGHT);
    }

    public virtual void OnTurnUp()
    {
        leader.Turn(Direction.UP);
    }

    public virtual void OnTurnDown()
    {
        leader.Turn(Direction.DOWN);
    }

    public virtual void AddBody(ComradeType type, int number)
    {
        Vector3 pos = new Vector3(100, 100);
        GameObject body = (GameObject)Instantiate(ComradeManager.Instance.GetObjectByType(type), pos, Quaternion.identity, transform);
        BaseBody baseBody = body.GetComponent<BaseBody>();
        try
        {
            baseBody.recorder = recorder; // Đã sửa ở đây
            baseBody.SetNumber(number, distance);
            baseBody.Turn(Direction.FOLLOW);
        }
        catch (Exception e)
        {
            Debug.Log("Error Create");
        }
        bodies.Add(body);
    }

    public virtual void RemoveBody(int index)
    {
        for (int i = index; i < bodies.Count; i++)
        {
            Destroy(bodies[i].gameObject);
        }

        bodies.RemoveRange(index, bodies.Count - index);
    }

    public virtual void OnDie()
    {
        RemoveBody(0);
        //Application.LoadLevel("Main");
        Destroy(gameObject);
    }

    public virtual void OnHitLine(int index)
    {
        //RemoveBody(index);
    }

    public int GetBodyCount()
    {
        return bodies.Count;
    }

    public void AddHP(int hp)
    {
        foreach (GameObject b in bodies)
        {
            b.GetComponent<BaseBody>().AddHP(hp);
        }
    }
}
