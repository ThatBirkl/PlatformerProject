using UnityEngine;
using System.Collections;

public class BlackBehaviour : PlayerBehaviour
{
     new void Awake()
    {
        base.Awake();
        SetCharacter();
    }

    #region SetCharacter
    /** Sets values for Black
    * For example Jump hight, or run speed
    * Values for Black and White will not always stay the same
    */
    protected override void SetCharacter()
    {
        moveSettings.runVelocity = MoveSettingsBlack.runVelocityBlack;
        moveSettings.jumpVelocity = MoveSettingsBlack.jumpVelocityBlack;
        character = false;
        GameManager.setCharacter(false, gameObject);
    }

    [System.Serializable]
    public class MoveSettingsBlack
    {
        public static float runVelocityBlack = 6;
        public static float jumpVelocityBlack = 6;
    }
    #endregion

    #region Abilities

    public override void Abilities()
    {
        EnergyBlast();
    }

    public void EnergyBlast()
    {

    }

    #endregion
}
