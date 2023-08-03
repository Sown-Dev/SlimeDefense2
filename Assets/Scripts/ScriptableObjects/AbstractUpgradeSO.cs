using UnityEngine;

public  class UpgradeSO: ScriptableObject{
    [SerializeField] public virtual Upgrade u{
        get;
        set;
    }

    public bool Unlocked= true;

}