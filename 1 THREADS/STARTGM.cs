using GTA;

public partial class MAIN {

    public const bool IS_DEBUG = true;
    public const bool DISABLE_CHEATS = false;
    public const int DEBUG_GAME_LEVEL = 2;

    // ---------------------------------------------------------------------------------------------------------------------------

    static Timer MISSION_GLOBAL_TIMER_1;
    static StatusText MISSION_GLOBAL_STATUS_TEXT_1, MISSION_GLOBAL_STATUS_TEXT_2, MISSION_GLOBAL_STATUS_TEXT_3;
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
            wait( 0 );
            Gosub += MAIN_CUTSCENE;
            if( IS_DEBUG ) {
                CURRENT_GAME_LEVEL.value = DEBUG_GAME_LEVEL;
                and( CURRENT_GAME_LEVEL == 1, delegate { set_float_stat( StatsID_Float.MAX_HEALTH, 800.0 ); } );
                and( CURRENT_GAME_LEVEL == 2, delegate { set_float_stat( StatsID_Float.MAX_HEALTH, 1000.0 ); } );
            } else { 
              create_thread<SELECTG>();
              wait( IS_CURRENT_GAME_SELECTED == false );
            }
            __disable_player_controll_in_cutscene( false );
            end_thread();
        }

        private void BASE_GAME_SETUP( LabelGosub label ) {

            #region C.R.A.S.H. BASE SETUP
            CRASH_START_X.value = 1546.9318;
            CRASH_START_Y.value = -1681.6016;
            CRASH_START_Z.value = 13.5588;
            CRASH_ICON.value = RadarIconID.CRASH;
            CRASH_TOTAL_MISSION_PASSED.value = 0;
            #endregion

            #region BLACK LIST BASE SETUP
            BLACK_LIST_START_X.value = 1812.5254;
            BLACK_LIST_START_Y.value = -1111.5702;
            BLACK_LIST_START_Z.value = 24.0781;
            BLACK_LIST_MISSION_PASSED.value = 0; // 6; 5; 4; 3; 2; 1;
            BLACK_LIST_MISSION_STAGE.value = 0; // 4095; 1023; 255; 63; 15; 7;
            BLACK_LIST_MISSION_NAME.value = "@BLS@0";
            #endregion

            #region INCORPORATION BASE SETUP
            INCORP_START_X.value = -2758.0469;
            INCORP_START_Y.value = 371.9743;
            INCORP_START_Z.value = 4.3454;
            INCORP_MISSION_PASSED.value = 0;
            #endregion


            #region CJ & CV BASE SETUP
            CJ_START_X.value = 2498.9802;
            CJ_START_Y.value = -1685.6517;
            CJ_START_Z.value = 13.4478;
            CJ_TOTAL_MISSION_PASSED.value = 0;
            #endregion

            #region ZERO BASE SETUP
            ZERO_START_X.value = -2245.2568;
            ZERO_START_Y.value = 132.2962;
            ZERO_START_Z.value = 34.3203;
            ZERO_TOTAL_MISSION_PASSED.value = 0;
            //STRIP_IN_LV_ASSET_MONEY.create( UNLOCK2.X, UNLOCK2.Y, UNLOCK2.Z, 5000, 5000 );
            #endregion

            #region REMAX BASE SETUP
            REMAX_START_X.value = 259.2836;
            REMAX_START_Y.value = -272.1956;
            REMAX_START_Z.value = 1.5836;
            REMAX_TOTAL_MISSION_PASSED.value = 0;
            //SHOP_ASSET_MONEY.create( UNLOCK1.X, UNLOCK1.Y, UNLOCK1.Z, 5000, 5000 );
            #endregion


            // DEBUG START     
            //ZERO_TOTAL_MISSION_PASSED.value = 0;
            //create_thread<ZRSTART>();
            INCORP_START_X.value = 2478.4768;
            INCORP_START_Y.value = -1667.3813;
            INCORP_START_Z.value = 13.3303;
            INCORP_MISSION_PASSED.value = 14;
            create_thread<INCORST>();
            // DEBUG END






            #region CAR PARK
            CAR_PARK.init_with_number_plate( CJ_PROTOTYPE_CAR, -2763.6313, 358.3386, 3.8557, 270.0, FBITRUCK, "VITAL", 0, 1, forceSpawn_bool: 1 ).set_chance_to_generate( CJ_PROTOTYPE_CAR, 0 ).set_to_player_owned( CJ_PROTOTYPE_CAR, true );
            #endregion

            #region PROPERTIES            
            SHOP_IN_COUNTRISIDE_MARKER.create_short_range( RadarIconID.PROPERTY_RED, UNLOCK1.X, UNLOCK1.Y, UNLOCK1.Z ).set_radar_mode( 2 );
            SHOP_IN_COUNTRISIDE_PICKUP.create_locked_property( sString.LOCKED_PROPERTY, UNLOCK1.X, UNLOCK1.Y, UNLOCK1.Z );
            STRIP_IN_LV_MARKER.create_short_range( RadarIconID.PROPERTY_RED, UNLOCK2.X, UNLOCK2.Y, UNLOCK2.Z ).set_radar_mode( 2 );
            STRIP_IN_LV_PICKUP.create_locked_property( sString.LOCKED_PROPERTY, UNLOCK2.X, UNLOCK2.Y, UNLOCK2.Z );
            #endregion

            #region OBJECTS
            FS_WALL.create( 4241, 246.358, 108.459, 909.463 ).set_visibility( false ).set_rotation( 0.0, 90.0, 270.0 ).keep_in_memory( true );
            #endregion

            #region RESPECT AND PROGRESS

            /*----------------------------*\
            | MISSION | PROGRESS | RESPECT |
            |------------------------------|
            | CJMISS  | +16      | +10     |
            | CRASH   | +8       | +0      |
            | REMAX   | +15      | +0      |
            | INCORP  | +15      | +0      |
            | MAFIA   | +7       | +0      |
            | ZERO    | +5       | +0      |
            | BLIST   | +2       | +0      |
            |------------------------------|
            | TOTAL   | +68      | +10     |
            \*----------------------------*/

            set_max_progress( 68 );
            set_total_respect_points( 1000 + 10 );
            #endregion

        }

        private void MAIN_CUTSCENE( LabelGosub label ) {
            wait( 1000 );
            __clear_text();
            __disable_player_controll_in_cutscene( true );
            __set_player_ignore( true );
            __toggle_cinematic( true );

            clear_area( false, 2471.8965, -1685.6204, 13.5078, 300.0 );
            a.set_position( 2471.8965, -1685.6204, 13.5078 );

            // load and create entities

            wait( 1000 );
            __fade( true, false );
            Scene += delegate {
                wait( 5000 );

            };
            __fade( false, true );

            // destroy all entities

            clear_area( false, 2488.562, -1666.865, 12.8757, 300.0 );
            __renderer_at( 2488.562, -1666.865, 12.8757 );
            a.set_position( 2488.562, -1666.865, 12.8757 );
            __camera_default();
            __toggle_cinematic( false );
            __set_player_ignore( false );
            wait( 1000 );
            fade( 1, 500 );
            __set_entered_names( true );
        }

    }

}