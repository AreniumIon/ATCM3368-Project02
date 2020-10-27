using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Freezable : MonoBehaviour
{
    //Reference to frozen visuals/hitboxes (set in prefabs)
    [SerializeField] protected GameObject frozenObject;
    
    //General Freeze Variables
    public static float freezeDuration = 30f;
    public bool isFrozen = false;
    protected float timeFrozen = 0;

    protected void UpdateFreeze()
    {
        if (isFrozen)
        {
            if (timeFrozen >= freezeDuration)
            {
                Unfreeze();
                timeFrozen = 0;
            }
            else
                timeFrozen += Time.deltaTime;
        }
    }

    public abstract void Freeze();
    public abstract void Unfreeze();
    public void RefreshFreeze()
    {
        timeFrozen = 0;
    }

}
