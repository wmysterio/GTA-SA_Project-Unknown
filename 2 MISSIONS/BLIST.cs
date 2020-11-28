using GTA;
//using GTA.Plugins;

public partial class MAIN {

    public class BLIST : Mission {

        private void ___jump_failed_no_put_player( string gxt ) {
            missionFailedText.value = gxt;
            Jump += FAILED_NOPUT_PLAYER;
        }
        private void ___jump_failed_put_player( string gxt ) {
            missionFailedText.value = gxt;
            Jump += FAILED_PUT_PLAYER;
        }

        // ---------------------------------------------------------------------------------------------------------------------------

        static double START_POINT_X = 31.2427, START_POINT_Y = -2660.6902, START_POINT_Z = 39.5398, START_POINT_A = 90.0;
        static int PASSED_MONEY = 200000, RING_WAV_ID = 20600;/* 23000 */
        static ushort POSITION_ARRAY_SIZE = 99, POINT4_ARRAY_SIZE = 4, CUTSCENE_ENTITY_ARRAY_SIZE = 10;

        // ---------------------------------------------------------------------------------------------------------------------------

        Int no_suget_duel, loaded_path, is_first_mission, player_car_model, enemy_car_model, enemy_actor_model, total_sub_mission,
            main_panel_activated_row, loop_index, race_checkpoint_type, cops_min_wanted_level, cops_wanted_to_zero_flag, loop_index2,
            race_stopwatch, race_need_speed, cops_current_wanted_level, race_total_passed_4point, race_4point_car_speed_int,
            final_first_race_passed;

        Float race_start_x, race_start_y, race_start_z, race_start_a, enemy_start_x, enemy_start_y, enemy_start_z, enemy_start_a,
              race_path_speed, smission_carp_x, smission_carp_y, smission_carp_z, smission_care_x, smission_care_y, smission_care_z,
              smission_distance, race_checkpoint_position_x, race_checkpoint_position_y, race_checkpoint_position_z,
              race_checkpoint_direction_x, race_checkpoint_direction_y, race_checkpoint_direction_z, race_4point_car_speed,
              final_point_x, final_point_y, final_point_z;

        CarComponent temp_car_component;
        Car enemy_car, player_car, temp_player_car;
        Actor enemy_actor;
        Marker enemy_marker;
        Checkpoint race_marker;
        Panel main_panel;
        Memory selected_jump;
        sString missionFailedText;
        RaceCheckpoint race_checkpoint;
        ASPack cutscene_as_pack;
        Heli cutscene_heli;

        Array<Int> checkpoint_add_time = POSITION_ARRAY_SIZE;
        Array<Float> checkpoint_x = POSITION_ARRAY_SIZE, checkpoint_y = POSITION_ARRAY_SIZE, checkpoint_z = POSITION_ARRAY_SIZE;

        Array<Float> point4_x = POINT4_ARRAY_SIZE, point4_y = POINT4_ARRAY_SIZE, point4_z = POINT4_ARRAY_SIZE;
        Array<Checkpoint> point4_checkpoints = POINT4_ARRAY_SIZE;
        Array<RaceCheckpoint> point4_race_checkpoints = POINT4_ARRAY_SIZE;

        Array<Memory> labels = 12;
        Array<sString> TEXT_PANEL = 40;

        Array<Actor> cutcsene_actors = local_array( 0, CUTSCENE_ENTITY_ARRAY_SIZE );
        Array<Car> cutcsene_cars = local_array( 10, CUTSCENE_ENTITY_ARRAY_SIZE );
        Array<Object> cutcsene_objects = local_array( 20, CUTSCENE_ENTITY_ARRAY_SIZE );

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            g.release();
            p.enable_group_recruitment( false ).clear_wanted_level();
            use_gxt_table( "RACETOR" );
            missionFailedText.value = sString.DUMMY;
            no_suget_duel.value = false;
            loaded_path.value = -1;
            is_first_mission.value = false;
            wait( 1500 );
            jump_table( BLACK_LIST_MISSION_PASSED, table => {
                table.Auto += ON_UNRANK; // 0
                table.Auto += ON_RANK_6; // 1
                table.Auto += ON_RANK_5; // 2
                table.Auto += ON_RANK_4; // 3
                table.Auto += ON_RANK_3; // 4
                table.Auto += ON_RANK_2; // 5
                table.Auto += ON_RANK_1; // 6

                void ON_UNRANK( LabelCase l ) {
                    is_first_mission.value = true;
                    chdir( @"Sound\BLIST" );
                    wait( AUDIO_BG.load( 999999 ).is_ready );
                    Gosub += PLAY_CUTSCENE_0B;
                    race_start_x.value = 1717.5402;
                    race_start_y.value = -1005.6797;
                    race_start_z.value = 23.6005;
                    race_start_a.value = 174.3112;
                    player_car_model.value = CarModel.GREENWOO;
                    enemy_start_x.value = 1674.1576;
                    enemy_start_y.value = -1029.8257;
                    enemy_start_z.value = 23.585;
                    enemy_start_a.value = 269.3724;
                    enemy_car_model.value = CarModel.GREENWOO;
                    enemy_actor_model.value = ActorModel.WMYJG;
                    loaded_path.value = 908;
                    race_path_speed.value = 0.78; // 1.0;
                    Gosub += CREATE_PLAYER_CAR;
                    Gosub += CREATE_ENEMY_CAR_AND_ACTOR;
                    Gosub += CUSTOMIZE_START_MISSION;
                    Gosub += LOAD_PATH;
                    wait( 1000 );
                    AUDIO_BG.play();
                    __fade( true, true );
                    Jump += RACE_FIRST_RACE_LOOP;
                }
                void ON_RANK_6( LabelCase l ) {
                    and( BLACK_LIST_MISSION_STAGE == 7, delegate { Jump += RACE_6_FINAL; ; } );
                    total_sub_mission.value = 2; // 3 - 1
                    labels[ 0 ].Label += RACE_6_0;
                    labels[ 1 ].Label += RACE_6_1;
                    labels[ 2 ].Label += RACE_6_2;
                    Gosub += PLACE_PLAYER;
                    // Race names  
                    TEXT_PANEL[ 2 ].value = "@BLS@1";
                    TEXT_PANEL[ 3 ].value = "@BLS@1";
                    TEXT_PANEL[ 4 ].value = "@BLS@2";
                    // Race car names
                    TEXT_PANEL[ 14 ].value = "TAHOMA";
                    TEXT_PANEL[ 15 ].value = "INTRUDR";
                    TEXT_PANEL[ 16 ].value = "MANANA";
                    // Race zones  
                    TEXT_PANEL[ 26 ].value = "BACKO";
                    TEXT_PANEL[ 27 ].value = "RED";
                    TEXT_PANEL[ 28 ].value = "OCEAF";
                    Gosub += CREATE_PANEL;
                    Jump += CASE_RANK_SELECTION_LOOP;
                }
                void ON_RANK_5( LabelCase l ) {
                    and( BLACK_LIST_MISSION_STAGE == 15, delegate { Jump += RACE_5_FINAL; } );
                    total_sub_mission.value = 3; // 4 - 1
                    labels[ 0 ].Label += RACE_5_0;
                    labels[ 1 ].Label += RACE_5_1;
                    labels[ 2 ].Label += RACE_5_2;
                    labels[ 3 ].Label += RACE_5_3;
                    Gosub += PLACE_PLAYER;
                    // Race names  
                    TEXT_PANEL[ 2 ].value = "@BLS@3";
                    TEXT_PANEL[ 3 ].value = "@BLS@2";
                    TEXT_PANEL[ 4 ].value = "@BLS@3";
                    TEXT_PANEL[ 5 ].value = "@BLS@1";
                    // Race car names
                    TEXT_PANEL[ 14 ].value = "STALION";
                    TEXT_PANEL[ 15 ].value = "VINCENT";
                    TEXT_PANEL[ 16 ].value = "SAVANNA";
                    TEXT_PANEL[ 17 ].value = "BLADE";
                    // Race zones  
                    TEXT_PANEL[ 26 ].value = "ELQUE";
                    TEXT_PANEL[ 27 ].value = "OVS";
                    TEXT_PANEL[ 28 ].value = "ROD";
                    TEXT_PANEL[ 29 ].value = "ROBAD";
                    Gosub += CREATE_PANEL;
                    Jump += CASE_RANK_SELECTION_LOOP;
                }
                void ON_RANK_4( LabelCase l ) {
                    and( BLACK_LIST_MISSION_STAGE == 63, delegate { Jump += RACE_4_FINAL; } );
                    total_sub_mission.value = 5; // 6 - 1
                    labels[ 0 ].Label += RACE_4_0;
                    labels[ 1 ].Label += RACE_4_1;
                    labels[ 2 ].Label += RACE_4_2;
                    labels[ 3 ].Label += RACE_4_3;
                    labels[ 4 ].Label += RACE_4_4;
                    labels[ 5 ].Label += RACE_4_5;
                    Gosub += PLACE_PLAYER;
                    // Race names  
                    TEXT_PANEL[ 2 ].value = "@BLS@2";
                    TEXT_PANEL[ 3 ].value = "@BLS@3";
                    TEXT_PANEL[ 4 ].value = "@BLS@2";
                    TEXT_PANEL[ 5 ].value = "@BLS@29";
                    TEXT_PANEL[ 6 ].value = "@BLS@29";
                    TEXT_PANEL[ 7 ].value = "@BLS@1";
                    // Race car names
                    TEXT_PANEL[ 14 ].value = "SUNRISE";
                    TEXT_PANEL[ 15 ].value = "HERMES";
                    TEXT_PANEL[ 16 ].value = "ESPERAN";
                    TEXT_PANEL[ 17 ].value = "BUCCANE";
                    TEXT_PANEL[ 18 ].value = "SABRE";
                    TEXT_PANEL[ 19 ].value = "OCEANIC";
                    // Race zones  
                    TEXT_PANEL[ 26 ].value = "LFL";
                    TEXT_PANEL[ 27 ].value = "BONE";
                    TEXT_PANEL[ 28 ].value = "SUNMA";
                    TEXT_PANEL[ 29 ].value = "PALO";
                    TEXT_PANEL[ 30 ].value = "REDW";
                    TEXT_PANEL[ 31 ].value = "PANOP";
                    Gosub += CREATE_PANEL;
                    Jump += CASE_RANK_SELECTION_LOOP;
                }
                void ON_RANK_3( LabelCase l ) {
                    and( BLACK_LIST_MISSION_STAGE == 255, delegate { Jump += RACE_3_FINAL; } );
                    total_sub_mission.value = 7; // 8 - 1
                    labels[ 0 ].Label += RACE_3_0;
                    labels[ 1 ].Label += RACE_3_1;
                    labels[ 2 ].Label += RACE_3_2;
                    labels[ 3 ].Label += RACE_3_3;
                    labels[ 4 ].Label += RACE_3_4;
                    labels[ 5 ].Label += RACE_3_5;
                    labels[ 6 ].Label += RACE_3_6;
                    labels[ 7 ].Label += RACE_3_7;
                    Gosub += PLACE_PLAYER;
                    // Race names  
                    TEXT_PANEL[ 2 ].value = "@BLS@3";
                    TEXT_PANEL[ 3 ].value = "@BLS@2";
                    TEXT_PANEL[ 4 ].value = "@BLS@1";
                    TEXT_PANEL[ 5 ].value = "@BLS@3";
                    TEXT_PANEL[ 6 ].value = "@BLS@2";
                    TEXT_PANEL[ 7 ].value = "@BLS@29";
                    TEXT_PANEL[ 8 ].value = "@BLS@1";
                    TEXT_PANEL[ 9 ].value = "@BLS@29";
                    // Race car names        
                    TEXT_PANEL[ 14 ].value = "EMPEROR";
                    TEXT_PANEL[ 15 ].value = "STAFFRD";
                    TEXT_PANEL[ 16 ].value = "ELEGANT";
                    TEXT_PANEL[ 17 ].value = "NEBULA";
                    TEXT_PANEL[ 18 ].value = "REGINA";
                    TEXT_PANEL[ 19 ].value = "SOLAIR";
                    TEXT_PANEL[ 20 ].value = "SENTINL";
                    TEXT_PANEL[ 21 ].value = "MERIT";
                    // Race zones            
                    TEXT_PANEL[ 26 ].value = "CUNTC";
                    TEXT_PANEL[ 27 ].value = "DILLI";
                    TEXT_PANEL[ 28 ].value = "PRP";
                    TEXT_PANEL[ 29 ].value = "BLUAC";
                    TEXT_PANEL[ 30 ].value = "SFDWT";
                    TEXT_PANEL[ 31 ].value = "LDOC";
                    TEXT_PANEL[ 32 ].value = "RED";
                    TEXT_PANEL[ 33 ].value = "LOT";
                    Gosub += CREATE_PANEL;
                    Jump += CASE_RANK_SELECTION_LOOP;
                }
                void ON_RANK_2( LabelCase l ) {
                    and( BLACK_LIST_MISSION_STAGE == 1023, delegate { Jump += RACE_2_FINAL; } );
                    total_sub_mission.value = 9; // 10 - 1
                    labels[ 0 ].Label += RACE_2_0;
                    labels[ 1 ].Label += RACE_2_1;
                    labels[ 2 ].Label += RACE_2_2;
                    labels[ 3 ].Label += RACE_2_3;
                    labels[ 4 ].Label += RACE_2_4;
                    labels[ 5 ].Label += RACE_2_5;
                    labels[ 6 ].Label += RACE_2_6;
                    labels[ 7 ].Label += RACE_2_7;
                    labels[ 8 ].Label += RACE_2_8;
                    labels[ 9 ].Label += RACE_2_9;
                    Gosub += PLACE_PLAYER;
                    // Race names
                    TEXT_PANEL[ 2 ].value = "@BLS@2";
                    TEXT_PANEL[ 3 ].value = "@BLS@1";
                    TEXT_PANEL[ 4 ].value = "@BLS@29";
                    TEXT_PANEL[ 5 ].value = "@BLS@2";
                    TEXT_PANEL[ 6 ].value = "@BLS@3";
                    TEXT_PANEL[ 7 ].value = "@BLS@29";
                    TEXT_PANEL[ 8 ].value = "@BLS@1";
                    TEXT_PANEL[ 9 ].value = "@BLS@29";
                    TEXT_PANEL[ 10 ].value = "@BLS@3";
                    TEXT_PANEL[ 11 ].value = "@BLS@2";
                    // Race car names
                    TEXT_PANEL[ 14 ].value = "PREMIER";
                    TEXT_PANEL[ 15 ].value = "URANUS";
                    TEXT_PANEL[ 16 ].value = "JESTER";
                    TEXT_PANEL[ 17 ].value = "STRETCH";
                    TEXT_PANEL[ 18 ].value = "ELEGY";
                    TEXT_PANEL[ 19 ].value = "WASHING";
                    TEXT_PANEL[ 20 ].value = "FELTZER";
                    TEXT_PANEL[ 21 ].value = "STRATUM";
                    TEXT_PANEL[ 22 ].value = "WINDSOR";
                    TEXT_PANEL[ 23 ].value = "TRASHM";
                    // Race zones
                    TEXT_PANEL[ 26 ].value = "LVA";
                    TEXT_PANEL[ 27 ].value = "BONE";
                    TEXT_PANEL[ 28 ].value = "JUNIHO";
                    TEXT_PANEL[ 29 ].value = "MULINT";
                    TEXT_PANEL[ 30 ].value = "SILLY";
                    TEXT_PANEL[ 31 ].value = "VERO";
                    TEXT_PANEL[ 32 ].value = "CALT";
                    TEXT_PANEL[ 33 ].value = "ANGPI";
                    TEXT_PANEL[ 34 ].value = "PAYAS";
                    TEXT_PANEL[ 35 ].value = "SFDWT";
                    Gosub += CREATE_PANEL;
                    Jump += CASE_RANK_SELECTION_LOOP;
                }
                void ON_RANK_1( LabelCase l ) {
                    and( BLACK_LIST_MISSION_STAGE == 4095, delegate { Jump += RACE_1_FINAL1; } ); // RACE_1_FINAL2
                    total_sub_mission.value = 11; // 12 - 1
                    labels[ 0 ].Label += RACE_1_0;
                    labels[ 1 ].Label += RACE_1_1;
                    labels[ 2 ].Label += RACE_1_2;
                    labels[ 3 ].Label += RACE_1_3;
                    labels[ 4 ].Label += RACE_1_4;
                    labels[ 5 ].Label += RACE_1_5;
                    labels[ 6 ].Label += RACE_1_6;
                    labels[ 7 ].Label += RACE_1_7;
                    labels[ 8 ].Label += RACE_1_8;
                    labels[ 9 ].Label += RACE_1_9;
                    labels[ 10 ].Label += RACE_1_10;
                    labels[ 11 ].Label += RACE_1_11;
                    Gosub += PLACE_PLAYER;
                    // Race names
                    TEXT_PANEL[ 2 ].value = "@BLS@29";
                    TEXT_PANEL[ 3 ].value = "@BLS@1";
                    TEXT_PANEL[ 4 ].value = "@BLS@3";
                    TEXT_PANEL[ 5 ].value = "@BLS@29";
                    TEXT_PANEL[ 6 ].value = "@BLS@2";
                    TEXT_PANEL[ 7 ].value = "@BLS@1";
                    TEXT_PANEL[ 8 ].value = "@BLS@3";
                    TEXT_PANEL[ 9 ].value = "@BLS@3";
                    TEXT_PANEL[ 10 ].value = "@BLS@4";
                    TEXT_PANEL[ 11 ].value = "@BLS@2";
                    TEXT_PANEL[ 12 ].value = "@BLS@1";
                    TEXT_PANEL[ 13 ].value = "@BLS@29";
                    // Race car names
                    TEXT_PANEL[ 14 ].value = "ALPHA";
                    TEXT_PANEL[ 15 ].value = "ZR350";
                    TEXT_PANEL[ 16 ].value = "CHEETAH";
                    TEXT_PANEL[ 17 ].value = "CLUB";
                    TEXT_PANEL[ 18 ].value = "RANGER";
                    TEXT_PANEL[ 19 ].value = "BLISTAC";
                    TEXT_PANEL[ 20 ].value = "BUFFALO";
                    TEXT_PANEL[ 21 ].value = "SUPERGT";
                    TEXT_PANEL[ 22 ].value = "COMET";
                    TEXT_PANEL[ 23 ].value = "BULLET";
                    TEXT_PANEL[ 24 ].value = "EUROS";
                    TEXT_PANEL[ 25 ].value = "BANSHEE";
                    // Race zones
                    TEXT_PANEL[ 26 ].value = "SFAIR";
                    TEXT_PANEL[ 27 ].value = "VAIR";
                    TEXT_PANEL[ 28 ].value = "BARRA";
                    TEXT_PANEL[ 29 ].value = "JEF";
                    TEXT_PANEL[ 30 ].value = "PER1";
                    TEXT_PANEL[ 31 ].value = "MUL";
                    TEXT_PANEL[ 32 ].value = "ROBAD";
                    TEXT_PANEL[ 33 ].value = "HBARNS";
                    TEXT_PANEL[ 34 ].value = "WHET";
                    TEXT_PANEL[ 35 ].value = "BONE";
                    TEXT_PANEL[ 36 ].value = "ROCE";
                    TEXT_PANEL[ 37 ].value = "LAIR";
                    Gosub += CREATE_PANEL;
                    Jump += CASE_RANK_SELECTION_LOOP;
                }

            } );
            Jump += CASE_END;

            OnPassed = ON_PASSED_DEFAULT;
            OnFailed = ON_FAILED_DEFAULT;
            OnClear = ON_CLEAR_DEFAULT;
        }

        private void CASE_END( LabelJump label ) {
            Gosub += MOVE_PLAYER;
            create_thread<BLSTART>();
            @return();
        }

        private void SHOW_SUGET_SCENE_AFTER( LabelJump label ) {
            play_audio_event_at_position( 0.0, 0.0, 0.0, 1058 );
            race_checkpoint.disable();
            race_marker.disable_if_exist();
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            __set_entered_names( false );
            p.clear_wanted_level();
            __disable_player_controll_in_cutscene( true );
            __fade( false, true );
            AUDIO_BG.unload();
            clear_area( true, START_POINT_X, START_POINT_Y, START_POINT_Z, 10.0 );
            a.teleport_without_car( START_POINT_X, START_POINT_Y, START_POINT_Z, START_POINT_A );
            player_car.destroy_if_exist();
            enemy_actor.destroy_if_exist();
            enemy_car.destroy_if_exist();
            wait( AUDIO_BG.is_stopped );
            __camera_default();
            jump_table( BLACK_LIST_MISSION_PASSED, table => {
                table.Auto += delegate { jump( table.EndLabel ); };
                table.Auto += delegate { Gosub += PLAY_CUTSCENE_6A; jump( table.EndLabel ); }; // 6
                table.Auto += delegate { Gosub += PLAY_CUTSCENE_5A; jump( table.EndLabel ); }; // 5
                table.Auto += delegate { Gosub += PLAY_CUTSCENE_4A; jump( table.EndLabel ); }; // 4
                table.Auto += delegate { Gosub += PLAY_CUTSCENE_3A; jump( table.EndLabel ); }; // 3
                table.Auto += delegate { Gosub += PLAY_CUTSCENE_2A; jump( table.EndLabel ); }; // 2
                table.Auto += delegate {                                                       // 1
                    and( final_first_race_passed == 0, delegate {
                        Jump += RACE_1_FINAL2;
                    } );
                    Gosub += PLAY_CUTSCENE_1A;
                    jump( table.EndLabel );
                };
            } );
            Gosub += PLACE_PLAYER;
            Jump += PASSED;
        }

        #region FIRST RACE ( CASE == 0 )

        private void RACE_FIRST_RACE_LOOP( LabelJump label ) {
            Gosub += RACE_GENERIC_RESTORE_PLAYER_CONTROL;
            enemy_car.start_path( loaded_path ).set_path_speed( race_path_speed );
            enemy_actor.set_cant_be_dragged_out( true );
            set_vehicle_traffic_density_multiplier( 0.7 );
            set_ped_traffic_density_multiplier( 1.0 );
            enemy_marker.create_above_vehicle( enemy_car ).set_radar_mode( 1 ).set_type( 1 );
            show_text_highpriority( "@BLS@43", 6000, 1 );
            Jump += RACE_FIRST_RACE_LOOP1;
        }

        private void RACE_FIRST_RACE_LOOP1( LabelJump label ) {
            wait( 0 );
            enemy_actor.do_if_dead( delegate { ___jump_failed_no_put_player( "@BLS@40" ); } );
            enemy_car.do_if_wrecked( delegate { ___jump_failed_no_put_player( "@BLS@39" ); } );
            player_car.get_position( smission_carp_x, smission_carp_y, smission_carp_z );
            enemy_car.get_position( smission_care_x, smission_care_y, smission_care_z );
            get_distance_3d( smission_carp_x, smission_carp_y, smission_carp_z, smission_care_x, smission_care_y, smission_care_z, smission_distance );
            and( smission_distance > 200.0, !enemy_car.is_on_screen(), delegate { ___jump_failed_no_put_player( "@BLS@42" ); } );
            and(
                enemy_car.is_near_point_3d( false, START_POINT_X, START_POINT_Y, START_POINT_Z, 25.0, 25.0, 25.0 ),
                a.is_near_point_3d( false, START_POINT_X, START_POINT_Y, START_POINT_Z, 14.0, 14.0, 14.0 )
            , delegate {
                Jump += CASE_CHASE_FIN;
            } );
            jump( RACE_FIRST_RACE_LOOP1 );
        }

