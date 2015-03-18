﻿using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEditor;

public enum UType
{
    Infantry = 1,
    Jeep = 2,
    Tank = 3,
    SAM = 4,
    Turret = 5,
    SmallBase = 6,
    MainBase = 7,
    Shots = 8,
    PlayerPlane = 9,
    PlayerMech = 10
}

public enum WeaponsType
{
    Guns = 1,
    Missiles = 2,
    GunsAndMissiles = 3
}

public enum ProgramType
{
    StandGround = 1,
    Guard = 2,
    NearestBase = 3,
    AttackMain = 4,
    ShotFired = 5,
}

public abstract class Unit : Object
{
    protected Unit()
    {
        _CurTeam = "Neutral";
    }

    public Unit(string curTeam, ProgramType unitProgram, UType unitType)
    {
        _CurTeam = curTeam;
        _UnitType = unitType;
        _UnitProgram = unitProgram;
    }

    public string _CurTeam { get; set; }
    public UType _UnitType { get; set; }
    public ProgramType _UnitProgram { get; set; }

    public abstract bool _IsShootable { get; set; }
    public abstract bool _CanShoot { get; set; }
    public abstract bool _IsCapturable { get; set; }
    public abstract int _Life { get; set; }
    public abstract int _Energy { get; set; }
    public abstract int _Guns { get; set; }
    public abstract int _Missiles { get; set; }
    public abstract int _GunRange { get; set; }
    public abstract int _MissileRange { get; set; }
    public abstract int _GuardRange { get; set; }
    public abstract bool _CanMove { get; set; }
    public abstract bool _IsDead { get; set; }
    public abstract int _Cost { get; set; }
    public abstract int _Player1UnitCapture { get; set; }
    public abstract int _Player2UnitCapture { get; set; }
    public abstract float _ProductionTime { get; set; }
    public abstract ProgramType[] _PossiblePrograms { get; set; }
    public abstract WeaponsType _Weapons { get; set; }
    public abstract int _GunAttackDamage { get; set; }
    public abstract int _MissileAttackDamage { get; set; }
    public abstract int _CargoSpaceOfUnit { get; set; }
    public abstract int _CargoSpace { get; set; }
    
    public abstract GameObject[] _SmallBaseArray { get; set; }
    public abstract GameObject[] _MainBaseArray { get; set; }
    public abstract Transform _CurrentTransform { get; set; }
    public abstract Transform _DropTransform { get; set; }
    public abstract GameObject _CurTarget { get; set; }
    public abstract GameObject _CurDestination { get; set; }
    public abstract NavMeshAgent _Nav { get; set; }
    public abstract AreaLightColor[] _AreaLightsArray { get; set; }
    public abstract MiniMapBeacon _MiniMapBeacon { get; set; }
    public abstract Transform _TargetTransformOffset { get; set; }
    public abstract Transform _TargetTransform { get; set; }
    public abstract Transform _ShotOriginTransform1 { get; set; }
    public abstract Transform _ShotOriginTransform2 { get; set; }
    public abstract bool _CanTransform { get; set; }
    public abstract float _TransformWaitTime { get; set; }
    public abstract float _NextTimeToTransform { get; set; }
    public abstract float _NextPickUpAfterDropOff { get; set; }
    public abstract float _TimeBetweenDropOffAndPickUp { get; set; }
    public abstract bool _CurrentlyRegenerating { get; set; }

    public abstract GameObject[] _Cargo { get; set; }

