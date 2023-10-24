using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerWeaponType
{
    Neddle,
    Dart
}

public class BasePlayerWeapon : MonoBehaviour {

    protected float dame;

    protected PlayerWeaponType type;

    public float speed;

    public Vector3 targetPosition;

    private Vector3 newVector;

    public virtual void Init(PlayerWeaponType type)
    {
        this.type = type;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Enemy")
        {
            Destroy(gameObject);
            //target.gameObject.GetComponent<BaseBody>().UpdateHp(60);
        }
    }

    public virtual void FlyToPosition()
    {
        if (targetPosition != null)
        {
            gameObject.transform.position += newVector * speed * Time.deltaTime;
        }
    }

    public virtual void SetTargetPosition(Vector3 pos)
    {
        newVector = pos - gameObject.transform.position;
        targetPosition = pos;
    }
}
