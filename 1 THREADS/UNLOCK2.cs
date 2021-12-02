using GTA;

public partial class MAIN {

    static RadarMarker      STRIP_IN_LV_MARKER;
    static Pickup           STRIP_IN_LV_PICKUP;
    static AssetMoneyPickup STRIP_IN_LV_ASSET_MONEY;

    public class UNLOCK2 : Thread {

        public const double X = 2511.4297, Y = 2122.0391, Z = 10.3402;
        const int PRICE = 75000;

        public override void START( LabelJump label ) {
            STRIP_IN_LV_MARKER.disable();
            STRIP_IN_LV_PICKUP.destroy();
            STRIP_IN_LV_MARKER.create_short_range( RadarIconID.PROPERTY_GREEN, X, Y, Z ).set_radar_mode( 2 );
            STRIP_IN_LV_PICKUP.create_forsale_property( sString.FORSALE_PROPERTY, X, Y, Z, PRICE );
            Jump += LOOP;
        }

        private void LOOP( LabelJump label ) {
            wait( DefaultWaitTime );
            jf( LOOP, p.is_defined(), STRIP_IN_LV_PICKUP.is_picked_up() );
            STRIP_IN_LV_MARKER.disable();
            STRIP_IN_LV_PICKUP.destroy();
            increment_int_stat( StatsID.PROPERTY_BUDGET, PRICE );
            ZERO_TOTAL_MISSION_PASSED += 1;
            play_sound( 1149, 0.0, 0.0, 0.0 );
            __disable_player_controll_in_cutscene( true );
            __set_player_ignore( true );
            __fade( false, true );
            __toggle_cinematic( true );
            __renderer_at( 2525.0508, 2157.9312, 23.1946 );
            CAMERA.set_position( 2525.0508, 2157.9312, 23.1946 ).set_point_at( 2503.4937, 2127.658, 17.7542, 2 );
            wait( 1000 );
            __clear_text();
            __fade( true, true );
            play_music( MusicID.PROPERTY_BUYED );
            show_text_styled( "BUYPRO", 1000, 2 );
            wait( 6000 );
            __fade( false, true );
            __camera_default();
            __set_player_ignore( false );
            __toggle_cinematic( false );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            __fade( true );
            end_thread();
        }

    }

}