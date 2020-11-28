using GTA;
using GTA.Plugins;

public partial class MAIN {

    static RadarMarker SHOP_IN_COUNTRISIDE_MARKER;
    static Pickup SHOP_IN_COUNTRISIDE_PICKUP;

    public class UNLOCK1 : Thread {

        public const double X = 254.6288, Y = -65.008, Z = 1.3281;
        const int PRICE = 160000;

        public override void START( LabelJump label ) {
            SHOP_IN_COUNTRISIDE_MARKER.disable();
            SHOP_IN_COUNTRISIDE_PICKUP.destroy();
            SHOP_IN_COUNTRISIDE_MARKER.create_short_range( RadarIconID.PROPERTY_GREEN, X, Y, Z ).set_radar_mode( 2 );
            SHOP_IN_COUNTRISIDE_PICKUP.create_forsale_property( sString.FORSALE_PROPERTY, X, Y, Z, PRICE );
            Jump += LOOP;
        }

        private void LOOP( LabelJump label ) {
            wait( DefaultWaitTime );
            jf( LOOP, p.is_defined(), SHOP_IN_COUNTRISIDE_PICKUP.is_picked_up() );
            SHOP_IN_COUNTRISIDE_MARKER.disable();
            SHOP_IN_COUNTRISIDE_PICKUP.destroy();
            increment_int_stat( StatsID.PROPERTY_BUDGET, PRICE );
            // create_thread<REMAX>();
            play_sound( 1149, 0.0, 0.0, 0.0 );
            __disable_player_controll_in_cutscene( true );
            __set_player_ignore( true );
            __fade( false, true );
            __toggle_cinematic( true );
            __renderer_at( 241.3808, -73.9149, 6.2957 );
            CAMERA.set_position( 241.3808, -73.9149, 6.2957 ).set_point_at( 250.1326, -64.3964, 2.6312, 2 );
            __clear_text();
            wait( 1000 );
            __fade( true, true );
            play_music( MusicID.PROPERTY_BUYED );
            show_text_styled( "BUYPRO", 5000, 2 );
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