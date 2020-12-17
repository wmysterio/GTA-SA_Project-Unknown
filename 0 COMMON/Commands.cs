using GTA;
//using GTA.Plugins;

public partial class MAIN {

    static void __toggle_cinematic( bool enable ) {
        PlayerActor.hide_weapons_in_scene( enable );
        enable_radar( !enable );
        enable_hud( !enable );
        enable_widescreen( enable );
        text_clear_all();
        remove_text_box();
    }
    static void __fade( Int type_bool, bool delay = false ) {
        set_fade_color_rgb( 0, 0, 0 );
        fade( type_bool, 500 );
        if( delay )
            wait( 500 );
    }
    static void __set_police_generator( bool state ) {
        enable_police_helis( state );
        enable_police_bikes( state );
        set_create_random_cops( state );
        set_cops_chase_criminals( state );
    }
    static void __renderer_at( Float x, Float y, Float z ) { refresh_game_renderer( x, y ); CAMERA.refresh( x, y, z ); }
    static void __camera_default() { CAMERA.restore_with_jumpcut().set_behind_player(); }
    static void __clear_text() { remove_text_box(); text_clear_all(); }
    static void __set_player_ignore( Int val_bool ) { PlayerChar.ignored_by_cops( val_bool ).ignored_by_everyone( val_bool ); }
    static void __set_traffic( Float val ) { set_vehicle_traffic_density_multiplier( val ); set_ped_traffic_density_multiplier( val ); }
    static void __show_mission_name( sString gxt ) { show_text_styled( sString.DUMMY, 1000, 2 ); clear_text_with_style( true ); show_text_styled( gxt, 1000, 2 ); }
    static void __set_entered_names( Int val_bool ) { show_entered_vehicle_name( val_bool ); show_entered_zone_name( val_bool ); }
    static void __disable_player_controll_in_cutscene( bool state ) { PlayerActor.set_immunities( state ); PlayerChar.can_move( !state ); }
    static void __normalize_AI_for_vehicle<T>( T hVehicke ) where T : GTA.Core.Vehicle<T> {
        hVehicke.not_damaged_when_flipped( true )
                .add_upsidedown_check()
                .add_stuck_check( 4.0, 3000 )
                .set_stay_in_fast_lane( true )
                .set_only_damaged_by_player( true )
                .set_can_go_against_traffic( false )
                .enable_validate_position( false );
    }
    static void __normalize_AI_for_driver( Actor hActor) {
        hActor.set_can_be_shot_in_vehicle( false )
              .set_cant_be_dragged_out( true )
              .set_stay_in_vehicle_when_jacked( true )
              .set_suffers_critical_hits( false )
              .set_get_out_upside_down_vehicle( false )
              .enable_validate_position( false );
    }

}