    public void Targeting(GameObject possibleTarget)
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        if (_IsShootable && !_IsDead)
        {
            _Life -= damageAmount;
            if (_Life <= 0)
            {
                _IsDead = true;
            }
        }
    }
    public GameObject Shoot(GameObject curTargetGameObject)
    {
        if (_CanShoot && !_IsDead)
        {

        }
        return curTargetGameObject;
    }

    public GameObject Move(GameObject gameContObject, GameObject curClosestGameObject, Transform curUnitTransform)
    {
        UnityEngine.Debug.Log("Running Move Function");
        if (_CanMove && !_IsDead && _UnitProgram != ProgramType.StandGround)
        {
            //UnityEngine.Debug.Log("First If Statement, Can Move");
            //if (curClosestGameObject.GetComponent<UnitController>() = null && curClosestGameObject.GetComponent<UnitController>().ThisUnit._CurTeam != _CurTeam && !curClosestGameObject.GetComponent<UnitController>().ThisUnit._IsDead)
            //{
            switch (_UnitProgram)
            {
                case ProgramType.Guard:

                    break;
                case ProgramType.NearestBase:
                    //NearestBase

                    //UnityEngine.Debug.Log("Nearest Base Case");
                    //UnityEngine.Debug.Log(string.Format("FindingNearestBase Function Ran"));
                    {
                        float distance = 4000f;
                        GameObject tempClosestBase = null;
                        //Debug.Log(string.Format("FindingNearestBase Function If UnitProgram is NearestBase"));
                        for (int k = 0; k < _SmallBaseArray.Length; k++)
                        {
                            //UnityEngine.Debug.Log(string.Format("Small Base Test: {0}", k));
                           // UnityEngine.Debug.Log(string.Format("Small Base Test if Null: {0}",_SmallBaseArray[k].GetComponent<UnitController>() == null));
                            //UnityEngine.Debug.Log(string.Format("Small Base Test if unitGame Object is Null: {0}",_CurrentTransform == null));
                            if (_SmallBaseArray[k].GetComponent<UnitController>() != null && _CurTeam != null && _SmallBaseArray[k].GetComponent<UnitController>().ThisUnit._CurTeam != _CurTeam)
                            {
                                if (distance >=
                                    Vector3.Distance(_SmallBaseArray[k].transform.position, _CurrentTransform.position))
                                {
                                    distance = Vector3.Distance(gameContObject.GetComponent<GameController>().smallBases[k].transform.position,
                                        _CurrentTransform.position);
                                    tempClosestBase = _SmallBaseArray[k];
                                    //UnityEngine.Debug.Log(string.Format("Closest Small Base {0}", k));
                                }
                            }
                        }


                        for (int z = 0; z < _MainBaseArray.Length; z++)
                        {
                            //UnityEngine.Debug.Log(string.Format("Testing Main Base {0}", z));
                            if (_MainBaseArray[z].GetComponent<UnitController>() != null && _CurTeam != null && _MainBaseArray[z].GetComponent<UnitController>().ThisUnit._CurTeam != _CurTeam)
                            {
                                if (distance >=
                                    Vector3.Distance(gameContObject.GetComponent<GameController>().mainBases[z].transform.position, _CurrentTransform.position))
                                {
                                    distance = Vector3.Distance(_MainBaseArray[z].transform.position,
                                        _CurrentTransform.position);
                                    tempClosestBase = _MainBaseArray[z];
                                    //UnityEngine.Debug.Log(string.Format("Closest Main Base {0}", z));
                                }
                            }
                        }

                        if (tempClosestBase != null)
                        {
                            //UnityEngine.Debug.Log(string.Format("Closest Base Returned: {0}", tempClosestBase.transform.position));
                        }

                        curClosestGameObject = tempClosestBase;

                        if (_UnitType == UType.Infantry && curClosestGameObject.GetComponent<UnitController>().ThisUnit._UnitType == UType.SmallBase)
                        {
                            _Nav.SetDestination(new Vector3(curClosestGameObject.transform.position.x + 10.2f, curClosestGameObject.transform.position.y, curClosestGameObject.transform.position.z + 10.2f));
                        }
                        else
                        {
                            _Nav.SetDestination(curClosestGameObject.transform.position);
                        }

                    }
                    break; //NearestBase



                case ProgramType.AttackMain:
                    curClosestGameObject = gameContObject.GetComponent<GameController>()
                        .FindNearestBase(_UnitGameObject);
                    break;

            }
            //}
        }
        return curClosestGameObject;
    }
    public void Death()
    {
        if (_IsDead)
        {
            _CanMove = false;
            _CanShoot = false;
            _IsShootable = false;
            if (_UnitType == UType.PlayerPlane || _UnitType == UType.PlayerMech)
            {
                _UnitGameObject.SetActive(false);
                //! Have an Explosion Have camera Quickly Pan back to base (Or just have ship quickly Move back, because camera will follow)
            }
            else
            {
                //! Have and Explosion and or a slow transition through the terrain
                Destroy(_UnitGameObject);
            }
        }
    }

    // If unit dies will re-spawn at base
    public void Respawn()
    {
        Transform tempRespawnTransform = GameObject.FindGameObjectWithTag(_CurTeam + "PlayerSpawnPoint").transform;
        if (_UnitType == UType.PlayerPlane)
        {
            _UnitGameObject.transform.position = tempRespawnTransform.position;
            _UnitGameObject.transform.rotation = tempRespawnTransform.rotation;
            //! Put Delay and something for re-spawn effect
            _UnitGameObject.SetActive(true);
            // Re-initialize PlayerValues
            _UnitGameObject.GetComponent<UnitController>().ThisUnit = new PlayerPlane(_CurTeam, ProgramType.StandGround, _UnitGameObject);
        }
        else
        {
            //! Put Delay and something for re-spawn effect
            //! Create new PlayerPlane Unit at tempRespawnTransform
            Destroy(_UnitGameObject);
        }
    }


    // Might need to implement this in the UnitController instead of the unit Class
    // Run when you hit the button for Transform/DropOff and PickUp
    public void TransformPickUpDropOff(GameObject unitPickUp = null)
    {
        // Checks if it is a PLayer and it can Transform
        // If it can't Transform then, it can't drop off units
        if (_CanTransform && (_UnitType == UType.PlayerPlane || _UnitType == UType.PlayerMech))
        {
            // Tests if there is cargo(to drop-off) or a unit for pickup
            if (_Cargo != null || unitPickUp != null)
            {
                // Tests If your picking up after dropping off or vice-versa too soon
                // To prevent immediate pickup after drop off or vice-versa
                if (_TimeBetweenDropOffAndPickUp <= Time.time)
                {
                    // Test if there is a unit to pickup and you have enough space in your cargohold for it
                    if (unitPickUp != null && _CargoSpace <= unitPickUp.GetComponent<UnitController>().ThisUnit._CargoSpaceOfUnit)
                    {
                        _NextPickUpAfterDropOff = Time.time + _TimeBetweenDropOffAndPickUp;
                        //! Pickup Unit and store in Cargo and Decrement CargoSpaceOfUnit
                        //! Need to save all attributes of the unit in the cargo bay
                    }
                    // If there is no unit you are trying to pickup and you have cargo
                    else if (_Cargo != null && unitPickUp == null)
                    {
                        _NextPickUpAfterDropOff = Time.time + _TimeBetweenDropOffAndPickUp;
                        //! DropOff unit and Increment CargoSpaceOfUnit
                        //! Need to set all the attributes of the unit to the new instantiation of the unit.
                    }
                }
            }
            // If there is no Cargo and there is no Unit to PickUP - TRANSFORM (More than meets the eye)
            else 
            {
                // Tests if your transforming too soon
                if (_NextTimeToTransform <= Time.time)
                {
                    // Tests which form the Player is currently in and Change to the opposite
                    if (_UnitType == UType.PlayerPlane)
                    {
                        _NextTimeToTransform = Time.time + _TransformWaitTime;
                        //! Change to PlayerMech
                        //! Pass all the (Life, Energy, Guns, and _NextTimeToTransform ) of Plane to Mech
                        Destroy(_UnitGameObject);
                    }
                    else
                    {
                        _NextTimeToTransform = Time.time + _TransformWaitTime;
                        //! Change to PlayerPlane
                        //! Pass all the (Life, Energy, Guns, and _NextTimeToTransform ) of Mech to Plane
                        Destroy(_UnitGameObject);
                    }
                }
            }
        }
    }

    public void RegenUnitOrderPickUp()
    {
        // If Currently Over base YouCan't Transfrom and Your Currently Regenerating
        if (!_CanTransform && _CurrentlyRegenerating)
        {
            //! Regenerate Life, Guns, Energy
            //! Pickup Unit that is Ordered
            
        }
    }

    /// <summary>
    /// Bases the capture.
    /// </summary>
    /// <param name="otherGameObject">The other game object.</param>
    public void BaseCapture(GameObject otherGameObject)
    {
        // Tests if Current Unit is a SmallBase
        if (_UnitType == UType.SmallBase)
        {
            // Tests if the unit that entered the base is on Player1's Team 
            if (otherGameObject.GetComponent<UnitController>().ThisUnit._CurTeam == "Player1")
            {
                // Tests if the # of units in the base is less than 4
                if (_Player1UnitCapture < 4)
                {
                    // Removes the infantry unit that entered the base
                    DestroyObject(otherGameObject);
                    // Increments the _Player1UnitCapture
                    _Player1UnitCapture += 1;

                    // Switch is used to change the color of the Spheres that indicate the # of units for Player1 are in the base
                    switch (_Player1UnitCapture)
                    {
                        case 1:
                            _UnitGameObject.GetComponentInChildren<Player1UnitCapture>().GetComponentInChildren<Sphere1>().ColorCaptured("Player1");
                            break;
                        case 2:
                            _UnitGameObject.GetComponentInChildren<Player1UnitCapture>().GetComponentInChildren<Sphere2>().ColorCaptured("Player1");
                            break;
                        case 3:
                            _UnitGameObject.GetComponentInChildren<Player1UnitCapture>().GetComponentInChildren<Sphere3>().ColorCaptured("Player1");
                            break;
                        case 4:
                            _UnitGameObject.GetComponentInChildren<Player1UnitCapture>().GetComponentInChildren<Sphere4>().ColorCaptured("Player1");
                            break;
                    }
                    
                    // Tests if Player2 has any units in the base
                    if (_Player2UnitCapture > 0)
                    {
                        // Changes the Spheres for Player 2 back to a Neutral color to show how many units are needed to recapture/prevent capture
                        switch (_Player2UnitCapture)
                        {
                            case 1:
                                _UnitGameObject.GetComponentInChildren<Player2UnitCapture>().GetComponentInChildren<Sphere1>().ColorCaptured("Neutral");
                                break;
                            case 2:
                                _UnitGameObject.GetComponentInChildren<Player2UnitCapture>().GetComponentInChildren<Sphere2>().ColorCaptured("Neutral");
                                break;
                            case 3:
                                _UnitGameObject.GetComponentInChildren<Player2UnitCapture>().GetComponentInChildren<Sphere3>().ColorCaptured("Neutral");
                                break;
                            case 4:
                                _UnitGameObject.GetComponentInChildren<Player2UnitCapture>().GetComponentInChildren<Sphere4>().ColorCaptured("Neutral");
                                break;
                        }
                        // Decrements _PLayer2UnitCapture
                        _Player2UnitCapture -= 1;
                    }
                }
            }
            else // Since the Unit isn't for Player1 then it figures it must be Player2's Infantry Unit
            {
                // Tests if the # of units in the base is less than 4
                if (_Player2UnitCapture < 4)
                {
                    // Removes the infantry unit that entered the base
                    DestroyObject(otherGameObject);
                    // Increments the _Player2UnitCapture
                    _Player2UnitCapture += 1;
                    // Switch is used to change the color of the Spheres that indicate the # of units for Player2 are in the base
                    switch (_Player2UnitCapture)
                    {
                        case 1:
                            _UnitGameObject.GetComponentInChildren<Player2UnitCapture>().GetComponentInChildren<Sphere1>().ColorCaptured("Player2");
                            break;
                        case 2:
                            _UnitGameObject.GetComponentInChildren<Player2UnitCapture>().GetComponentInChildren<Sphere2>().ColorCaptured("Player2");
                            break;
                        case 3:
                            _UnitGameObject.GetComponentInChildren<Player2UnitCapture>().GetComponentInChildren<Sphere3>().ColorCaptured("Player2");
                            break;
                        case 4:
                            _UnitGameObject.GetComponentInChildren<Player2UnitCapture>().GetComponentInChildren<Sphere4>().ColorCaptured("Player2");
                            break;
                    }
                    // Tests if Player1 has any units in the base
                    if (_Player1UnitCapture > 0)
                    {
                        // Changes the Spheres for Player1 back to a Neutral color to show how many units are needed to recapture/prevent capture
                        switch (_Player1UnitCapture)
                        {
                            case 1:
                                _UnitGameObject.GetComponentInChildren<Player1UnitCapture>().GetComponentInChildren<Sphere1>().ColorCaptured("Neutral");
                                break;
                            case 2:
                                _UnitGameObject.GetComponentInChildren<Player1UnitCapture>().GetComponentInChildren<Sphere2>().ColorCaptured("Neutral");
                                break;
                            case 3:
                                _UnitGameObject.GetComponentInChildren<Player1UnitCapture>().GetComponentInChildren<Sphere3>().ColorCaptured("Neutral");
                                break;
                            case 4:
                                _UnitGameObject.GetComponentInChildren<Player1UnitCapture>().GetComponentInChildren<Sphere4>().ColorCaptured("Neutral");
                                break;
                        }
                        // Decrements _Player1UnitCapture
                        _Player1UnitCapture -= 1;
                    }
                }
            }
            // When _Player1UnitCapture = 4 then switch team to Player1
            if (_Player1UnitCapture == 4)
            {
                UnityEngine.Debug.Log("Building Captured for Player1");
                _CurTeam = "Player1";
                _MiniMapBeacon.ColorCaptured("Player1");
                foreach (var theLights in _AreaLightsArray)
                {
                    theLights.ColorCaptured("Player1");
                }
            }

            // When _Player2UnitCapture = 4 then switch team to Player2
            if (_Player2UnitCapture == 4)
            {
                UnityEngine.Debug.Log("Building Captured for Player2");
                _CurTeam = "Player2";
                _MiniMapBeacon.ColorCaptured("Player2");
                foreach (var theLights in _AreaLightsArray)
                {
                    theLights.ColorCaptured("Player2");
                }
            }
        }
    }
    public abstract GameObject _UnitGameObject { get; set; }


}

