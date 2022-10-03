using GTA;

#pragma warning disable CS0649

public partial class MAIN {

    static Float INCORP_START_X, INCORP_START_Y, INCORP_START_Z;
    static Int INCORP_MISSION_PASSED;

    public class INCORST : Thread {

        static RadarMarker hMarker;

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            and( 2 > INCORP_MISSION_PASSED, delegate {
                hMarker.create_long_range( RadarIconID.ANCHOR, INCORP_START_X, INCORP_START_Y, INCORP_START_Z ).set_radar_mode( 2 );
            }, delegate {
                hMarker.create_short_range( RadarIconID.ANCHOR, INCORP_START_X, INCORP_START_Y, INCORP_START_Z ).set_radar_mode( 2 );
            } );
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
                 a.is_near_point_3d( 0, INCORP_START_X, INCORP_START_Y, INCORP_START_Z, 30.0, 30.0, 30.0 )
            );
            jf( LOOP2,
                 a.is_near_point_3d_stopped_on_foot( 1, INCORP_START_X, INCORP_START_Y, INCORP_START_Z, 1.25, 1.25, 2.0 ),
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
            start_mission<ICMISS>();
            end_thread();
        }

        private void SETUP_MISSION_NAMES( LabelGosub label ) {
            sString[] names = {
                "@INC@00", "@INC@01", "@INC@02", "@INC@03", "@INC@04", "@INC@05", "@INC@06", "@INC@07", 
                "@INC@08", "@INC@09", "@INC@10", "@INC@11", "@INC@12", "@INC@13", "@INC@14"
            };
            for( int i = 0; i < names.Length; i++ ) {
                and( INCORP_MISSION_PASSED == i, delegate {
                    CURRENT_MISSION_NAME.value = names[ i ];
                } );
            }
        }

    }

}