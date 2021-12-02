using GTA;

public partial class MAIN {

    static Float ZERO_START_X, ZERO_START_Y, ZERO_START_Z;
    static Int ZERO_TOTAL_MISSION_PASSED;

    public class ZRSTART : Thread {

        static RadarMarker hMarker;

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            hMarker.create_long_range( RadarIconID.ZERO, ZERO_START_X, ZERO_START_Y, ZERO_START_Z ).set_radar_mode( 2 ); 
            Jump += LOOP;
        }

        private void LOOP( LabelJump label ) {
            wait( DefaultWaitTime );
            Jump += LOOP2;
        }

        private void LOOP2( LabelJump label ) {
            wait( 0 );
            jf( LOOP, OnMission == 0, p.is_defined(), a.is_defined() );
            and( LOOP,
                 !is_fading(),
                 !a.is_dead(),
                 !a.is_busted(),
                 a.is_near_point_3d( 0, ZERO_START_X, ZERO_START_Y, ZERO_START_Z, 30.0, 30.0, 30.0 )
            );
            jf( LOOP2,
                 a.is_near_point_3d_stopped_on_foot( 1, ZERO_START_X, ZERO_START_Y, ZERO_START_Z, 1.25, 1.25, 2.0 ),
                 p.is_controllable(),
                 !p.is_on_jetpack()
            );
            and( ZERO_TOTAL_MISSION_PASSED == 1, delegate {
                and( !is_text_priority_displayed(), delegate { show_text_highpriority( "@ZRO@05", 6000, 1 ); } );
                jump( LOOP2 );
            } );
            __disable_player_controll_in_cutscene( true );
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            hMarker.disable();
            Gosub += SETUP_MISSION_NAMES;
            __show_mission_name( CURRENT_MISSION_NAME );
            __fade( 0, false );
            start_mission<ZRMISS>();
            end_thread();
        }

        private void SETUP_MISSION_NAMES( LabelGosub label ) {
            sString[] names = { "@ZRO@00", "@ZRO@01", "@ZRO@02", "@ZRO@03", "@ZRO@04" };
            and( ZERO_TOTAL_MISSION_PASSED == 0, delegate {
                CURRENT_MISSION_NAME.value = names[ 0 ];
            } );
            for( int i = 1, j = 2; i < names.Length; i++, j++ ) {
                and( ZERO_TOTAL_MISSION_PASSED == j, delegate {
                    CURRENT_MISSION_NAME.value = names[ i ];
                } );
            }
        }

    }

}