// Infantry movement class
public sealed class Infantry : Unit
{
    private const int MaxUnitLife = 300;
    private const int MaxUnitEnergy = 100;
    private const int MaxUnitGuns = 100;
    private const int MaxUnitMissiles = 0;
    private const int GunDamage = 8;
    private const int MissileDamage = 0;
    private const int CargoSpaceOfUnit = 1;
    private const int CargoSpace = 0;
    private const GameObject[] Cargo = null;
    private const WeaponsType Weapons = WeaponsType.Guns;
    private const int Cost = 40;
    private const float ProductionTIme = 6;
    private readonly ProgramType[] PossibleProgTypes = new ProgramType[4] 
    {
        ProgramType.StandGround,
        ProgramType.Guard,
        ProgramType.NearestBase,
        ProgramType.AttackMain
    };

    public Infantry()
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = MaxUnitLife;
        _Energy = MaxUnitEnergy;
        _Missiles = MaxUnitMissiles;
        _Guns = MaxUnitGuns;
        _CargoSpaceOfUnit = CargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = Weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = true;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
        _UnitGameObject = null;
    }

    public Infantry(string curTeam, ProgramType unitProgram, GameObject unitGameObject, UType unitType = UType.Infantry, int life = MaxUnitLife, int energy = MaxUnitEnergy, int guns = MaxUnitGuns, int missiles = MaxUnitMissiles, WeaponsType weapons = WeaponsType.Guns, int cargoSpaceOfUnit = CargoSpaceOfUnit)
        : base(curTeam, unitProgram, unitType)
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = life;
        _Energy = energy;
        _Missiles = MaxUnitMissiles;
        _Guns = guns;
        _CargoSpaceOfUnit = cargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = true;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
        _UnitGameObject = unitGameObject;
    }

    public override bool _IsShootable { get; set; }
    public override bool _CanShoot { get; set; }
    public override bool _IsCapturable { get; set; }
    public override int _Life { get; set; }
    public override int _Energy { get; set; }
    public override int _Guns { get; set; }
    public override int _Missiles { get; set; }
    public override int _GunRange { get; set; }
    public override int _MissileRange { get; set; }
    public override int _GuardRange { get; set; }
    public override bool _CanMove { get; set; }
    public override bool _IsDead { get; set; }
    public override int _Cost { get; set; }
    public override int _Player1UnitCapture { get; set; }
    public override int _Player2UnitCapture { get; set; }
    public override float _ProductionTime { get; set; }
    public override ProgramType[] _PossiblePrograms { get; set; }
    public override WeaponsType _Weapons { get; set; }
    public override int _GunAttackDamage { get; set; }
    public override int _MissileAttackDamage { get; set; }
    public override int _CargoSpaceOfUnit { get; set; }
    public override int _CargoSpace { get; set; }

    public override GameObject[] _SmallBaseArray { get; set; }
    public override GameObject[] _MainBaseArray { get; set; }
    public override Transform _CurrentTransform { get; set; }
    public override Transform _DropTransform { get; set; }
    public override GameObject _CurTarget { get; set; }
    public override GameObject _CurDestination { get; set; }
    public override NavMeshAgent _Nav { get; set; }
    public override AreaLightColor[] _AreaLightsArray { get; set; }
    public override MiniMapBeacon _MiniMapBeacon { get; set; }
    public override Transform _TargetTransformOffset { get; set; }
    public override Transform _TargetTransform { get; set; }
    public override Transform _ShotOriginTransform1 { get; set; }
    public override Transform _ShotOriginTransform2 { get; set; }
    public override bool _CanTransform { get; set; }
    public override float _TransformWaitTime { get; set; }
    public override float _NextTimeToTransform { get; set; }
    public override float _NextPickUpAfterDropOff { get; set; }
    public override float _TimeBetweenDropOffAndPickUp { get; set; }
    public override bool _CurrentlyRegenerating { get; set; }

    public override GameObject[] _Cargo { get; set; }

    public override GameObject _UnitGameObject { get; set; }
}

public sealed class Jeep : Unit
{
    private const int MaxUnitLife = 300;
    private const int MaxUnitEnergy = 100;
    private const int MaxUnitGuns = 100;
    private const int MaxUnitMissiles = 0;
    private const int GunDamage = 8;
    private const int MissileDamage = 0;
    private const int CargoSpaceOfUnit = 1;
    private const int CargoSpace = 0;
    private const GameObject[] Cargo = null;
    private const WeaponsType Weapons = WeaponsType.Guns;
    private const int Cost = 40;
    private const float ProductionTIme = 6;
    private readonly ProgramType[] PossibleProgTypes = new ProgramType[4] 
    {
        ProgramType.StandGround,
        ProgramType.Guard,
        ProgramType.NearestBase,
        ProgramType.AttackMain
    };

