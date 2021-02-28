using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [Header("IK Points")]
    public Transform leftHandPoint;
    public Transform rightHandPoint;

    [Header("Firing Events")]
    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackEnd;

   
    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void AttackStart()
    {
        OnAttackStart.Invoke();
    }

    public virtual void AttackEnd()
    {
        OnAttackEnd.Invoke();
    }
}
