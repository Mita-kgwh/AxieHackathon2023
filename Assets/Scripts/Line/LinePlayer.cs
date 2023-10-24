using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRecorder
{
    public Vector3 position;
    public Direction direction;

    public PathRecorder(Vector3 position, Direction direction)
    {
        this.position = position;
        this.direction = direction;
    }
}

public class LinePlayer : MonoBehaviour {

    public CameraController cameraController;
    public ComradeType leaderType;
    public List<ComradeType> follower;
    public float speed;
    public int distance;
    public float startDistance;

    [HideInInspector]
    public BaseBody leader;
    [HideInInspector]
    public List<PathRecorder> recorder;

    protected List<GameObject> bodies = new List<GameObject>();

    protected float invincibleTime = 0f;

    protected virtual void Start()
    {
        CreateLeader();
        CreateFollower();
    }

    protected virtual void CreateLeader()
    {
        GameObject go = (GameObject)Instantiate(ComradeManager.Instance.GetObjectByType(leaderType), transform);
        leader = go.GetComponent<BaseBody>();
       
        if (leader != null) // Nếu BaseBody tồn tại
        {
            // Cho camera di theo leader
            cameraController.player = leader.gameObject;

            // Tạo recorder để lưu vị trí
            recorder = new List<PathRecorder>();

            Vector3 offset = Vector3.down* speed *Time.fixedDeltaTime;
            for (int i = 0; i < bodies.Count * distance; i++)
            {
                recorder.Add(new PathRecorder(leader.transform.position - i * offset, Direction.DOWN));
            }

            // Gán recorder của linePlayer cho leader
            leader.recorder = recorder;

            // Cho thằng này làm leader
            leader.leader = true;

            leader.dir = Direction.DOWN;

            // gán this vào linePlayer của leader dùng để check đụng hàng
            leader.linePlayer = this;
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

    protected virtual void FixedUpdate()
    {
    
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
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (distance * (bodies.Count) < recorder.Count)
                AddBody(ComradeType.RED, bodies.Count);
        }
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
        if (bodies.Count < 7)
        {
            Vector3 pos = new Vector3(100, 100);
            GameObject body = (GameObject)Instantiate(ComradeManager.Instance.GetObjectByType(type), pos, Quaternion.identity, transform);
            BaseBody baseBody = body.GetComponent<BaseBody>();
            try
            {
                baseBody.linePlayer = this;
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
        Application.LoadLevel("GameOver");
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

    public void SetInvincible(float time)
    {
        leader.SetInvincible(time);
    }

    [ContextMenu("Slow")]
    public void Slow()
    {
        //for (int i = 1; i < bodies.Count * distance; i++)
        //{
        //    recorder[i].position = recorder[i + distance].position;
        //    recorder[i].direction = recorder[i + distance].direction;
        //}

        //for (int i = 0; i < bodies.Count; i++)
        //{
        //    bodies[i].GetComponent<BaseBody>().Slow(i, distance);
        //}
        //leader.Slow();
    }
    
}