    public Jeep()
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = MaxUnitLife;
        _Energy = MaxUnitEnergy;
        _Missiles = MaxUnitMissiles;
        _Guns = MaxUnitGuns;
        _CargoSpaceOfUnit = CargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = Weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = true;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
    }

    public Jeep(string curTeam, ProgramType unitProgram, GameObject unitGameObject, UType unitType = UType.Jeep, int life = MaxUnitLife, int energy = MaxUnitEnergy,
        int guns = MaxUnitGuns, int missiles = MaxUnitMissiles, WeaponsType weapons = WeaponsType.Guns, int cargoSpaceOfUnit = CargoSpaceOfUnit)
        : base(curTeam, unitProgram, unitType)
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = life;
        _Energy = energy;
        _Missiles = MaxUnitMissiles;
        _Guns = guns;
        _CargoSpaceOfUnit = cargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = true;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
        _UnitGameObject = unitGameObject;

    }

    public override bool _IsShootable { get; set; }
    public override bool _CanShoot { get; set; }
    public override bool _IsCapturable { get; set; }
    public override int _Life { get; set; }
    public override int _Energy { get; set; }
    public override int _Guns { get; set; }
    public override int _Missiles { get; set; }
    public override int _GunRange { get; set; }
    public override int _MissileRange { get; set; }
    public override int _GuardRange { get; set; }
    public override bool _CanMove { get; set; }
    public override bool _IsDead { get; set; }
    public override int _Cost { get; set; }
    public override int _Player1UnitCapture { get; set; }
    public override int _Player2UnitCapture { get; set; }
    public override float _ProductionTime { get; set; }
    public override ProgramType[] _PossiblePrograms { get; set; }
    public override WeaponsType _Weapons { get; set; }
    public override int _GunAttackDamage { get; set; }
    public override int _MissileAttackDamage { get; set; }
    public override int _CargoSpaceOfUnit { get; set; }
    public override int _CargoSpace { get; set; }

    public override GameObject[] _SmallBaseArray { get; set; }
    public override GameObject[] _MainBaseArray { get; set; }
    public override Transform _CurrentTransform { get; set; }
    public override Transform _DropTransform { get; set; }
    public override GameObject _CurTarget { get; set; }
    public override GameObject _CurDestination { get; set; }
    public override NavMeshAgent _Nav { get; set; }
    public override AreaLightColor[] _AreaLightsArray { get; set; }
    public override MiniMapBeacon _MiniMapBeacon { get; set; }
    public override Transform _TargetTransformOffset { get; set; }
    public override Transform _TargetTransform { get; set; }
    public override Transform _ShotOriginTransform1 { get; set; }
    public override Transform _ShotOriginTransform2 { get; set; }
    public override bool _CanTransform { get; set; }
    public override float _TransformWaitTime { get; set; }
    public override float _NextTimeToTransform { get; set; }
    public override float _NextPickUpAfterDropOff { get; set; }
    public override float _TimeBetweenDropOffAndPickUp { get; set; }
    public override bool _CurrentlyRegenerating { get; set; }

    public override GameObject[] _Cargo { get; set; }

    public override GameObject _UnitGameObject { get; set; }
}

// Tank Unit Class
public sealed class Tank : Unit
{
    private const int MaxUnitLife = 300;
    private const int MaxUnitEnergy = 100;
    private const int MaxUnitGuns = 100;
    private const int MaxUnitMissiles = 0;
    private const int GunDamage = 8;
    private const int MissileDamage = 0;
    private const int CargoSpaceOfUnit = 1;
    private const int CargoSpace = 0;
    private const GameObject[] Cargo = null;
    private const WeaponsType Weapons = WeaponsType.Guns;
    private const int Cost = 40;
    private const float ProductionTIme = 6;
    private readonly ProgramType[] PossibleProgTypes = new ProgramType[4] 
    {
        ProgramType.StandGround,
        ProgramType.Guard,
        ProgramType.NearestBase,
        ProgramType.AttackMain
    };

    public Tank()
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = MaxUnitLife;
        _Energy = MaxUnitEnergy;
        _Missiles = MaxUnitMissiles;
        _Guns = MaxUnitGuns;
        _CargoSpaceOfUnit = CargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = Weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = true;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
    }

    public Tank(string curTeam, ProgramType unitProgram, GameObject unitGameObject, UType unitType = UType.Tank, int life = MaxUnitLife, int energy = MaxUnitEnergy,
        int guns = MaxUnitGuns, int missiles = MaxUnitMissiles, WeaponsType weapons = WeaponsType.Guns, int cargoSpaceOfUnit = CargoSpaceOfUnit)
        : base(curTeam, unitProgram, unitType)
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = life;
        _Energy = energy;
        _Missiles = MaxUnitMissiles;
        _Guns = guns;
        _CargoSpaceOfUnit = cargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = true;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
        _UnitGameObject = unitGameObject;

    }

    public override bool _IsShootable { get; set; }
    public override bool _CanShoot { get; set; }
    public override bool _IsCapturable { get; set; }
    public override int _Life { get; set; }
    public override int _Energy { get; set; }
    public override int _Guns { get; set; }
    public override int _Missiles { get; set; }
    public override int _GunRange { get; set; }
    public override int _MissileRange { get; set; }
    public override int _GuardRange { get; set; }
    public override bool _CanMove { get; set; }
    public override bool _IsDead { get; set; }
    public override int _Cost { get; set; }
    public override int _Player1UnitCapture { get; set; }
    public override int _Player2UnitCapture { get; set; }
    public override float _ProductionTime { get; set; }
    public override ProgramType[] _PossiblePrograms { get; set; }
    public override WeaponsType _Weapons { get; set; }
    public override int _GunAttackDamage { get; set; }
    public override int _MissileAttackDamage { get; set; }
    public override int _CargoSpaceOfUnit { get; set; }
    public override int _CargoSpace { get; set; }

    public override GameObject[] _SmallBaseArray { get; set; }
    public override GameObject[] _MainBaseArray { get; set; }
    public override Transform _CurrentTransform { get; set; }
    public override Transform _DropTransform { get; set; }
    public override GameObject _CurTarget { get; set; }
    public override GameObject _CurDestination { get; set; }
    public override NavMeshAgent _Nav { get; set; }
    public override AreaLightColor[] _AreaLightsArray { get; set; }
    public override MiniMapBeacon _MiniMapBeacon { get; set; }
    public override Transform _TargetTransformOffset { get; set; }
    public override Transform _TargetTransform { get; set; }
    public override Transform _ShotOriginTransform1 { get; set; }
    public override Transform _ShotOriginTransform2 { get; set; }
    public override bool _CanTransform { get; set; }
    public override float _TransformWaitTime { get; set; }
    public override float _NextTimeToTransform { get; set; }
    public override float _NextPickUpAfterDropOff { get; set; }
    public override float _TimeBetweenDropOffAndPickUp { get; set; }
    public override bool _CurrentlyRegenerating { get; set; }

    public override GameObject[] _Cargo { get; set; }

    public override GameObject _UnitGameObject { get; set; }
}

