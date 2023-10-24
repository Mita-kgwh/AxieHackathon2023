using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {

    public float speed;
    public Direction direction;
    public Animator anim;

    // Thời gian giữa những lần đổi hướng
    public float timeChange;

    // Lấy vị trí cũ của AI, để lỡ đụng tường sẽ trigger ChangeDirection 
    // ==> gọi MoveBackward() để ra khỏi vùng trigger
    private Vector3 previousPosition;

    private float elapsedTimeChange;

    delegate void Action();
    Action Move;

    public int mistake;

    /// <summary>
    /// ------------------------- FUNCTION
    /// </summary>

    protected virtual void OnEnable()
    {
        elapsedTimeChange = 0f;
        Move = GetAction(direction);
        SetMoveAnimation(direction);
    }

	protected virtual void FixedUpdate()
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
        Move();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            Debug.Log(col.name);
            ChangeDirection(true);
        }
    }

    // AI Execute
    protected virtual void MoveLeft()
    {
        direction = Direction.LEFT;
        Vector3 offset = Vector3.left * speed * Time.fixedDeltaTime;
        transform.position += offset;
        previousPosition -= offset * mistake;  
    }
    protected virtual void MoveRight()
    {
        direction = Direction.RIGHT;
        Vector3 offset = Vector3.right * speed * Time.fixedDeltaTime;
        transform.position += offset;
        previousPosition -= offset * mistake;
    }
    protected virtual void MoveUp()
    {
        direction = Direction.UP;
        Vector3 offset = Vector3.up * speed * Time.fixedDeltaTime;
        transform.position += offset;
        previousPosition -= offset * mistake;
    }
    protected virtual void MoveDown()
    {
        direction = Direction.DOWN;
        Vector3 offset = Vector3.down * speed * Time.fixedDeltaTime;
        transform.position += offset;
        previousPosition -= offset * mistake;
    }
    protected virtual void Stand()
    {

    }
    protected virtual void MoveBackward()
    {
        transform.position = previousPosition;  
    }
    Action GetAction(Direction dir)
    {
        switch (dir)
        {
            case Direction.LEFT: return MoveLeft;
            case Direction.RIGHT: return MoveRight;
            case Direction.UP: return MoveUp;
            case Direction.DOWN: return MoveDown;
            default: return Stand;
        }
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
                    Move = MoveUp;
                }
                else
                {
                    direction = Direction.DOWN;
                    Move = MoveDown;
                }
                break;

            case Direction.RIGHT:
                if (rand == 0)
                {
                    direction = Direction.UP;
                    Move = MoveUp;
                }
                else
                {
                    direction = Direction.DOWN;
                    Move = MoveDown;
                }
                break;

            case Direction.DOWN:
                if (rand == 0)
                {
                    direction = Direction.LEFT;
                    Move = MoveLeft;
                }
                else
                {
                    direction = Direction.RIGHT;
                    Move = MoveRight;
                }
                break;

            case Direction.UP:
                if (rand == 0)
                {
                    direction = Direction.LEFT;
                    Move = MoveLeft;
                }
                else
                {
                    direction = Direction.RIGHT;
                    Move = MoveRight;
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
