using GTA;

public partial class MAIN {

    static Float REMAX_START_X, REMAX_START_Y, REMAX_START_Z;
    static Int REMAX_TOTAL_MISSION_PASSED;

    public class REMAXST : Thread {

        static RadarMarker hMarker;

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            hMarker.create_long_range( RadarIconID.RYDER, REMAX_START_X, REMAX_START_Y, REMAX_START_Z ).set_radar_mode( 2 ); 
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
                 a.is_near_point_3d( 0, REMAX_START_X, REMAX_START_Y, REMAX_START_Z, 30.0, 30.0, 30.0 )
            );
            jf( LOOP2,
                 a.is_near_point_3d_stopped_on_foot( 1, REMAX_START_X, REMAX_START_Y, REMAX_START_Z, 1.25, 1.25, 2.0 ),
                 p.is_controllable(),
                 !p.is_on_jetpack()
            );
            __disable_player_controll_in_cutscene( true );
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            hMarker.disable();
            Gosub += SETUP_MISSION_NAMES;
            __show_mission_name( CURRENT_MISSION_NAME );
            __fade( 0, false );
            //start_mission<REMAX>();
            end_thread();
        }

        private void SETUP_MISSION_NAMES( LabelGosub label ) {
            sString[] names = { 
                "@RMS@00", "@RMS@01", "@RMS@02", "@RMS@03", "@RMS@04", "@RMS@05", "@RMS@06", 
                "@RMS@07", "@RMS@08", "@RMS@09", "@RMS@10", "@RMS@11", "@RMS@12", "@RMS@13", "@RMS@14"
            };
            for( int i = 0; i < names.Length; i++ ) {
                and( REMAX_TOTAL_MISSION_PASSED == i, delegate {
                    CURRENT_MISSION_NAME.value = names[ i ];
                } );
            }
        }

    }

}