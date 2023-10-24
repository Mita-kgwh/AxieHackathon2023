using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicProjectile : MonoBehaviour {

    public float dame;

    public float speed;

    public Direction direction;

    private Animator anim;

    private float timeToExploise;

    private bool isNormalStatus;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        timeToExploise = Random.Range(2, 5.7f);
        isNormalStatus = true;
    }

    void Update()
    {
        Quaternion rotate = Quaternion.Euler(0, 0, 0);
        timeToExploise -= Time.deltaTime;
        if(timeToExploise <=0)
        {
            if (CameraController.Instance.CheckInCamera(gameObject.transform.position))
                anim.SetBool("isExploise", true);
            rotate = Quaternion.Euler(0, 0, 0);
            speed = 0;
            isNormalStatus = false;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > timeToExploise + 5 && !anim.IsInTransition(0))
        {
            anim.enabled = false;
            Destroy(gameObject);
        }
        if(isNormalStatus)
            switch (direction)
            {
                case Direction.DOWN:
                    anim.SetBool("isUp", false);
                    anim.SetBool("isDown", true);
                    rotate = Quaternion.Euler(0, 0, 0);
                    gameObject.transform.position += Time.deltaTime * speed * Vector3.down;
                    break;
                case Direction.UP:
                    anim.SetBool("isUp", true);
                    anim.SetBool("isDown", false);
                    rotate = Quaternion.Euler(0, 0, 0);
                    gameObject.transform.position += Time.deltaTime * speed * Vector3.up;
                    break;
                case Direction.LEFT:
                    anim.SetBool("isUp", true);
                    anim.SetBool("isDown", false);
                    rotate = Quaternion.Euler(0, 0, 90);
                    gameObject.transform.position += Time.deltaTime * speed * Vector3.left;
                    break;
                case Direction.RIGHT:
                    anim.SetBool("isUp", true);
                    anim.SetBool("isDown", false);
                    rotate = Quaternion.Euler(0, 0, -90);
                    gameObject.transform.position += Time.deltaTime * speed * Vector3.right;
                    break;
            }
        gameObject.transform.rotation = rotate;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            if (isNormalStatus)
                target.GetComponent<BaseBody>().OnHit(dame);
            else
            {
                gameObject.GetComponent<CircleCollider2D>().radius *= 1.5f;
                target.GetComponent<BaseBody>().OnHit(dame * 1.5f);
            }
        }
    }
}
