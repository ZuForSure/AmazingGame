using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LookAtTarget : MyMonoBehaviour
{
    protected virtual void AimTarget(Vector3 target)
    {
        Vector3 diff = target - this.transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
}
