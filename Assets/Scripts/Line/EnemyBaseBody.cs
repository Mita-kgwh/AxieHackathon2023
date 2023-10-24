using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseBody : MonoBehaviour {

    public Direction dir;
    public bool leader;
    public GameObject hp;
    public float health;

    protected float curHealth;
    protected Animator anim;
    private delegate void Action();
    private Action Move;
    private int pos;
    private float speed = 1f;


    [HideInInspector]
    public List<PathRecorder> recorder;
    [HideInInspector]
    public int number;
    [HideInInspector]
    public LinePlayer linePlayer;

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    public Direction direction;

    // Thời gian giữa những lần đổi hướng
    public float timeChange;

    // Lấy vị trí cũ của AI, để lỡ đụng tường sẽ trigger ChangeDirection 
    // ==> gọi MoveBackward() để ra khỏi vùng trigger
    private Vector3 previousPosition;

    private float elapsedTimeChange;

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (leader)
        {
            // Lấy vị trí cũ
            previousPosition = transform.position;

            // Xử lý tự chuyển hướng
            elapsedTimeChange += Time.fixedDeltaTime;
            if (elapsedTimeChange >= timeChange)
            {
                ChangeDirection(false);
                elapsedTimeChange = 0f;
            }

            // Di chuyển
            if (recorder != null)
                recorder.Add(new PathRecorder(transform.position, dir));
        }

        Move();
        SetAnimation(dir);
    }

    public virtual void Init()
    {
        if (Move != Follow)
        {
            Move = TurnDown;
        }


        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Break();
        }

    }

    public virtual void Turn(Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                if (dir != Direction.RIGHT)
                {
                    Move = TurnLeft;
                }
                break;
            case Direction.RIGHT:
                if (dir != Direction.LEFT)
                {
                    Move = TurnRight;
                }
                break;
            case Direction.UP:
                if (dir != Direction.DOWN)
                {
                    Move = TurnUp;
                }
                break;
            case Direction.DOWN:
                if (dir != Direction.UP)
                {
                    Move = TurnDown;
                }
                break;
            case Direction.FOLLOW:
                Move = Follow;
                break;
        }
    }

    public virtual void TurnLeft()
    {
        this.dir = Direction.LEFT;
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    public virtual void TurnRight()
    {
        this.dir = Direction.RIGHT;
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    public virtual void TurnUp()
    {
        this.dir = Direction.UP;
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    public virtual void TurnDown()
    {
        this.dir = Direction.DOWN;
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    public virtual void Follow()
    {
        transform.position = recorder[pos].position;
        dir = recorder[pos].direction;
        pos += change;
    }

    int change = 2;

    public virtual void OnHit(float dame)
    {
        curHealth -= dame;
        if (curHealth <= 0)
        {
            //EffectManager.Instance.ApplyEffect(TYPE_FX.Collision, this.gameObject);
            Destroy(this.gameObject);
        }
        float ratio = curHealth / health;

        Vector3 scale = new Vector3(ratio, 1, 1);

        hp.transform.localScale = scale;
    }

    public virtual void OnHit(BaseBody target, float dame)
    {
        target.health -= dame;
    }

    public virtual void OnHitLine(int index)
    {
        if (linePlayer)
        {
            linePlayer.OnHitLine(index);
        }
    }

    public virtual void OnDie()
    { }

    public virtual void OnAttack()
    { }

    public virtual void SetNumber(int number, int space)
    {
        this.number = number;
        this.pos = this.recorder.Count - number * space;
    }

    public virtual void SetAnimation(Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                anim.SetBool("isLeft", true);
                anim.SetBool("isRight", false);
                anim.SetBool("isUp", false);
                anim.SetBool("isDown", false);
                break;

            case Direction.RIGHT:
                anim.SetBool("isLeft", false);
                anim.SetBool("isRight", true);
                anim.SetBool("isUp", false);
                anim.SetBool("isDown", false);
                break;

            case Direction.UP:
                anim.SetBool("isLeft", false);
                anim.SetBool("isRight", false);
                anim.SetBool("isUp", true);
                anim.SetBool("isDown", false);
                break;

            case Direction.DOWN:
                anim.SetBool("isLeft", false);
                anim.SetBool("isRight", false);
                anim.SetBool("isUp", false);
                anim.SetBool("isDown", true);
                break;
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("HitDown"))
            anim.SetBool("isHit", false);

    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            OnHitLine(col.GetComponent<BaseBody>().number);
        }

        if (col.tag == "Wall")
        {
            Debug.Log(col.name);
            ChangeDirection(true);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    
   
    /// <summary>
    /// ------------------------- FUNCTION
    /// </summary>

    protected virtual void OnEnable()
    {
        elapsedTimeChange = 0f;
    }

    protected virtual void MoveBackward()
    {
        transform.position = previousPosition;
    }

    /*--------------------------------AI actions 
    */

    // Đổi hướng khi gặp vật cản, hoặc hết thời gian, trigger = true ~ goị từ onTrigger
    protected virtual void ChangeDirection(bool trigger)
    {
        // Lùi lại
        if (trigger)
            MoveBackward();

        // Xử lý chuyển hướng
        int rand = Random.Range(0, 2);
        switch (direction)
        {
            case Direction.LEFT:
                if (rand == 0)
                {
                    direction = Direction.UP;
                    Move = TurnUp;
                }
                else
                {
                    direction = Direction.DOWN;
                    Move = TurnDown;
                }
                break;

            case Direction.RIGHT:
                if (rand == 0)
                {
                    direction = Direction.UP;
                    Move = TurnUp;
                }
                else
                {
                    direction = Direction.DOWN;
                    Move = TurnDown;
                }
                break;

            case Direction.DOWN:
                if (rand == 0)
                {
                    direction = Direction.LEFT;
                    Move = TurnLeft;
                }
                else
                {
                    direction = Direction.RIGHT;
                    Move = TurnRight;
                }
                break;

            case Direction.UP:
                if (rand == 0)
                {
                    direction = Direction.LEFT;
                    Move = TurnLeft;
                }
                else
                {
                    direction = Direction.RIGHT;
                    Move = TurnRight;
                }
                break;
        }

        // Chuyển đổi animation
        SetMoveAnimation(direction);
    }

    // Đánh khi gặp player
    protected virtual void Attack()
    {

    }

    // Chạy khi gặp player
    protected virtual void Run()
    {

    }

    public virtual void SetMoveAnimation(Direction dir)
    {
        if (anim != null)
        {
            switch (dir)
            {
                case Direction.LEFT:
                    anim.SetBool("isLeft", true);
                    anim.SetBool("isRight", false);
                    anim.SetBool("isUp", false);
                    anim.SetBool("isDown", false);
                    break;
                case Direction.RIGHT:
                    anim.SetBool("isLeft", false);
                    anim.SetBool("isRight", true);
                    anim.SetBool("isUp", false);
                    anim.SetBool("isDown", false);
                    break;
                case Direction.UP:
                    anim.SetBool("isLeft", false);
                    anim.SetBool("isRight", false);
                    anim.SetBool("isUp", true);
                    anim.SetBool("isDown", false);
                    break;
                case Direction.DOWN:
                    anim.SetBool("isLeft", false);
                    anim.SetBool("isRight", false);
                    anim.SetBool("isUp", false);
                    anim.SetBool("isDown", true);
                    break;
            }
        }
    }
}