        private void CASE_CHASE_FIN( LabelJump label ) {
            and( enemy_marker.is_enabled(), delegate { enemy_marker.disable(); } );
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            __set_entered_names( false );
            a.set_immunities( 1, 1, 1, 1, 1 );
            p.clear_wanted_level().can_move( false );
            and( a.is_in_any_vehicle(), delegate { Gosub += REPAIR_PLAYER_CAR; } );
            AUDIO_BG.unload();
            __fade( false, true );
            and( a.is_in_any_vehicle(), delegate {
                a.remove_from_vehicle_and_place_at( 34.3467, -2653.2686, 400.4981 );
            }, delegate {
                a.set_position( 34.3467, -2653.2686, 400.4981 );
            } );
            clear_area( true, START_POINT_X, START_POINT_Y, START_POINT_Z, 300.0 );
            a.set_position( START_POINT_X, START_POINT_Y, START_POINT_Z ).set_z_angle( START_POINT_A );
            wait( AUDIO_BG.is_stopped );
            Gosub += PLAY_CUTSCENE_0A;
            Gosub += RACE_GENERIC_RESTORE_PLAYER_CONTROL;
            Jump += PASSED_CHASE;
        }

        #endregion

        #region SELECT A QUALIFY RACE ( CASE != 0 )

        private void CASE_RANK_SELECTION_LOOP( LabelJump label ) {
            wait( 0 );
            and( is_language_changed(), delegate {
                main_panel.remove();
                Gosub += CREATE_PANEL;
            } );
            and( is_game_key_pressed( Keys.VEHICLE_ENTER_EXIT ), delegate {
                __fade( false, true );
                Gosub += REMOVE_PANEL;
                Jump += CASE_RANK_SELECTION_LOOP_END;
            } );
            and( is_game_key_pressed( Keys.PED_SPRINT ), delegate {
                main_panel.get_active_row( main_panel_activated_row );
                selected_jump.value = labels[ main_panel_activated_row ];
                Gosub += REMOVE_PANEL;
                override_next_restart( START_POINT_X, START_POINT_Y, START_POINT_Z, START_POINT_A );
                __fade( false, true );
                jump( selected_jump );
            } );
            jump( CASE_RANK_SELECTION_LOOP );
        }

        private void CASE_RANK_SELECTION_LOOP_END( LabelJump label ) {
            Jump += CASE_END;
        }

        private void CREATE_PANEL( LabelGosub label ) {
            enable_radar( false );
            set_text_boxes_width( 350 );
            show_permanent_text_box( "TATTA" );
            main_panel.create( sString.DUMMY, 29.0, 98.0, 280.0, 3, true, true, PanelAlign.CENTER )
                .set_column_data( 0, "@BLS@5", TEXT_PANEL[ 2 ], TEXT_PANEL[ 3 ], TEXT_PANEL[ 4 ], TEXT_PANEL[ 5 ], TEXT_PANEL[ 6 ], TEXT_PANEL[ 7 ], TEXT_PANEL[ 8 ], TEXT_PANEL[ 9 ], TEXT_PANEL[ 10 ], TEXT_PANEL[ 11 ], TEXT_PANEL[ 12 ], TEXT_PANEL[ 13 ] )
                .set_column_data( 1, "@BLS@27", TEXT_PANEL[ 14 ], TEXT_PANEL[ 15 ], TEXT_PANEL[ 16 ], TEXT_PANEL[ 17 ], TEXT_PANEL[ 18 ], TEXT_PANEL[ 19 ], TEXT_PANEL[ 20 ], TEXT_PANEL[ 21 ], TEXT_PANEL[ 22 ], TEXT_PANEL[ 23 ], TEXT_PANEL[ 24 ], TEXT_PANEL[ 25 ] )
                .set_column_data( 2, "@BLS@28", TEXT_PANEL[ 26 ], TEXT_PANEL[ 27 ], TEXT_PANEL[ 28 ], TEXT_PANEL[ 29 ], TEXT_PANEL[ 30 ], TEXT_PANEL[ 31 ], TEXT_PANEL[ 32 ], TEXT_PANEL[ 33 ], TEXT_PANEL[ 34 ], TEXT_PANEL[ 35 ], TEXT_PANEL[ 36 ], TEXT_PANEL[ 37 ] )
                .set_column_width( 0, 150 )
                .set_column_width( 1, 150 )
                .set_column_width( 2, 260 )
                .set_column_alignment( 0, PanelAlign.LEFT )
                .set_column_alignment( 1, PanelAlign.LEFT )
                .set_column_alignment( 2, PanelAlign.LEFT );
            to( loop_index, 0, total_sub_mission, e => {
                and( BLACK_LIST_MISSION_STAGE.is_bit_set( loop_index ), delegate {
                    main_panel.highlight_row( loop_index, true );
                } );
            } );
        }

        private void REMOVE_PANEL( LabelGosub label ) {
            enable_radar( true );
            __clear_text();
            set_text_boxes_width( 200 );
            main_panel.remove();
        }

        private void SET_ALL_DUMMY( LabelGosub label ) {
            for( ushort i = 2; i < TEXT_PANEL.count; i++ )
                TEXT_PANEL[ i ].value = sString.DUMMY;
        }

        #endregion

        #region QUALIFY RACES

        private void RACE_6_0( LabelJump label ) {
            race_start_x.value = -1006.9645;
            race_start_y.value = -1993.1404;
            race_start_z.value = 70.9259;
            race_start_a.value = 147.221;
            player_car_model.value = CarModel.TAHOMA;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_checkpoint_position_x.value = -1130.4773;
            race_checkpoint_position_y.value = -2571.4651;
            race_checkpoint_position_z.value = 71.4022;
            race_checkpoint_type.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 19000;
            Gosub += CREATE_RACE_CHECKPOINT;
            Jump += RACE_GENERIC_SPRINT_START;
        }

        private void RACE_6_1( LabelJump label ) {
            race_start_x.value = 538.8965;
            race_start_y.value = -142.6171;
            race_start_z.value = 37.2122;
            race_start_a.value = 258.4155;
            player_car_model.value = CarModel.INTRUDER;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_checkpoint_position_x.value = 1208.6589;
            race_checkpoint_position_y.value = -222.1032;
            race_checkpoint_position_z.value = 31.2555;
            race_checkpoint_type.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 25000;
            Gosub += CREATE_RACE_CHECKPOINT;
            Jump += RACE_GENERIC_SPRINT_START;
        }

        private void RACE_6_2( LabelJump label ) {
            race_start_x.value = -2687.8179;
            race_start_y.value = 142.9885;
            race_start_z.value = 3.9389;
            race_start_a.value = 91.0671;
            player_car_model.value = CarModel.MANANA;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            cops_min_wanted_level.value = 2;
            cops_wanted_to_zero_flag.value = 0;
            MISSION_GLOBAL_TIMER_1.value = 120000;
            Jump += RACE_GENERIC_COPS_START;
        }

        // ---

        private void RACE_5_0( LabelJump label ) {
            race_start_x.value = -1428.9956;
            race_start_y.value = 2471.6895;
            race_start_z.value = 60.8942;
            race_start_a.value = 341.0556;
            player_car_model.value = CarModel.STALLION;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            MISSION_GLOBAL_TIMER_1.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX 
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 7;   // TOTAL POINTS - 1 
            // ALL position point X
            checkpoint_x[ 0 ].value = -1353.8298;
            checkpoint_x[ 1 ].value = -1634.5664;
            checkpoint_x[ 2 ].value = -1546.9979;
            checkpoint_x[ 3 ].value = -1545.5499;
            checkpoint_x[ 4 ].value = -1496.2504;
            checkpoint_x[ 5 ].value = -1380.6322;
            checkpoint_x[ 6 ].value = -1198.4952;
            checkpoint_x[ 7 ].value = -764.7676;
            // ALL position point Y
            checkpoint_y[ 0 ].value = 2650.7368;
            checkpoint_y[ 1 ].value = 2719.1306;
            checkpoint_y[ 2 ].value = 2671.989;
            checkpoint_y[ 3 ].value = 2557.7693;
            checkpoint_y[ 4 ].value = 2601.2234;
            checkpoint_y[ 5 ].value = 2602.0291;
            checkpoint_y[ 6 ].value = 2689.1917;
            checkpoint_y[ 7 ].value = 2730.7678;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 50.7801;
            checkpoint_z[ 1 ].value = 57.4863;
            checkpoint_z[ 2 ].value = 55.3652;
            checkpoint_z[ 3 ].value = 55.364;
            checkpoint_z[ 4 ].value = 55.3635;
            checkpoint_z[ 5 ].value = 54.7542;
            checkpoint_z[ 6 ].value = 45.5417;
            checkpoint_z[ 7 ].value = 45.1943;
            // ALL timer seconds adder
            checkpoint_add_time[ 0 ].value = 10000;
            checkpoint_add_time[ 1 ].value = 11000;
            checkpoint_add_time[ 2 ].value = 7000;
            checkpoint_add_time[ 3 ].value = 7000;
            checkpoint_add_time[ 4 ].value = 7000;
            checkpoint_add_time[ 5 ].value = 7000;
            checkpoint_add_time[ 6 ].value = 8000;
            checkpoint_add_time[ 7 ].value = 1000;
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_CHECKPOINT_START;
        }

        private void RACE_5_1( LabelJump label ) {
            race_start_x.value = 2252.7178;
            race_start_y.value = 2038.2081;
            race_start_z.value = 10.4189;
            race_start_a.value = 181.6743;
            player_car_model.value = CarModel.VINCENT;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            cops_min_wanted_level.value = 2;
            cops_wanted_to_zero_flag.value = 0;
            MISSION_GLOBAL_TIMER_1.value = 180000;
            Jump += RACE_GENERIC_COPS_START;
        }

        private void RACE_5_2( LabelJump label ) {
            race_start_x.value = 301.9749;
            race_start_y.value = -1485.8059;
            race_start_z.value = 32.2987;
            race_start_a.value = 309.7876;
            player_car_model.value = CarModel.SAVANNA;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            MISSION_GLOBAL_TIMER_1.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX 
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 10;  // TOTAL POINTS - 1 
            // ALL position point X
            checkpoint_x[ 0 ].value = 503.0342;
            checkpoint_x[ 1 ].value = 526.3802;
            checkpoint_x[ 2 ].value = 796.1172;
            checkpoint_x[ 3 ].value = 772.0358;
            checkpoint_x[ 4 ].value = 839.166;
            checkpoint_x[ 5 ].value = 932.6604;
            checkpoint_x[ 6 ].value = 1288.3818;
            checkpoint_x[ 7 ].value = 1356.5276;
            checkpoint_x[ 8 ].value = 1357.1481;
            checkpoint_x[ 9 ].value = 1373.3915;
            checkpoint_x[ 10 ].value = 1716.8041;
            // ALL position point Y
            checkpoint_y[ 0 ].value = -1343.7954;
            checkpoint_y[ 1 ].value = -1409.8468;
            checkpoint_y[ 2 ].value = -1402.278;
            checkpoint_y[ 3 ].value = -1583.2455;
            checkpoint_y[ 4 ].value = -1609.494;
            checkpoint_y[ 5 ].value = -1572.3896;
            checkpoint_y[ 6 ].value = -1571.1742;
            checkpoint_y[ 7 ].value = -1384.2269;
            checkpoint_y[ 8 ].value = -1125.6647;
            checkpoint_y[ 9 ].value = -1038.4204;
            checkpoint_y[ 10 ].value = -1023.3928;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 15.5526;
            checkpoint_z[ 1 ].value = 15.5655;
            checkpoint_z[ 2 ].value = 13.0162;
            checkpoint_z[ 3 ].value = 12.9832;
            checkpoint_z[ 4 ].value = 12.9274;
            checkpoint_z[ 5 ].value = 12.9903;
            checkpoint_z[ 6 ].value = 12.9878;
            checkpoint_z[ 7 ].value = 13.0964;
            checkpoint_z[ 8 ].value = 23.2795;
            checkpoint_z[ 9 ].value = 25.9163;
            checkpoint_z[ 10 ].value = 23.5101;
            // ALL timer seconds adder
            checkpoint_add_time[ 0 ].value = 11000;
            checkpoint_add_time[ 1 ].value = 9000;
            checkpoint_add_time[ 2 ].value = 9000;
            checkpoint_add_time[ 3 ].value = 9000;
            checkpoint_add_time[ 4 ].value = 8000;
            checkpoint_add_time[ 5 ].value = 5000;
            checkpoint_add_time[ 6 ].value = 5000;
            checkpoint_add_time[ 7 ].value = 9000;
            checkpoint_add_time[ 8 ].value = 7000;
            checkpoint_add_time[ 9 ].value = 4000;
            checkpoint_add_time[ 10 ].value = 2000;
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_CHECKPOINT_START;
        }

        private void RACE_5_3( LabelJump label ) {
            race_start_x.value = -1147.9069;
            race_start_y.value = 1797.4347;
            race_start_z.value = 39.8775;
            race_start_a.value = 275.5434;
            player_car_model.value = CarModel.TAHOMA;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_checkpoint_position_x.value = -440.3059;
            race_checkpoint_position_y.value = 2044.1918;
            race_checkpoint_position_z.value = 60.4466;
            race_checkpoint_type.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 35000;
            Gosub += CREATE_RACE_CHECKPOINT;
            Jump += RACE_GENERIC_SPRINT_START;
        }

        // ---

        private void RACE_4_0( LabelJump label ) {
            race_start_x.value = 2626.2942;
            race_start_y.value = -1350.7317;
            race_start_z.value = 34.471;
            race_start_a.value = 269.7687;
            player_car_model.value = CarModel.SUNRISE;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            cops_min_wanted_level.value = 3;
            cops_wanted_to_zero_flag.value = 0;
            MISSION_GLOBAL_TIMER_1.value = 120000;
            Jump += RACE_GENERIC_COPS_START;
        }

        private void RACE_4_1( LabelJump label ) {
            race_start_x.value = -291.8905;
            race_start_y.value = 1260.5519;
            race_start_z.value = 24.0923;
            race_start_a.value = 206.0634;
            player_car_model.value = CarModel.HERMES;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            MISSION_GLOBAL_TIMER_1.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX 
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 10;  // TOTAL POINTS - 1
            // ALL position point X
            checkpoint_x[ 0 ].value = -120.2598;
            checkpoint_x[ 1 ].value = 86.7504;
            checkpoint_x[ 2 ].value = -251.0267;
            checkpoint_x[ 3 ].value = -275.7644;
            checkpoint_x[ 4 ].value = -194.8347;
            checkpoint_x[ 5 ].value = -290.2623;
            checkpoint_x[ 6 ].value = -122.0993;
            checkpoint_x[ 7 ].value = 151.1165;
            checkpoint_x[ 8 ].value = 223.4217;
            checkpoint_x[ 9 ].value = 266.5853;
            checkpoint_x[ 10 ].value = 354.607;
            // ALL position point Y
            checkpoint_y[ 0 ].value = 1254.7985;
            checkpoint_y[ 1 ].value = 1198.5574;
            checkpoint_y[ 2 ].value = 1197.7938;
            checkpoint_y[ 3 ].value = 1027.553;
            checkpoint_y[ 4 ].value = 1018.4623;
            checkpoint_y[ 5 ].value = 806.1741;
            checkpoint_y[ 6 ].value = 823.9033;
            checkpoint_y[ 7 ].value = 896.3835;
            checkpoint_y[ 8 ].value = 973.3634;
            checkpoint_y[ 9 ].value = 1227.8822;
            checkpoint_y[ 10 ].value = 1432.0419;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 17.7081;
            checkpoint_z[ 1 ].value = 18.1192;
            checkpoint_z[ 2 ].value = 19.2119;
            checkpoint_z[ 3 ].value = 19.2077;
            checkpoint_z[ 4 ].value = 19.2082;
            checkpoint_z[ 5 ].value = 14.4474;
            checkpoint_z[ 6 ].value = 20.1568;
            checkpoint_z[ 7 ].value = 20.2167;
            checkpoint_z[ 8 ].value = 27.8253;
            checkpoint_z[ 9 ].value = 15.2037;
            checkpoint_z[ 10 ].value = 6.3097;
            // ALL timer seconds adder
            checkpoint_add_time[ 0 ].value = 12000;
            checkpoint_add_time[ 1 ].value = 8000;
            checkpoint_add_time[ 2 ].value = 10000;
            checkpoint_add_time[ 3 ].value = 8000;
            checkpoint_add_time[ 4 ].value = 5000;
            checkpoint_add_time[ 5 ].value = 9000;
            checkpoint_add_time[ 6 ].value = 9000;
            checkpoint_add_time[ 7 ].value = 9000;
            checkpoint_add_time[ 8 ].value = 4000;
            checkpoint_add_time[ 9 ].value = 9000;
            checkpoint_add_time[ 10 ].value = 4000;
            __set_traffic( 1.0 );
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_CHECKPOINT_START;
        }

        private void RACE_4_2( LabelJump label ) {
            race_start_x.value = -2253.6011;
            race_start_y.value = 2356.8228;
            race_start_z.value = 4.5328;
            race_start_a.value = 109.9932;
            player_car_model.value = CarModel.ESPERANT;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            cops_min_wanted_level.value = 3;
            cops_wanted_to_zero_flag.value = 0;
            MISSION_GLOBAL_TIMER_1.value = 180000;
            Jump += RACE_GENERIC_COPS_START;
        }

        private void RACE_4_3( LabelJump label ) {
            race_start_x.value = 2344.2415;
            race_start_y.value = 248.6808;
            race_start_z.value = 25.8506;
            race_start_a.value = 178.4715;
            player_car_model.value = CarModel.BUCCANEE;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_need_speed.value = 155;
            cops_min_wanted_level.value = 0;
            MISSION_GLOBAL_TIMER_1.value = 100000;
            // ALL position point X
            point4_x[ 0 ].value = 2344.1687;
            point4_x[ 1 ].value = 2294.0503;
            point4_x[ 2 ].value = 2095.1079;
            point4_x[ 3 ].value = 2522.4446;
            // ALL position point Y 
            point4_y[ 0 ].value = -83.5495;
            point4_y[ 1 ].value = 128.5309;
            point4_y[ 2 ].value = 41.4873;
            point4_y[ 3 ].value = -8.2524;
            // ALL position point Z
            point4_z[ 0 ].value = 25.8517;
            point4_z[ 1 ].value = 25.8508;
            point4_z[ 2 ].value = 25.8516;
            point4_z[ 3 ].value = 25.8531;
            Jump += RACE_GENERIC_4POINT_START;
        }

        private void RACE_4_4( LabelJump label ) {
            race_start_x.value = 1374.4955;
            race_start_y.value = 2051.0962;
            race_start_z.value = 10.2828;
            race_start_a.value = 272.7748;
            player_car_model.value = CarModel.SABRE;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_need_speed.value = 155;
            cops_min_wanted_level.value = 0;
            MISSION_GLOBAL_TIMER_1.value = 100000;
            // ALL position point X
            point4_x[ 0 ].value = 1840.8165;
            point4_x[ 1 ].value = 1707.1162;
            point4_x[ 2 ].value = 1358.9785;
            point4_x[ 3 ].value = 1538.0226;
            // ALL position point Y 
            point4_y[ 0 ].value = 2173.8657;
            point4_y[ 1 ].value = 1960.3606;
            point4_y[ 2 ].value = 1873.4614;
            point4_y[ 3 ].value = 1972.4124;
            // ALL position point Z
            point4_z[ 0 ].value = 10.2834;
            point4_z[ 1 ].value = 10.2747;
            point4_z[ 2 ].value = 10.2942;
            point4_z[ 3 ].value = 10.2741;
            Jump += RACE_GENERIC_4POINT_START;
        }

        private void RACE_4_5( LabelJump label ) {
            race_start_x.value = -737.8134;
            race_start_y.value = 227.4099;
            race_start_z.value = 2.0478;
            race_start_a.value = 160.3381;
            player_car_model.value = CarModel.OCEANIC;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_checkpoint_position_x.value = -820.9014;
            race_checkpoint_position_y.value = -116.0159;
            race_checkpoint_position_z.value = 62.5989;
            race_checkpoint_type.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 46000;
            __set_traffic( 1.0 );
            Gosub += CREATE_RACE_CHECKPOINT;
            Jump += RACE_GENERIC_SPRINT_START;
        }

        // ---

        private void RACE_3_0( LabelJump label ) {
            race_start_x.value = -2653.3149;
            race_start_y.value = -244.5107;
            race_start_z.value = 5.6046;
            race_start_a.value = 354.1488;
            player_car_model.value = CarModel.EMPEROR;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            MISSION_GLOBAL_TIMER_1.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX 
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 10;  // TOTAL POINTS - 1
            // ALL position point X
            checkpoint_x[ 0 ].value = -2655.5601;
            checkpoint_x[ 1 ].value = -2704.2209;
            checkpoint_x[ 2 ].value = -2707.2498;
            checkpoint_x[ 3 ].value = -2807.0645;
            checkpoint_x[ 4 ].value = -2853.1833;
            checkpoint_x[ 5 ].value = -2857.7346;
            checkpoint_x[ 6 ].value = -2829.9758;
            checkpoint_x[ 7 ].value = -2863.5762;
            checkpoint_x[ 8 ].value = -2580.5669;
            checkpoint_x[ 9 ].value = -2369.3364;
            checkpoint_x[ 10 ].value = -2366.2039;
            // ALL position point Y
            checkpoint_y[ 0 ].value = -71.0273;
            checkpoint_y[ 1 ].value = -70.7498;
            checkpoint_y[ 2 ].value = 285.4928;
            checkpoint_y[ 3 ].value = 288.7791;
            checkpoint_y[ 4 ].value = 460.8986;
            checkpoint_y[ 5 ].value = 728.1498;
            checkpoint_y[ 6 ].value = 964.5407;
            checkpoint_y[ 7 ].value = 1213.5902;
            checkpoint_y[ 8 ].value = 1349.8604;
            checkpoint_y[ 9 ].value = 1377.4518;
            checkpoint_y[ 10 ].value = 1184.9755;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 3.838;
            checkpoint_z[ 1 ].value = 3.952;
            checkpoint_z[ 2 ].value = 3.9446;
            checkpoint_z[ 3 ].value = 6.6851;
            checkpoint_z[ 4 ].value = 3.8762;
            checkpoint_z[ 5 ].value = 29.1235;
            checkpoint_z[ 6 ].value = 43.5618;
            checkpoint_z[ 7 ].value = 5.2331;
            checkpoint_z[ 8 ].value = 6.697;
            checkpoint_z[ 9 ].value = 6.7788;
            checkpoint_z[ 10 ].value = 40.5043;
            // ALL timer seconds adder
            checkpoint_add_time[ 0 ].value = 10000;
            checkpoint_add_time[ 1 ].value = 10640;
            checkpoint_add_time[ 2 ].value = 13000;
            checkpoint_add_time[ 3 ].value = 5350;
            checkpoint_add_time[ 4 ].value = 6847;
            checkpoint_add_time[ 5 ].value = 10277;
            checkpoint_add_time[ 6 ].value = 10038;
            checkpoint_add_time[ 7 ].value = 12150;
            checkpoint_add_time[ 8 ].value = 14160;
            checkpoint_add_time[ 9 ].value = 7360;
            checkpoint_add_time[ 10 ].value = 5000;
            __set_traffic( 1.0 );
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_CHECKPOINT_START;
        }

        private void RACE_3_1( LabelJump label ) {
            race_start_x.value = 663.5873;
            race_start_y.value = -470.6936;
            race_start_z.value = 16.1913;
            race_start_a.value = 204.0146;
            player_car_model.value = CarModel.STAFFORD;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            cops_min_wanted_level.value = 3;
            cops_wanted_to_zero_flag.value = 0;
            MISSION_GLOBAL_TIMER_1.value = 240000;
            Jump += RACE_GENERIC_COPS_START;
        }

        private void RACE_3_2( LabelJump label ) {
            race_start_x.value = 1482.8727;
            race_start_y.value = 2824.1614;
            race_start_z.value = 10.3974;
            race_start_a.value = 183.3043;
            player_car_model.value = CarModel.ELEGANT;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_checkpoint_position_x.value = 2286.4443;
            race_checkpoint_position_y.value = 2513.3911;
            race_checkpoint_position_z.value = 10.2476;
            race_checkpoint_type.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 47000;
            __set_traffic( 1.0 );
            Gosub += CREATE_RACE_CHECKPOINT;
            Jump += RACE_GENERIC_SPRINT_START;
        }