public sealed class SAM : Unit
{
    private const int MaxUnitLife = 300;
    private const int MaxUnitEnergy = 100;
    private const int MaxUnitGuns = 100;
    private const int MaxUnitMissiles = 0;
    private const int GunDamage = 8;
    private const int MissileDamage = 0;
    private const int CargoSpaceOfUnit = 1;
    private const int CargoSpace = 0;
    private const GameObject[] Cargo = null;
    private const WeaponsType Weapons = WeaponsType.Guns;
    private const int Cost = 40;
    private const float ProductionTIme = 6;
    private readonly ProgramType[] PossibleProgTypes = 
    {
        ProgramType.StandGround,
        ProgramType.Guard,
        ProgramType.NearestBase,
        ProgramType.AttackMain
    };

    public SAM()
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = MaxUnitLife;
        _Energy = MaxUnitEnergy;
        _Missiles = MaxUnitMissiles;
        _Guns = MaxUnitGuns;
        _CargoSpaceOfUnit = CargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = Weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = true;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
    }

    public SAM(string curTeam, ProgramType unitProgram, GameObject unitGameObject, UType unitType = UType.SAM, int life = MaxUnitLife, int energy = MaxUnitEnergy,
        int guns = MaxUnitGuns, int missiles = MaxUnitMissiles, WeaponsType weapons = WeaponsType.Guns, int cargoSpaceOfUnit = CargoSpaceOfUnit)
        : base(curTeam, unitProgram, unitType)
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = life;
        _Energy = energy;
        _Missiles = MaxUnitMissiles;
        _Guns = guns;
        _CargoSpaceOfUnit = cargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = true;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
        _UnitGameObject = unitGameObject;

    }

    public override bool _IsShootable { get; set; }
    public override bool _CanShoot { get; set; }
    public override bool _IsCapturable { get; set; }
    public override int _Life { get; set; }
    public override int _Energy { get; set; }
    public override int _Guns { get; set; }
    public override int _Missiles { get; set; }
    public override int _GunRange { get; set; }
    public override int _MissileRange { get; set; }
    public override int _GuardRange { get; set; }
    public override bool _CanMove { get; set; }
    public override bool _IsDead { get; set; }
    public override int _Cost { get; set; }
    public override int _Player1UnitCapture { get; set; }
    public override int _Player2UnitCapture { get; set; }
    public override float _ProductionTime { get; set; }
    public override ProgramType[] _PossiblePrograms { get; set; }
    public override WeaponsType _Weapons { get; set; }
    public override int _GunAttackDamage { get; set; }
    public override int _MissileAttackDamage { get; set; }
    public override int _CargoSpaceOfUnit { get; set; }
    public override int _CargoSpace { get; set; }

    public override GameObject[] _SmallBaseArray { get; set; }
    public override GameObject[] _MainBaseArray { get; set; }
    public override Transform _CurrentTransform { get; set; }
    public override Transform _DropTransform { get; set; }
    public override GameObject _CurTarget { get; set; }
    public override GameObject _CurDestination { get; set; }
    public override NavMeshAgent _Nav { get; set; }
    public override AreaLightColor[] _AreaLightsArray { get; set; }
    public override MiniMapBeacon _MiniMapBeacon { get; set; }
    public override Transform _TargetTransformOffset { get; set; }
    public override Transform _TargetTransform { get; set; }
    public override Transform _ShotOriginTransform1 { get; set; }
    public override Transform _ShotOriginTransform2 { get; set; }
    public override bool _CanTransform { get; set; }
    public override float _TransformWaitTime { get; set; }
    public override float _NextTimeToTransform { get; set; }
    public override float _NextPickUpAfterDropOff { get; set; }
    public override float _TimeBetweenDropOffAndPickUp { get; set; }
    public override bool _CurrentlyRegenerating { get; set; }

    public override GameObject[] _Cargo { get; set; }

    public override GameObject _UnitGameObject { get; set; }
}


public sealed class Turret : Unit
{
    private const int MaxUnitLife = 300;
    private const int MaxUnitEnergy = 100;
    private const int MaxUnitGuns = 100;
    private const int MaxUnitMissiles = 0;
    private const int GunDamage = 8;
    private const int MissileDamage = 0;
    private const int CargoSpaceOfUnit = 1;
    private const int CargoSpace = 0;
    private const GameObject[] Cargo = null;
    private const WeaponsType Weapons = WeaponsType.Guns;
    private const int Cost = 40;
    private const float ProductionTIme = 6;
    private readonly ProgramType[] PossibleProgTypes = 
    {
        ProgramType.StandGround
    };

    public Turret()
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = MaxUnitLife;
        _Energy = MaxUnitEnergy;
        _Missiles = MaxUnitMissiles;
        _Guns = MaxUnitGuns;
        _CargoSpaceOfUnit = CargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = Weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = false;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
    }

    public Turret(string curTeam, ProgramType unitProgram, GameObject unitGameObject, UType unitType = UType.Turret, int life = MaxUnitLife, int energy = MaxUnitEnergy,
        int guns = MaxUnitGuns, int missiles = MaxUnitMissiles, WeaponsType weapons = WeaponsType.Guns, int cargoSpaceOfUnit = CargoSpaceOfUnit)
        : base(curTeam, unitProgram, unitType)
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = life;
        _Energy = energy;
        _Missiles = MaxUnitMissiles;
        _Guns = guns;
        _CargoSpaceOfUnit = cargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = false;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
        _UnitGameObject = unitGameObject;

    }

    public override bool _IsShootable { get; set; }
    public override bool _CanShoot { get; set; }
    public override bool _IsCapturable { get; set; }
    public override int _Life { get; set; }
    public override int _Energy { get; set; }
    public override int _Guns { get; set; }
    public override int _Missiles { get; set; }
    public override int _GunRange { get; set; }
    public override int _MissileRange { get; set; }
    public override int _GuardRange { get; set; }
    public override bool _CanMove { get; set; }
    public override bool _IsDead { get; set; }
    public override int _Cost { get; set; }
    public override int _Player1UnitCapture { get; set; }
    public override int _Player2UnitCapture { get; set; }
    public override float _ProductionTime { get; set; }
    public override ProgramType[] _PossiblePrograms { get; set; }
    public override WeaponsType _Weapons { get; set; }
    public override int _GunAttackDamage { get; set; }
    public override int _MissileAttackDamage { get; set; }
    public override int _CargoSpaceOfUnit { get; set; }
    public override int _CargoSpace { get; set; }

    public override GameObject[] _SmallBaseArray { get; set; }
    public override GameObject[] _MainBaseArray { get; set; }
    public override Transform _CurrentTransform { get; set; }
    public override Transform _DropTransform { get; set; }
    public override GameObject _CurTarget { get; set; }
    public override GameObject _CurDestination { get; set; }
    public override NavMeshAgent _Nav { get; set; }
    public override AreaLightColor[] _AreaLightsArray { get; set; }
    public override MiniMapBeacon _MiniMapBeacon { get; set; }
    public override Transform _TargetTransformOffset { get; set; }
    public override Transform _TargetTransform { get; set; }
    public override Transform _ShotOriginTransform1 { get; set; }
    public override Transform _ShotOriginTransform2 { get; set; }
    public override bool _CanTransform { get; set; }
    public override float _TransformWaitTime { get; set; }
    public override float _NextTimeToTransform { get; set; }
    public override float _NextPickUpAfterDropOff { get; set; }
    public override float _TimeBetweenDropOffAndPickUp { get; set; }
    public override bool _CurrentlyRegenerating { get; set; }

    public override GameObject[] _Cargo { get; set; }

    public override GameObject _UnitGameObject { get; set; }
}


