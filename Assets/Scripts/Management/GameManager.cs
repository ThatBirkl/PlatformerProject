using UnityEngine; 
using System.Collections;

[RequireComponent(typeof(DropManager))]
public class GameManager : MonoBehaviour
{
    #region lives
    private static int livesWhite = 100;
    private static int livesBlack = 100;
    private static int maxLivesWhite = 100;
    private static int maxLivesBlack = 100;
    #endregion
    #region Money
    private static int moneyWhite = 0;
    private static int moneyBlack = 0;
    private static int bonesWhite = 0;
    private static int bonesBlack = 0;
    #endregion
    #region Energy
    //added  two 0 at the end to simulate the digits after the comma
    //for representation on screen I have to go with %100
    private static int energyWhite = 10000;
    private static int energyBlack = 5000;
    private static int maxEnergyWhite = 10000;
    private static int maxEnergyBlack = 5000;
    #endregion
    #region Abilities
    /// <Ability indices>
    /// 0   :   empty slot
    /// 1   :   b   energyblast; if energy ball doesn't hit enemy but a wall it turns into energy pickup
    /// 1   :   w   rewind
    /// 2   :   b   
    /// 2   :   w   forcefield
    /// </summary>

    //White
    public static int primaryWhite = 0;
    public static int secundaryWhite = 0;
    public static bool rewind = false;
    public static bool rewinding = false;
    public static bool forcefield = false;
    //Black
    public static int primaryBlack = 0;
    public static int secundaryBlack = 0;
    public static bool energyblast = false;
    #endregion
    #region MISC
    public static bool currentlyPlaying;
    public static GameObject player;
    #endregion

    //true ist bei allem einfach grundsätzlich immer White

    public GameManager()
    {
        Paused = false;
    }

    #region Functions for Lives
    public static void addLives(int add, bool character)
    {
        if (character) //character true, dann White
        {
            livesWhite = livesWhite + add;
            if (livesWhite > maxLivesWhite)
            {
                livesWhite = maxLivesWhite;
            }
            if (livesWhite <= 0)
            {
                player.GetComponent<WhiteBehaviour>().Respawn();
            }
        }
        else //character false, dann Black
        {
            livesBlack = livesBlack + add;
            if (livesBlack > maxLivesBlack)
            {
                livesBlack = maxLivesBlack;
            }
            if (livesBlack <= 0)
            {
                player.GetComponent<BlackBehaviour>().Respawn();
            }
        }
    }

    public static void addMaxLives(int add, bool character)
    {
        if (character)
        {
            maxLivesWhite = maxLivesWhite + add;
        }
        else
        {
            maxLivesBlack = maxLivesBlack + add;
        }
    }

    public static int GiveLives(bool character)
    {
        if (character)
        {
            return livesWhite;
        }
        else
        {
            return livesBlack;
        }

    }

    public static void ResetLives(bool character)
    {
        if (character)
        {
            livesWhite = maxLivesWhite;
        }
        else
        {
            livesBlack = maxLivesBlack;
        }
    }

    public static int GiveMaxLives(bool character)
    {
        if (character)
        {
            return maxLivesWhite;
        }
        else
        {
            return maxLivesBlack;
        }
    }
    #endregion

    #region Functions for Money
    public static void addMoney(int add, bool character)
    {
        if (character)
        {
            moneyWhite = moneyWhite + add;
        }
        else
        {
            moneyBlack = moneyBlack + add;
        }
    }

    public static void addBones(int add, bool character)
    {
        if (character)
        {
            bonesWhite = bonesWhite + add;
        }
        else
        {
            bonesBlack = bonesBlack + add;
        }
    }

    public static  int GiveMoney(bool character)
    {
        if (character)
        {
            return moneyWhite;
        }
        else
        {
            return moneyBlack;
        }
    }
    #endregion

    #region Functions for Energy
    public static void addEnergy(int add, bool character)
    {
        if (character)
        {
            energyWhite = energyWhite + add;
            if (energyWhite > maxEnergyWhite)
            {
                energyWhite = maxEnergyWhite;
            }
        }
        else
        {
            energyBlack = energyBlack + add;
            if (energyBlack > maxEnergyBlack)
            {
                energyBlack = maxEnergyBlack;
            }
        }
    }

    public static void addMaxEnergy(int add, bool character)
    {
        if (character)
        {
            maxEnergyWhite = maxEnergyWhite + add;
        }
        else
        {
            maxEnergyBlack = maxEnergyBlack + add;
        }
    }

    public static int GiveEnergy(bool character)
    {
        if (character)
        {
            return energyWhite;
        }
        else
        {
            return energyBlack;
        }
    }

    public static void ResetEnergy(bool character)
    {
        if (character)
        {
            energyWhite = maxEnergyWhite;
        }
        else
        {
            energyBlack = maxEnergyBlack;
        }
    }

    public static int GiveMaxEnergy(bool character)
    {
        if (character)
        {
            return maxEnergyWhite;
        }
        else
        {
            return maxEnergyBlack;
        }
    }
    #endregion

    #region Abilities

    public static int GivePrimary(bool character)
    {
        if (character)
        {
            return primaryWhite;
        }
        else
        {
            return primaryBlack;
        }
    }

    public static int GiveSecundary(bool character)
    {
        if (character)
        {
            return secundaryWhite;
        }
        else
        {
            return secundaryBlack;
        }
    }

    public static void SetAbilities(string ability, bool primary, bool character)
    {
        if (character) //White
        {
            if (primary)
            {
                if (ability.Equals("Nix"))
                {
                    primaryWhite = 0;
                }
                if (ability.Equals("Rewind") && rewind == true)
                {
                    primaryWhite = 1;
                }
                if (ability.Equals("Forcefield") && forcefield == true)
                {
                    primaryWhite = 2;
                }
            }
            else
            {
                if (ability.Equals("Nix"))
                {
                    secundaryWhite = 0;
                }
                if (ability.Equals("Rewind") && rewind == true)
                {
                    secundaryWhite = 1;
                }
                if (ability.Equals("Forcefield") && forcefield == true)
                {
                    secundaryWhite = 2;
                }
            }
        }
        else //Black
        {
            if (primary)
            {
                if (ability.Equals("Nix"))
                {
                    primaryBlack = 0;
                }
                if (ability.Equals("EnergBlast") && energyblast == true)
                {
                    primaryBlack = 1;
                }
            }
            else
            {
                if (ability.Equals("Nix"))
                {
                    secundaryBlack = 0;
                }
                if (ability.Equals("EnergyBlast") && energyblast == true)
                {
                    secundaryBlack = 1;
                }
            }
        }
    }

    public static void ActivateAbility(string ability)
    {
        if (ability.Equals("Rewind"))
        {
            rewind = true;
        }
        if (ability.Equals("Forcefield"))
        {
            forcefield = true;
        }
        if (ability.Equals("Energyblast"))
        {
            energyblast = true;
        }
    }


    #region Rewind
    public static bool Paused
    {
        get;
        set;
    }
    #endregion

    #region Forcefield
    #endregion

    #endregion

    #region MISC
    public static void setCharacter(bool character, GameObject _player)
    {
        currentlyPlaying = character;
        player = _player;
    }
    #endregion

    private void FixedUpdate()
    {
        if (rewinding && energyWhite > 0)
        {
            addEnergy(-10, true);
        }
    }
}
