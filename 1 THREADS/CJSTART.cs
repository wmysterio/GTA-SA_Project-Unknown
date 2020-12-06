using GTA;

public partial class MAIN {

    static Float CJ_START_X, CJ_START_Y, CJ_START_Z;
    static Int CJ_TOTAL_MISSION_PASSED;
    static Car CJ_PROTOTYPE_CAR;

    public class CJSTART : Thread {

        static RadarMarker hMarker;

        Int markerID;

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            markerID.value = RadarIconID.CJ;
            and(
                CJ_TOTAL_MISSION_PASSED > 2,
                11 > CJ_TOTAL_MISSION_PASSED // need correct later
            , delegate { markerID.value = RadarIconID.CESAR_VIALPANDO; } );
            hMarker.create_long_range( markerID, CJ_START_X, CJ_START_Y, CJ_START_Z ).set_radar_mode( 2 ); 
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
                 a.is_near_point_3d( 0, CJ_START_X, CJ_START_Y, CJ_START_Z, 30.0, 30.0, 30.0 )
            );
            jf( LOOP2,
                 a.is_near_point_3d_stopped_on_foot( 1, CJ_START_X, CJ_START_Y, CJ_START_Z, 1.25, 1.25, 2.0 ),
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
            start_mission<CJMISS>();
            end_thread();
        }

        private void SETUP_MISSION_NAMES( LabelGosub label ) {
            sString[] names = { 
                "@CJS@00", "@CJS@01", "@CJS@02", "@CJS@03", "@CJS@04", "@CJS@05", "@CJS@06", "@CJS@07", 
                "@CJS@08", "@CJS@09", "@CJS@10", "@CJS@11", "@CJS@12", "@CJS@13", "@CJS@14", "@CJS@15"
            };
            for( int i = 0; i < names.Length; i++ ) {
                and( CJ_TOTAL_MISSION_PASSED == i, delegate {
                    CURRENT_MISSION_NAME.value = names[ i ];
                } );
            }
        }

    }

}