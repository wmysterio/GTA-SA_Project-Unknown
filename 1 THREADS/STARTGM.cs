using GTA;

public partial class MAIN {

    public const bool IS_DEBUG = true;
    public const bool DISABLE_RELEASE_CHEATS = false;

    // ---------------------------------------------------------------------------------------------------------------------------

    static Timer MISSION_GLOBAL_TIMER_1;
    static StatusText MISSION_GLOBAL_STATUS_TEXT_1, MISSION_GLOBAL_STATUS_TEXT_2;
    static sString CURRENT_MISSION_NAME;

    // ---------------------------------------------------------------------------------------------------------------------------

    public class STARTGM : Thread {

        static Object FS_WALL;

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            create_thread<SAVEGM>();
            create_thread<BUY_PRO>();
            create_thread<AUDIOBG>();
            create_thread<AUDIOPL>();
            create_thread<CJPHONE>();
            Gosub += BASE_GAME_SETUP;
            create_thread<CJSTART>();
            __camera_default();
            wait( 1000 );
            fade( 1, 1000 );
            wait( 100 );
            __clear_text();
            end_thread();
        }

        private void BASE_GAME_SETUP( LabelGosub label ) {

            #region CJ & CV BASE SETUP
            CJ_START_X.value = 2498.9802;
            CJ_START_Y.value = -1685.6517;
            CJ_START_Z.value = 13.4478;
            CJ_TOTAL_MISSION_PASSED.value = 0;
            #endregion

            #region C.R.A.S.H. BASE SETUP
            CRASH_START_X.value = 1546.9318;
            CRASH_START_Y.value = -1681.6016;
            CRASH_START_Z.value = 13.5588;
            CRASH_TOTAL_MISSION_PASSED.value = 0;
            #endregion

            #region REMAX BASE SETUP
            REMAX_START_X.value = 259.2836;
            REMAX_START_Y.value = -272.1956;
            REMAX_START_Z.value = 1.5836;
            REMAX_TOTAL_MISSION_PASSED.value = 0;
            #endregion


            // DEBUG START
            CRASH_TOTAL_MISSION_PASSED.value = 2;
            create_thread<CRSTART>();
            // DEBUG END


            #region BLACK LIST BASE SETUP
            BLACK_LIST_START_X.value = 1812.5254;
            BLACK_LIST_START_Y.value = -1111.5702;
            BLACK_LIST_START_Z.value = 24.0781;
            BLACK_LIST_MISSION_PASSED.value = 0; // 6; 5; 4; 3; 2; 1;
            BLACK_LIST_MISSION_STAGE.value = 0; // 4095; 1023; 255; 63; 15; 7;
            BLACK_LIST_MISSION_NAME.value = "@BLS@0";
            #endregion

            #region CAR PARK
            CAR_PARK.init_with_number_plate( CJ_PROTOTYPE_CAR, 2488.1101, -1683.4895, 12.9456, 89.0883, FBITRUCK, "VITAL", forceSpawn_bool: 1 ).set_chance_to_generate( CJ_PROTOTYPE_CAR, 0 ).set_to_player_owned( CJ_PROTOTYPE_CAR, true );
            #endregion

            #region PROPERTIES            
            SHOP_IN_COUNTRISIDE_MARKER.create_short_range( RadarIconID.PROPERTY_RED, UNLOCK1.X, UNLOCK1.Y, UNLOCK1.Z ).set_radar_mode( 2 );
            SHOP_IN_COUNTRISIDE_PICKUP.create_locked_property( sString.LOCKED_PROPERTY, UNLOCK1.X, UNLOCK1.Y, UNLOCK1.Z );
            #endregion

            #region OBJECTS
            FS_WALL.create( 4241, 246.358, 108.459, 909.463 ).set_visibility( false ).set_rotation( 0.0, 90.0, 270.0 ).keep_in_memory( true );
            #endregion

            #region RESPECT AND PROGRESS

            /*----------------------------*\
            | MISSION | PROGRESS | RESPECT |
            |------------------------------|
            | CJMISS  | +16      | +10     |
            | CRASH   | +9       | +0      |
            | REMAX   | +15      | +0      |
            | INCORP  | +10      | +0      |
            | MAFIA   | +7       | +0      |
            | ZERO    | +4       | +0      |
            | BLIST   | +2       | +0      |
            |------------------------------|
            | TOTAL   | +63      | +10     |
            \*----------------------------*/

            set_max_progress( 63 );
            set_total_respect_points( 1000 + 10 );
            #endregion

        }

    }

}