public sealed class SmallBase : Unit
{
    private const int MaxUnitLife = 300;
    private const int MaxUnitEnergy = 100;
    private const int MaxUnitGuns = 100;
    private const int MaxUnitMissiles = 0;
    private const int GunDamage = 8;
    private const int MissileDamage = 0;
    private const int CargoSpaceOfUnit = 1;
    private const int CargoSpace = 0;
    private const GameObject[] Cargo = null;
    private const WeaponsType Weapons = WeaponsType.Guns;
    private const int Cost = 40;
    private const float ProductionTIme = 6;
    private readonly ProgramType[] PossibleProgTypes = 
    {
        ProgramType.StandGround,
        ProgramType.Guard,
        ProgramType.NearestBase,
        ProgramType.AttackMain
    };

    public SmallBase()
    {
        _IsShootable = false;
        _CanShoot = false;
        _IsCapturable = true;
        _Life = MaxUnitLife;
        _Energy = MaxUnitEnergy;
        _Missiles = MaxUnitMissiles;
        _Guns = MaxUnitGuns;
        _CargoSpaceOfUnit = CargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = Weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = false;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
    }

    public SmallBase(string curTeam, ProgramType unitProgram, GameObject unitGameObject, UType unitType = UType.SmallBase, int life = MaxUnitLife, int energy = MaxUnitEnergy,
        int guns = MaxUnitGuns, int missiles = MaxUnitMissiles, WeaponsType weapons = WeaponsType.Guns, int cargoSpaceOfUnit = CargoSpaceOfUnit)
        : base(curTeam, unitProgram, unitType)
    {
        _IsShootable = false;
        _CanShoot = false;
        _IsCapturable = true;
        _Life = life;
        _Energy = energy;
        _Missiles = MaxUnitMissiles;
        _Guns = guns;
        _CargoSpaceOfUnit = cargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = false;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
        _UnitGameObject = unitGameObject;
        _AreaLightsArray = _UnitGameObject.GetComponentsInChildren<AreaLightColor>();
        _MiniMapBeacon = _UnitGameObject.GetComponentInChildren<MiniMapBeacon>();
        if (_CurTeam == "Player1")
        {
            _Player1UnitCapture = 4;
            _UnitGameObject.GetComponentInChildren<Player1UnitCapture>().GetComponentInChildren<Sphere1>().ColorCaptured("Player1");
            _UnitGameObject.GetComponentInChildren<Player1UnitCapture>().GetComponentInChildren<Sphere2>().ColorCaptured("Player1");
            _UnitGameObject.GetComponentInChildren<Player1UnitCapture>().GetComponentInChildren<Sphere3>().ColorCaptured("Player1");
            _UnitGameObject.GetComponentInChildren<Player1UnitCapture>().GetComponentInChildren<Sphere4>().ColorCaptured("Player1");
            foreach (var theLights in _AreaLightsArray)
            {
                theLights.ColorCaptured("Player1");
            }
            _MiniMapBeacon.ColorCaptured("Player1");
        }

        if (_CurTeam == "Player2")
        {
            _Player2UnitCapture = 4;
            _UnitGameObject.GetComponentInChildren<Player2UnitCapture>().GetComponentInChildren<Sphere1>().ColorCaptured("Player2");
            _UnitGameObject.GetComponentInChildren<Player2UnitCapture>().GetComponentInChildren<Sphere2>().ColorCaptured("Player2");
            _UnitGameObject.GetComponentInChildren<Player2UnitCapture>().GetComponentInChildren<Sphere3>().ColorCaptured("Player2");
            _UnitGameObject.GetComponentInChildren<Player2UnitCapture>().GetComponentInChildren<Sphere4>().ColorCaptured("Player2");
            foreach (var theLights in _AreaLightsArray)
            {
                theLights.ColorCaptured("Player2");
            }
            _MiniMapBeacon.ColorCaptured("Player2");
        }

    }

    public override bool _IsShootable { get; set; }
    public override bool _CanShoot { get; set; }
    public override bool _IsCapturable { get; set; }
    public override int _Life { get; set; }
    public override int _Energy { get; set; }
    public override int _Guns { get; set; }
    public override int _Missiles { get; set; }
    public override int _GunRange { get; set; }
    public override int _MissileRange { get; set; }
    public override int _GuardRange { get; set; }
    public override bool _CanMove { get; set; }
    public override bool _IsDead { get; set; }
    public override int _Cost { get; set; }
    public override int _Player1UnitCapture { get; set; }
    public override int _Player2UnitCapture { get; set; }
    public override float _ProductionTime { get; set; }
    public override ProgramType[] _PossiblePrograms { get; set; }
    public override WeaponsType _Weapons { get; set; }
    public override int _GunAttackDamage { get; set; }
    public override int _MissileAttackDamage { get; set; }
    public override int _CargoSpaceOfUnit { get; set; }
    public override int _CargoSpace { get; set; }

    public override GameObject[] _SmallBaseArray { get; set; }
    public override GameObject[] _MainBaseArray { get; set; }
    public override Transform _CurrentTransform { get; set; }
    public override Transform _DropTransform { get; set; }
    public override GameObject _CurTarget { get; set; }
    public override GameObject _CurDestination { get; set; }
    public override NavMeshAgent _Nav { get; set; }
    public override AreaLightColor[] _AreaLightsArray { get; set; }
    public override MiniMapBeacon _MiniMapBeacon { get; set; }
    public override Transform _TargetTransformOffset { get; set; }
    public override Transform _TargetTransform { get; set; }
    public override Transform _ShotOriginTransform1 { get; set; }
    public override Transform _ShotOriginTransform2 { get; set; }
    public override bool _CanTransform { get; set; }
    public override float _TransformWaitTime { get; set; }
    public override float _NextTimeToTransform { get; set; }
    public override float _NextPickUpAfterDropOff { get; set; }
    public override float _TimeBetweenDropOffAndPickUp { get; set; }
    public override bool _CurrentlyRegenerating { get; set; }

    public override GameObject[] _Cargo { get; set; }

    public override GameObject _UnitGameObject { get; set; }
}


public sealed class MainBase : Unit
{
    private const int MaxUnitLife = 300;
    private const int MaxUnitEnergy = 100;
    private const int MaxUnitGuns = 100;
    private const int MaxUnitMissiles = 0;
    private const int GunDamage = 8;
    private const int MissileDamage = 0;
    private const int CargoSpaceOfUnit = 1;
    private const int CargoSpace = 0;
    private const GameObject[] Cargo = null;
    private const WeaponsType Weapons = WeaponsType.Guns;
    private const int Cost = 40;
    private const float ProductionTIme = 6;
    private readonly ProgramType[] PossibleProgTypes = 
    {
        ProgramType.StandGround,
        ProgramType.Guard,
        ProgramType.NearestBase,
        ProgramType.AttackMain
    };

    public MainBase()
    {
        _IsShootable = true;
        _CanShoot = false;
        _IsCapturable = false;
        _Life = MaxUnitLife;
        _Energy = MaxUnitEnergy;
        _Missiles = MaxUnitMissiles;
        _Guns = MaxUnitGuns;
        _CargoSpaceOfUnit = CargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = Weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = false;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
    }