        private void RACE_3_3( LabelJump label ) {
            race_start_x.value = -264.0361;
            race_start_y.value = -178.0791;
            race_start_z.value = 2.8378;
            race_start_a.value = 289.8618;
            player_car_model.value = CarModel.NEBULA;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            MISSION_GLOBAL_TIMER_1.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX 
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 10;  // TOTAL POINTS - 1
            // ALL position point X
            checkpoint_x[ 0 ].value = 116.6541;
            checkpoint_x[ 1 ].value = 133.8979;
            checkpoint_x[ 2 ].value = 330.5952;
            checkpoint_x[ 3 ].value = 332.0269;
            checkpoint_x[ 4 ].value = 542.4365;
            checkpoint_x[ 5 ].value = 748.0524;
            checkpoint_x[ 6 ].value = 1133.7094;
            checkpoint_x[ 7 ].value = 1281.8684;
            checkpoint_x[ 8 ].value = 1256.8784;
            checkpoint_x[ 9 ].value = 1316.1746;
            checkpoint_x[ 10 ].value = 1124.8573;
            // ALL position point Y
            checkpoint_y[ 0 ].value = -211.5726;
            checkpoint_y[ 1 ].value = -75.7486;
            checkpoint_y[ 2 ].value = -71.9492;
            checkpoint_y[ 3 ].value = -140.8484;
            checkpoint_y[ 4 ].value = -142.8066;
            checkpoint_y[ 5 ].value = -154.592;
            checkpoint_y[ 6 ].value = -64.6875;
            checkpoint_y[ 7 ].value = -95.7213;
            checkpoint_y[ 8 ].value = 176.7296;
            checkpoint_y[ 9 ].value = 309.8902;
            checkpoint_y[ 10 ].value = 417.1217;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 1.1177;
            checkpoint_z[ 1 ].value = 1.1157;
            checkpoint_z[ 2 ].value = 1.117;
            checkpoint_z[ 3 ].value = 1.1092;
            checkpoint_z[ 4 ].value = 37.0264;
            checkpoint_z[ 5 ].value = 18.8516;
            checkpoint_z[ 6 ].value = 22.5237;
            checkpoint_z[ 7 ].value = 36.7863;
            checkpoint_z[ 8 ].value = 19.1009;
            checkpoint_z[ 9 ].value = 19.0945;
            checkpoint_z[ 10 ].value = 26.6808;
            // ALL timer seconds adder
            checkpoint_add_time[ 0 ].value = 17000;
            checkpoint_add_time[ 1 ].value = 8000;
            checkpoint_add_time[ 2 ].value = 7000;
            checkpoint_add_time[ 3 ].value = 4000;
            checkpoint_add_time[ 4 ].value = 10000;
            checkpoint_add_time[ 5 ].value = 6000;
            checkpoint_add_time[ 6 ].value = 14000;
            checkpoint_add_time[ 7 ].value = 7000;
            checkpoint_add_time[ 8 ].value = 10000;
            checkpoint_add_time[ 9 ].value = 5000;
            checkpoint_add_time[ 10 ].value = 5000;
            __set_traffic( 1.0 );
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_CHECKPOINT_START;
        }

        private void RACE_3_4( LabelJump label ) {
            race_start_x.value = -1504.8716;
            race_start_y.value = 1005.8498;
            race_start_z.value = 6.9847;
            race_start_a.value = 138.8624;
            player_car_model.value = CarModel.REGINA;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            cops_min_wanted_level.value = 4;
            cops_wanted_to_zero_flag.value = 0;
            MISSION_GLOBAL_TIMER_1.value = 120000;
            Jump += RACE_GENERIC_COPS_START;
        }

        private void RACE_3_5( LabelJump label ) {
            race_start_x.value = 2281.53;
            race_start_y.value = -2318.1343;
            race_start_z.value = 13.2018;
            race_start_a.value = 246.5797;
            player_car_model.value = CarModel.SOLAIR;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_need_speed.value = 135;
            cops_min_wanted_level.value = 0;
            MISSION_GLOBAL_TIMER_1.value = 90000;
            // ALL position point X
            point4_x[ 0 ].value = 2484.9895;
            point4_x[ 1 ].value = 2255.5896;
            point4_x[ 2 ].value = 2303.5933;
            point4_x[ 3 ].value = 2596.7917;
            // ALL position point Y 
            point4_y[ 0 ].value = -2608.658;
            point4_y[ 1 ].value = -2663.1484;
            point4_y[ 2 ].value = -2355.8411;
            point4_y[ 3 ].value = -2424.8369;
            // ALL position point Z
            point4_z[ 0 ].value = 13.1301;
            point4_z[ 1 ].value = 13.108;
            point4_z[ 2 ].value = 13.0459;
            point4_z[ 3 ].value = 13.1129;
            __set_traffic( 1.0 );
            Jump += RACE_GENERIC_4POINT_START;
        }

        private void RACE_3_6( LabelJump label ) {
            race_start_x.value = 2788.5137;
            race_start_y.value = -133.8538;
            race_start_z.value = 36.1836;
            race_start_a.value = 17.0049;
            player_car_model.value = CarModel.SENTINEL;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_checkpoint_position_x.value = 1807.036;
            race_checkpoint_position_y.value = 852.9388;
            race_checkpoint_position_z.value = 10.345;
            race_checkpoint_type.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 55000;
            __set_traffic( 1.0 );
            Gosub += CREATE_RACE_CHECKPOINT;
            Jump += RACE_GENERIC_SPRINT_START;
        }

        private void RACE_3_7( LabelJump label ) {
            race_start_x.value = 2625.4124;
            race_start_y.value = 1173.2032;
            race_start_z.value = 10.4896;
            race_start_a.value = 92.9199;
            player_car_model.value = CarModel.MERIT;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_need_speed.value = 133;
            cops_min_wanted_level.value = 0;
            MISSION_GLOBAL_TIMER_1.value = 150000;
            // ALL position point X
            point4_x[ 0 ].value = 2575.1206;
            point4_x[ 1 ].value = 2447.6963;
            point4_x[ 2 ].value = 2623.4661;
            point4_x[ 3 ].value = 2606.355;
            // ALL position point Y 
            point4_y[ 0 ].value = 1089.48;
            point4_y[ 1 ].value = 1159.0732;
            point4_y[ 2 ].value = 1254.5964;
            point4_y[ 3 ].value = 1035.3032;
            // ALL position point Z
            point4_z[ 0 ].value = 10.4895;
            point4_z[ 1 ].value = 10.4887;
            point4_z[ 2 ].value = 10.3406;
            point4_z[ 3 ].value = 10.3425;
            __set_traffic( 1.0 );
            Jump += RACE_GENERIC_4POINT_START;
        }

        // ---

        private void RACE_2_0( LabelJump label ) {
            race_start_x.value = 1671.6895;
            race_start_y.value = 956.3987;
            race_start_z.value = 10.272;
            race_start_a.value = 150.3635;
            player_car_model.value = CarModel.PREMIER;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            cops_min_wanted_level.value = 4;
            cops_wanted_to_zero_flag.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 180000;
            Jump += RACE_GENERIC_COPS_START;
        }

        private void RACE_2_1( LabelJump label ) {
            race_start_x.value = 580.1552;
            race_start_y.value = 1665.2616;
            race_start_z.value = 6.6097;
            race_start_a.value = 102.3542;
            player_car_model.value = CarModel.URANUS;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_checkpoint_position_x.value = 616.0771;
            race_checkpoint_position_y.value = 336.2517;
            race_checkpoint_position_z.value = 18.8628;
            race_checkpoint_type.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 46000;
            p.set_minimum_wanted_level( 3 );
            __set_traffic( 1.0 );
            Gosub += CREATE_RACE_CHECKPOINT;
            Jump += RACE_GENERIC_SPRINT_START;
        }

        private void RACE_2_2( LabelJump label ) {
            race_start_x.value = -2366.449;
            race_start_y.value = 1114.6938;
            race_start_z.value = 55.183;
            race_start_a.value = 248.6745;
            player_car_model.value = CarModel.JESTER;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_need_speed.value = 120;
            cops_min_wanted_level.value = 3;
            MISSION_GLOBAL_TIMER_1.value = 180000;
            // ALL position point X
            point4_x[ 0 ].value = -2180.2844;
            point4_x[ 1 ].value = -2258.4734;
            point4_x[ 2 ].value = -2409.8916;
            point4_x[ 3 ].value = -2523.623;
            // ALL position point Y 
            point4_y[ 0 ].value = 918.0303;
            point4_y[ 1 ].value = 1044.2383;
            point4_y[ 2 ].value = 957.799;
            point4_y[ 3 ].value = 1065.6565;
            // ALL position point Z
            point4_z[ 0 ].value = 78.5185;
            point4_z[ 1 ].value = 83.2563;
            point4_z[ 2 ].value = 44.7559;
            point4_z[ 3 ].value = 59.5848;
            __set_traffic( 1.0 );
            Jump += RACE_GENERIC_4POINT_START;
        }

        private void RACE_2_3( LabelJump label ) {
            race_start_x.value = 1625.5414;
            race_start_y.value = -1035.2953;
            race_start_z.value = 23.5598;
            race_start_a.value = 104.4194;
            player_car_model.value = CarModel.STRETCH;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            cops_min_wanted_level.value = 4;
            cops_wanted_to_zero_flag.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 120000;
            Jump += RACE_GENERIC_COPS_START;
        }

        private void RACE_2_4( LabelJump label ) {
            race_start_x.value = -2255.2378;
            race_start_y.value = -400.2619;
            race_start_z.value = 50.5322;
            race_start_a.value = 215.4915;
            player_car_model.value = CarModel.ELEGY;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            MISSION_GLOBAL_TIMER_1.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX 
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 8;   // TOTAL POINTS - 1
            // ALL position point X
            checkpoint_x[ 0 ].value = -2052.8877;
            checkpoint_x[ 1 ].value = -1988.7581;
            checkpoint_x[ 2 ].value = -2154.4602;
            checkpoint_x[ 3 ].value = -2158.5623;
            checkpoint_x[ 4 ].value = -1929.7161;
            checkpoint_x[ 5 ].value = -1922.0112;
            checkpoint_x[ 6 ].value = -1988.6713;
            checkpoint_x[ 7 ].value = -1998.8204;
            checkpoint_x[ 8 ].value = -1862.4611;
            // ALL position point Y
            checkpoint_y[ 0 ].value = -592.8478;
            checkpoint_y[ 1 ].value = -710.7228;
            checkpoint_y[ 2 ].value = -716.2751;
            checkpoint_y[ 3 ].value = -997.9041;
            checkpoint_y[ 4 ].value = -1004.8433;
            checkpoint_y[ 5 ].value = -721.4495;
            checkpoint_y[ 6 ].value = -724.4345;
            checkpoint_y[ 7 ].value = -1101.3851;
            checkpoint_y[ 8 ].value = -1155.7632;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 29.5515;
            checkpoint_z[ 1 ].value = 31.5355;
            checkpoint_z[ 2 ].value = 31.4902;
            checkpoint_z[ 3 ].value = 31.4837;
            checkpoint_z[ 4 ].value = 31.4843;
            checkpoint_z[ 5 ].value = 31.4907;
            checkpoint_z[ 6 ].value = 31.5369;
            checkpoint_z[ 7 ].value = 31.3871;
            checkpoint_z[ 8 ].value = 30.5057;
            // ALL timer seconds adder
            checkpoint_add_time[ 0 ].value = 13000;
            checkpoint_add_time[ 1 ].value = 10000;
            checkpoint_add_time[ 2 ].value = 10000;
            checkpoint_add_time[ 3 ].value = 10000;
            checkpoint_add_time[ 4 ].value = 6000;
            checkpoint_add_time[ 5 ].value = 6000;
            checkpoint_add_time[ 6 ].value = 6000;
            checkpoint_add_time[ 7 ].value = 6000;
            checkpoint_add_time[ 8 ].value = 6000;
            __set_traffic( 1.0 );
            p.set_minimum_wanted_level( 3 );
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_CHECKPOINT_START;
        }

        private void RACE_2_5( LabelJump label ) {
            race_start_x.value = 1210.614;
            race_start_y.value = -1694.2618;
            race_start_z.value = 13.1977;
            race_start_a.value = 182.3508;
            player_car_model.value = CarModel.WASHING;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_need_speed.value = 110;
            cops_min_wanted_level.value = 3;
            MISSION_GLOBAL_TIMER_1.value = 80000;
            // ALL position point X
            point4_x[ 0 ].value = 1128.3448;
            point4_x[ 1 ].value = 1016.9966;
            point4_x[ 2 ].value = 1099.1093;
            point4_x[ 3 ].value = 1262.7869;
            // ALL position point Y 
            point4_y[ 0 ].value = -1532.9099;
            point4_y[ 1 ].value = -1491.1344;
            point4_y[ 2 ].value = -1755.3989;
            point4_y[ 3 ].value = -1804.187;
            // ALL position point Z
            point4_z[ 0 ].value = 14.9916;
            point4_z[ 1 ].value = 13.0331;
            point4_z[ 2 ].value = 13.007;
            point4_z[ 3 ].value = 13.0585;
            __set_traffic( 1.0 );
            Jump += RACE_GENERIC_4POINT_START;
        }

        private void RACE_2_6( LabelJump label ) {
            race_start_x.value = -2077.771;
            race_start_y.value = 1345.2283;
            race_start_z.value = 6.6902;
            race_start_a.value = 225.2527;
            player_car_model.value = CarModel.FELTZER;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_checkpoint_position_x.value = -1519.7225;
            race_checkpoint_position_y.value = 687.096;
            race_checkpoint_position_z.value = 6.7731;
            race_checkpoint_type.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 37500;
            __set_traffic( 1.0 );
            p.set_minimum_wanted_level( 3 );
            Gosub += CREATE_RACE_CHECKPOINT;
            Jump += RACE_GENERIC_SPRINT_START;
        }

        private void RACE_2_7( LabelJump label ) {
            race_start_x.value = -2010.2799;
            race_start_y.value = -2497.2007;
            race_start_z.value = 32.5998;
            race_start_a.value = 145.4963;
            player_car_model.value = CarModel.STRATUM;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_need_speed.value = 148;
            cops_min_wanted_level.value = 2;
            MISSION_GLOBAL_TIMER_1.value = 120000;
            // ALL position point X
            point4_x[ 0 ].value = -2307.9128;
            point4_x[ 1 ].value = -2092.8789;
            point4_x[ 2 ].value = -2082.7207;
            point4_x[ 3 ].value = -2229.9172;
            // ALL position point Y 
            point4_y[ 0 ].value = -2297.3901;
            point4_y[ 1 ].value = -2365.7124;
            point4_y[ 2 ].value = -2293.2424;
            point4_y[ 3 ].value = -2442.1831;
            // ALL position point Z
            point4_z[ 0 ].value = 22.7441;
            point4_z[ 1 ].value = 30.3201;
            point4_z[ 2 ].value = 30.1449;
            point4_z[ 3 ].value = 30.145;
            __set_traffic( 1.0 );
            Jump += RACE_GENERIC_4POINT_START;
        }

        private void RACE_2_8( LabelJump label ) {
            race_start_x.value = -221.5292;
            race_start_y.value = 2621.887;
            race_start_z.value = 62.2002;
            race_start_a.value = 1.312;
            player_car_model.value = CarModel.WINDSOR;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            MISSION_GLOBAL_TIMER_1.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX 
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 8;   // TOTAL POINTS - 1
            // ALL position point X
            checkpoint_x[ 0 ].value = -187.5011;
            checkpoint_x[ 1 ].value = -189.2137;
            checkpoint_x[ 2 ].value = -252.4616;
            checkpoint_x[ 3 ].value = -257.376;
            checkpoint_x[ 4 ].value = -371.2586;
            checkpoint_x[ 5 ].value = -188.2815;
            checkpoint_x[ 6 ].value = -188.1481;
            checkpoint_x[ 7 ].value = -255.5374;
            checkpoint_x[ 8 ].value = -237.7494;
            // ALL position point Y
            checkpoint_y[ 0 ].value = 2647.2341;
            checkpoint_y[ 1 ].value = 2743.9768;
            checkpoint_y[ 2 ].value = 2748.1021;
            checkpoint_y[ 3 ].value = 2634.5437;
            checkpoint_y[ 4 ].value = 2684.3335;
            checkpoint_y[ 5 ].value = 2701.7153;
            checkpoint_y[ 6 ].value = 2635.9399;
            checkpoint_y[ 7 ].value = 2642.5942;
            checkpoint_y[ 8 ].value = 2816.4905;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 62.7368;
            checkpoint_z[ 1 ].value = 61.9714;
            checkpoint_z[ 2 ].value = 61.991;
            checkpoint_z[ 3 ].value = 61.9905;
            checkpoint_z[ 4 ].value = 63.7368;
            checkpoint_z[ 5 ].value = 61.971;
            checkpoint_z[ 6 ].value = 62.6811;
            checkpoint_z[ 7 ].value = 61.9933;
            checkpoint_z[ 8 ].value = 61.1797;
            // ALL timer seconds adder
            checkpoint_add_time[ 0 ].value = 4000;
            checkpoint_add_time[ 1 ].value = 4000;
            checkpoint_add_time[ 2 ].value = 3000;
            checkpoint_add_time[ 3 ].value = 4500;
            checkpoint_add_time[ 4 ].value = 6500;
            checkpoint_add_time[ 5 ].value = 7000;
            checkpoint_add_time[ 6 ].value = 4000;
            checkpoint_add_time[ 7 ].value = 4000;
            checkpoint_add_time[ 8 ].value = 6000;
            __set_traffic( 1.0 );
            p.set_minimum_wanted_level( 3 );
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_CHECKPOINT_START;
        }

        private void RACE_2_9( LabelJump label ) {
            race_start_x.value = -1987.2626;
            race_start_y.value = 546.3812;
            race_start_z.value = 35.5052;
            race_start_a.value = 96.3514;
            player_car_model.value = CarModel.TRASH;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            cops_min_wanted_level.value = 3;
            cops_wanted_to_zero_flag.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 190000;
            Jump += RACE_GENERIC_COPS_START;
        }

        // ---

        private void RACE_1_0( LabelJump label ) {
            race_start_x.value = -1460.4576;
            race_start_y.value = -187.1936;
            race_start_z.value = 5.5883;
            race_start_a.value = 268.9299;
            player_car_model.value = CarModel.ALPHA;
            Gosub += CREATE_PLAYER_CAR;
            player_car.set_health( 1500 );
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_need_speed.value = 162;
            cops_min_wanted_level.value = 0;
            MISSION_GLOBAL_TIMER_1.value = 180000;
            // ALL position point X
            point4_x[ 0 ].value = -1463.2958;
            point4_x[ 1 ].value = -1339.9979;
            point4_x[ 2 ].value = -1342.3256;
            point4_x[ 3 ].value = -1419.3405;
            // ALL position point Y 
            point4_y[ 0 ].value = 38.149;
            point4_y[ 1 ].value = -134.4052;
            point4_y[ 2 ].value = -242.4048;
            point4_y[ 3 ].value = -284.0017;
            // ALL position point Z
            point4_z[ 0 ].value = 5.5937;
            point4_z[ 1 ].value = 5.5893;
            point4_z[ 2 ].value = 5.5892;
            point4_z[ 3 ].value = 5.59;
            Jump += RACE_GENERIC_4POINT_START;
        }

        private void RACE_1_1( LabelJump label ) {
            race_start_x.value = 1583.1656;
            race_start_y.value = 1832.6517;
            race_start_z.value = 10.4171;
            race_start_a.value = 92.0178;
            player_car_model.value = CarModel.ZR350;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_checkpoint_position_x.value = 1006.7865;
            race_checkpoint_position_y.value = 2455.1853;
            race_checkpoint_position_z.value = 10.357;
            race_checkpoint_type.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 45000;
            __set_traffic( 1.0 );
            p.set_minimum_wanted_level( 3 );
            Gosub += CREATE_RACE_CHECKPOINT;
            Jump += RACE_GENERIC_SPRINT_START;
        }

        private void RACE_1_2( LabelJump label ) {
            race_start_x.value = -751.0707;
            race_start_y.value = 1531.547;
            race_start_z.value = 26.4607;
            race_start_a.value = 80.8664;
            player_car_model.value = CarModel.CHEETAH;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            MISSION_GLOBAL_TIMER_1.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX 
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 8;   // TOTAL POINTS - 1
            // ALL position point X
            checkpoint_x[ 0 ].value = -804.1308;
            checkpoint_x[ 1 ].value = -810.1133;
            checkpoint_x[ 2 ].value = -761.7728;
            checkpoint_x[ 3 ].value = -759.3464;
            checkpoint_x[ 4 ].value = -844.0811;
            checkpoint_x[ 5 ].value = -856.3604;
            checkpoint_x[ 6 ].value = -893.4912;
            checkpoint_x[ 7 ].value = -878.3032;
            checkpoint_x[ 8 ].value = -746.6526;
            // ALL position point Y
            checkpoint_y[ 0 ].value = 1538.433;
            checkpoint_y[ 1 ].value = 1576.5608;
            checkpoint_y[ 2 ].value = 1576.1548;
            checkpoint_y[ 3 ].value = 1496.9868;
            checkpoint_y[ 4 ].value = 1492.2489;
            checkpoint_y[ 5 ].value = 1603.4058;
            checkpoint_y[ 6 ].value = 1544.8025;
            checkpoint_y[ 7 ].value = 1498.7954;
            checkpoint_y[ 8 ].value = 1481.8024;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 26.406;
            checkpoint_z[ 1 ].value = 26.4049;
            checkpoint_z[ 2 ].value = 26.406;
            checkpoint_z[ 3 ].value = 24.3492;
            checkpoint_z[ 4 ].value = 17.7102;
            checkpoint_z[ 5 ].value = 26.2186;
            checkpoint_z[ 6 ].value = 25.2712;
            checkpoint_z[ 7 ].value = 23.7754;
            checkpoint_z[ 8 ].value = 22.7497;
            // ALL timer seconds adder
            checkpoint_add_time[ 0 ].value = 3000;
            checkpoint_add_time[ 1 ].value = 2000;
            checkpoint_add_time[ 2 ].value = 2700;
            checkpoint_add_time[ 3 ].value = 3900;
            checkpoint_add_time[ 4 ].value = 3400;
            checkpoint_add_time[ 5 ].value = 4900;
            checkpoint_add_time[ 6 ].value = 4000;
            checkpoint_add_time[ 7 ].value = 3000;
            checkpoint_add_time[ 8 ].value = 4500;
            __set_traffic( 1.0 );
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_CHECKPOINT_START;
        }

        private void RACE_1_3( LabelJump label ) {
            race_start_x.value = 2118.8745;
            race_start_y.value = -1148.0685;
            race_start_z.value = 24.0606;
            race_start_a.value = 348.4342;
            player_car_model.value = CarModel.CLUB;
            Gosub += CREATE_PLAYER_CAR;
            player_car.set_health( 1500 );
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_need_speed.value = 166;
            cops_min_wanted_level.value = 3;
            MISSION_GLOBAL_TIMER_1.value = 120000;
            // ALL position point X
            point4_x[ 0 ].value = 1972.7495;
            point4_x[ 1 ].value = 1972.8442;
            point4_x[ 2 ].value = 1863.2109;
            point4_x[ 3 ].value = 2049.0249;
            // ALL position point Y 
            point4_y[ 0 ].value = -1084.7953;
            point4_y[ 1 ].value = -1228.1683;
            point4_y[ 2 ].value = -1048.4752;
            point4_y[ 3 ].value = -1103.8849;
            // ALL position point Z
            point4_z[ 0 ].value = 24.7486;
            point4_z[ 1 ].value = 24.546;
            point4_z[ 2 ].value = 23.5676;
            point4_z[ 3 ].value = 24.1975;
            __set_traffic( 1.0 );
            Jump += RACE_GENERIC_4POINT_START;
        }

        private void RACE_1_4( LabelJump label ) {
            race_start_x.value = 1516.1931;
            race_start_y.value = -1656.9877;
            race_start_z.value = 13.7774;
            race_start_a.value = 271.9144;
            player_car_model.value = CarModel.COPCARRU;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            cops_min_wanted_level.value = 5;
            cops_wanted_to_zero_flag.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 120000;
            Jump += RACE_GENERIC_COPS_START;
        }

