using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    public float timeLife;

    protected float timer;

    // dành cho FX áp dụng lên 1 đối tượng nào đó
    protected GameObject target;
    public virtual void Init(GameObject _Target)
    {
        target = _Target;
    }

    protected virtual void Start()
    {
        timer = timeLife;
    }

    protected virtual void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0f)
        {
            End();
        }
    }

    protected virtual void End()
    {
        Destroy(this.gameObject);
    }

}