    public MainBase(string curTeam, ProgramType unitProgram, GameObject unitGameObject, UType unitType = UType.MainBase, int life = MaxUnitLife, int energy = MaxUnitEnergy,
        int guns = MaxUnitGuns, int missiles = MaxUnitMissiles, WeaponsType weapons = WeaponsType.Guns, int cargoSpaceOfUnit = CargoSpaceOfUnit)
        : base(curTeam, unitProgram, unitType)
    {
        _IsShootable = true;
        _CanShoot = false;
        _IsCapturable = false;
        _Life = life;
        _Energy = energy;
        _Missiles = MaxUnitMissiles;
        _Guns = guns;
        _CargoSpaceOfUnit = cargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = false;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
        _UnitGameObject = unitGameObject;
    }

    public override bool _IsShootable { get; set; }
    public override bool _CanShoot { get; set; }
    public override bool _IsCapturable { get; set; }
    public override int _Life { get; set; }
    public override int _Energy { get; set; }
    public override int _Guns { get; set; }
    public override int _Missiles { get; set; }
    public override int _GunRange { get; set; }
    public override int _MissileRange { get; set; }
    public override int _GuardRange { get; set; }
    public override bool _CanMove { get; set; }
    public override bool _IsDead { get; set; }
    public override int _Cost { get; set; }
    public override int _Player1UnitCapture { get; set; }
    public override int _Player2UnitCapture { get; set; }
    public override float _ProductionTime { get; set; }
    public override ProgramType[] _PossiblePrograms { get; set; }
    public override WeaponsType _Weapons { get; set; }
    public override int _GunAttackDamage { get; set; }
    public override int _MissileAttackDamage { get; set; }
    public override int _CargoSpaceOfUnit { get; set; }
    public override int _CargoSpace { get; set; }

    public override GameObject[] _SmallBaseArray { get; set; }
    public override GameObject[] _MainBaseArray { get; set; }
    public override Transform _CurrentTransform { get; set; }
    public override Transform _DropTransform { get; set; }
    public override GameObject _CurTarget { get; set; }
    public override GameObject _CurDestination { get; set; }
    public override NavMeshAgent _Nav { get; set; }
    public override AreaLightColor[] _AreaLightsArray { get; set; }
    public override MiniMapBeacon _MiniMapBeacon { get; set; }
    public override Transform _TargetTransformOffset { get; set; }
    public override Transform _TargetTransform { get; set; }
    public override Transform _ShotOriginTransform1 { get; set; }
    public override Transform _ShotOriginTransform2 { get; set; }
    public override bool _CanTransform { get; set; }
    public override float _TransformWaitTime { get; set; }
    public override float _NextTimeToTransform { get; set; }
    public override float _NextPickUpAfterDropOff { get; set; }
    public override float _TimeBetweenDropOffAndPickUp { get; set; }
    public override bool _CurrentlyRegenerating { get; set; }

    public override GameObject[] _Cargo { get; set; }

    public override GameObject _UnitGameObject { get; set; }
}

public sealed class Shots : Unit
{
    private const int MaxUnitLife = 300;
    private const int MaxUnitEnergy = 100;
    private const int MaxUnitGuns = 100;
    private const int MaxUnitMissiles = 0;
    private const int GunDamage = 8;
    private const int MissileDamage = 0;
    private const int CargoSpaceOfUnit = 1;
    private const int CargoSpace = 0;
    private const GameObject[] Cargo = null;
    private const WeaponsType Weapons = WeaponsType.Guns;
    private const int Cost = 40;
    private const float ProductionTIme = 6;
    private readonly ProgramType[] PossibleProgTypes = 
    {
        ProgramType.StandGround,
        ProgramType.Guard,
        ProgramType.NearestBase,
        ProgramType.AttackMain
    };

    public Shots()
    {
        _IsShootable = false;
        _CanShoot = false;
        _IsCapturable = false;
        _Life = MaxUnitLife;
        _Energy = MaxUnitEnergy;
        _Missiles = MaxUnitMissiles;
        _Guns = MaxUnitGuns;
        _CargoSpaceOfUnit = CargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = Weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = false;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
    }

    public Shots(string curTeam, GameObject unitGameObject, ProgramType unitProgram = ProgramType.ShotFired, UType unitType = UType.Shots, int life = MaxUnitLife, int energy = MaxUnitEnergy,
        int guns = MaxUnitGuns, int missiles = MaxUnitMissiles, WeaponsType weapons = WeaponsType.Guns, int cargoSpaceOfUnit = CargoSpaceOfUnit)
        : base(curTeam, unitProgram, unitType)
    {
        _IsShootable = false;
        _CanShoot = false;
        _IsCapturable = false;
        _Life = life;
        _Energy = energy;
        _Missiles = MaxUnitMissiles;
        _Guns = guns;
        _CargoSpaceOfUnit = cargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = false;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
        _UnitGameObject = unitGameObject;

    }

    public override bool _IsShootable { get; set; }
    public override bool _CanShoot { get; set; }
    public override bool _IsCapturable { get; set; }
    public override int _Life { get; set; }
    public override int _Energy { get; set; }
    public override int _Guns { get; set; }
    public override int _Missiles { get; set; }
    public override int _GunRange { get; set; }
    public override int _MissileRange { get; set; }
    public override int _GuardRange { get; set; }
    public override bool _CanMove { get; set; }
    public override bool _IsDead { get; set; }
    public override int _Cost { get; set; }
    public override int _Player1UnitCapture { get; set; }
    public override int _Player2UnitCapture { get; set; }
    public override float _ProductionTime { get; set; }
    public override ProgramType[] _PossiblePrograms { get; set; }
    public override WeaponsType _Weapons { get; set; }
    public override int _GunAttackDamage { get; set; }
    public override int _MissileAttackDamage { get; set; }
    public override int _CargoSpaceOfUnit { get; set; }
    public override int _CargoSpace { get; set; }

    public override GameObject[] _SmallBaseArray { get; set; }
    public override GameObject[] _MainBaseArray { get; set; }
    public override Transform _CurrentTransform { get; set; }
    public override Transform _DropTransform { get; set; }
    public override GameObject _CurTarget { get; set; }
    public override GameObject _CurDestination { get; set; }
    public override NavMeshAgent _Nav { get; set; }
    public override AreaLightColor[] _AreaLightsArray { get; set; }
    public override MiniMapBeacon _MiniMapBeacon { get; set; }
    public override Transform _TargetTransformOffset { get; set; }
    public override Transform _TargetTransform { get; set; }
    public override Transform _ShotOriginTransform1 { get; set; }
    public override Transform _ShotOriginTransform2 { get; set; }
    public override bool _CanTransform { get; set; }
    public override float _TransformWaitTime { get; set; }
    public override float _NextTimeToTransform { get; set; }
    public override float _NextPickUpAfterDropOff { get; set; }
    public override float _TimeBetweenDropOffAndPickUp { get; set; }
    public override bool _CurrentlyRegenerating { get; set; }

    public override GameObject[] _Cargo { get; set; }

    public override GameObject _UnitGameObject { get; set; }
}


public sealed class PlayerPlane : Unit
{
    private const int MaxUnitLife = 300;
    private const int MaxUnitEnergy = 100;
    private const int MaxUnitGuns = 100;
    private const int MaxUnitMissiles = 0;
    private const int GunDamage = 8;
    private const int MissileDamage = 0;
    private const int CargoSpaceOfUnit = 1;
    private const int CargoSpace = 0;
    private const GameObject[] Cargo = null;
    private const WeaponsType Weapons = WeaponsType.Guns;
    private const int Cost = 40;
    private const float ProductionTIme = 6;
    private readonly ProgramType[] PossibleProgTypes = 
    {
        ProgramType.StandGround,
        ProgramType.Guard,
        ProgramType.NearestBase,
        ProgramType.AttackMain
    };

