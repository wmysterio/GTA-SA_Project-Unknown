using GTA;

public partial class MAIN {

    static Float CRASH_START_X, CRASH_START_Y, CRASH_START_Z;
    static Int CRASH_TOTAL_MISSION_PASSED, CRASH_FBI_FLAG;

    public class CRSTART : Thread {

        static RadarMarker hMarker;

        Int currentHour, currentMinute;

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            hMarker.create_long_range( RadarIconID.CRASH, CRASH_START_X, CRASH_START_Y, CRASH_START_Z ).set_radar_mode( 2 ); 
            Jump += LOOP;
        }

        private void LOOP( LabelJump label ) {
            wait( DefaultWaitTime );
            Jump += LOOP2;
        }

        private void LOOP2( LabelJump label ) {
            wait( 0 );
            jf( LOOP,
                OnMission == 0,
                p.is_defined(),
                a.is_defined()
            );
            and( LOOP,
                 !is_fading(),
                 !a.is_dead(),
                 !a.is_busted(),
                 a.is_near_point_3d( 0, CRASH_START_X, CRASH_START_Y, CRASH_START_Z, 30.0, 30.0, 30.0 )
            );
            jf( LOOP2,
                 a.is_near_point_3d_stopped_on_foot( 1, CRASH_START_X, CRASH_START_Y, CRASH_START_Z, 1.25, 1.25, 2.0 ),
                 p.is_controllable(),
                 !p.is_on_jetpack()
            );
            and( CRASH_TOTAL_MISSION_PASSED == 3, delegate {
                get_current_time( currentHour, currentMinute );
                and( 17 > currentHour, delegate {
                    show_text_1number_highpriority( "@CRS@29", 17, 1, 1 );
                    jump( LOOP2 );
                } );
            } );
            __disable_player_controll_in_cutscene( true );
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            hMarker.disable();
            Gosub += SETUP_MISSION_NAMES;
            __show_mission_name( CURRENT_MISSION_NAME );
            __fade( 0, false );
            start_mission<CRMISS>();
            end_thread();
        }

        private void SETUP_MISSION_NAMES( LabelGosub label ) {
            sString[] names = { "@CRS@00", "@CRS@01", "@CRS@02", "@CRS@03", "@CRS@04", "@CRS@05", "@CRS@06", "@CRS@07", "@CRS@08" };
            for( int i = 0; i < names.Length; i++ ) {
                and( CRASH_TOTAL_MISSION_PASSED == i, delegate {
                    CURRENT_MISSION_NAME.value = names[ i ];
                } );
            }
        }

    }

}