        private void RACE_1_5( LabelJump label ) {
            race_start_x.value = 1312.8163;
            race_start_y.value = -730.3506;
            race_start_z.value = 92.9609;
            race_start_a.value = 319.4435;
            player_car_model.value = CarModel.BLISTAC;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_checkpoint_position_x.value = 1297.3679;
            race_checkpoint_position_y.value = -1867.3246;
            race_checkpoint_position_z.value = 13.1459;
            race_checkpoint_type.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 46000;
            __set_traffic( 1.0 );
            p.set_minimum_wanted_level( 3 );
            Gosub += CREATE_RACE_CHECKPOINT;
            Jump += RACE_GENERIC_SPRINT_START;
        }

        private void RACE_1_6( LabelJump label ) {
            race_start_x.value = -1650.6521;
            race_start_y.value = 2083.4443;
            race_start_z.value = 18.6265;
            race_start_a.value = 8.0542;
            player_car_model.value = CarModel.BUFFALO;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            MISSION_GLOBAL_TIMER_1.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX 
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 8;   // TOTAL POINTS - 1
            // ALL position point X
            checkpoint_x[ 0 ].value = -1865.1385;
            checkpoint_x[ 1 ].value = -1996.126;
            checkpoint_x[ 2 ].value = -2274.1755;
            checkpoint_x[ 3 ].value = -2568.3154;
            checkpoint_x[ 4 ].value = -2758.8154;
            checkpoint_x[ 5 ].value = -2684.1011;
            checkpoint_x[ 6 ].value = -2689.6826;
            checkpoint_x[ 7 ].value = -2688.5732;
            checkpoint_x[ 8 ].value = -2615.2637;
            // ALL position point Y
            checkpoint_y[ 0 ].value = 2339.8997;
            checkpoint_y[ 1 ].value = 2593.5381;
            checkpoint_y[ 2 ].value = 2676.0964;
            checkpoint_y[ 3 ].value = 2668.4995;
            checkpoint_y[ 4 ].value = 2551.885;
            checkpoint_y[ 5 ].value = 2158.6487;
            checkpoint_y[ 6 ].value = 2163.1877;
            checkpoint_y[ 7 ].value = 1598.8048;
            checkpoint_y[ 8 ].value = 1132.605;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 39.5735;
            checkpoint_z[ 1 ].value = 50.867;
            checkpoint_z[ 2 ].value = 55.1611;
            checkpoint_z[ 3 ].value = 71.1155;
            checkpoint_z[ 4 ].value = 94.9121;
            checkpoint_z[ 5 ].value = 55.374;
            checkpoint_z[ 6 ].value = 55.0791;
            checkpoint_z[ 7 ].value = 63.8541;
            checkpoint_z[ 8 ].value = 55.1386;
            // ALL timer seconds adder
            checkpoint_add_time[ 0 ].value = 13000;
            checkpoint_add_time[ 1 ].value = 8000;
            checkpoint_add_time[ 2 ].value = 9000;
            checkpoint_add_time[ 3 ].value = 7000;
            checkpoint_add_time[ 4 ].value = 6000;
            checkpoint_add_time[ 5 ].value = 11000;
            checkpoint_add_time[ 6 ].value = 9000;
            checkpoint_add_time[ 7 ].value = 9000;
            checkpoint_add_time[ 8 ].value = 5000;
            __set_traffic( 1.0 );
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_CHECKPOINT_START;
        }

        private void RACE_1_7( LabelJump label ) {
            race_start_x.value = 752.3909;
            race_start_y.value = 339.4297;
            race_start_z.value = 19.8433;
            race_start_a.value = 195.1615;
            player_car_model.value = CarModel.SUPERGT;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            MISSION_GLOBAL_TIMER_1.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX 
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 10;  // TOTAL POINTS - 1 
            // ALL position point X
            checkpoint_x[ 0 ].value = 599.9483;
            checkpoint_x[ 1 ].value = 237.593;
            checkpoint_x[ 2 ].value = -194.554;
            checkpoint_x[ 3 ].value = -299.6597;
            checkpoint_x[ 4 ].value = 21.6857;
            checkpoint_x[ 5 ].value = 10.9625;
            checkpoint_x[ 6 ].value = -134.9265;
            checkpoint_x[ 7 ].value = -405.8585;
            checkpoint_x[ 8 ].value = -646.2416;
            checkpoint_x[ 9 ].value = -896.4345;
            checkpoint_x[ 10 ].value = -1220.0642;
            // ALL position point Y
            checkpoint_y[ 0 ].value = 301.9743;
            checkpoint_y[ 1 ].value = 51.0704;
            checkpoint_y[ 2 ].value = 236.2985;
            checkpoint_y[ 3 ].value = -176.8756;
            checkpoint_y[ 4 ].value = -499.4745;
            checkpoint_y[ 5 ].value = -706.9036;
            checkpoint_y[ 6 ].value = -977.8068;
            checkpoint_y[ 7 ].value = -829.1274;
            checkpoint_y[ 8 ].value = -998.1329;
            checkpoint_y[ 9 ].value = -1111.1882;
            checkpoint_y[ 10 ].value = -780.207;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 18.6741;
            checkpoint_z[ 1 ].value = 1.8731;
            checkpoint_z[ 2 ].value = 11.5154;
            checkpoint_z[ 3 ].value = 0.5212;
            checkpoint_z[ 4 ].value = 8.112;
            checkpoint_z[ 5 ].value = 5.0068;
            checkpoint_z[ 6 ].value = 25.6265;
            checkpoint_z[ 7 ].value = 47.0844;
            checkpoint_z[ 8 ].value = 67.7323;
            checkpoint_z[ 9 ].value = 98.2717;
            checkpoint_z[ 10 ].value = 63.3874;
            // ALL timer seconds adder
            checkpoint_add_time[ 0 ].value = 10000;
            checkpoint_add_time[ 1 ].value = 13000;
            checkpoint_add_time[ 2 ].value = 13000;
            checkpoint_add_time[ 3 ].value = 13000;
            checkpoint_add_time[ 4 ].value = 11000;
            checkpoint_add_time[ 5 ].value = 8000;
            checkpoint_add_time[ 6 ].value = 10000;
            checkpoint_add_time[ 7 ].value = 10000;
            checkpoint_add_time[ 8 ].value = 8000;
            checkpoint_add_time[ 9 ].value = 12000;
            checkpoint_add_time[ 10 ].value = 9000;
            __set_traffic( 1.0 );
            p.set_minimum_wanted_level( 3 );
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_CHECKPOINT_START;
        }

        private void RACE_1_8( LabelJump label ) {
            race_start_x.value = -1596.8551;
            race_start_y.value = -1604.5587;
            race_start_z.value = 35.8839;
            race_start_a.value = 293.6649;
            player_car_model.value = CarModel.COMET;
            enemy_start_x.value = -1594.5083;
            enemy_start_y.value = -1609.7495;
            enemy_start_z.value = 35.8852;
            enemy_start_a.value = 291.1827;
            enemy_car_model.value = CarModel.COMET;
            enemy_actor_model.value = ActorModel.WMYJG;
            loaded_path.value = 900;
            race_path_speed.value = 0.97;
            Gosub += CREATE_PLAYER_CAR;
            Gosub += CREATE_ENEMY_CAR_AND_ACTOR;
            Gosub += LOAD_PATH;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            no_suget_duel.value = 1;
            race_checkpoint_type.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX  
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 8;   // TOTAL POINTS - 1 
            // ALL position point X
            checkpoint_x[ 0 ].value = -1368.493;
            checkpoint_x[ 1 ].value = -1164.3683;
            checkpoint_x[ 2 ].value = -864.0972;
            checkpoint_x[ 3 ].value = -769.3582;
            checkpoint_x[ 4 ].value = -719.6852;
            checkpoint_x[ 5 ].value = -708.7427;
            checkpoint_x[ 6 ].value = -644.6606;
            checkpoint_x[ 7 ].value = -505.7187;
            checkpoint_x[ 8 ].value = -369.1519;
            // ALL position point Y
            checkpoint_y[ 0 ].value = -1659.6624;
            checkpoint_y[ 1 ].value = -1885.595;
            checkpoint_y[ 2 ].value = -1872.554;
            checkpoint_y[ 3 ].value = -1719.5576;
            checkpoint_y[ 4 ].value = -1307.4797;
            checkpoint_y[ 5 ].value = -1713.9966;
            checkpoint_y[ 6 ].value = -1300.3411;
            checkpoint_y[ 7 ].value = -1029.5826;
            checkpoint_y[ 8 ].value = -811.593;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 44.9486;
            checkpoint_z[ 1 ].value = 77.6805;
            checkpoint_z[ 2 ].value = 88.0721;
            checkpoint_z[ 3 ].value = 96.2654;
            checkpoint_z[ 4 ].value = 63.7568;
            checkpoint_z[ 5 ].value = 45.32;
            checkpoint_z[ 6 ].value = 19.3027;
            checkpoint_z[ 7 ].value = 23.999;
            checkpoint_z[ 8 ].value = 28.4285;
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_DUEL_START;
        }

        private void RACE_1_9( LabelJump label ) {
            race_start_x.value = 707.6089;
            race_start_y.value = 1945.3821;
            race_start_z.value = 4.9577;
            race_start_a.value = 179.2977;
            player_car_model.value = CarModel.BULLET;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            cops_min_wanted_level.value = 5;
            cops_wanted_to_zero_flag.value = 0;
            MISSION_GLOBAL_TIMER_1.value = 250000;
            Jump += RACE_GENERIC_COPS_START;
        }

        private void RACE_1_10( LabelJump label ) {
            race_start_x.value = 2484.8699;
            race_start_y.value = 2512.646;
            race_start_z.value = 10.3182;
            race_start_a.value = 90.0;
            player_car_model.value = CarModel.EUROS;
            Gosub += CREATE_PLAYER_CAR;
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_checkpoint_position_x.value = 2048.2969;
            race_checkpoint_position_y.value = 834.3013;
            race_checkpoint_position_z.value = 6.2333;
            race_checkpoint_type.value = 1;
            MISSION_GLOBAL_TIMER_1.value = 68000;
            __set_traffic( 1.0 );
            p.set_minimum_wanted_level( 3 );
            Gosub += CREATE_RACE_CHECKPOINT;
            Jump += RACE_GENERIC_SPRINT_START;
        }

        private void RACE_1_11( LabelJump label ) {
            race_start_x.value = 1442.0353;
            race_start_y.value = -2287.8269;
            race_start_z.value = 12.9565;
            race_start_a.value = 92.039;
            player_car_model.value = CarModel.BANSHEE;
            Gosub += CREATE_PLAYER_CAR;
            player_car.set_health( 1500 );
            wait( 1500 );
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            race_need_speed.value = 190;
            cops_min_wanted_level.value = 3;
            MISSION_GLOBAL_TIMER_1.value = 90000;
            // ALL position point X
            point4_x[ 0 ].value = 1802.7753;
            point4_x[ 1 ].value = 1800.8335;
            point4_x[ 2 ].value = 1584.7406;
            point4_x[ 3 ].value = 1583.6554;
            // ALL position point Y 
            point4_y[ 0 ].value = -2195.2461;
            point4_y[ 1 ].value = -2378.4153;
            point4_y[ 2 ].value = -2319.1587;
            point4_y[ 3 ].value = -2254.0269;
            // ALL position point Z
            point4_z[ 0 ].value = 12.7856;
            point4_z[ 1 ].value = 12.7823;
            point4_z[ 2 ].value = 12.7922;
            point4_z[ 3 ].value = 12.7919;
            __set_traffic( 1.0 );
            Jump += RACE_GENERIC_4POINT_START;
        }

        #endregion

        #region GENERIC LOOPS

        private void RACE_GENERIC_SPRINT_START( LabelJump label ) {
            Gosub += RACE_GENERIC_RESTORE_PLAYER_CONTROL;
            race_checkpoint_direction_x.value = race_checkpoint_position_x;
            race_checkpoint_direction_y.value = race_checkpoint_position_y;
            race_checkpoint_direction_z.value = race_checkpoint_position_z;
            MISSION_GLOBAL_TIMER_1.start( TimerType.DOWN, "BB_19" );
            show_text_highpriority( "@BLS@33", 6000, 1 );
            Jump += RACE_GENERIC_SPRINT_LOOP;
        }

        private void RACE_GENERIC_SPRINT_LOOP( LabelJump label ) {
            wait( 0 );
            player_car.do_if_wrecked( delegate {
                and( !a.is_busted(), !a.is_dead(), delegate { ___jump_failed_put_player( "RACES24" ); } );
            } );
            and( !a.is_in_vehicle( player_car ), delegate { ___jump_failed_put_player( "RACES20" ); } );
            and( 0 >= MISSION_GLOBAL_TIMER_1, delegate { ___jump_failed_put_player( "BB_17" ); } );
            and(
                a.is_in_vehicle( player_car ),
                a.is_near_point_3d_in_vehicle( false, race_checkpoint_position_x, race_checkpoint_position_y, race_checkpoint_position_z, 6.0, 6.0, 6.0 )
            , delegate {
                Jump += PASSED_QUALIFY;
            } );
            jump( RACE_GENERIC_SPRINT_LOOP );
        }

        private void RACE_GENERIC_COPS_START( LabelJump label ) {
            Gosub += RACE_GENERIC_RESTORE_PLAYER_CONTROL;
            p.set_minimum_wanted_level( cops_min_wanted_level );
            set_sensitivity_to_crime( 1.25 );
            __set_traffic( 1.0 );
            MISSION_GLOBAL_TIMER_1.start( TimerType.DOWN, "BB_19" );
            show_text_highpriority( "@BLS@32", 6000, 1 );
            Jump += RACE_GENERIC_COPS_LOOP;
        }

        private void RACE_GENERIC_COPS_LOOP( LabelJump label ) {
            wait( 0 );
            player_car.do_if_wrecked( delegate {
                and( !a.is_busted(), !a.is_dead(), delegate { ___jump_failed_put_player( "RACES24" ); } );
            } );
            and( !a.is_in_vehicle( player_car ), delegate { ___jump_failed_put_player( "RACES20" ); } );
            p.get_wanted_level( cops_current_wanted_level );
            and( cops_min_wanted_level > cops_current_wanted_level, delegate { ___jump_failed_put_player( "@BLS@30" ); } );
            and( 0 >= MISSION_GLOBAL_TIMER_1, delegate {
                MISSION_GLOBAL_TIMER_1.stop();
                and( cops_wanted_to_zero_flag == 1, delegate { Jump += RACE_GENERIC_COPS_START2; } );
                p.clear_wanted_level();
                player_car.set_door_status( 2 );
                __set_traffic( 0.0 );
                Jump += PASSED_QUALIFY;
            } );
            jump( RACE_GENERIC_COPS_LOOP );
        }

        private void RACE_GENERIC_COPS_START2( LabelJump label ) {
            show_text_highpriority( "@BLS@31", 5000, 1 );
            set_sensitivity_to_crime( 0.9 );
            set_free_respray( true );
            Jump += RACE_GENERIC_COPS_LOOP2;
        }

        private void RACE_GENERIC_COPS_LOOP2( LabelJump label ) {
            wait( 0 );
            player_car.do_if_wrecked( delegate {
                and( !a.is_busted(), !a.is_dead(), delegate { ___jump_failed_put_player( "RACES24" ); } );
            } );
            and( !a.is_in_vehicle( player_car ), delegate { ___jump_failed_put_player( "RACES20" ); } );
            p.get_wanted_level( cops_current_wanted_level );
            and( cops_current_wanted_level == 0, delegate {
                p.clear_wanted_level();
                player_car.set_door_status( 2 );
                __set_traffic( 0.0 );
                Jump += PASSED_QUALIFY;
            } );
            jump( RACE_GENERIC_COPS_LOOP2 );
        }

        private void RACE_GENERIC_CHECKPOINT_START( LabelJump label ) {
            Gosub += RACE_GENERIC_RESTORE_PLAYER_CONTROL;
            MISSION_GLOBAL_TIMER_1.start( TimerType.DOWN, "BB_19" );
            show_text_highpriority( "@BLS@34", 6000, 1 );
            Jump += RACE_GENERIC_CHECKPOINT_LOOP;
        }

        private void RACE_GENERIC_CHECKPOINT_LOOP( LabelJump label ) {
            wait( 0 );
            player_car.do_if_wrecked( delegate {
                and( !a.is_busted(), !a.is_dead(), delegate { ___jump_failed_put_player( "RACES24" ); } );
            } );
            and( !a.is_in_vehicle( player_car ), delegate { ___jump_failed_put_player( "RACES20" ); } );
            and( 0 >= MISSION_GLOBAL_TIMER_1, delegate { ___jump_failed_put_player( "BB_17" ); } );
            and( a.is_in_vehicle( player_car ), a.is_near_point_3d_in_vehicle( false, checkpoint_x[ loop_index ], checkpoint_y[ loop_index ], checkpoint_z[ loop_index ], 6.0, 6.0, 6.0 ), delegate {
                loop_index += 1;
                loop_index2 += 1;
                and( loop_index > race_stopwatch, delegate {
                    Jump += PASSED_QUALIFY;
                }, delegate {
                    Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
                } );
            } );
            jump( RACE_GENERIC_CHECKPOINT_LOOP );
        }

        private void RACE_GENERIC_4POINT_START( LabelJump label ) {
            Gosub += RACE_GENERIC_RESTORE_PLAYER_CONTROL;
            race_total_passed_4point.value = 0;
            MISSION_GLOBAL_STATUS_TEXT_1.value = 0;
            MISSION_GLOBAL_STATUS_TEXT_2.value = race_need_speed;
            MISSION_GLOBAL_STATUS_TEXT_1.create( StatusTextType.NUMBER, 1, "@BLS@35" );
            MISSION_GLOBAL_STATUS_TEXT_2.create( StatusTextType.NUMBER, 2, "@BLS@36" );
            MISSION_GLOBAL_TIMER_1.start( TimerType.DOWN, "BB_19" );
            and( cops_min_wanted_level > 0, delegate { p.set_minimum_wanted_level( cops_min_wanted_level ); } );
            to( loop_index, 0, 3, delegate {
                point4_race_checkpoints[ loop_index ].create( point4_x[ loop_index ], point4_y[ loop_index ], point4_z[ loop_index ], point4_x[ loop_index ], point4_y[ loop_index ], point4_z[ loop_index ], 2, 6.0 );
                point4_checkpoints[ loop_index ].create( point4_x[ loop_index ], point4_y[ loop_index ], point4_z[ loop_index ] );
            } );
            show_text_highpriority( "@BLS@37", 6000, 1 );
            Jump += RACE_GENERIC_4POINT_LOOP;
        }

        private void RACE_GENERIC_4POINT_LOOP( LabelJump label ) {
            wait( 0 );
            player_car.do_if_wrecked( delegate {
                and( !a.is_busted(), !a.is_dead(), delegate { ___jump_failed_put_player( "RACES24" ); } );
            } );
            and( !a.is_in_vehicle( player_car ), delegate { ___jump_failed_put_player( "RACES20" ); } );
            and( 0 >= MISSION_GLOBAL_TIMER_1, delegate { ___jump_failed_put_player( "BB_17" ); } );
            to( loop_index, 0, 3, delegate {
                and( point4_checkpoints[ loop_index ].is_enabled(), delegate {
                    and( a.is_in_vehicle( player_car ), a.is_near_point_3d_in_vehicle( false, point4_x[ loop_index ], point4_y[ loop_index ], point4_z[ loop_index ], 6.0, 6.0, 6.0 ), delegate {
                        point4_race_checkpoints[ loop_index ].disable();
                        point4_checkpoints[ loop_index ].disable();
                        race_total_passed_4point += 1;
                        player_car.get_speed( race_4point_car_speed );
                        race_4point_car_speed.to_integer( race_4point_car_speed_int );
                        MISSION_GLOBAL_STATUS_TEXT_1 += race_4point_car_speed_int;
                        and( 4 > race_total_passed_4point, delegate {
                            play_audio_event_at_position( 0.0, 0.0, 0.0, 1058 );
                        } );
                    } );
                } );
            } );
            and( race_total_passed_4point == 4, delegate {
                and( MISSION_GLOBAL_STATUS_TEXT_1 >= race_need_speed, delegate {
                    Jump += PASSED_QUALIFY;
                }, delegate {
                    ___jump_failed_put_player( "@BLS@38" );
                } );
            } );
            jump( RACE_GENERIC_4POINT_LOOP );
        }

        private void RACE_GENERIC_DUEL_START( LabelJump label ) {
            Gosub += RACE_GENERIC_RESTORE_PLAYER_CONTROL;
            enemy_car.start_path( loaded_path ).set_path_speed( race_path_speed );
            enemy_actor.set_cant_be_dragged_out( true );
            enemy_marker.create_above_vehicle( enemy_car ).set_radar_mode( 2 ).set_size( 2 );
            final_point_x.value = checkpoint_x[ race_stopwatch ];
            final_point_y.value = checkpoint_y[ race_stopwatch ];
            final_point_z.value = checkpoint_z[ race_stopwatch ];
            Jump += RACE_GENERIC_DUEL_LOOP;
        }

        private void RACE_GENERIC_DUEL_LOOP( LabelJump label ) {
            wait( 0 );
            player_car.do_if_wrecked( delegate {
                and( !a.is_busted(), !a.is_dead(), delegate { ___jump_failed_put_player( "RACES24" ); } );
            } );
            and( !a.is_in_vehicle( player_car ), delegate { ___jump_failed_put_player( "RACES20" ); } );
            enemy_car.do_if_wrecked( delegate { ___jump_failed_put_player( "@BLS@39" ); } );
            enemy_actor.do_if_dead( delegate { ___jump_failed_put_player( "@BLS@40" ); } );
            and( enemy_actor.is_near_point_3d( false, final_point_x, final_point_y, final_point_z, 6.0, 6.0, 6.0 ), delegate {
                ___jump_failed_put_player( "@BLS@41" );
            } );
            and( a.is_in_vehicle( player_car ), a.is_near_point_3d_in_vehicle( false, checkpoint_x[ loop_index ], checkpoint_y[ loop_index ], checkpoint_z[ loop_index ], 6.0, 6.0, 6.0 ), delegate {
                loop_index += 1;
                loop_index2 += 1;
                and( loop_index > race_stopwatch, delegate {
                    and( no_suget_duel == true, delegate {
                        Jump += PASSED_QUALIFY;
                    } );
                    Jump += SHOW_SUGET_SCENE_AFTER;
                }, delegate {
                    Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
                } );
            } );
            jump( RACE_GENERIC_DUEL_LOOP );
        }

        #endregion

        #region RACE BASE GOSUBS

        private void CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER( LabelGosub label ) {
            race_checkpoint.disable();
            race_marker.disable_if_exist();
            and( loop_index != 0, delegate { play_audio_event_at_position( 0.0, 0.0, 0.0, 1058 ); } );
            race_checkpoint_position_x.value = checkpoint_x[ loop_index ];
            race_checkpoint_position_y.value = checkpoint_y[ loop_index ];
            race_checkpoint_position_z.value = checkpoint_z[ loop_index ];
            and( loop_index2 > race_stopwatch, delegate {
                race_checkpoint_direction_x.value = race_checkpoint_position_x;
                race_checkpoint_direction_y.value = race_checkpoint_position_y;
                race_checkpoint_direction_z.value = race_checkpoint_position_z;
                race_checkpoint_type.value = 1;
            }, delegate {
                race_checkpoint_direction_x.value = checkpoint_x[ loop_index2 ];
                race_checkpoint_direction_y.value = checkpoint_y[ loop_index2 ];
                race_checkpoint_direction_z.value = checkpoint_z[ loop_index2 ];
            } );
            MISSION_GLOBAL_TIMER_1 += checkpoint_add_time[ loop_index ];
            race_checkpoint.create( race_checkpoint_position_x, race_checkpoint_position_y, race_checkpoint_position_z, race_checkpoint_direction_x, race_checkpoint_direction_y, race_checkpoint_direction_z, race_checkpoint_type, 6.0 );
            race_marker.create( race_checkpoint_position_x, race_checkpoint_position_y, race_checkpoint_position_z );
        }