    public PlayerPlane()
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = MaxUnitLife;
        _Energy = MaxUnitEnergy;
        _Missiles = MaxUnitMissiles;
        _Guns = MaxUnitGuns;
        _CargoSpaceOfUnit = CargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = Weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = true;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
    }

    public PlayerPlane(string curTeam, ProgramType unitProgram, GameObject unitGameObject, UType unitType = UType.PlayerPlane, int life = MaxUnitLife, int energy = MaxUnitEnergy,
        int guns = MaxUnitGuns, int missiles = MaxUnitMissiles, WeaponsType weapons = WeaponsType.Guns, int cargoSpaceOfUnit = CargoSpaceOfUnit)
        : base(curTeam, unitProgram, unitType)
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = life;
        _Energy = energy;
        _Missiles = MaxUnitMissiles;
        _Guns = guns;
        _CargoSpaceOfUnit = cargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = true;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
        _UnitGameObject = unitGameObject;

    }

    public override bool _IsShootable { get; set; }
    public override bool _CanShoot { get; set; }
    public override bool _IsCapturable { get; set; }
    public override int _Life { get; set; }
    public override int _Energy { get; set; }
    public override int _Guns { get; set; }
    public override int _Missiles { get; set; }
    public override int _GunRange { get; set; }
    public override int _MissileRange { get; set; }
    public override int _GuardRange { get; set; }
    public override bool _CanMove { get; set; }
    public override bool _IsDead { get; set; }
    public override int _Cost { get; set; }
    public override int _Player1UnitCapture { get; set; }
    public override int _Player2UnitCapture { get; set; }
    public override float _ProductionTime { get; set; }
    public override ProgramType[] _PossiblePrograms { get; set; }
    public override WeaponsType _Weapons { get; set; }
    public override int _GunAttackDamage { get; set; }
    public override int _MissileAttackDamage { get; set; }
    public override int _CargoSpaceOfUnit { get; set; }
    public override int _CargoSpace { get; set; }

    public override GameObject[] _SmallBaseArray { get; set; }
    public override GameObject[] _MainBaseArray { get; set; }
    public override Transform _CurrentTransform { get; set; }
    public override Transform _DropTransform { get; set; }
    public override GameObject _CurTarget { get; set; }
    public override GameObject _CurDestination { get; set; }
    public override NavMeshAgent _Nav { get; set; }
    public override AreaLightColor[] _AreaLightsArray { get; set; }
    public override MiniMapBeacon _MiniMapBeacon { get; set; }
    public override Transform _TargetTransformOffset { get; set; }
    public override Transform _TargetTransform { get; set; }
    public override Transform _ShotOriginTransform1 { get; set; }
    public override Transform _ShotOriginTransform2 { get; set; }
    public override bool _CanTransform { get; set; }
    public override float _TransformWaitTime { get; set; }
    public override float _NextTimeToTransform { get; set; }
    public override float _NextPickUpAfterDropOff { get; set; }
    public override float _TimeBetweenDropOffAndPickUp { get; set; }
    public override bool _CurrentlyRegenerating { get; set; }

    public override GameObject[] _Cargo { get; set; }

    public override GameObject _UnitGameObject { get; set; }
}

public sealed class PlayerMech : Unit
{
    private const int MaxUnitLife = 300;
    private const int MaxUnitEnergy = 100;
    private const int MaxUnitGuns = 100;
    private const int MaxUnitMissiles = 0;
    private const int GunDamage = 8;
    private const int MissileDamage = 0;
    private const int CargoSpaceOfUnit = 1;
    private const int CargoSpace = 0;
    private const GameObject[] Cargo = null;
    private const WeaponsType Weapons = WeaponsType.Guns;
    private const int Cost = 40;
    private const float ProductionTIme = 6;
    private readonly ProgramType[] PossibleProgTypes = 
    {
        ProgramType.StandGround,
        ProgramType.Guard,
        ProgramType.NearestBase,
        ProgramType.AttackMain
    };

    public PlayerMech()
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = MaxUnitLife;
        _Energy = MaxUnitEnergy;
        _Missiles = MaxUnitMissiles;
        _Guns = MaxUnitGuns;
        _CargoSpaceOfUnit = CargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = Weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = true;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
    }

    public PlayerMech(string curTeam, ProgramType unitProgram, GameObject unitGameObject, UType unitType = UType.PlayerMech, int life = MaxUnitLife, int energy = MaxUnitEnergy,
        int guns = MaxUnitGuns, int missiles = MaxUnitMissiles, WeaponsType weapons = WeaponsType.Guns, int cargoSpaceOfUnit = CargoSpaceOfUnit)
        : base(curTeam, unitProgram, unitType)
    {
        _IsShootable = true;
        _CanShoot = true;
        _IsCapturable = false;
        _Life = life;
        _Energy = energy;
        _Missiles = MaxUnitMissiles;
        _Guns = guns;
        _CargoSpaceOfUnit = cargoSpaceOfUnit;
        _GunAttackDamage = GunDamage;
        _MissileAttackDamage = MissileDamage;
        _Weapons = weapons;
        _CargoSpace = CargoSpace;
        _Cargo = Cargo;
        _UnitGameObject = null;
        _CanMove = true;
        _IsDead = false;
        _Cost = Cost;
        _ProductionTime = ProductionTIme;
        _PossiblePrograms = PossibleProgTypes;
        _UnitGameObject = unitGameObject;

    }

    public override bool _IsShootable { get; set; }
    public override bool _CanShoot { get; set; }
    public override bool _IsCapturable { get; set; }
    public override int _Life { get; set; }
    public override int _Energy { get; set; }
    public override int _Guns { get; set; }
    public override int _Missiles { get; set; }
    public override int _GunRange { get; set; }
    public override int _MissileRange { get; set; }
    public override int _GuardRange { get; set; }
    public override bool _CanMove { get; set; }
    public override bool _IsDead { get; set; }
    public override int _Cost { get; set; }
    public override int _Player1UnitCapture { get; set; }
    public override int _Player2UnitCapture { get; set; }
    public override float _ProductionTime { get; set; }
    public override ProgramType[] _PossiblePrograms { get; set; }
    public override WeaponsType _Weapons { get; set; }
    public override int _GunAttackDamage { get; set; }
    public override int _MissileAttackDamage { get; set; }
    public override int _CargoSpaceOfUnit { get; set; }
    public override int _CargoSpace { get; set; }

    public override GameObject[] _SmallBaseArray { get; set; }
    public override GameObject[] _MainBaseArray { get; set; }
    public override Transform _CurrentTransform { get; set; }
    public override Transform _DropTransform { get; set; }
    public override GameObject _CurTarget { get; set; }
    public override GameObject _CurDestination { get; set; }
    public override NavMeshAgent _Nav { get; set; }
    public override AreaLightColor[] _AreaLightsArray { get; set; }
    public override MiniMapBeacon _MiniMapBeacon { get; set; }
    public override Transform _TargetTransformOffset { get; set; }
    public override Transform _TargetTransform { get; set; }
    public override Transform _ShotOriginTransform1 { get; set; }
    public override Transform _ShotOriginTransform2 { get; set; }
    public override bool _CanTransform { get; set; }
    public override float _TransformWaitTime { get; set; }
    public override float _NextTimeToTransform { get; set; }
    public override float _NextPickUpAfterDropOff { get; set; }
    public override float _TimeBetweenDropOffAndPickUp { get; set; }
    public override bool _CurrentlyRegenerating { get; set; }

    public override GameObject[] _Cargo { get; set; }

    public override GameObject _UnitGameObject { get; set; }
}


public class GlobalVariables : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
