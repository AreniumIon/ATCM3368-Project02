using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenObject : MonoBehaviour
{
    [SerializeField] Freezable freezable;

    public void RefreshFreeze()
    {
        freezable.RefreshFreeze();
    }

    public void Unfreeze()
    {
        freezable.Unfreeze();
    }
}