        private void CREATE_RACE_CHECKPOINT( LabelGosub label ) {
            race_checkpoint.disable();
            race_marker.disable_if_exist();
            race_checkpoint.create( race_checkpoint_position_x, race_checkpoint_position_y, race_checkpoint_position_z, race_checkpoint_direction_x, race_checkpoint_direction_y, race_checkpoint_direction_z, race_checkpoint_type, 6.0 );
            race_marker.create( race_checkpoint_position_x, race_checkpoint_position_y, race_checkpoint_position_z );
        }

        private void RACE_321_AND_START( LabelGosub label ) {
            show_text_styled( "RACE2", 1100, 4 ); // 3
            play_audio_event_at_position( 0.0, 0.0, 0.0, 1056 );
            wait( 1000 );
            show_text_styled( "RACE3", 1100, 4 ); // 2
            play_audio_event_at_position( 0.0, 0.0, 0.0, 1056 );
            wait( 1000 );
            show_text_styled( "RACE4", 1100, 4 ); // 1
            play_audio_event_at_position( 0.0, 0.0, 0.0, 1056 );
            wait( 1000 );
            show_text_styled( "RACE5", 1100, 4 ); // START!
            play_audio_event_at_position( 0.0, 0.0, 0.0, 1057 );
        }

        private void PLACE_PLAYER( LabelGosub label ) {
            clear_area( true, START_POINT_X, START_POINT_Y, START_POINT_Z, 30.0 );
            __renderer_at( START_POINT_X, START_POINT_Y, START_POINT_Z );
            a.teleport_without_car( START_POINT_X, START_POINT_Y, START_POINT_Z, START_POINT_A );
            __camera_default();
            __clear_text();
            Gosub += SET_ALL_DUMMY;
            __fade( true, true );
        }

        private void REPAIR_PLAYER_CAR( LabelGosub label ) {
            a.extinguish_current_car_if_exist( temp_player_car );
        }

        private void CREATE_PLAYER_CAR( LabelGosub label ) {
            load_requested_models( player_car_model );
            clear_area( true, race_start_x, race_start_y, race_start_z, 300.0 );
            __renderer_at( race_start_x, race_start_y, race_start_z );
            player_car.create( player_car_model, race_start_x, race_start_y, race_start_z ).set_z_angle( race_start_a );
            destroy_model( player_car_model );
            a.put_into_vehicle_as_driver( player_car );
            set_radio_station( RadioStation.OFF );
            __camera_default();
        }

