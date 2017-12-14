using UnityEngine;
using System.Collections;
using System;

public class WhiteBehaviour : PlayerBehaviour, RInterface
{
    private GameObject forcefieldPrefab;
    new void Awake()
    {
        base.Awake();
        SetCharacter();
        forcefieldPrefab = Resources.Load<GameObject>("Prefabs/Forcefield");
    }

    #region Set Character
    /** Sets values for White
    * For example Jump hight, or run speed
    * Values for Black and White will not always stay the same
    */
    protected override void SetCharacter()
    {
        moveSettings.runVelocity = MoveSettingsWhite.runVelocityWhite;
        moveSettings.jumpVelocity = MoveSettingsWhite.jumpVelocityWhite;
        character = true;
        GameManager.setCharacter(true, gameObject);
    }

    [System.Serializable]
    public class MoveSettingsWhite
    {
        public static float runVelocityWhite = 6;
        public static float jumpVelocityWhite = 8;
    }
    #endregion

    #region Abilities

    public override void Abilities()
    {
        Forcefield();
    }

    #region Rewind

    ///Those don't do anything for White; She isn't affected by her ability to reverse Time;
    ///Only the world around her is, such as enemies, moving platform etc
    public void LoadTRObject(RObject robject)
    {
        ;
    }

    public void SaveTRObject()
    {
        ;
    }

    public void Stop()
    {
        ;
    }

    public void Reactivate()
    {
        ;
    }
    #endregion

    #region Forcefield

    public void Forcefield()
    {
        if (GameManager.GiveEnergy(true) >= 0)
        {
            if ((GameManager.GivePrimary(true) == 2 && InputManager.GetPrimaryAbilityDown()) || (GameManager.GiveSecundary(true) == 2 && InputManager.GetSecundaryAbilityDown()))
            {
                Vector3 spawnPosition = gameObject.transform.position;
                spawnPosition.y = spawnPosition.y - (gameObject.transform.localScale.y * 0.3f);
                spawnPosition.x = spawnPosition.x - (gameObject.transform.localScale.x * 0.3f);
                Instantiate(forcefieldPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                protect = true;
            }
        }
    }

    #endregion

    #endregion
}
