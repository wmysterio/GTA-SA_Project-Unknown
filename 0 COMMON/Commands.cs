using System;
using GTA;
using GTA.Core;
using static GTA.Core.Script;
//using GTA.Plugins;

static class Extension {

    #region VEHICLE
    public static void do_if_wrecked<T>( this Vehicle<T> hVehicle, Action action ) where T : Vehicle<T> {
        or( hVehicle.is_wrecked(), hVehicle.is_in_water(), action );
    }
    public static void destroy_if_exist<T>( this Vehicle<T> hVehicle ) where T : Vehicle<T> {
        and( hVehicle.is_defined(), delegate {
            hVehicle.remove_references();
            and( PlayerActor.is_in_vehicle( hVehicle ), delegate {
                hVehicle.set_tires_vulnerable( false ).set_immunities( false );
            }, delegate { hVehicle.destroy(); } );
        } );
    }
    public static void extinguish<T>( this Vehicle<T> hVehicle ) where T : Vehicle<T> {
        and( hVehicle.is_burning(), delegate {
            hVehicle.set_health( 300 );
        } );
    }
    #endregion

    #region CHECKPOINT
    public static void disable_if_exist( this Checkpoint hCheckpoint ) {
        and( hCheckpoint.is_enabled(), delegate { hCheckpoint.disable(); } );
    }
    #endregion

    #region DECISION MAKER
    public static void create_normal( this DecisionMaker hDecisionMaker, bool stayPut = false ) {
        hDecisionMaker.load( stayPut ? 0 : 2 ).add_event_response( 36, 1024, 0.0, 100.0, 0.0, 0.0, 0, 1 );
    }
    #endregion

    #region OBJECT
    public static void destroy_if_exist( this GTA.Object hObject ) {
        and( hObject.is_exists(), delegate { hObject.remove_references().destroy(); } );
    }
    #endregion

    #region MARKER
    public static void disable_if_exist( this Marker hMarker ) {
        and( hMarker.is_enabled(), delegate { hMarker.disable(); } );
    }
    #endregion

    #region PICKUP
    public static void create_if_need( this Pickup hPickup, Int weaponNumber, Int weaponModel, Int maxAmmo, Float x, Float y, Float z, Int temp ) {
        and( PlayerActor.is_has_weapon( weaponNumber ), delegate {
            PlayerActor.get_ammo_in_weapon( weaponNumber, temp );
            and( maxAmmo > temp, delegate {
                temp.sub( maxAmmo, temp );
                hPickup.create_with_ammo( weaponModel, PickupType.ONCE, temp, x, y, z );
            } );
        }, delegate {
            hPickup.create_with_ammo( weaponModel, PickupType.ONCE, maxAmmo, x, y, z );
        } );
    }
    public static void destroy_if_exist( this Pickup hPickup ) {
        and( hPickup.is_exist(), delegate { hPickup.destroy(); } );
    }
    #endregion

    #region ACTOR
    public static void destroy_if_exist( this Actor hActor ) {
        and( hActor.is_defined(), delegate { hActor.remove_references().destroy(); } );
    }
    public static void put_at( this Actor hActor, Float x, Float y, Float z, Float angle = null ) {
        hActor.set_position( x, y, z );
        if( !ReferenceEquals( angle, null ) )
            hActor.set_z_angle( angle );
    }
    public static void extinguish_current_car_if_exist<T>( this Actor hActor, Vehicle<T> hVehicleTemp ) where T : Vehicle<T> {
        and( hActor.is_in_any_vehicle(), delegate {
            hActor.get_current_vehicle( hVehicleTemp );
            hVehicleTemp.extinguish();
        } );
    }
    public static void do_if_dead( this Actor hActor, Action action ) {
        and( hActor.is_dead(), action );
    }
    public static void teleport_without_car( this Actor hActor, Float x, Float y, Float z, Float angle = null ) {
        and( hActor.is_in_any_vehicle(), delegate {
            hActor.remove_from_vehicle_and_place_at( x, y, z );
        }, delegate {
            hActor.set_position( x, y, z );
        } );
        if( !ReferenceEquals( angle, null ) )
            hActor.set_z_angle( angle );
    }
    #endregion

}