        private void CREATE_ENEMY_CAR_AND_ACTOR( LabelGosub label ) {
            load_requested_models( enemy_car_model, enemy_actor_model );
            enemy_car.create( enemy_car_model, enemy_start_x, enemy_start_y, enemy_start_z ).set_z_angle( enemy_start_a );
            enemy_actor.create_in_vehicle_driverseat( 24, enemy_actor_model, enemy_car ).set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER ).set_decision_maker( 65543 );
            destroy_model( enemy_car_model, enemy_actor_model );
        }

        private void LOAD_PATH( LabelGosub label ) {
            load_path( loaded_path );
            wait( is_path_available( loaded_path ) );
        }

        private void RACE_GENERIC_RESTORE_PLAYER_CONTROL( LabelGosub label ) {
            __set_player_ignore( false );
            __set_entered_names( true );
            __clear_text();
            p.can_move( true );
        }

        private void MOVE_PLAYER( LabelGosub label ) {
            clear_area( true, START_POINT_X, START_POINT_Y, START_POINT_Z, 2.0 );
            __renderer_at( START_POINT_X, START_POINT_Y, START_POINT_Z );
            gosub_clear();
            a.teleport_without_car( START_POINT_X, START_POINT_Y, START_POINT_Z, START_POINT_A );
            wait( 1000 );
            __camera_default();
            __fade( true, true );
            __disable_player_controll_in_cutscene( false );
        }

        #endregion

        #region BOSS RACES

        private void RACE_6_FINAL( LabelJump label ) {
            chdir( @"Sound\BLIST" );
            AUDIO_BG.load( 999998 );
            wait( AUDIO_BG.is_ready );
            set_current_time( 23, 0 );
            set_weather( WeatherID.EXTRASUNNY_LA );
            force_weather( WeatherID.EXTRASUNNY_LA );
            Gosub += PLAY_CUTSCENE_6B;
            race_start_x.value = 1708.4875;
            race_start_y.value = -694.2148;
            race_start_z.value = 45.7122;
            race_start_a.value = 353.4303;
            player_car_model.value = CarModel.PREVION;
            enemy_start_x.value = 1713.3278;
            enemy_start_y.value = -695.3274;
            enemy_start_z.value = 45.7367;
            enemy_start_a.value = 350.98;
            enemy_car_model.value = CarModel.PRIMO;
            enemy_actor_model.value = ActorModel.WMYJG;
            loaded_path.value = 901;
            race_path_speed.value = 0.94;
            Gosub += CREATE_PLAYER_CAR;
            Gosub += CREATE_ENEMY_CAR_AND_ACTOR;
            Gosub += CUSTOMIZE_CARS6;
            Gosub += LOAD_PATH;
            wait( 1500 );
            AUDIO_BG.play();
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            no_suget_duel.value = false;
            race_checkpoint_type.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX  
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 8;   // TOTAL POINTS - 1 
            // ALL position point X
            checkpoint_x[ 0 ].value = 1690.0862;
            checkpoint_x[ 1 ].value = 1664.3739;
            checkpoint_x[ 2 ].value = 1650.1202;
            checkpoint_x[ 3 ].value = 1963.0477;
            checkpoint_x[ 4 ].value = 2537.9907;
            checkpoint_x[ 5 ].value = 2755.0574;
            checkpoint_x[ 6 ].value = 2879.1394;
            checkpoint_x[ 7 ].value = 2887.3564;
            checkpoint_x[ 8 ].value = 2824.9248;
            // ALL position point Y
            checkpoint_y[ 0 ].value = -382.1809;
            checkpoint_y[ 1 ].value = -70.8906;
            checkpoint_y[ 2 ].value = 208.4294;
            checkpoint_y[ 3 ].value = 289.2302;
            checkpoint_y[ 4 ].value = 291.2969;
            checkpoint_y[ 5 ].value = -53.1558;
            checkpoint_y[ 6 ].value = -624.4019;
            checkpoint_y[ 7 ].value = -1284.645;
            checkpoint_y[ 8 ].value = -1852.2496;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 39.5373;
            checkpoint_z[ 1 ].value = 35.5923;
            checkpoint_z[ 2 ].value = 30.8601;
            checkpoint_z[ 3 ].value = 32.9727;
            checkpoint_z[ 4 ].value = 28.8204;
            checkpoint_z[ 5 ].value = 34.2718;
            checkpoint_z[ 6 ].value = 10.4825;
            checkpoint_z[ 7 ].value = 10.5528;
            checkpoint_z[ 8 ].value = 10.5816;
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_DUEL_START;
        }

        private void RACE_5_FINAL( LabelJump label ) {
            chdir( @"Sound\BLIST" );
            AUDIO_BG.load( 999997 );
            wait( AUDIO_BG.is_ready );
            set_current_time( 22, 0 );
            set_weather( WeatherID.CLOUDY_LA );
            force_weather( WeatherID.CLOUDY_LA );
            Gosub += PLAY_CUTSCENE_5B;
            race_start_x.value = -325.5419;
            race_start_y.value = -2187.1328;
            race_start_z.value = 27.9526;
            race_start_a.value = 198.3533;
            player_car_model.value = CarModel.FORTUNE;
            enemy_start_x.value = -330.6521;
            enemy_start_y.value = -2188.8552;
            enemy_start_z.value = 28.0421;
            enemy_start_a.value = 196.0424;
            enemy_car_model.value = CarModel.CADRONA;
            enemy_actor_model.value = ActorModel.BMYST;
            loaded_path.value = 902;
            race_path_speed.value = 0.92;
            Gosub += CREATE_PLAYER_CAR;
            Gosub += CREATE_ENEMY_CAR_AND_ACTOR;
            Gosub += CUSTOMIZE_CARS5;
            Gosub += LOAD_PATH;
            wait( 1500 );
            AUDIO_BG.play();
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            no_suget_duel.value = false;
            race_checkpoint_type.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX  
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 8;   // TOTAL POINTS - 1 
            // ALL position point X
            checkpoint_x[ 0 ].value = -136.0305;
            checkpoint_x[ 1 ].value = -32.8136;
            checkpoint_x[ 2 ].value = -330.2795;
            checkpoint_x[ 3 ].value = -839.9954;
            checkpoint_x[ 4 ].value = -1358.8151;
            checkpoint_x[ 5 ].value = -1882.6058;
            checkpoint_x[ 6 ].value = -2533.47;
            checkpoint_x[ 7 ].value = -2882.2703;
            checkpoint_x[ 8 ].value = -2896.3894;
            // ALL position point Y
            checkpoint_y[ 0 ].value = -2446.1147;
            checkpoint_y[ 1 ].value = -2707.3037;
            checkpoint_y[ 2 ].value = -2798.0864;
            checkpoint_y[ 3 ].value = -2806.8098;
            checkpoint_y[ 4 ].value = -2875.6238;
            checkpoint_y[ 5 ].value = -2581.9451;
            checkpoint_y[ 6 ].value = -2146.6033;
            checkpoint_y[ 7 ].value = -1772.354;
            checkpoint_y[ 8 ].value = -1130.8202;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 36.6954;
            checkpoint_z[ 1 ].value = 41.6571;
            checkpoint_z[ 2 ].value = 58.7093;
            checkpoint_z[ 3 ].value = 70.5039;
            checkpoint_z[ 4 ].value = 53.4347;
            checkpoint_z[ 5 ].value = 60.4736;
            checkpoint_z[ 6 ].value = 30.3976;
            checkpoint_z[ 7 ].value = 30.2926;
            checkpoint_z[ 8 ].value = 9.1274;
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_DUEL_START;
        }

        private void RACE_4_FINAL( LabelJump label ) {
            chdir( @"Sound\BLIST" );
            AUDIO_BG.load( 999996 );
            wait( AUDIO_BG.is_ready );
            set_current_time( 20, 0 );
            set_weather( WeatherID.EXTRASUNNY_SMOG_LA );
            force_weather( WeatherID.EXTRASUNNY_SMOG_LA );
            Gosub += PLAY_CUTSCENE_4B;
            race_start_x.value = -1894.7034;
            race_start_y.value = -1192.0343;
            race_start_z.value = 38.928;
            race_start_a.value = 0.0;
            player_car_model.value = CarModel.SLAMVAN;
            enemy_start_x.value = -1900.7241;
            enemy_start_y.value = -1192.6681;
            enemy_start_z.value = 38.8694;
            enemy_start_a.value = 0.0;
            enemy_car_model.value = CarModel.REMINGTN;
            enemy_actor_model.value = ActorModel.VBMYCR;
            loaded_path.value = 903;
            race_path_speed.value = 0.88;
            set_next_vehicle_model_variation( CarModel.SLAMVAN, 0 );
            Gosub += CREATE_PLAYER_CAR;
            Gosub += CREATE_ENEMY_CAR_AND_ACTOR;
            Gosub += CUSTOMIZE_CARS4;
            Gosub += LOAD_PATH;
            wait( 1500 );
            AUDIO_BG.play();
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            no_suget_duel.value = false;
            race_checkpoint_type.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX  
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 8;   // TOTAL POINTS - 1 
            // ALL position point X
            checkpoint_x[ 0 ].value = -1897.9214;
            checkpoint_x[ 1 ].value = -1898.1833;
            checkpoint_x[ 2 ].value = -1868.3823;
            checkpoint_x[ 3 ].value = -1709.1034;
            checkpoint_x[ 4 ].value = -1801.3595;
            checkpoint_x[ 5 ].value = -1742.7915;
            checkpoint_x[ 6 ].value = -1563.2389;
            checkpoint_x[ 7 ].value = -1585.0493;
            checkpoint_x[ 8 ].value = -1845.4875;
            // ALL position point Y
            checkpoint_y[ 0 ].value = -877.3207;
            checkpoint_y[ 1 ].value = -209.216;
            checkpoint_y[ 2 ].value = 201.8124;
            checkpoint_y[ 3 ].value = 481.0706;
            checkpoint_y[ 4 ].value = 368.9103;
            checkpoint_y[ 5 ].value = 308.6566;
            checkpoint_y[ 6 ].value = 503.1771;
            checkpoint_y[ 7 ].value = 1118.7117;
            checkpoint_y[ 8 ].value = 1398.1937;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 44.3501;
            checkpoint_z[ 1 ].value = 37.6341;
            checkpoint_z[ 2 ].value = 37.7569;
            checkpoint_z[ 3 ].value = 37.6472;
            checkpoint_z[ 4 ].value = 16.5062;
            checkpoint_z[ 5 ].value = 6.491;
            checkpoint_z[ 6 ].value = 6.4401;
            checkpoint_z[ 7 ].value = 6.4433;
            checkpoint_z[ 8 ].value = 6.5826;
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_DUEL_START;
        }

        private void RACE_3_FINAL( LabelJump label ) {
            chdir( @"Sound\BLIST" );
            AUDIO_BG.load( 999995 );
            wait( AUDIO_BG.is_ready );
            set_current_time( 16, 0 );
            set_weather( WeatherID.RAINY_SF );
            force_weather( WeatherID.RAINY_SF );
            Gosub += PLAY_CUTSCENE_3B;
            race_start_x.value = 230.2314;
            race_start_y.value = 2752.958;
            race_start_z.value = 59.5626;
            race_start_a.value = 254.8814;
            player_car_model.value = CarModel.ADMIRAL;
            enemy_start_x.value = 228.4595;
            enemy_start_y.value = 2747.1873;
            enemy_start_z.value = 59.5163;
            enemy_start_a.value = 255.2212;
            enemy_car_model.value = CarModel.MAJESTIC;
            enemy_actor_model.value = ActorModel.WFYST;
            loaded_path.value = 904;
            race_path_speed.value = 0.9;
            Gosub += CREATE_PLAYER_CAR;
            Gosub += CREATE_ENEMY_CAR_AND_ACTOR;
            Gosub += CUSTOMIZE_CARS3;
            Gosub += LOAD_PATH;
            wait( 1500 );
            AUDIO_BG.play();
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            no_suget_duel.value = false;
            race_checkpoint_type.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX  
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 8;   // TOTAL POINTS - 1 
            // ALL position point X
            checkpoint_x[ 0 ].value = 417.1092;
            checkpoint_x[ 1 ].value = 881.9512;
            checkpoint_x[ 2 ].value = 790.8337;
            checkpoint_x[ 3 ].value = 813.9758;
            checkpoint_x[ 4 ].value = 777.1326;
            checkpoint_x[ 5 ].value = 725.4247;
            checkpoint_x[ 6 ].value = 212.4776;
            checkpoint_x[ 7 ].value = -129.3891;
            checkpoint_x[ 8 ].value = -617.263;
            // ALL position point Y
            checkpoint_y[ 0 ].value = 2700.3284;
            checkpoint_y[ 1 ].value = 2652.7983;
            checkpoint_y[ 2 ].value = 2227.1648;
            checkpoint_y[ 3 ].value = 1821.2859;
            checkpoint_y[ 4 ].value = 1457.3158;
            checkpoint_y[ 5 ].value = 1106.8409;
            checkpoint_y[ 6 ].value = 961.2632;
            checkpoint_y[ 7 ].value = 822.2632;
            checkpoint_y[ 8 ].value = 1186.6821;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 60.3429;
            checkpoint_z[ 1 ].value = 28.9278;
            checkpoint_z[ 2 ].value = 8.9079;
            checkpoint_z[ 3 ].value = 3.2709;
            checkpoint_z[ 4 ].value = 20.0205;
            checkpoint_z[ 5 ].value = 28.079;
            checkpoint_z[ 6 ].value = 27.8663;
            checkpoint_z[ 7 ].value = 20.5862;
            checkpoint_z[ 8 ].value = 9.4338;
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_DUEL_START;
        }

        private void RACE_2_FINAL( LabelJump label ) {
            chdir( @"Sound\BLIST" );
            AUDIO_BG.load( 999994 );
            wait( AUDIO_BG.is_ready );
            set_current_time( 22, 0 );
            set_weather( WeatherID.SUNNY_DESERT );
            force_weather( WeatherID.SUNNY_DESERT );
            Gosub += PLAY_CUTSCENE_2B;
            race_start_x.value = 1391.7064;
            race_start_y.value = -944.884;
            race_start_z.value = 33.9836;
            race_start_a.value = 82.3759;
            player_car_model.value = CarModel.SULTAN;
            enemy_start_x.value = 1392.0852;
            enemy_start_y.value = -940.487;
            enemy_start_z.value = 33.8083;
            enemy_start_a.value = 82.434;
            enemy_car_model.value = CarModel.FLASH;
            enemy_actor_model.value = ActorModel.VWMYCD;
            loaded_path.value = 905;
            race_path_speed.value = 0.9;
            Gosub += CREATE_PLAYER_CAR;
            Gosub += CREATE_ENEMY_CAR_AND_ACTOR;
            Gosub += CUSTOMIZE_CARS2;
            Gosub += LOAD_PATH;
            wait( 1500 );
            AUDIO_BG.play();
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            no_suget_duel.value = false;
            race_checkpoint_type.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX  
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 14;   // TOTAL POINTS - 1 
            // ALL position point X
            checkpoint_x[ 0 ].value = 957.4955;
            checkpoint_x[ 1 ].value = 740.5155;
            checkpoint_x[ 2 ].value = 495.5753;
            checkpoint_x[ 3 ].value = 121.7804;
            checkpoint_x[ 4 ].value = 208.1195;
            checkpoint_x[ 5 ].value = 399.1917;
            checkpoint_x[ 6 ].value = 495.1022;
            checkpoint_x[ 7 ].value = 877.0102;
            checkpoint_x[ 8 ].value = 1047.3179;
            checkpoint_x[ 9 ].value = 1025.1312;
            checkpoint_x[ 10 ].value = 1357.8356;
            checkpoint_x[ 11 ].value = 1773.1909;
            checkpoint_x[ 12 ].value = 2158.9919;
            checkpoint_x[ 13 ].value = 2188.0308;
            checkpoint_x[ 14 ].value = 2386.3765;
            // ALL position point Y
            checkpoint_y[ 0 ].value = -969.3427;
            checkpoint_y[ 1 ].value = -1068.9243;
            checkpoint_y[ 2 ].value = -1280.2465;
            checkpoint_y[ 3 ].value = -1603.49;
            checkpoint_y[ 4 ].value = -1732.9321;
            checkpoint_y[ 5 ].value = -1772.8776;
            checkpoint_y[ 6 ].value = -1725.7479;
            checkpoint_y[ 7 ].value = -1785.0833;
            checkpoint_y[ 8 ].value = -1844.415;
            checkpoint_y[ 9 ].value = -2242.7725;
            checkpoint_y[ 10 ].value = -2638.5439;
            checkpoint_y[ 11 ].value = -2684.739;
            checkpoint_y[ 12 ].value = -2632.1174;
            checkpoint_y[ 13 ].value = -2396.4717;
            checkpoint_y[ 14 ].value = -2189.2891;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 38.1999;
            checkpoint_z[ 1 ].value = 22.6155;
            checkpoint_z[ 2 ].value = 15.0852;
            checkpoint_z[ 3 ].value = 9.9151;
            checkpoint_z[ 4 ].value = 3.8983;
            checkpoint_z[ 5 ].value = 4.8013;
            checkpoint_z[ 6 ].value = 10.8882;
            checkpoint_z[ 7 ].value = 13.0725;
            checkpoint_z[ 8 ].value = 12.8367;
            checkpoint_z[ 9 ].value = 12.3989;
            checkpoint_z[ 10 ].value = 12.78;
            checkpoint_z[ 11 ].value = 5.352;
            checkpoint_z[ 12 ].value = 12.8317;
            checkpoint_z[ 13 ].value = 12.8344;
            checkpoint_z[ 14 ].value = 12.8368;
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_DUEL_START;
        }

        private void RACE_1_FINAL1( LabelJump label ) {
            final_first_race_passed.value = false;
            chdir( @"Sound\BLIST" );
            AUDIO_BG.load( 999993 );
            wait( AUDIO_BG.is_ready );
            set_current_time( 3, 0 );
            set_weather( WeatherID.CLOUDY_VEGAS );
            force_weather( WeatherID.CLOUDY_VEGAS );
            Gosub += PLAY_CUTSCENE_1B;
            race_start_x.value = 1234.1494;
            race_start_y.value = 1940.1233;
            race_start_z.value = 6.2527;
            race_start_a.value = 24.8098;
            player_car_model.value = CarModel.INFERNUS;
            enemy_start_x.value = 1199.8602;
            enemy_start_y.value = 1941.6168;
            enemy_start_z.value = 6.125;
            enemy_start_a.value = 335.3667;
            enemy_car_model.value = CarModel.TURISMO;
            enemy_actor_model.value = ActorModel.SWMYCR;
            loaded_path.value = 906;
            race_path_speed.value = 0.92;
            Gosub += CREATE_PLAYER_CAR;
            Gosub += CREATE_ENEMY_CAR_AND_ACTOR;
            Gosub += CUSTOMIZE_CARS1;
            Gosub += LOAD_PATH;
            wait( 1500 );
            AUDIO_BG.play();
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            no_suget_duel.value = false;
            race_checkpoint_type.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX  
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 17;  // TOTAL POINTS - 1 
            // ALL position point X
            checkpoint_x[ 0 ].value = 1253.3036;
            checkpoint_x[ 1 ].value = 1671.7897;
            checkpoint_x[ 2 ].value = 1791.6088;
            checkpoint_x[ 3 ].value = 1788.7056;
            checkpoint_x[ 4 ].value = 1789.2075;
            checkpoint_x[ 5 ].value = 1767.4916;
            checkpoint_x[ 6 ].value = 1404.6353;
            checkpoint_x[ 7 ].value = 1037.2853;
            checkpoint_x[ 8 ].value = 705.9747;
            checkpoint_x[ 9 ].value = 243.3759;
            checkpoint_x[ 10 ].value = -331.4474;
            checkpoint_x[ 11 ].value = -710.7531;
            checkpoint_x[ 12 ].value = -860.2297;
            checkpoint_x[ 13 ].value = -994.6329;
            checkpoint_x[ 14 ].value = -1052.0522;
            checkpoint_x[ 15 ].value = -904.5975;
            checkpoint_x[ 16 ].value = -774.6328;
            checkpoint_x[ 17 ].value = -720.0871;
            // ALL position point Y
            checkpoint_y[ 0 ].value = 2359.771;
            checkpoint_y[ 1 ].value = 2443.2947;
            checkpoint_y[ 2 ].value = 2298.5889;
            checkpoint_y[ 3 ].value = 1806.192;
            checkpoint_y[ 4 ].value = 1389.9998;
            checkpoint_y[ 5 ].value = 852.483;
            checkpoint_y[ 6 ].value = 868.3264;
            checkpoint_y[ 7 ].value = 789.0037;
            checkpoint_y[ 8 ].value = 675.741;
            checkpoint_y[ 9 ].value = 750.9266;
            checkpoint_y[ 10 ].value = 567.8048;
            checkpoint_y[ 11 ].value = 693.3741;
            checkpoint_y[ 12 ].value = 1056.1511;
            checkpoint_y[ 13 ].value = 1294.728;
            checkpoint_y[ 14 ].value = 1180.3909;
            checkpoint_y[ 15 ].value = 923.7436;
            checkpoint_y[ 16 ].value = 719.9243;
            checkpoint_y[ 17 ].value = 946.6101;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 6.124;
            checkpoint_z[ 1 ].value = 6.6092;
            checkpoint_z[ 2 ].value = 5.2337;
            checkpoint_z[ 3 ].value = 6.1247;
            checkpoint_z[ 4 ].value = 6.1332;
            checkpoint_z[ 5 ].value = 9.7413;
            checkpoint_z[ 6 ].value = 6.2588;
            checkpoint_z[ 7 ].value = 10.075;
            checkpoint_z[ 8 ].value = 9.108;
            checkpoint_z[ 9 ].value = 5.672;
            checkpoint_z[ 10 ].value = 15.5997;
            checkpoint_z[ 11 ].value = 16.3358;
            checkpoint_z[ 12 ].value = 24.5746;
            checkpoint_z[ 13 ].value = 39.7124;
            checkpoint_z[ 14 ].value = 38.2479;
            checkpoint_z[ 15 ].value = 18.2594;
            checkpoint_z[ 16 ].value = 17.5854;
            checkpoint_z[ 17 ].value = 11.5194;
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_DUEL_START;
        }

        private void RACE_1_FINAL2( LabelJump label ) {
            final_first_race_passed.value = true;
            chdir( @"Sound\BLIST" );
            AUDIO_BG.load( 999992 );
            wait( AUDIO_BG.is_ready );
            Gosub += PLAY_CUTSCENE_1C;
            race_start_x.value = -139.253;
            race_start_y.value = 605.6201;
            race_start_z.value = 1.4686;
            race_start_a.value = 16.7871;
            player_car_model.value = CarModel.TURISMO;
            enemy_start_x.value = -134.42;
            enemy_start_y.value = 606.8711;
            enemy_start_z.value = 1.5965;
            enemy_start_a.value = 14.8897;
            enemy_car_model.value = CarModel.INFERNUS;
            enemy_actor_model.value = ActorModel.SWMYCR;
            loaded_path.value = 907;
            race_path_speed.value = 0.84;
            Gosub += CREATE_PLAYER_CAR;
            Gosub += CREATE_ENEMY_CAR_AND_ACTOR;
            Gosub += CUSTOMIZE_CARS1A;
            Gosub += LOAD_PATH;
            wait( 1500 );
            AUDIO_BG.play();
            __fade( true, true );
            Gosub += RACE_321_AND_START;
            no_suget_duel.value = false;
            race_checkpoint_type.value = 0;
            loop_index.value = 0;       // CURRENT POINT INDEX  
            loop_index2.value = 1;      // NEXT POINT INDEX
            race_stopwatch.value = 9;   // TOTAL POINTS - 1 
            // ALL position point X
            checkpoint_x[ 0 ].value = -274.5143;
            checkpoint_x[ 1 ].value = -121.2681;
            checkpoint_x[ 2 ].value = -472.3113;
            checkpoint_x[ 3 ].value = -812.1365;
            checkpoint_x[ 4 ].value = -878.6624;
            checkpoint_x[ 5 ].value = -1179.6979;
            checkpoint_x[ 6 ].value = -1329.3652;
            checkpoint_x[ 7 ].value = -1437.587;
            checkpoint_x[ 8 ].value = -1621.0105;
            checkpoint_x[ 9 ].value = -1635.604;
            // ALL position point Y
            checkpoint_y[ 0 ].value = 618.5314;
            checkpoint_y[ 1 ].value = 815.3954;
            checkpoint_y[ 2 ].value = 1050.7773;
            checkpoint_y[ 3 ].value = 1355.6741;
            checkpoint_y[ 4 ].value = 1650.6195;
            checkpoint_y[ 5 ].value = 1809.4255;
            checkpoint_y[ 6 ].value = 1993.6234;
            checkpoint_y[ 7 ].value = 2368.8945;
            checkpoint_y[ 8 ].value = 2386.7905;
            checkpoint_y[ 9 ].value = 2716.6675;
            // ALL position point Z
            checkpoint_z[ 0 ].value = 13.0178;
            checkpoint_z[ 1 ].value = 20.1371;
            checkpoint_z[ 2 ].value = 10.547;
            checkpoint_z[ 3 ].value = 13.1254;
            checkpoint_z[ 4 ].value = 26.5427;
            checkpoint_z[ 5 ].value = 40.4431;
            checkpoint_z[ 6 ].value = 52.3066;
            checkpoint_z[ 7 ].value = 52.7063;
            checkpoint_z[ 8 ].value = 50.6961;
            checkpoint_z[ 9 ].value = 57.2704;
            Gosub += CREATE_RACE_CHECKPOINT_FOR_SECUNDOMER;
            Jump += RACE_GENERIC_DUEL_START;
        }

        #endregion

        #region CAR CUSTOMIZATION

        private void CUSTOMIZE_START_MISSION( LabelGosub label ) {
            player_car.set_colors( 7, 7 );
            enemy_car.set_colors( 3, 3 );
        }

        private void CUSTOMIZE_CARS6( LabelGosub label ) {
            load_car_component( 1021, 1000, 1096, 1022, 1001, 1081 );
            load_requested_models();
            wait( is_car_component_available( 1021, 1000, 1096, 1022, 1001, 1081 ) );
            player_car.add_component( 1021, temp_car_component )
                      .add_component( 1000, temp_car_component )
                      .add_component( 1096, temp_car_component )
                      .set_colors( 53, 0 );
            enemy_car.add_component( 1022, temp_car_component )
                     .add_component( 1001, temp_car_component )
                     .add_component( 1081, temp_car_component )
                     .set_colors( 122, 16 );
            release_car_component( 1021, 1000, 1096, 1022, 1001, 1081 );
        }

        private void CUSTOMIZE_CARS5( LabelGosub label ) {
            load_car_component( 1082, 1021, 1015, 1007, 1073 );
            load_requested_models();
            wait( is_car_component_available( 1082, 1021, 1015, 1007, 1073 ) );
            player_car.add_component( 1082, temp_car_component )
                      .set_colors( 17, 9 );
            enemy_car.add_component( 1021, temp_car_component )
                     .add_component( 1015, temp_car_component )
                     .add_component( 1007, temp_car_component )
                     .add_component( 1073, temp_car_component )
                     .set_colors( 25, 2 );
            release_car_component( 1082, 1021, 1015, 1007, 1073 );
        }

        private void CUSTOMIZE_CARS4( LabelGosub label ) {
            load_car_component( 1117, 1115, 1113, 1110, 1119, 1077, 1127, 1185, 1100, 1178, 1122, 1075 );
            load_requested_models();
            wait( is_car_component_available( 1117, 1115, 1113, 1110, 1119, 1077, 1127, 1185, 1100, 1178, 1122, 1075 ) );
            player_car.add_component( 1117, temp_car_component )
                      .add_component( 1115, temp_car_component )
                      .add_component( 1113, temp_car_component )
                      .add_component( 1110, temp_car_component )
                      .add_component( 1119, temp_car_component )
                      .add_component( 1077, temp_car_component )
                      .set_colors( 106, 25 ).set_current_paintjob( 2 );
            enemy_car.add_component( 1127, temp_car_component )
                     .add_component( 1185, temp_car_component )
                     .add_component( 1100, temp_car_component )
                     .add_component( 1178, temp_car_component )
                     .add_component( 1122, temp_car_component )
                     .add_component( 1075, temp_car_component )
                     .set_colors( 3, 15 ).set_current_paintjob( 1 );
            release_car_component( 1117, 1115, 1113, 1110, 1119, 1077, 1127, 1185, 1100, 1178, 1122, 1075 );
        }

        private void CUSTOMIZE_CARS3( LabelGosub label ) {
            load_car_component( 1077, 1145, 1018, 1016, 1007, 1074 );
            load_requested_models();
            wait( is_car_component_available( 1077, 1145, 1018, 1016, 1007, 1074 ) );
            player_car.add_component( 1077, temp_car_component )
                      .set_colors( 42, 13 );
            enemy_car.add_component( 1145, temp_car_component )
                     .add_component( 1018, temp_car_component )
                     .add_component( 1016, temp_car_component )
                     .add_component( 1007, temp_car_component )
                     .add_component( 1074, temp_car_component )
                     .set_colors( 5, 5 );
            release_car_component( 1077, 1145, 1018, 1016, 1007, 1074 );
        }

        private void CUSTOMIZE_CARS2( LabelGosub label ) {
            load_car_component( 1029, 1169, 1141, 1033, 1138, 1031, 1085, 1045, 1152, 1151, 1053, 1050, 1047, 1048, 1073 );
            load_requested_models();
            wait( is_car_component_available( 1029, 1169, 1141, 1033, 1138, 1031, 1085, 1045, 1152, 1151, 1053, 1050, 1047, 1048, 1073 ) );
            player_car.add_component( 1029, temp_car_component )
                      .add_component( 1169, temp_car_component )
                      .add_component( 1141, temp_car_component )
                      .add_component( 1033, temp_car_component )
                      .add_component( 1138, temp_car_component )
                      .add_component( 1031, temp_car_component )
                      .add_component( 1085, temp_car_component )
                      .set_colors( 76, 2 ).set_current_paintjob( 3 );
            enemy_car.add_component( 1045, temp_car_component )
                     .add_component( 1152, temp_car_component )
                     .add_component( 1151, temp_car_component )
                     .add_component( 1053, temp_car_component )
                     .add_component( 1050, temp_car_component )
                     .add_component( 1047, temp_car_component )
                     .add_component( 1048, temp_car_component )
                     .add_component( 1073, temp_car_component )
                     .set_colors( 86, 25 ).set_current_paintjob( 0 );
            release_car_component( 1029, 1169, 1141, 1033, 1138, 1031, 1085, 1045, 1152, 1151, 1053, 1050, 1047, 1048, 1073 );
        }

        private void CUSTOMIZE_CARS1( LabelGosub label ) {
            load_car_component( 1074, 1096 );
            load_requested_models();
            wait( is_car_component_available( 1074, 1096 ) );
            player_car.add_component( 1074, temp_car_component )
                      .set_colors( 1, 0 );
            enemy_car.add_component( 1096, temp_car_component )
                     .set_colors( 0, 0 );
            release_car_component( 1074, 1096 );
        }

        private void CUSTOMIZE_CARS1A( LabelGosub label ) {
            load_car_component( 1074, 1096 );
            load_requested_models();
            wait( is_car_component_available( 1074, 1096 ) );
            player_car.add_component( 1096, temp_car_component )
                      .set_colors( 0, 0 );
            enemy_car.add_component( 1074, temp_car_component )
                     .set_colors( 1, 0 );
            release_car_component( 1074, 1096 );
        }

        #endregion

        #region CUTSCENES

        private void PLAY_CUTSCENE_0B( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var john = cutcsene_actors[ 1 ];

            var car = cutcsene_cars[ 0 ];

            __toggle_cinematic( true );
            clear_area( true, 1818.8503, -1121.3219, 24.0781, 300.0 );
            __renderer_at( 1811.4783, -1107.3956, 24.0781 );
            a.set_position( 1799.2805, -1130.3569, 24.0859 );
            load_special_actor( "COPJOHN", 1 );
            load_requested_models( CarModel.GREENWOO );
            wait( is_special_actor_loaded( 1 ) );
            car.create( CarModel.GREENWOO, 1811.6932, -1111.1489, 23.7646 ).set_z_angle( 0.0 ).set_colors( 7, 7 );
            player.create_in_vehicle_passenger_seat( ActorType.MISSION1, NULL, car, 0 ).set_muted( true ).disable_speech( true );
            john.create( ActorType.MISSION1, SPECIAL01, 1813.6903, -1109.7563, 23.0781 ).set_z_angle( 132.1567 ).set_muted( true ).disable_speech( true );
            chdir( @"Sound\BLIST\0B" );
            AUDIO_PL.load( 12 );
            wait( AUDIO_PL.is_ready );
            CAMERA.set_position( 1811.4783, -1107.3956, 25.0781 ).set_point_at( 1813.1891, -1111.0835, 24.0781, 2 );
            player.stop_facial_talk().task.look_at_actor( john, 100000 );
            john.stop_facial_talk().task.look_at_actor( player, 100000 );
            wait( 1000 );
            __fade( 1, false );
            Scene += l => {
                wait( 500 );

                AUDIO_PL.play(); // 0
                john.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 5000 );
                show_text_highpriority( "@BL@0", 5000, 1 );
                wait( 5000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 1
                player.start_facial_talk( 3000 );
                show_text_highpriority( "@BL@1", 3000, 1 );
                wait( 3000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 2
                john.start_facial_talk( 5000 );
                show_text_highpriority( "@BL@2", 5000, 1 );
                wait( 500 );
                john.task.perform_animation( "FUCKU", "PED", 4.0, 0, 0, 0, 0, 1330 );
                wait( 4500 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 3
                player.start_facial_talk( 6000 );
                show_text_highpriority( "@BL@3", 6000, 1 );
                wait( 6000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 4
                john.start_facial_talk( 9000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 9000 );
                show_text_highpriority( "@BL@4", 9000, 1 );
                wait( 9000 );
                john.stop_facial_talk();

                CAMERA.set_position( 1819.4132, -1123.8369, 24.0781 ).set_point_at( 1813.1891, -1111.0835, 24.0781, 2 );

                AUDIO_PL.play(); // 5
                john.task.perform_animation( "XPRESSscratch", "PED", 4.0, 0, 0, 0, 0, 4000 );
                player.start_facial_talk( 4000 );
                show_text_highpriority( "@BL@5", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                CAMERA.transverse_position( 1819.4132, -1123.8369, 24.0781, 1817.4315, -1119.6432, 24.0781, 13200, 1 );

                AUDIO_PL.play(); // 6
                john.start_facial_talk( 8000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 8000 );
                show_text_highpriority( "@BL@6", 8000, 1 );
                wait( 8000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 7
                john.task.perform_animation( "XPRESSscratch", "PED", 4.0, 0, 0, 0, 0, 5000 );
                player.start_facial_talk( 5000 );
                show_text_highpriority( "@BL@7", 5000, 1 );
                wait( 5000 );
                player.stop_facial_talk();

                CAMERA.set_position( 1817.4315, -1119.6432, 24.0781 ).set_point_at( 1813.1891, -1111.0835, 24.0781, 2 );

                AUDIO_PL.play(); // 8
                john.start_facial_talk( 13000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 13000 );
                show_text_highpriority( "@BL@8", 13000, 1 );
                wait( 13000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 9
                john.task.perform_animation( "XPRESSscratch", "PED", 4.0, 0, 0, 0, 0, 6500 );
                player.start_facial_talk( 6500 );
                show_text_highpriority( "@BL@9", 6500, 1 );
                wait( 6500 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 10
                john.start_facial_talk( 8000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 8000 );
                show_text_highpriority( "@BL@10", 8000, 1 );
                wait( 8000 );
                john.stop_facial_talk();

                john.clear_tasks().set_walk_style( WalkStyle.FATMAN ).task.walk_to_point( 1816.9677, -1144.6384, 24.0024, 180.5562, 1.0 );
                wait( 2000 );

                AUDIO_PL.play(); // 11
                player.start_facial_talk( 3000 );
                show_text_highpriority( "@BL@11", 3000, 1 );
                wait( 3000 );
                player.stop_facial_talk().task.shuffle_to_next_car_seat( car );
                wait( 500 );
            };
            __fade( 0, true );
            __toggle_cinematic( false );
            CAMERA.transverse_position( 1819.4132, -1123.8369, 24.0781, 1817.4315, -1119.6432, 24.0781, 0, 2 );
            destroy_model( CarModel.GREENWOO );
            unload_special_actor( 1 );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        private void PLAY_CUTSCENE_0A( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var darius = cutcsene_actors[ 1 ];
            var vik = cutcsene_actors[ 2 ];
            var marcy = cutcsene_actors[ 3 ];
            var leicter = cutcsene_actors[ 4 ];
            var sonny = cutcsene_actors[ 5 ];
            Int[] usedModels = new Int[] { WMYJG, VBMYCR, WFYST, VWMYCD, SWMYCR, INFO, CELLPHONE };

            __toggle_cinematic( true );
            select_interior( 1 );
            a.link_to_interior( 1 );
            clear_area( true, 684.3957, -462.4003, -24.8099, 300.0 );
            a.set_position( 680.4469, -459.0183, -25.6172 );
            __renderer_at( 684.3957, -462.4003, -25.8099 );
            load_requested_models( usedModels );
            cutcsene_objects[ 0 ].create( INFO, 685.0822, -453.1645, -25.6172 );
            cutcsene_objects[ 1 ].create( INFO, 684.7867, -459.1293, -25.6099 );
            to( loop_index, 0, 1, e => {
                cutcsene_objects[ loop_index ].set_collision_detection( false ).set_visibility( false ).link_to_interior( 1 );
            } );
            player.create( ActorType.MISSION1, NULL, 678.2982, -458.028, -25.6099 ).set_z_angle( 4.1476 );
            darius.create( ActorType.MISSION1, SWMYCR, 687.8539, -460.0482, -24.4067 ).set_z_angle( 2.8943 );
            vik.create( ActorType.MISSION1, VWMYCD, 686.473, -460.046, -24.4067 ).set_z_angle( 5.0876 );
            marcy.create( ActorType.MISSION1, WFYST, 687.846, -458.9236, -24.4067 ).set_z_angle( 185.2326 );
            leicter.create( ActorType.MISSION1, VBMYCR, 686.4058, -459.0107, -24.4141 ).set_z_angle( 187.426 );
            sonny.create( ActorType.MISSION1, WMYJG, 678.1765, -452.9822, -24.6172 ).set_z_angle( 268.9168 );
            to( loop_index, 0, 5, e => {
                cutcsene_actors[ loop_index ].link_to_interior( 1 ).set_muted( true ).disable_speech( true );
                and( loop_index != 0, loop_index != 5, delegate { cutcsene_actors[ loop_index ].task.perform_animation( "SEAT_IDLE", "PED", 4.0, 1, 0, 0, 0, -1 ); } );
            } );
            chdir( @"Sound\BLIST\0A" );
            AUDIO_PL.load( 15 );
            wait( AUDIO_PL.is_ready );
            CAMERA.set_position( 684.1369, -463.3928, -25.6099 ).set_point_at( 684.9445, -459.3615, -25.6099, 2 );
            cutscene_as_pack.clear().define( actor => {
                actor.walk_to_point( 678.1013, -453.1666, -25.6172, 275.7869, 1.0 )
                     .walk_to_point( 684.7503, -453.1301, -25.6172, 180.5561, 1.0 )
                     .walk_to_point( 684.6844, -456.9572, -25.6099, 209.6729, 1.0 )
                     .rotate_to_actor( darius );
            } );
            player.assign_to_AS_pack( cutscene_as_pack );
            cutscene_as_pack.clear().define( actor => {
                actor.goto_object( cutcsene_objects[ 0 ], -1, 1.0 )
                     .goto_object( cutcsene_objects[ 1 ], -1, 1.0 )
                     .rotate_to_actor( darius );
            } );
            sonny.assign_to_AS_pack( cutscene_as_pack );
            cutscene_as_pack.clear();
            wait( 1000 );
            __fade( 1, false );
            Scene += delegate {
                wait( 500 );
                wait( sonny.is_near_point_3d_on_foot( false, 684.7867, -459.1293, -25.6099, 1.0, 1.0, 1.0 ) );
                darius.task.look_at_actor( sonny, -1 );

                AUDIO_PL.play(); // 0
                darius.start_facial_talk( 4000 );
                show_text_highpriority( "@BL@12", 4000, 1 );
                wait( 4000 );
                darius.stop_facial_talk();

                AUDIO_PL.play(); // 1
                sonny.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@BL@13", 4000, 1 );
                wait( 4000 );
                sonny.stop_facial_talk();

                leicter.task.look_at_actor( sonny, -1 );

                AUDIO_PL.play(); // 2
                leicter.start_facial_talk( 5000 );
                show_text_highpriority( "@BL@14", 5000, 1 );
                wait( 5000 );
                leicter.stop_facial_talk();

                AUDIO_PL.play(); // 3
                sonny.start_facial_talk( 1500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 1500 );
                show_text_highpriority( "@BL@15", 1500, 1 );
                wait( 1500 );
                sonny.stop_facial_talk();

                CAMERA.set_position( 682.0664, -457.8268, -24.6172 ).set_point_at( 687.1406, -460.0067, -25.4067, 2 );

                marcy.task.look_at_actor( darius, -1 );
                vik.task.look_at_actor( sonny, -1 );

                AUDIO_PL.play(); // 4
                vik.start_facial_talk( 6000 );
                show_text_highpriority( "@BL@16", 6000, 1 );
                wait( 6000 );
                vik.stop_facial_talk();

                darius.task.look_at_actor( player, -1 );
                vik.task.look_at_actor( player, -1 );

                AUDIO_PL.play(); // 5
                darius.start_facial_talk( 8000 );
                show_text_highpriority( "@BL@17", 8000, 1 );
                wait( 8000 );
                darius.stop_facial_talk();

                CAMERA.set_point_at( 684.6844, -456.9572, -25.1099, 1 );

                AUDIO_PL.play(); // 6
                player.start_facial_talk( 6000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@BL@18", 6000, 1 );
                wait( 6000 );
                player.stop_facial_talk();

                CAMERA.set_point_at( 687.1406, -460.0067, -25.4067, 1 );

                AUDIO_PL.play(); // 7
                darius.start_facial_talk( 8000 );
                show_text_highpriority( "@BL@19", 8000, 1 );
                wait( 8000 );
                darius.stop_facial_talk();

                AUDIO_PL.play(); // 8
                show_text_highpriority( "@BL@20", 6000, 1 );
                wait( 6000 );

                leicter.task.look_at_actor( darius, -1 );
                darius.task.look_at_actor( leicter, -1 );

                AUDIO_PL.play(); // 9
                leicter.start_facial_talk( 8000 );
                show_text_highpriority( "@BL@21", 8000, 1 );
                wait( 8000 );
                leicter.stop_facial_talk();

                AUDIO_PL.play(); // 10
                darius.start_facial_talk( 6000 );
                show_text_highpriority( "@BL@22", 6000, 1 );
                wait( 6000 );
                darius.stop_facial_talk();
            };
            AUDIO_PL.play( -1 );
            __fade( 0, true );
            __clear_text();
            select_interior( 0 );
            clear_area( true, START_POINT_X, START_POINT_Y, START_POINT_Z, 8.0 );
            clear_area( true, 29.0112, -2640.5049, 39.4312, 8.0 );
            a.link_to_interior( 0 );
            player.link_to_interior( 0 ).clear_tasks().clear_tasks_immediately().task.clear_look_at();
            a.set_position( START_POINT_X, START_POINT_Y, START_POINT_Z ).set_z_angle( START_POINT_A );
            player.set_position( 29.0112, -2640.5049, 39.4312 ).set_z_angle( 95.8424 );
            wait( 1500 );
            player.task.hold_cellphone( true );
            __renderer_at( 23.1322, -2640.9238, 40.4378 );
            CAMERA.set_position( 23.1322, -2640.9238, 40.4378 ).set_point_at( 29.0112, -2640.5049, 40.4312, 2 );;
            __fade( 1, false );
            Scene += delegate {
                wait( 2200 );

                AUDIO_PL.play( 11 ); // 11
                player.start_facial_talk( 4000 );
                show_text_highpriority( "@BL@23", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 12
                show_text_highpriority( "@BL@24", 3000, 1 );
                wait( 3000 );

                AUDIO_PL.play(); // 13
                player.start_facial_talk( 5000 );
                show_text_highpriority( "@BL@25", 5000, 1 );
                wait( 5000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 14
                show_text_highpriority( "@BL@26", 2000, 1 );
                wait( 2000 );
                player.task.hold_cellphone( false );
                wait( 1000 );
            };
            __fade( 0, true );
            destroy_model( usedModels );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        private void PLAY_CUTSCENE_6B( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var sonny = cutcsene_actors[ 1 ];

            __toggle_cinematic( true );
            clear_area( true, 1732.212, -711.5898, 50.0955, 300.0 );
            __renderer_at( 1732.212, -711.5898, 50.0955 );
            load_animation( "SWEET" );
            load_requested_models( WMYJG );
            chdir( @"Sound\BLIST\6B" );
            AUDIO_PL.load( 3 );
            wait( AUDIO_PL.is_ready, is_animation_loaded( "SWEET" ) );
            player.create( ActorType.MISSION1, NULL, 1732.1704, -711.8998, 49.1113 ).set_z_angle( 180.0 ).set_muted( true ).disable_speech( true );
            sonny.create( ActorType.MISSION1, WMYJG, 1732.17041, -712.8998, 49.1851 ).set_z_angle( 0.0 ).set_muted( true ).disable_speech( true );
            CAMERA.set_position( 1726.7305, -712.62, 50.5035 ).set_point_at( 1730.379, -712.4454, 50.0563, 2 );
            wait( 1000 );
            __fade( 1, false );
            Scene += delegate {

                player.task.perform_animation( "plyr_hndshldr_01", "SWEET", 4.0, 0, 0, 0, 0, 16500 );
                wait( 500 );

                AUDIO_PL.play(); // 0
                sonny.start_facial_talk( 8000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 8000 );
                show_text_highpriority( "@BL@27", 8000, 1 );
                wait( 8000 );
                sonny.stop_facial_talk();

                AUDIO_PL.play(); // 1
                sonny.start_facial_talk( 8000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 8000 );
                show_text_highpriority( "@BL@28", 8000, 1 );
                wait( 8000 );
                sonny.stop_facial_talk();

                AUDIO_PL.play(); // 2
                player.start_facial_talk( 8000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 8000 );
                show_text_highpriority( "@BL@29", 8000, 1 );
                wait( 6000 );
                sonny.task.rotate_angle( 90.0 );
                player.stop_facial_talk().set_walk_style( WalkStyle.PLAYER ).task.walk_to_point( 1715.9048, -716.9094, 48.357, 75.8999, 1.0 );
                wait( 2000 );

            };
            __fade( 0, true );
            __toggle_cinematic( false );
            release_animation( "SWEET" );
            destroy_model( WMYJG );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        private void PLAY_CUTSCENE_6A( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var sonny = cutcsene_actors[ 1 ];
            var car = cutcsene_cars[ 0 ];
            Int[] usedModels = new Int[] { WMYJG, PRIMO };

            __toggle_cinematic( true );
            clear_area( true, 2829.8838, -1889.311, 10.9375, 300.0 );
            __renderer_at( 2829.8838, -1889.311, 10.9375 );
            load_car_component( 1022, 1001, 1081 );
            load_requested_models( usedModels );
            wait( is_car_component_available( 1022, 1001, 1081 ) );
            AUDIO_PL.load( 4 );
            wait( AUDIO_PL.is_ready );
            car.create( CarModel.PRIMO, 2831.084, -1886.3143, 10.5485 ).set_z_angle( 152.0031 ).set_colors( 122, 16 )
               .add_component( 1022, temp_car_component )
               .add_component( 1001, temp_car_component )
               .add_component( 1081, temp_car_component );
            sonny.create_in_vehicle_driverseat( ActorType.MISSION1, WMYJG, car );
            player.create( ActorType.MISSION1, NULL, 2832.1282, -1889.0292, 9.9375 ).set_z_angle( 11.0177 );
            CAMERA.set_position( 2829.2065, -1890.8125, 11.9375 ).set_point_at( 2832.5137, -1886.8602, 10.9375, 2 );
            cutscene_as_pack.define( ped => {
                ped.exit_vehicle().rotate_to_actor( player );
            } );
            wait( 1000 );
            sonny.assign_to_AS_pack( cutscene_as_pack );
            cutscene_as_pack.clear();
            __fade( 1, false );
            Scene += delegate {
                wait( 1500 );
                player.task.rotate_to_actor( sonny );

                AUDIO_PL.play(); // 0
                player.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 5000 );
                show_text_highpriority( "@BL@30", 5000, 1 );
                wait( 5000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 1
                sonny.start_facial_talk( 6000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@BL@31", 6000, 1 );
                wait( 6000 );
                sonny.stop_facial_talk();

                AUDIO_PL.play(); // 2
                player.start_facial_talk( 7000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 7000 );
                show_text_highpriority( "@BL@32", 7000, 1 );
                wait( 5000 );
                player.task.walk_to_point( 2836.5532, -1905.2063, 10.9375, 182.4127, 1.0 );
                wait( 2000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 3
                sonny.start_facial_talk( 2000 ).task.perform_animation( "FUCKU", "PED", 4.0, 0, 0, 0, 0, 2000 );
                show_text_highpriority( "@BL@33", 2000, 1 );
                wait( 2000 );
                sonny.stop_facial_talk();
            };
            __fade( 0, true );
            __toggle_cinematic( false );
            destroy_model( usedModels );
            release_car_component( 1022, 1001, 1081 );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        private void PLAY_CUTSCENE_5B( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var darius = cutcsene_actors[ 1 ];
            var vik = cutcsene_actors[ 2 ];
            var markus = cutcsene_actors[ 3 ];
            Int[] usedModels = new Int[] { SWMYCR, VWMYCD, BMYST };

            __toggle_cinematic( true );
            clear_area( true, 684.3957, -462.4003, -24.8099, 300.0 );
            __renderer_at( 684.3957, -462.4003, -25.8099 );
            select_interior( 1 );
            a.link_to_interior( 1 ).set_position( 684.2412, -464.4989, -25.6099 );
            load_animation( "BAR" );
            load_requested_models( usedModels );
            chdir( @"Sound\BLIST\5B" );
            AUDIO_PL.load( 5 );
            wait( AUDIO_PL.is_ready, is_animation_loaded( "BAR" ) );
            player.create( ActorType.MISSION1, NULL, 679.0638, -454.9103, -24.6099 ).set_z_angle( 264.7518 ).link_to_interior( 1 );
            darius.create( ActorType.MISSION1, SWMYCR, 680.6841, -453.8069, -24.6172 ).set_z_angle( 199.3444 ).link_to_interior( 1 );
            vik.create( ActorType.MISSION1, VWMYCD, 682.0438, -453.694, -24.6172 ).set_z_angle( 171.1442 ).link_to_interior( 1 );
            markus.create( ActorType.MISSION1, BMYST, 681.5868, -455.5416, -24.6099 ).set_z_angle( 1.9426 ).link_to_interior( 1 );
            player.task.rotate_to_actor( markus );
            darius.task.rotate_to_actor( markus );
            vik.task.rotate_to_actor( markus ).perform_animation( "Barserve_loop", "BAR", 3.0, 1, 0, 0, 0, -1 );
            wait( 1000 );
            CAMERA.set_position( 684.739, -455.5931, -25.1099 ).set_point_at( 681.6689, -454.7907, -25.6099, 2 );
            __fade( 1, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                vik.start_facial_talk( 6000 );
                show_text_highpriority( "@BL@34", 6000, 1 );
                wait( 6000 );
                vik.stop_facial_talk();

                AUDIO_PL.play(); // 1
                markus.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 9000 );
                show_text_highpriority( "@BL@35", 5000, 1 );
                wait( 5000 );
                markus.stop_facial_talk();

                AUDIO_PL.play(); // 2
                markus.start_facial_talk( 4000 ).task.look_at_actor( player, 10000 );
                show_text_highpriority( "@BL@36", 4000, 1 );
                wait( 4000 );
                markus.stop_facial_talk();

                AUDIO_PL.play(); // 3
                player.start_facial_talk( 6000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 ).look_at_actor( markus, 6000 );
                show_text_highpriority( "@BL@37", 6000, 1 );
                wait( 5000 );
                player.task.perform_animation( "FUCKU", "PED", 4.0, 0, 0, 0, 0, 1330 );
                darius.task.rotate_to_actor( player );
                wait( 2000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 4
                darius.start_facial_talk( 11000 );
                player.task.look_at_actor( darius, 12000 );
                markus.task.look_at_actor( darius, 12000 );
                darius.task.look_at_actor( player, 12000 );
                show_text_highpriority( "@BL@38", 11000, 1 );
                wait( 11000 );
                darius.stop_facial_talk();
                wait( 1000 );
            };
            __fade( 0, true );
            __toggle_cinematic( false );
            destroy_model( usedModels );
            release_animation( "BAR" );
            AUDIO_PL.unload();
            select_interior( 0 );
            a.link_to_interior( 0 );
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        private void PLAY_CUTSCENE_5A( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];

            __toggle_cinematic( true );
            clear_area( true, -2900.2336, -1206.312, 8.3018, 300.0 );
            __renderer_at( -2899.7197, -1203.031, 9.314 );
            load_wav( RING_WAV_ID, 1 );
            load_requested_models( CELLPHONE );
            chdir( @"Sound\BLIST\5A" );
            AUDIO_PL.load( 2 );
            wait( AUDIO_PL.is_ready, is_wav_loaded( 1 ) );
            player.create( ActorType.MISSION1, NULL, -2900.2336, -1206.312, 8.3018 ).set_z_angle( 352.7905 );
            link_wav_to_actor( 1, player );
            wait( 1000 );
            CAMERA.set_position( -2899.7197, -1203.031, 9.814 ).set_point_at( -2900.2336, -1206.312, 9.3018, 2 );
            __fade( 1, false );
            Scene += delegate {
                wait( 500 );
                play_wav( 1 );

                wait( is_wav_ended( 1 ) );

                player.task.hold_cellphone( true );
                wait( 2000 );

                AUDIO_PL.play(); //0
                show_text_highpriority( "@BL@39", 12000, 1 );
                wait( 12000 );

                AUDIO_PL.play(); // 1
                player.start_facial_talk( 4000 );
                show_text_highpriority( "@BL@40", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk().task.hold_cellphone( false );
                wait( 1000 );
            };
            __fade( 0, true );
            __toggle_cinematic( false );
            destroy_model( CELLPHONE );
            unload_wav( 1 );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        private void PLAY_CUTSCENE_4B( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var leicter = cutcsene_actors[ 1 ];

            var leicter_car = cutcsene_cars[ 0 ];

            __toggle_cinematic( true );
            __renderer_at( -1896.9537, -1184.55, 39.1904 );
            load_car_component( 1127, 1185, 1100, 1178, 1122, 1075 );
            load_requested_models( REMINGTN, VBMYCR );
            wait( is_car_component_available( 1127, 1185, 1100, 1178, 1122, 1075 ) );
            chdir( @"Sound\BLIST\4B" );
            AUDIO_PL.load( 3 );
            wait( AUDIO_PL.is_ready );
            leicter_car.create( CarModel.REMINGTN, -1900.7241, -1192.6681, 38.8694 ).set_z_angle( 0.0 )
                       .add_component( 1127, temp_car_component )
                       .add_component( 1185, temp_car_component )
                       .add_component( 1100, temp_car_component )
                       .add_component( 1178, temp_car_component )
                       .add_component( 1122, temp_car_component )
                       .add_component( 1075, temp_car_component )
                       .set_colors( 3, 15 ).set_current_paintjob( 1 );
            leicter.create_in_vehicle_driverseat( ActorType.MISSION1, VBMYCR, leicter_car );
            player.create( ActorType.MISSION1, NULL, -1898.3662, -1192.0258, 38.404 ).task.rotate_to_actor( leicter );
            wait( 1000 );
            CAMERA.set_position( -1900.7241, -1188.1498, 39.033 ).set_point_at( -1900.7241, -1192.6681, 38.3694, 2 );
            __fade( 1, false );
            Scene += delegate {
                wait( 2000 );
                CAMERA.set_position( -1902.7241, -1188.1498, 41.033 );
                set_interpolation_parameters( 0.0, 4000 );
                CAMERA.set_point_at( -1900.7241, -1192.6681, 38.3694, 1 );
                leicter.task.look_at_actor( player, 60000 );
                player.task.look_at_actor( leicter, 60000 );
                wait( 1000 );

                AUDIO_PL.play(); // 0
                leicter.start_facial_talk( 7000 );
                show_text_highpriority( "@BL@41", 7000, 1 );
                wait( 7000 );
                leicter.stop_facial_talk();

                AUDIO_PL.play(); // 1
                player.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@BL@42", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 2
                leicter.start_facial_talk( 5000 );
                show_text_highpriority( "@BL@43", 5000, 1 );
                wait( 5000 );
                leicter.stop_facial_talk();

                wait( 1000 );
            };
            __fade( 0, true );
            __toggle_cinematic( false );
            destroy_model( REMINGTN, VBMYCR );
            release_car_component( 1127, 1185, 1100, 1178, 1122, 1075 );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        private void PLAY_CUTSCENE_4A( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var crouch = cutcsene_actors[ 1 ];

            __toggle_cinematic( true );
            clear_area( true, -1870.1569, 1422.5929, 7.1798, 300.0 );
            __renderer_at( -1870.1569, 1422.5929, 7.1798 );
            load_requested_models( OMORI );
            chdir( @"Sound\BLIST\4A" );
            AUDIO_PL.load( 10 );
            wait( AUDIO_PL.is_ready );
            player.create( ActorType.MISSION1, NULL, -1870.1569, 1422.5929, 7.1798 ).set_z_angle( 226.8571 );
            crouch.create( ActorType.MISSION1, OMORI, -1859.9563, 1413.5466, 6.1875 ).set_z_angle( 49.8714 );
            cutscene_as_pack.clear().define( ped => {
                ped.walk_to_point( -1869.1304, 1421.637, 7.1813, 49.558, 1.0 );
            } );
            crouch.assign_to_AS_pack( cutscene_as_pack );
            cutscene_as_pack.clear();
            CAMERA.set_position( -1867.2974, 1425.0681, 7.5801 ).set_point_at( -1859.9563, 1413.5466, 6.1875, 2 );
            wait( 1000 );
            __fade( 1, false );
            Scene += delegate {
                wait( 500 );
                set_interpolation_parameters( 0.0, 7000 );
                CAMERA.set_point_at( -1868.9446, 1422.1292, 7.1811, 1 );

                AUDIO_PL.play(); // 0
                show_text_highpriority( "@BL@44", 4000, 1 );

                wait( crouch.is_near_point_3d_on_foot( false, -1869.1304, 1421.637, 7.1813, 1.25, 1.25, 1.25 ) );

                crouch.clear_tasks().task.look_at_actor( player, 90000 );
                player.task.look_at_actor( crouch, 90000 );

                AUDIO_PL.play(); // 1
                crouch.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@BL@45", 4000, 1 );
                wait( 4000 );
                crouch.stop_facial_talk();

                AUDIO_PL.play(); // 2
                player.start_facial_talk( 2000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 2000 );
                show_text_highpriority( "@BL@46", 2000, 1 );
                wait( 2000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 3
                crouch.start_facial_talk( 6500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6500 );
                show_text_highpriority( "@BL@47", 6500, 1 );
                wait( 6500 );
                crouch.stop_facial_talk();

                AUDIO_PL.play(); // 4
                player.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@BL@48", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 5
                crouch.start_facial_talk( 6000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@BL@49", 6000, 1 );
                wait( 6000 );
                crouch.stop_facial_talk();

                AUDIO_PL.play(); // 6
                player.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@BL@50", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 7
                crouch.start_facial_talk( 7000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 7000 );
                show_text_highpriority( "@BL@51", 7000, 1 );
                wait( 7000 );
                crouch.stop_facial_talk();

                AUDIO_PL.play(); // 8
                player.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 5000 );
                show_text_highpriority( "@BL@52", 5000, 1 );
                wait( 5000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 9
                crouch.start_facial_talk( 8000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 8000 );
                show_text_highpriority( "@BL@53", 8000, 1 );
                wait( 8000 );
                crouch.stop_facial_talk();

                wait( 1000 );
            };
            __fade( 0, true );
            __toggle_cinematic( false );
            destroy_model( OMORI );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        private void PLAY_CUTSCENE_3B( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var marcy = cutcsene_actors[ 1 ];

            __toggle_cinematic( true );
            clear_area( true, 29.3322, -2640.1804, 40.4299, 300.0 );
            __renderer_at( 29.3322, -2640.1804, 40.4299 );
            load_requested_models( WFYST );
            chdir( @"Sound\BLIST\3B" );
            AUDIO_PL.load( 4 );
            wait( AUDIO_PL.is_ready );
            player.create( ActorType.MISSION1, NULL, 30.0, -2642.1597, 39.4396 ).set_z_angle( 90.0 );
            marcy.create( ActorType.MISSION1, WFYST, 29.0, -2642.1597, 39.4396 ).set_z_angle( 270.0 );
            wait( 1000 );
            CAMERA.set_position( 29.5, -2640.0, 40.9299 ).set_point_at( 29.5, -2642.0, 40.9337, 2 );
            __fade( 1, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                marcy.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 5000 );
                show_text_highpriority( "@BL@54", 5000, 1 );
                wait( 5000 );
                marcy.stop_facial_talk();

                AUDIO_PL.play(); // 1
                player.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 5000 );
                show_text_highpriority( "@BL@55", 5000, 1 );
                wait( 5000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 2
                marcy.start_facial_talk( 6000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@BL@56", 6000, 1 );
                wait( 6000 );
                marcy.stop_facial_talk();

                AUDIO_PL.play(); // 3
                player.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@BL@57", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

            };
            __fade( 0, true );
            __toggle_cinematic( false );
            destroy_model( WFYST );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        private void PLAY_CUTSCENE_3A( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var marcy = cutcsene_actors[ 1 ];

            var cigar = cutcsene_objects[ 0 ];

            __toggle_cinematic( true );
            clear_area( true, -631.0139, 1192.6025, 10.1928, 300.0 );
            __renderer_at( -631.0139, 1192.6025, 10.1928 );
            load_animation( "SMOKING" );
            load_requested_models( CIGAR, WFYST );
            chdir( @"Sound\BLIST\3A" );
            AUDIO_PL.load( 3 );
            wait( AUDIO_PL.is_ready, is_animation_loaded( "SMOKING" ) );
            cigar.create( ObjectModel.CIGAR, 0.0, 0.0, 0.0 );
            player.create( ActorType.MISSION1, NULL, -630.4789, 1191.6798, 11.089 ).set_z_angle( 204.3696 );
            marcy.create( ActorType.MISSION1, WFYST, -629.9453, 1190.562, 10.0152 ).set_z_angle( 21.8503 ).task.pick_up_object( cigar, 0.04, 0.1, -0.02, 5, 16, "NULL", "NULL", -1 );
            player.task.look_at_actor( marcy, 1000000 );
            marcy.task.look_at_actor( player, 1000000 ).perform_animation( "M_smklean_loop", "SMOKING", 4.0, 1, 0, 0, 0, -1 );
            wait( 1000 );
            CAMERA.set_position( -636.7469, 1191.8594, 13.4585 ).set_point_at( -630.5638, 1190.8945, 11.0748, 2 );
            __fade( 1, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                marcy.start_facial_talk( 5000 );
                show_text_highpriority( "@BL@58", 5000, 1 );
                wait( 5000 );
                marcy.stop_facial_talk();

                AUDIO_PL.play(); // 1
                player.start_facial_talk( 6000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@BL@59", 6000, 1 );
                wait( 6000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 2
                marcy.start_facial_talk( 3000 );
                show_text_highpriority( "@BL@60", 3000, 1 );
                wait( 3000 );
                marcy.stop_facial_talk();

            };
            __fade( 0, true );
            __toggle_cinematic( false );
            destroy_model( CIGAR, WFYST );
            release_animation( "SMOKING" );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
            wait( 1000 );
        }

        private void PLAY_CUTSCENE_2B( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var vik = cutcsene_actors[ 1 ];

            var cs_player_car = cutcsene_cars[ 0 ];
            var vik_car = cutcsene_cars[ 1 ];

            __toggle_cinematic( true );
            clear_area( true, 1388.043, -935.0217, 34.1875, 300.0 );
            __renderer_at( 1388.043, -935.0217, 34.1875 );
            load_requested_models( SULTAN, FLASH, VWMYCD );
            chdir( @"Sound\BLIST\2B" );
            AUDIO_PL.load( 5 );
            wait( AUDIO_PL.is_ready );
            player_car.create( CarModel.SULTAN, 1391.7064, -944.884, 33.9836 ).set_z_angle( 82.3759 );
            enemy_car.create( CarModel.FLASH, 1392.0852, -940.487, 33.8083 ).set_z_angle( 82.434 );
            Gosub += CUSTOMIZE_CARS2;
            cs_player_car.value = player_car;
            vik_car.value = enemy_car;
            player.create_in_vehicle_driverseat( ActorType.MISSION1, NULL, cs_player_car );
            vik.create_in_vehicle_driverseat( ActorType.MISSION1, VWMYCD, vik_car );
            player.task.look_at_actor( vik, 1000000 );
            vik.task.look_at_actor( player, 1000000 );
            wait( 1000 );
            CAMERA.set_position( 1388.043, -935.0217, 34.1875 ).set_point_at( 1391.0114, -942.3994, 34.2981, 2 );
            __fade( 1, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                vik.start_facial_talk( 6000 );
                show_text_highpriority( "@BL@61", 6000, 1 );
                wait( 6000 );
                vik.stop_facial_talk();

                AUDIO_PL.play(); // 1
                player.start_facial_talk( 3000 );
                show_text_highpriority( "@BL@62", 3000, 1 );
                wait( 3000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 2
                vik.start_facial_talk( 6000 );
                show_text_highpriority( "@BL@63", 6000, 1 );
                wait( 6000 );
                vik.stop_facial_talk();

                CAMERA.set_position( 1389.043, -947.5217, 34.3875 ).set_point_at( 1391.0114, -942.3994, 34.2981, 2 );

                AUDIO_PL.play(); // 3
                player.start_facial_talk( 6000 );
                show_text_highpriority( "@BL@64", 6000, 1 );
                wait( 6000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 4
                vik.start_facial_talk( 2000 );
                show_text_highpriority( "@BL@65", 2000, 1 );
                wait( 2000 );
                vik.stop_facial_talk();

            };
            __fade( 0, true );
            __toggle_cinematic( false );
            destroy_model( SULTAN, FLASH, VWMYCD );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        private void PLAY_CUTSCENE_2A( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];

            __toggle_cinematic( true );
            clear_area( true, 2380.9229, -2170.7952, 13.5469, 300.0 );
            __renderer_at( 2380.9229, -2170.7952, 13.5469 );
            load_wav( RING_WAV_ID, 1 );
            load_requested_models( CELLPHONE );
            chdir( @"Sound\BLIST\2A" );
            AUDIO_PL.load( 3 );
            wait( AUDIO_PL.is_ready, is_wav_loaded( 1 ) );
            player.create( ActorType.MISSION1, NULL, 2377.0789, -2168.2458, 12.5536 ).set_z_angle( 232.8597 );
            wait( 1000 );
            CAMERA.set_position( 2380.9229, -2170.7952, 13.5469 ).set_point_at( 2377.0789, -2168.2458, 13.5536, 2 );
            __fade( 1, false );
            Scene += delegate {
                play_wav( 1 );
                wait( 500 );
                player.task.hold_cellphone( true );

                wait( is_wav_ended( 1 ) );

                AUDIO_PL.play(); // 0
                show_text_highpriority( "@BL@66", 6000, 1 );
                wait( 6000 );

                AUDIO_PL.play(); // 1
                show_text_highpriority( "@BL@67", 6500, 1 );
                wait( 6500 );

                AUDIO_PL.play(); // 2
                show_text_highpriority( "@BL@68", 6000, 1 );
                wait( 6500 );

            };
            __fade( 0, true );
            __toggle_cinematic( false );
            destroy_model( ObjectModel.CELLPHONE );
            unload_wav( 1 );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
            wait( 1000 );
        }

        private void PLAY_CUTSCENE_1B( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var darius = cutcsene_actors[ 1 ];
            var vik = cutcsene_actors[ 2 ];
            var marcy = cutcsene_actors[ 3 ];
            var leicter = cutcsene_actors[ 4 ];
            var sonny = cutcsene_actors[ 5 ];
            var markus = cutcsene_actors[ 6 ];
            var crouch = cutcsene_actors[ 7 ];

            var briefcase = cutcsene_objects[ 0 ];

            Int[] usedModels = new Int[] { BRIEFCASE, WMYJG, VBMYCR, WFYST, VWMYCD, SWMYCR, BMYST, OMORI };

            __toggle_cinematic( true );
            clear_area( true, 1139.7396, 1930.3812, 10.8203, 300.0 );
            __renderer_at( 1139.7396, 1930.3812, 10.8203 );
            load_requested_models( usedModels );
            chdir( @"Sound\BLIST\1B" );
            AUDIO_PL.load( 5 );
            wait( AUDIO_PL.is_ready );
            player.create( ActorType.MISSION1, NULL, 1145.4137, 1925.9526, 9.8203 ).set_z_angle( 142.3289 );
            darius.create( ActorType.MISSION1, SWMYCR, 1141.6296, 1923.2518, 9.8203 ).set_z_angle( 251.9732 );
            vik.create( ActorType.MISSION1, VWMYCD, 1141.557, 1925.5273, 9.8203 ).set_z_angle( 225.3396 );
            marcy.create( ActorType.MISSION1, WFYST, 1143.2766, 1926.6119, 10.8203 ).set_z_angle( 184.3159 );
            leicter.create( ActorType.MISSION1, VBMYCR, 1145.8793, 1923.7914, 9.8203 ).set_z_angle( 129.7721 );
            sonny.create( ActorType.MISSION1, WMYJG, 1146.0676, 1921.1635, 9.8203 ).set_z_angle( 64.5981 );
            markus.create( ActorType.MISSION1, BMYST, 1141.5629, 1921.6583, 9.8203 ).set_z_angle( 277.0168 );
            crouch.create( ActorType.MISSION1, OMORI, 1143.5758, 1921.5675, 9.8203 ).set_z_angle( 2.3379 );
            briefcase.create( ObjectModel.BRIEFCASE, 0.0, 0.0, 0.0 );
            crouch.task.pick_up_object( briefcase, 0.0, 0.2, -0.25, 6, 270, "NULL", "NULL", 1 );
            wait( 1000 );
            CAMERA.set_position( 1144.7396, 1930.3812, 11.3203 ).set_point_at( 1141.0162, 1922.8914, 10.8203, 2 );
            __fade( 1, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                crouch.start_facial_talk( 6000 );
                show_text_highpriority( "@BL@69", 6000, 1 );
                wait( 6000 );
                crouch.stop_facial_talk();

                AUDIO_PL.play(); // 1
                crouch.start_facial_talk( 6500 );
                show_text_highpriority( "@BL@70", 6500, 1 );
                wait( 6500 );
                crouch.stop_facial_talk();

                CAMERA.set_position( 1142.318, 1923.061, 11.5203 ).set_point_at( 1141.6296, 1923.2518, 11.5203, 2 );

                AUDIO_PL.play(); // 2
                darius.start_facial_talk( 6000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@BL@71", 6000, 1 );
                wait( 6000 );
                darius.stop_facial_talk();

                CAMERA.set_position( 1143.1323, 1925.5757, 11.3203 ).set_point_at( 1143.2766, 1926.6119, 11.3203, 2 );

                AUDIO_PL.play(); // 3
                marcy.start_facial_talk( 6000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@BL@72", 6000, 1 );
                wait( 6000 );
                marcy.stop_facial_talk();

                CAMERA.set_position( 1142.318, 1923.061, 11.5203 ).set_point_at( 1141.6296, 1923.2518, 11.5203, 2 );

                AUDIO_PL.play(); // 4
                darius.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 3000 );
                show_text_highpriority( "@BL@73", 4500, 1 );
                wait( 3000 );
                darius.task.perform_animation( "FUCKU", "PED", 4.0, 0, 0, 0, 0, 1330 );
                wait( 1000 );
                darius.stop_facial_talk();

            };
            __fade( 0, true );
            __toggle_cinematic( false );
            destroy_model( usedModels );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        private void PLAY_CUTSCENE_1C( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var darius = cutcsene_actors[ 1 ];
            var crouch = cutcsene_actors[ 2 ];

            var infernus_car = cutcsene_cars[ 0 ];

            Int[] usedModels = new Int[] { INFERNUS, OMORI, SWMYCR };

            __toggle_cinematic( true );
            clear_area( true, -698.9442, 971.4367, 11.8547, 300.0 );
            __renderer_at( -698.9442, 971.4367, 11.8547 );
            load_requested_models( usedModels );
            chdir( @"Sound\BLIST\1C" );
            AUDIO_PL.load( 5 );
            wait( AUDIO_PL.is_ready );
            infernus_car.create( CarModel.INFERNUS, -698.9442, 971.4367, 11.8547 ).set_z_angle( 269.8259 ).set_colors( 1, 0 );
            player.create( ActorType.MISSION1, NULL, -700.2495, 952.2016, 11.3418 ).set_z_angle( 357.5675 ).set_walk_style( WalkStyle.PLAYER );
            darius.create( ActorType.MISSION1, SWMYCR, -700.0531, 969.4545, 11.3534 ).set_z_angle( 177.2776 ).task.rotate_to_actor( player ).look_at_actor( player, 9000000 );
            crouch.create( ActorType.MISSION1, OMORI, -698.5269, 969.4545, 12.3291 ).set_z_angle( 187.3768 ).task.rotate_to_actor( player ).look_at_actor( player, 9000000 );
            player.task.look_at_actor( darius, 9000000 ).walk_to_point( -699.6017, 963.2117, 12.3317, 0.0, 1.4 );
            CAMERA.attach_to_actor_look_at_actor( player, -2.0, -5.0, 1.5, darius, 0.0, 2 );
            wait( 1000 );
            __fade( 1, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                darius.start_facial_talk( 6000 ).task.perform_animation_secondary( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@BL@74", 6000, 1 );
                wait( 3000 );
                darius.task.walk_to_point( -699.7541, 964.8339, 12.3475, 180.0, 1.4 );
                wait( 3000 );
                darius.stop_facial_talk();

                wait( darius.is_stopped() );

                darius.task.rotate_to_actor( player );
                crouch.task.rotate_to_actor( player );
                player.task.rotate_to_actor( darius );
                CAMERA.set_position( -700.0531, 971.4545, 12.8291 ).set_point_at( -698.5269, 969.4545, 12.8291, 2 );

                AUDIO_PL.play(); // 1
                darius.start_facial_talk( 6000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@BL@75", 6000, 1 );
                wait( 3000 );
                darius.task.rotate_to_actor( crouch );
                wait( 3000 );
                darius.stop_facial_talk();

                CAMERA.set_position( -711.7541, 964.8339, 12.3475 ).set_point_at( -699.7541, 964.8339, 12.3475, 1 );

                wait( darius.is_stopped() );

                AUDIO_PL.play(); // 2
                player.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
                show_text_highpriority( "@BL@76", 4500, 1 );
                wait( 4500 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 3
                crouch.start_facial_talk( 3000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 3000 );
                show_text_highpriority( "@BL@77", 3000, 1 );
                darius.task.rotate_to_actor( player );
                wait( 3000 );
                crouch.stop_facial_talk();

                CAMERA.set_position( -698.7541, 962.8339, 13.3475 ).set_point_at( -699.7541, 964.8339, 12.3475, 2 );

                AUDIO_PL.play(); // 4
                player.start_facial_talk( 3000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 3000 );
                show_text_highpriority( "@BL@78", 3000, 1 );
                wait( 3000 );
                player.stop_facial_talk();

            };
            __fade( 0, true );
            __toggle_cinematic( false );
            destroy_model( usedModels );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        private void PLAY_CUTSCENE_1A( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var darius = cutcsene_actors[ 1 ];
            var crouch = cutcsene_actors[ 2 ];
            var john = cutcsene_actors[ 3 ];
            var marcy = cutcsene_actors[ 4 ];
            var heli_driver = cutcsene_actors[ 5 ];
            var cop2 = cutcsene_actors[ 6 ];
            var cop3 = cutcsene_actors[ 7 ];
            var cop4 = cutcsene_actors[ 8 ];
            var cop5 = cutcsene_actors[ 9 ];

            var john_police_car = cutcsene_cars[ 0 ];
            var infernus_car = cutcsene_cars[ 1 ];
            var turismo_car = cutcsene_cars[ 2 ];
            var cop_car1 = cutcsene_cars[ 3 ];
            var cop_car2 = cutcsene_cars[ 4 ];
            var polmav_car = cutscene_heli;

            Int[] usedModels = new Int[] { SWMYCR, OMORI, WFYST, COPCARVG, POLMAV, DSHER, SWAT, MP5LNG, COLT45, INFERNUS, TURISMO };

            __toggle_cinematic( true );
            clear_area( true, -1633.5066, 2727.2283, 56.8471, 300.0 );
            __renderer_at( -1633.5066, 2727.2283, 56.8471 );
            load_special_actor( "COPJOHN", 1 );
            load_animation( "CAR_CHAT" );
            load_requested_models( usedModels );
            chdir( @"Sound\BLIST\1A" );
            AUDIO_PL.load( 13 );
            wait( AUDIO_PL.is_ready, is_special_actor_loaded( 1 ) );
            infernus_car.create( CarModel.INFERNUS, -1636.8693, 2724.0312, 57.6151 ).set_z_angle( 337.9462 ).set_colors( 1, 0 );
            turismo_car.create( CarModel.TURISMO, -1633.5242, 2731.4536, 57.6345 ).set_z_angle( 241.1749 ).set_colors( 0, 0 );
            polmav_car.create( HeliModel.POLMAV, -1575.2455, 2572.6021, 90.9396 ).set_z_angle( 9.8093 ).enable_siren( 1 ).set_engine_broken( 1 ).set_blades_full_speed();
            cop_car1.create( CarModel.COPCARVG, -1622.1326, 2725.9338, 57.321 ).set_z_angle( 357.7494 ).enable_siren( 1 );
            cop_car2.create( CarModel.COPCARVG, -1642.6938, 2725.989, 58.0776 ).set_z_angle( 163.401 ).enable_siren( 1 );
            player.create( ActorType.MISSION1, NULL, -1633.5066, 2727.2283, 56.8471 ).set_z_angle( 187.426 );
            darius.create( ActorType.MISSION1, SWMYCR, -1630.6364, 2728.0066, 56.8074 ).set_z_angle( 171.7592 );
            crouch.create( ActorType.MISSION1, OMORI, -1626.8051, 2724.6445, 56.7327 ).set_z_angle( 74.3117 );
            john.create( ActorType.MISSION1, SPECIAL01, -1632.186, 2718.0403, 56.8131 ).set_z_angle( 357.5442 ).give_weapon( WeaponNumber.PISTOL, 30 ).set_armed_weapon( WeaponNumber.PISTOL );
            marcy.create( ActorType.MISSION1, WFYST, -1630.8829, 2717.9321, 56.8119 ).set_z_angle( 336.5506 ).give_weapon( WeaponNumber.PISTOL, 30 ).set_armed_weapon( WeaponNumber.PISTOL );
            heli_driver.create_in_vehicle_driverseat( ActorType.MISSION1, DSHER, polmav_car );
            polmav_car.fly_to( -1631.2179, 2702.0288, 74.0, 74.0, 74.0 );
            cop5.create_in_vehicle_driverseat( ActorType.MISSION1, DSHER, cop_car2 );
            cop2.create( ActorType.MISSION1, SWAT, -1624.6439, 2719.0083, 56.662 ).set_z_angle( 171.7592 ).give_weapon( WeaponNumber.MP5, 30 ).set_armed_weapon( WeaponNumber.MP5 );
            cop3.create( ActorType.MISSION1, SWAT, -1627.9687, 2717.3914, 56.8369 ).set_z_angle( 13.5243 ).give_weapon( WeaponNumber.MP5, 30 ).set_armed_weapon( WeaponNumber.MP5 );
            cop4.create( ActorType.MISSION1, SWAT, -1636.1093, 2719.2119, 57.9349 ).set_z_angle( 301.1437 ).give_weapon( WeaponNumber.MP5, 30 ).set_armed_weapon( WeaponNumber.MP5 );

            cutcsene_actors.each( loop_index, ped => {
                ped.set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 ).set_muted( true ).disable_speech( true ).shut_up_for_scripted_speech( true );
            } );

            wait( 100 );
            player.task.hands_up( 9999999 );
            darius.task.hands_up( 9999999 );
            crouch.task.hands_up( 9999999 );
            john.task.aim_at_actor( player, 9999999 );
            marcy.task.aim_at_actor( crouch, 9999999 );
            cop2.task.aim_at_actor( player, 9999999 );
            cop3.task.aim_at_actor( darius, 9999999 );
            cop4.task.aim_at_actor( crouch, 9999999 );
            CAMERA.attach_to_vehicle_look_at_actor( polmav_car, -2.5, -2.5, 0.0, player, 6.0, 2 );
            wait( 1000 );
            __fade( 1, false );
            Scene += delegate {

                wait( 1500 );

                AUDIO_PL.play(); // 0
                show_text_highpriority( "@BL@79", 8000, 1 );
                wait( 9500 );

                CAMERA.set_position( -1643.3318, 2732.0649, 58.8551 ).set_point_at( -1630.92, 2723.8879, 57.7919, 2 );

                AUDIO_PL.play(); // 1
                marcy.start_facial_talk( 7000 );
                show_text_highpriority( "@BL@80", 7000, 1 );
                wait( 7000 );
                marcy.stop_facial_talk();

                CAMERA.set_position( -1633.8379, 2716.8545, 57.7416 ).set_point_at( -1630.6364, 2728.0066, 57.8074, 2 );

                AUDIO_PL.play(); // 2
                darius.start_facial_talk( 7000 );
                show_text_highpriority( "@BL@81", 7000, 1 );
                wait( 7000 );
                darius.stop_facial_talk();

                CAMERA.set_position( -1643.3318, 2732.0649, 58.8551 ).set_point_at( -1630.92, 2723.8879, 57.7919, 2 );

                AUDIO_PL.play(); // 3
                marcy.start_facial_talk( 8000 );
                show_text_highpriority( "@BL@82", 8000, 1 );
                wait( 8000 );
                marcy.stop_facial_talk();

                AUDIO_PL.play(); // 4
                john.start_facial_talk( 6000 );
                show_text_highpriority( "@BL@83", 6000, 1 );
                wait( 6000 );
                john.stop_facial_talk();

            };
            __fade( 0, true );
            AUDIO_PL.play( -1 );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            clear_area( true, 16.6589, -2648.3911, 39.9647, 300.0 );
            __renderer_at( 16.6589, -2648.3911, 39.9647 );
            john_police_car.create( CarModel.COPCARVG, 16.6589, -2648.3911, 39.9647 ).set_z_angle( 183.4982 );
            marcy.create_in_vehicle_driverseat( ActorType.MISSION1, WFYST, john_police_car ).task.perform_animation( "CAR_Sc1_FR", "CAR_CHAT", 4.0, 1, 0, 0, 0, -1 );
            john.create_in_vehicle_passenger_seat( ActorType.MISSION1, SPECIAL01, john_police_car, 1 );
            player.create_in_vehicle_passenger_seat( ActorType.MISSION1, NULL, john_police_car, 2 );
            player.task.look_at_actor( john, 999999 );
            john.task.look_at_actor( player, 999999 );
            wait( 1000 );
            CAMERA.set_position( 14.7909, -2652.7837, 40.7785 ).set_point_at( 16.6589, -2648.3911, 40.1647, 2 );
            __fade( 1, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play( 5 ); // 5
                john.start_facial_talk( 6000 );
                show_text_highpriority( "@BL@84", 6000, 1 );
                wait( 6000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 6
                player.start_facial_talk( 2000 ).task.perform_animation( "CAR_Sc2_FL", "CAR_CHAT", 4.0, 0, 0, 0, 1, 5000 );
                show_text_highpriority( "@BL@85", 2000, 1 );
                wait( 2000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 7
                john.start_facial_talk( 6000 );
                show_text_highpriority( "@BL@86", 6000, 1 );
                wait( 6000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 8
                player.start_facial_talk( 5000 );
                show_text_highpriority( "@BL@87", 5000, 1 );
                wait( 5000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 9
                marcy.start_facial_talk( 5000 ).task.perform_animation( "CAR_Sc1_FL", "CAR_CHAT", 4.0, 0, 0, 0, 0, 5000 );
                show_text_highpriority( "@BL@88", 5000, 1 );
                wait( 5000 );
                marcy.stop_facial_talk().task.perform_animation( "CAR_Sc1_FR", "CAR_CHAT", 4.0, 1, 0, 0, 0, -1 );

                AUDIO_PL.play(); // 10
                player.start_facial_talk( 5000 );
                show_text_highpriority( "@BL@89", 5000, 1 );
                wait( 5000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 11
                john.start_facial_talk( 6000 ).task.perform_animation( "CAR_Sc1_BL", "CAR_CHAT", 4.0, 0, 0, 0, 0, 11000 );
                show_text_highpriority( "@BL@90", 6000, 1 );
                wait( 6000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 12
                player.start_facial_talk( 2000 );
                show_text_highpriority( "@BL@91", 2000, 1 );
                wait( 2000 );
                player.stop_facial_talk();

            };
            __fade( 0, true );
            __toggle_cinematic( false );
            destroy_model( usedModels );
            unload_special_actor( 1 );
            release_animation( "CAR_CHAT" );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITYS;
            __camera_default();
        }

        #endregion

        #region PASSED

        private void PASSED( LabelJump label ) {
            set_latest_mission_passed( BLACK_LIST_MISSION_NAME );
            BLACK_LIST_MISSION_STAGE.value = 0;
            BLACK_LIST_MISSION_PASSED += 1;
            and( BLACK_LIST_MISSION_PASSED > 6, jump_passed );
            jump_table( BLACK_LIST_MISSION_PASSED, table => {
                table.Auto += delegate { jump( table.EndLabel ); }; // 0
                table.Auto += delegate { jump( table.EndLabel ); }; // 1
                table.Auto += delegate { BLACK_LIST_MISSION_NAME.value = "@BLS@7"; jump( table.EndLabel ); }; // 5
                table.Auto += delegate { BLACK_LIST_MISSION_NAME.value = "@BLS@8"; jump( table.EndLabel ); }; // 4
                table.Auto += delegate { BLACK_LIST_MISSION_NAME.value = "@BLS@9"; jump( table.EndLabel ); }; // 3
                table.Auto += delegate { BLACK_LIST_MISSION_NAME.value = "@BLS@10"; jump( table.EndLabel ); }; // 2
                table.Auto += delegate { BLACK_LIST_MISSION_NAME.value = "@BLS@11"; jump( table.EndLabel ); }; // 1
            } );
            Jump += PASSED_END;
        }

        private void PASSED_CHASE( LabelJump label ) {
            set_latest_mission_passed( BLACK_LIST_MISSION_NAME );
            set_made_progress();
            BLACK_LIST_START_X.value = 28.3026;
            BLACK_LIST_START_Y.value = -2660.7744;
            BLACK_LIST_START_Z.value = 40.5403;
            BLACK_LIST_MISSION_NAME.value = "@BLS@6";
            BLACK_LIST_MISSION_STAGE.value = 0;
            BLACK_LIST_MISSION_PASSED += 1;
            create_thread<DISPLBL>();
            Jump += PASSED_END;
        }

        private void PASSED_QUALIFY( LabelJump label ) {
            play_audio_event_at_position( 0.0, 0.0, 0.0, 1058 );
            __fade( false, true );
            Gosub += MOVE_PLAYER;
            BLACK_LIST_MISSION_STAGE.set_bit( main_panel_activated_row );
            jump_table( BLACK_LIST_MISSION_PASSED, table => {
                table.Auto += delegate { jump( table.EndLabel ); }; // 0
                table.Auto += delegate { // 6
                    and( BLACK_LIST_MISSION_STAGE == 7, delegate { BLACK_LIST_MISSION_NAME.value = "@BLS@12"; } );
                    jump( table.EndLabel );
                };
                table.Auto += delegate { // 5
                    and( BLACK_LIST_MISSION_STAGE == 15, delegate { BLACK_LIST_MISSION_NAME.value = "@BLS@13"; } );
                    jump( table.EndLabel );
                };
                table.Auto += delegate { // 4
                    and( BLACK_LIST_MISSION_STAGE == 63, delegate { BLACK_LIST_MISSION_NAME.value = "@BLS@14"; } );
                    jump( table.EndLabel );
                };
                table.Auto += delegate { // 3
                    and( BLACK_LIST_MISSION_STAGE == 255, delegate { BLACK_LIST_MISSION_NAME.value = "@BLS@15"; } );
                    jump( table.EndLabel );
                };
                table.Auto += delegate { // 2
                    and( BLACK_LIST_MISSION_STAGE == 1023, delegate { BLACK_LIST_MISSION_NAME.value = "@BLS@16"; } );
                    jump( table.EndLabel );
                };
                table.Auto += delegate { // 1
                    and( BLACK_LIST_MISSION_STAGE == 4095, delegate { BLACK_LIST_MISSION_NAME.value = "@BLS@17"; } );
                    jump( table.EndLabel );
                };
            } );
            Jump += PASSED_END;
        }

        private void PASSED_END( LabelJump label ) {
            create_thread<BLSTART>();
            jump_passed();
        }

        private void ON_PASSED_DEFAULT() {
            cancel_override_restart();
            play_music( 1 );
            and( BLACK_LIST_MISSION_PASSED > 6, delegate {
                set_made_progress();
                p.add_money( PASSED_MONEY );
                show_text_1number_styled( "M_PASS", PASSED_MONEY, 5000, 1 );
            }, delegate {
                and( is_first_mission == 1, delegate {
                    show_text_styled( "M_PASSD", 5000, 1 );
                }, delegate {
                    show_text_styled( "RACES18", 5000, 1 );
                } );
            } );
        }

        #endregion

        #region FAIED

        private void FAILED_PUT_PLAYER( LabelJump label ) {
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            __set_entered_names( false );
            __disable_player_controll_in_cutscene( true );
            and( player_car.is_defined(), delegate {
                and( a.is_in_vehicle( player_car ), delegate {
                    a.set_cant_be_dragged_out( true );
                    player_car.set_door_status( 2 ).not_damaged_when_flipped( true );
                    and( player_car.is_burning(), delegate {
                        player_car.set_health( 300 );
                    } );
                } );
            } );
            __fade( false, true );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            Gosub += PLACE_PLAYER;
            Jump += FAILED_NOPUT_PLAYER;
        }

        private void FAILED_NOPUT_PLAYER( LabelJump label ) {
            jump_failed();
        }

        private void ON_FAILED_DEFAULT() {
            show_text_styled( "M_FAIL", 5000, 1 ); // ~r~–…CC…• ?PO‹A‡E­A!
            and( missionFailedText != sString.DUMMY, delegate {
                cancel_override_restart();
                show_text_lowpriority( missionFailedText, 6000, 1 );
            } );
            create_thread<BLSTART>();
        }

        #endregion

        #region CLEAR

        private void ON_CLEAR_DEFAULT() {
            release_weather();
            p.enable_group_recruitment( true ).clear_wanted_level();
            set_free_respray( false );
            set_text_boxes_width( 200 );
            set_sensitivity_to_crime( 1.0 );
            cutscene_as_pack.clear();
            MISSION_GLOBAL_STATUS_TEXT_1.remove();
            MISSION_GLOBAL_STATUS_TEXT_2.remove();
            race_checkpoint.disable();
            race_marker.disable_if_exist();
            enemy_marker.disable_if_exist();
            point4_checkpoints.each( loop_index, cp => {
                point4_race_checkpoints[ loop_index ].disable();
                cp.disable_if_exist();
            } );
            Gosub += CLEAR_PATH;
            Gosub += CLEAR_CUTSCENE_ENTITYS; // ! добавил, если игрок умрёт во время ролика
            MISSION_GLOBAL_TIMER_1.stop();
            player_car.destroy_if_exist();
            enemy_actor.destroy_if_exist();
            enemy_car.destroy_if_exist();
            AUDIO_BG.unload();
            wait( AUDIO_BG.is_stopped );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            __set_player_ignore( false );
            __set_traffic( 1.0 );
            __set_entered_names( true );
            __disable_player_controll_in_cutscene( false );
        }

        private void CLEAR_CUTSCENE_ENTITYS( LabelGosub label ) {
            cutcsene_objects.each( loop_index, o => { o.destroy_if_exist(); } );
            cutcsene_actors.each( loop_index, a => { a.destroy_if_exist(); } );
            cutcsene_cars.each( loop_index, v => { v.destroy_if_exist(); } );
            cutscene_heli.destroy_if_exist();
        }

        private void CLEAR_PATH( LabelGosub label ) {
            and( loaded_path != -1, delegate {
                and( is_path_available( loaded_path ), delegate { release_path( loaded_path ); } );
            } );
        }

        #endregion

    }

}