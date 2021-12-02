using GTA;

public partial class MAIN {

    static Int BLACK_LIST_MISSION_PASSED, BLACK_LIST_MISSION_STAGE, IS_BLACK_LIST_TUTORIAL_SHOWED;
    static Float BLACK_LIST_START_X, BLACK_LIST_START_Y, BLACK_LIST_START_Z;
    static RadarMarker BLACK_LIST_MARKER;
    static sString BLACK_LIST_MISSION_NAME;

    // ---------------------------------------------------------------------------------------------------------------------------

    public class BLSTART : Thread {

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            and( BLACK_LIST_MISSION_PASSED == 0, delegate {
                BLACK_LIST_MARKER.create_long_range( RadarIconID.CRASH, BLACK_LIST_START_X, BLACK_LIST_START_Y, BLACK_LIST_START_Z );
            }, delegate {
                BLACK_LIST_MARKER.create_short_range( RadarIconID.IMPOUND, BLACK_LIST_START_X, BLACK_LIST_START_Y, BLACK_LIST_START_Z );
            } );
            BLACK_LIST_MARKER.set_radar_mode( 2 );
            Jump += MAIL_LOOP;
        }

        private void MAIL_LOOP( LabelJump label ) {
            wait( DefaultWaitTime );
            Jump += MAIL_LOOP2;
        }

        private void MAIL_LOOP2( LabelJump label ) {
            wait( 0 );
            jf( MAIL_LOOP,
                OnMission == 0,
                p.is_defined(),
                a.is_defined()
            );
            and( MAIL_LOOP,
                !is_fading(),
                !a.is_busted(),
                !a.is_dead(),
                a.is_near_point_3d( 0, BLACK_LIST_START_X, BLACK_LIST_START_Y, BLACK_LIST_START_Z, 30.0, 30.0, 30.0 )
            );
            and( MAIL_LOOP2,
                a.is_near_point_3d_stopped_on_foot( 1, BLACK_LIST_START_X, BLACK_LIST_START_Y, BLACK_LIST_START_Z, 1.25, 1.25, 2.0 ),
                p.is_controllable(),
                !p.is_on_jetpack()
            );
            __disable_player_controll_in_cutscene( true );
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            BLACK_LIST_MARKER.disable();
            __set_entered_names( false );
            __show_mission_name( BLACK_LIST_MISSION_NAME );
            __fade( false );
            wait( is_fading() );
            __clear_text();
            start_mission<BLIST>();
            end_thread();
        }

    }

}