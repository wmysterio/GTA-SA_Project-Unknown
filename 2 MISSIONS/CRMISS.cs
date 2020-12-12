using GTA;

public partial class MAIN {

    public class CRMISS : Mission {

        void ___jump_failed_message( string gxt ) {
            failedMessage.value = gxt;
            jump_failed();
        }

        void ___load_path( ushort id ) {
            loaded_path.value = id;
            Gosub += LOAD_PATH;
        }

        // ---------------------------------------------------------------------------------------------------------------------------

        const ushort MAX_ARRAY_OF_ACTIVE_COUNT = 10, CUTSCENE_ENTITY_ARRAY_SIZE = 10, MAX_ARRAY_OF_REPLICAS = 20,
                     DELAY_BETWEEN_REPLICAS = 7000;

        // ---------------------------------------------------------------------------------------------------------------------------

        Int index, loaded_path, temp1, temp2, temp3, text_displayed, totalReplicasInDialog;
        Float tempX1, tempY1, tempZ1;
        sString failedMessage;
        DecisionMaker enemyDecisionMaker, friendDecisionMaker;
        Pickup helpWeapon;
        Car player_car;
        Checkpoint checkpoint;

        Array<Int> replicasPlayerTolking = MAX_ARRAY_OF_REPLICAS;
        Array<sString> replicasInDialog = MAX_ARRAY_OF_REPLICAS;
        Array<Actor> enemyActors = MAX_ARRAY_OF_ACTIVE_COUNT, friendActors = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Marker> enemyMarkers = MAX_ARRAY_OF_ACTIVE_COUNT, friendMarkers = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Car> enemyCars = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Object> targetObjects = MAX_ARRAY_OF_ACTIVE_COUNT;

        // 34 + 31 + 6*10 + 2 * 20 = 165 of 1023

        Array<Actor> cutcsene_actors = local_array( 0, CUTSCENE_ENTITY_ARRAY_SIZE );
        Array<Car> cutcsene_cars = local_array( 10, CUTSCENE_ENTITY_ARRAY_SIZE );
        Array<Object> cutcsene_objects = local_array( 20, CUTSCENE_ENTITY_ARRAY_SIZE );

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            wait( 1000 );
            enable_train_traffic( false );
            destroy_all_trains();
            failedMessage.value = sString.DUMMY;
            loaded_path.value = -1;
            p.enable_group_recruitment( false );
            g.release();
            __set_entered_names( false );
            __set_traffic( 0.0 );
            jump_table( CRASH_TOTAL_MISSION_PASSED, table => {
                table.Auto += MISSION_0;
                table.Auto += MISSION_1;
                table.Auto += MISSION_2;
                table.Auto += MISSION_3;
                table.Auto += MISSION_4;
                table.Auto += MISSION_5;
                table.Auto += MISSION_6;
                table.Auto += MISSION_7;
                table.Auto += MISSION_8;
            } );

            OnPassed = DEFAULT_PASSED;
            OnFailed = DEFAULT_FAILED;
            OnClear = DEFAULT_CLEAR;
        }

        #region MISSIONS

        #region Mission 0
        private void MISSION_0( LabelCase l ) {

            Int[] usedModels = { TAMPA, COPBIKE, LAPDM1, COLT45 };

            __set_police_generator( false );
            chdir( @"Sound\CRMISS" );
            AUDIO_BG.load( 999999, true );
            wait( AUDIO_BG.is_ready );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME ).play();

            Gosub += SCENE_0B;

            load_requested_models( usedModels );
            clear_area( true, 1607.5181, -1697.6582, 12.5469, 300.0 );
            __renderer_at( 1607.5181, -1697.6582, 12.5469 );
            a.set_position( 1607.5181, -1697.6582, 12.5469 ).set_z_angle( 180.4397 );
            player_car.create( TAMPA, 1607.8346, -1709.9978, 13.1263 ).set_z_angle( 180.0 ).set_colors( 1, 1 ).remove_references();
            helpWeapon.create_if_need( WeaponNumber.PISTOL, WeaponModel.COLT45, 75, 1611.0355, -1711.2484, 13.5469, temp1 );
            set_radio_station( RadioStation.OFF );
            set_current_time( 23, 58 );
            clear_area( true, 1024.6075, -2069.3616, 13.1615, 3.0 );
            clear_area( true, 1803.9709, -1716.6636, 13.5704, 3.0 );
            clear_area( true, 1383.4022, -1018.8989, 26.5571, 3.0 );
            clear_area( true, 2704.7639, -1276.3545, 57.9168, 3.0 );
            enemyCars[ 0 ].create( COPBIKE, 1024.6075, -2069.3616, 13.1615 ).set_z_angle( 255.563 );
            enemyCars[ 1 ].create( COPBIKE, 1803.9709, -1716.6636, 13.5704 ).set_z_angle( 178.5211 );
            enemyCars[ 2 ].create( COPBIKE, 1383.4022, -1018.8989, 26.5571 ).set_z_angle( 192.9562 );
            enemyCars[ 3 ].create( COPBIKE, 2704.7639, -1276.3545, 57.9168 ).set_z_angle( 312.4114 );
            to( index, 0, 3, delegate {
                enemyActors[ index ].create_in_vehicle_driverseat( ActorType.MISSION1, LAPDM1, enemyCars[ index ] )
                                    .set_health( 2 )
                                    .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                                    .set_cant_be_dragged_out( true );
                enemyMarkers[ index ].create_above_actor( enemyActors[ index ] ).set_type( false );
            } );
            destroy_model( usedModels );
            __camera_default();
            __set_entered_names( true );
            wait( 1000 );
            __set_player_ignore( false );
            set_ped_traffic_density_multiplier( 0.8 );
            set_vehicle_traffic_density_multiplier( 0.7 );
            __disable_player_controll_in_cutscene( false );
            AUDIO_BG.set_volume( 1.0 );
            p.clear_wanted_level();
            __fade( true );
            MISSION_GLOBAL_TIMER_1.value = 361000; // 1000ms + 1000ms * 60s * 6m
            MISSION_GLOBAL_TIMER_1.start( TimerType.DOWN, "BB_19" );
            show_text_highpriority( "@CRS@09", 6000, 1 );
            Cycle += delegate {
                wait( 0 );
                temp1.value = 0;
                and( 0 >= MISSION_GLOBAL_TIMER_1, delegate { ___jump_failed_message( "@CRS@11" ); } );
                to( index, 0, 3, delegate {
                    and( !enemyCars[ index ].is_wrecked(), !enemyCars[ index ].is_in_water(), delegate {
                        enemyCars[ index ].store_coords( tempX1, tempY1, tempZ1, 0.0, 4.75, 0.0 );
                        //draw_sphere( tempX1, tempY1, tempZ1, 5.0 ); // DEBUG
                        and( a.is_near_point_3d( false, tempX1, tempY1, tempZ1, 5.0, 5.0, 5.0 ), delegate {
                            ___jump_failed_message( "@CRS@10" );
                        } );
                    } );
                    enemyActors[ index ].do_if_dead( delegate {
                        temp1 += 1;
                        enemyMarkers[ index ].disable_if_exist();
                    } );
                    and( temp1 == 4, jump_passed );
                } );
            };
        }
        #endregion

        #region Mission 1
        private void MISSION_1( LabelCase l ) {

            Int[] usedModels = { TAMPA };

            chdir( @"Sound\CRMISS" );
            AUDIO_BG.load( 999998, true );
            wait( AUDIO_BG.is_ready );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME ).play();

            Gosub += SCENE_1B;

            load_special_actor( "1_REMAX", 1 );
            load_requested_models( usedModels );

            clear_area( true, 1588.3674, -1717.3674, 13.0984, 300.0 );
            __renderer_at( 1607.5181, -1697.6582, 12.5469 );
            a.set_position( 1607.5181, -1697.6582, 12.5469 ).set_z_angle( 180.0 ).set_muted( true );

            player_car.create( TAMPA, 1588.3674, -1717.3674, 13.0984 ).set_z_angle( 90.0 ).set_colors( 1, 1 );

            friendActors[ 0 ].create_in_vehicle_passenger_seat( ActorType.MISSION1, SPECIAL01, player_car, 0 )
                             .set_max_health( 2000 )
                             .set_health( 2000 )
                             .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                             .set_cant_be_dragged_out( true );

            destroy_model( usedModels );
            unload_special_actor( 1 );
            __camera_default();
            __set_entered_names( true );
            chdir( @"Sound\CRMISS\01" );
            AUDIO_PL.load( 12 );
            wait( AUDIO_PL.is_ready );
            wait( 1000 );
            __set_player_ignore( false );
            __set_traffic( 1.0 );
            __disable_player_controll_in_cutscene( false );
            p.clear_wanted_level();
            __fade( true );
            index.value = 0;
            Gosub += M1_SETUP_DIALOG;
            Jump += M1_ENTER_REMAX_CAR;
        }

        private void M1_ENTER_REMAX_CAR( LabelJump label ) {
            checkpoint.disable();
            friendMarkers[ 0 ].create_above_vehicle( player_car ).set_type( true );
            show_text_highpriority( "@CRS@12", 4000, 1 );
            Cycle += delegate {
                wait( 0 );
                player_car.do_if_wrecked( delegate { ___jump_failed_message( "@CRS@14" ); } );
                and( !friendActors[ 0 ].is_in_vehicle( player_car ), delegate { friendActors[ 0 ].task.die(); } );
                friendActors[ 0 ].do_if_dead( delegate { ___jump_failed_message( "@CRS@15" ); } );
                and(
                    a.is_in_vehicle( player_car ),
                    friendActors[ 0 ].is_in_vehicle( player_car )
                , delegate {
                    Jump += M1_GOTO_REMAX_HOME;
                } );
            };
        }

        private void M1_GOTO_REMAX_HOME( LabelJump label ) {
            friendMarkers[ 0 ].disable();
            checkpoint.create( 259.063, -272.0966, 1.5836 );
            and( text_displayed != true, delegate {
                show_text_highpriority( "@CRS@13", 5500, 1 );
                text_displayed.value = true;
                LocalTimer1.value = 0;
            } );
            Cycle += delegate {
                wait( 0 );
                and( totalReplicasInDialog > index, delegate {
                    and( LocalTimer1 > DELAY_BETWEEN_REPLICAS, delegate {
                        a.stop_facial_talk();
                        and( replicasPlayerTolking[ index ] == true, delegate { a.start_facial_talk( 6000 ); } );
                        show_text_highpriority( replicasInDialog[ index ], 6000, 1 );
                        AUDIO_PL.play();
                        index += 1;
                        LocalTimer1.value = 0;
                    } );
                } );
                and( index == totalReplicasInDialog, delegate {
                    and( LocalTimer1 > DELAY_BETWEEN_REPLICAS, delegate {
                        a.stop_facial_talk();
                        AUDIO_BG.set_volume( 1.0 );
                        index += 1;
                    } );
                } );
                player_car.do_if_wrecked( delegate { ___jump_failed_message( "@CRS@14" ); } );
                and( !friendActors[ 0 ].is_in_vehicle( player_car ), delegate { friendActors[ 0 ].task.die(); } );
                friendActors[ 0 ].do_if_dead( delegate { ___jump_failed_message( "@CRS@15" ); } );
                and( !a.is_in_vehicle( player_car ), delegate { Jump += M1_ENTER_REMAX_CAR; } );
                and(
                    a.is_near_point_3d_stopped_in_vehicle( 1, 259.063, -272.0966, 1.5836, 6.0, 6.0, 6.0 ),
                    friendActors[ 0 ].is_in_vehicle( player_car )
                , delegate { Jump += M1_END; } );
            };
        }

        private void M1_END( LabelJump label ) {
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            __disable_player_controll_in_cutscene( true );
            player_car.extinguish();
            __fade( 0, true );
            a.stop_facial_talk().teleport_without_car( 246.8233, -267.1011, 1.5781, 90.0 );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME );
            AUDIO_PL.play( -1 );
            __camera_default();
            wait( 1000 );
            fade( true, 500 );
            __disable_player_controll_in_cutscene( false );
            wait( 500 );
            jump_passed();
        }
        #endregion

        #region Mission 2
        private void MISSION_2( LabelCase l ) {

            Int[] usedModels = { BRAVURA };

            chdir( @"Sound\CRMISS" );
            AUDIO_BG.load( 999997, true );
            wait( AUDIO_BG.is_ready );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME ).play();
            Gosub += SCENE_2B;
            load_requested_models( usedModels );
            clear_area( true, 1607.5181, -1697.6582, 12.5469, 300.0 );
            __renderer_at( 1607.5181, -1697.6582, 12.5469 );
            a.set_position( 1607.5181, -1697.6582, 12.5469 ).set_z_angle( 180.4397 );
            clear_area( true, 747.9165, -1439.2003, 13.1177, 5.0 );
            player_car.create( BRAVURA, 747.9165, -1439.2003, 13.1177 )
                      .set_z_angle( 223.1663 )
                      .set_colors( 3, 3 )
                      .damage_door( DoorNumber.FRONT_LEFT_DOOR )
                      .deflate_tire( 2 )
                      .set_health( 500 );
            destroy_model( usedModels );
            __camera_default();
            __set_entered_names( true );
            wait( 1000 );
            __set_player_ignore( false );
            __disable_player_controll_in_cutscene( false );
            __set_traffic( 1.0 );
            AUDIO_BG.set_volume( 1.0 );
            p.clear_wanted_level();
            __fade( true );
            p.get_money( temp1 );
            and( 1000 > temp1, delegate {
                temp1.sub( 1000, temp1 );
                p.add_money( temp1 );
            } );
            Jump += M2_ENTER_A_CAR;
        }

        private void M2_ENTER_A_CAR( LabelJump label ) {
            checkpoint.disable();
            friendMarkers[ 0 ].create_above_vehicle( player_car ).set_type( true );
            show_text_highpriority( "@CRS@12", 6000, 1 );
            Cycle += delegate {
                wait( 0 );
                __checkProsecutorCarAndOther();
                and( a.is_in_vehicle( player_car ), delegate { Jump += M2_GOTO_8BOMB; } );
            };
        }

        private void M2_GOTO_8BOMB( LabelJump label ) {
            friendMarkers[ 0 ].disable();
            checkpoint.create( 1847.918, -1855.8789, 13.0386 );
            show_text_highpriority( "@CRS@16", 6000, 1 );
            Cycle += delegate {
                wait( 0 );
                __checkProsecutorCarAndOther();
                and( !a.is_in_vehicle( player_car ), delegate { Jump += M2_ENTER_A_CAR; } );
                and(
                    p.is_money_greater( 499 ),
                    Garage.is_closed( GarageName.LASBOMB ),
                    a.is_near_point_3d_stopped_in_vehicle( 1, 1847.918, -1855.8789, 13.0386, 3.0, 3.0, 3.0 ),
                    a.is_in_vehicle( player_car )
                , delegate {
                    checkpoint.disable();
                    LocalTimer1.value = 0;
                    Jump += WAIT_ATTACH_BOMB;
                } );
            };
        }

        private void WAIT_ATTACH_BOMB( LabelJump label ) {
            wait( 0 );
            __checkProsecutorCarAndOther();
            and( LocalTimer1 > 3000, delegate { Jump += M2_GOTO_PROKUROR_HOME; } );
            jump( WAIT_ATTACH_BOMB );
        }

        private void M2_ENTER_A_CAR2( LabelJump label ) {
            checkpoint.disable();
            friendMarkers[ 0 ].create_above_vehicle( player_car ).set_type( true );
            show_text_highpriority( "@CRS@12", 6000, 1 );
            Cycle += delegate {
                wait( 0 );
                __checkProsecutorCarAndOther();
                and( a.is_in_vehicle( player_car ), delegate { Jump += M2_GOTO_PROKUROR_HOME; } );
            };
        }

        private void M2_GOTO_PROKUROR_HOME( LabelJump label ) {
            friendMarkers[ 0 ].disable();
            checkpoint.create( 1106.9691, -732.1011, 100.4166 );
            show_text_highpriority( "@CRS@18", 6000, 1 );
            Cycle += delegate {
                wait( 0 );
                __checkProsecutorCarAndOther();
                and( !a.is_in_vehicle( player_car ), delegate { Jump += M2_ENTER_A_CAR2; } );
                and(
                    a.is_near_point_3d_stopped_in_vehicle( 1, 1106.9691, -732.1011, 100.4166, 2.0, 2.0, 2.0 ),
                    a.is_in_vehicle( player_car )
                , delegate {
                    Jump += M2_END;
                } );
            };
        }

        private void M2_END( LabelJump label ) {
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            __disable_player_controll_in_cutscene( true );
            __fade( 0, true );
            a.stop_facial_talk().teleport_without_car( 1089.8102, -735.4687, 102.9154, 139.6051 );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME );
            __camera_default();
            wait( 1000 );
            fade( true, 500 );
            __disable_player_controll_in_cutscene( false );
            wait( 500 );
            jump_passed();
        }

        private void __checkProsecutorCarAndOther() {
            and( p.is_wanted_level_greater( 0 ), delegate { ___jump_failed_message( "@CRS@19" ); } );
            player_car.do_if_wrecked( delegate { ___jump_failed_message( "@CRS@14" ); } );
            and( player_car.is_burning(), delegate { ___jump_failed_message( "@CRS@20" ); } );
            player_car.get_health( temp1 ).get_colors( temp2, temp3 );
            or(
                !player_car.is_tire_deflated( 2 ),
                !player_car.is_door_damaged( DoorNumber.FRONT_LEFT_DOOR ),
                temp2 != 3, temp3 != 3, temp1 > 500
            , delegate { ___jump_failed_message( "@CRS@17" ); } );
            and( !a.is_in_area_3d( 0, -20.0, -3000.0, -3000.0, 3000.0, -673.0, 3000.0 ), delegate {
                ___jump_failed_message( "@CRS@21" );
            } );
        }
        #endregion

        #region Mission 3
        private void MISSION_3( LabelCase l ) {

            Int[] usedModels = { SNIPER, LAPDM1, LVPD1, SFPD1 };

            chdir( @"Sound\CRMISS" );
            AUDIO_BG.load( 999996, true );
            wait( AUDIO_BG.is_ready );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME ).play();
            Gosub += SCENE_3B;
            load_requested_models( usedModels );
            clear_area( true, 1607.5181, -1697.6582, 12.5469, 300.0 );
            __renderer_at( 1607.5181, -1697.6582, 12.5469 );
            a.set_position( 1607.5181, -1697.6582, 12.5469 ).set_z_angle( 180.4397 );
            clear_area( true, 1725.6798, -1099.2974, 46.5746, 30.0 );
            enemyActors[ 0 ].create( ActorType.MISSION1, LAPDM1, 1707.0894, -1099.238, 42.5746 );
            enemyActors[ 1 ].create( ActorType.MISSION1, LVPD1, 1710.8187, -1099.4419, 42.5746 );
            enemyActors[ 2 ].create( ActorType.MISSION1, LAPDM1, 1716.9115, -1099.559, 42.5746 );
            enemyActors[ 3 ].create( ActorType.MISSION1, SFPD1, 1725.6798, -1099.2974, 42.5746 );
            to( index, 0, 2, h => {
                enemyActors[ index ].set_immunities( true )
                                    .set_visible( false )
                                    .set_untargetable( true )
                                    .lock_position( true )
                                    .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                                    .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 );
            } );
            helpWeapon.create_if_need( WeaponNumber.SNIPERRIFLE, WeaponModel.SNIPER, 10, 1588.5155, -992.762, 38.5221, temp1 );
            destroy_model( usedModels );
            __camera_default();
            __set_entered_names( true );
            wait( 1000 );
            __set_player_ignore( false );
            __disable_player_controll_in_cutscene( false );
            __set_traffic( 1.0 );
            AUDIO_BG.set_volume( 1.0 );
            __fade( true );
            Jump += M3_WAIT_WHEN_CJ_ON_BRIDGE;
        }

        private void M3_WAIT_WHEN_CJ_ON_BRIDGE( LabelJump label ) {
            checkpoint.create( 1583.4355, -997.7007, 38.4145 );
            show_text_highpriority( "@CRS@22", 6000, 1 );
            Cycle += delegate {
                wait( 0 );
                and(
                    a.is_near_point_3d_on_foot( 1, 1583.4355, -997.7007, 38.4145, 1.0, 1.0, 1.0 ),
                    a.is_has_weapon( WeaponNumber.SNIPERRIFLE )
                , delegate {
                    Jump += SHOW_SCENE_SELECT_TARGET;
                } );
            };
        }

        private void SHOW_SCENE_SELECT_TARGET( LabelJump label ) {
            checkpoint.disable();
            __disable_player_controll_in_cutscene( true );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME );
            __set_player_ignore( true );
            __fade( false, true );
            __set_traffic( 0.0 );
            Gosub += SCENE_3C;
            __disable_player_controll_in_cutscene( false );
            __clear_text();
            MISSION_GLOBAL_TIMER_1.value = 13000; // 13 * 1000
            MISSION_GLOBAL_TIMER_1.start( TimerType.DOWN, "BB_19" );
            wait( 1000 );
            __camera_default();
            __fade( true );
            AUDIO_BG.set_volume( 1.0 );
            Jump += WAIT_KILL_TARGET;
        }

        private void WAIT_KILL_TARGET( LabelJump label ) {
            enemyMarkers[ 0 ].create_above_actor( enemyActors[ 0 ] ).set_radar_mode( 1 );
            show_text_highpriority( "@CRS@23", 5000, 1 );
            Cycle += delegate {
                wait( 0 );
                and( 0 >= MISSION_GLOBAL_TIMER_1, delegate { ___jump_failed_message( "@CRS@25" ); } );
                and( !a.is_near_point_3d( 0, 1583.4355, -997.7007, 38.4145, 7.0, 7.0, 7.0 ), delegate { ___jump_failed_message( "@CRS@27" ); } );
                to( index, 0, 2, h => {
                    and( enemyActors[ index ].is_dead(), delegate {
                        and( index == 0, delegate {
                            a.get_current_weapon( temp1 );
                            get_weapon_model( temp1, temp1 );
                            and( temp1 != SNIPER, delegate { ___jump_failed_message( "@CRS@28" ); } );
                            jump_passed();
                        }, delegate { ___jump_failed_message( "@CRS@26" ); } );
                    } );
                } );
            };
        }
        #endregion




        private void MISSION_4( LabelCase l ) {
            jump_passed();
        }
        private void MISSION_5( LabelCase l ) {
            jump_passed();
        }
        private void MISSION_6( LabelCase l ) {
            jump_passed();
        }
        private void MISSION_7( LabelCase l ) {
            jump_passed();
        }
        private void MISSION_8( LabelCase l ) {
            jump_passed();
        }
        #endregion

        #region CUTSCENES
        private void SCENE_0B( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var john = cutcsene_actors[ 1 ];

            __toggle_cinematic( true );
            a.set_position( 1600.9196, -1688.0894, 19.8792 );
            wait( 1000 );

            set_current_time( 18, 0 );
            set_weather( WeatherID.EXTRASUNNY_LA );
            force_weather( WeatherID.EXTRASUNNY_LA );
            clear_area( false, 1600.9226, -1661.1029, 18.8792, 300.0 );
            __renderer_at( 1600.9226, -1661.1029, 18.8792 );
            load_special_actor( "COPJOHN", 1 );
            load_requested_models();
            chdir( @"Sound\CRMISS\0B" );
            AUDIO_PL.load( 5 );
            wait( is_special_actor_loaded( 1 ), AUDIO_PL.is_ready );
            player.create( ActorType.MISSION1, NULL, 1605.5203, -1651.3384, 12.5469 ).set_z_angle( 180.0 );
            john.create( ActorType.MISSION1, SPECIAL01, 1605.5203, -1652.3384, 12.5469 ).set_z_angle( 0.0 );
            unload_special_actor( 1 );
            CAMERA.set_position( 1607.0896, -1652.124, 13.9469 );
            CAMERA.set_point_at( 1605.5203, -1651.8384, 13.9469, 2 );
            wait( 1000 );
            __fade( true, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                john.start_facial_talk( 21000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 21000 );
                show_text_highpriority( "@CR@000", 8000, 1 );
                wait( 8000 );

                AUDIO_PL.play(); // 1
                show_text_highpriority( "@CR@001", 7000, 1 );
                wait( 7000 );

                AUDIO_PL.play(); // 2
                show_text_highpriority( "@CR@002", 6000, 1 );
                wait( 6000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 3
                player.start_facial_talk( 5000 ).task.perform_animation( "endchat_01", "PED", 4.0, 0, 0, 0, 0, 2000 );
                show_text_highpriority( "@CR@003", 5000, 1 );
                wait( 5000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 4
                john.start_facial_talk( 7000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 7000 );
                show_text_highpriority( "@CR@004", 7000, 1 );
                wait( 7000 );
                john.stop_facial_talk();

                wait( 1000 );
            };
            __fade( false, true );
            __toggle_cinematic( false );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_1B( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var john = cutcsene_actors[ 1 ];

            __toggle_cinematic( true );
            a.set_position( 1600.9196, -1688.0894, 19.8792 );
            wait( 1000 );
            clear_area( false, 1600.9226, -1661.1029, 18.8792, 300.0 );
            __renderer_at( 1600.9226, -1661.1029, 18.8792 );
            load_special_actor( "COPJOHN", 1 );
            load_requested_models();
            chdir( @"Sound\CRMISS\1B" );
            AUDIO_PL.load( 5 );
            wait( is_special_actor_loaded( 1 ), AUDIO_PL.is_ready );
            player.create( ActorType.MISSION1, NULL, 1605.5203, -1651.3384, 12.5469 ).set_z_angle( 180.0 );
            john.create( ActorType.MISSION1, SPECIAL01, 1605.5203, -1652.3384, 12.5469 ).set_z_angle( 0.0 );
            unload_special_actor( 1 );
            CAMERA.set_position( 1607.0896, -1652.124, 13.9469 );
            CAMERA.set_point_at( 1605.5203, -1651.8384, 13.9469, 2 );
            wait( 1000 );
            __fade( true, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                john.start_facial_talk( 6000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@CR@017", 6000, 1 );
                wait( 6000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 1
                player.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@CR@018", 5000, 1 );
                wait( 5000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 2
                john.start_facial_talk( 5500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 5500 );
                show_text_highpriority( "@CR@019", 5500, 1 );
                wait( 5500 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 3
                player.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@CR@020", 5000, 1 );
                wait( 5000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 4
                john.start_facial_talk( 6000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@CR@021", 6000, 1 );
                wait( 6000 );
                john.stop_facial_talk();

                wait( 1000 );
            };
            __fade( false, true );
            __toggle_cinematic( false );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_2B( LabelGosub label ) {
            //
            __toggle_cinematic( true );
            wait( 1000 );
            //clear_area( 1, 1177.72, -1852.2987, 12.3984, 300.0 );
            __fade( true, false );
            Scene += delegate {
                wait( 5000 );
                Comment = "...";
            };
            __fade( false, true );
            __toggle_cinematic( false );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_3B( LabelGosub label ) {
            //
            __toggle_cinematic( true );
            wait( 1000 );
            //clear_area( 1, 1177.72, -1852.2987, 12.3984, 300.0 );
            __fade( true, false );
            Scene += delegate {
                wait( 5000 );
                Comment = "...";
            };
            __fade( false, true );
            __toggle_cinematic( false );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_3C( LabelGosub label ) {
            __toggle_cinematic( true );
            wait( 500 );
            set_current_time( 23, 0 );
            clear_area( 1, 1715.5588, -1081.9011, 22.9062, 7.5 );
            enemyActors[ 0 ].lock_position( false ).put_at( 1680.3716, -1066.6653, 22.9509, 27.204 );
            enemyActors[ 1 ].lock_position( false ).put_at( 1678.8251, -1065.3247, 22.8984, 229.0908 );
            enemyActors[ 2 ].lock_position( false ).put_at( 1681.5151, -1065.0446, 22.9085, 163.0236 );
            enemyActors[ 3 ].lock_position( false ).put_at( 1677.688, -1066.5552, 22.8984, 257.6511 );
            enemyActors[ 0 ].task.rotate_to_actor( enemyActors[ 1 ] );
            enemyActors[ 1 ].task.rotate_to_actor( enemyActors[ 0 ] );
            enemyActors[ 2 ].task.rotate_to_actor( enemyActors[ 0 ] );
            enemyActors[ 3 ].task.rotate_to_actor( enemyActors[ 0 ] );
            __renderer_at( 1673.8917, -1063.4092, 23.8984 );
            CAMERA.set_position( 1673.8917, -1063.4092, 23.8984 ).set_point_at( 1680.3716, -1066.6653, 23.9509, 2 );
            wait( 750 );
            to( index, 0, 2, h => {
                enemyActors[ index ].set_immunities( false )
                                    .set_visible( true )
                                    .set_untargetable( false )
                                    .set_health( 2 );
            } );
            __fade( true, false );
            Scene += delegate {
                wait( 1000 );
                show_text_highpriority( "@CRS@24", 6000, 1 );
                wait( 6000 );
            };
            __fade( false, true );
            __toggle_cinematic( false );
            __clear_text();
        }
        #endregion

        #region REPLICAS
        private void M1_SETUP_DIALOG( LabelGosub label ) {
            replicasInDialog[ 0 ].value = "@CR@005";
            replicasInDialog[ 1 ].value = "@CR@006";
            replicasInDialog[ 2 ].value = "@CR@007";
            replicasInDialog[ 3 ].value = "@CR@008";
            replicasInDialog[ 4 ].value = "@CR@009";
            replicasInDialog[ 5 ].value = "@CR@010";
            replicasInDialog[ 6 ].value = "@CR@011";
            replicasInDialog[ 7 ].value = "@CR@012";
            replicasInDialog[ 8 ].value = "@CR@013";
            replicasInDialog[ 9 ].value = "@CR@014";
            replicasInDialog[ 10 ].value = "@CR@015";
            replicasInDialog[ 11 ].value = "@CR@016";
            replicasPlayerTolking[ 0 ].value = true;
            replicasPlayerTolking[ 1 ].value = false;
            replicasPlayerTolking[ 2 ].value = false;
            replicasPlayerTolking[ 3 ].value = false;
            replicasPlayerTolking[ 4 ].value = false;
            replicasPlayerTolking[ 5 ].value = true;
            replicasPlayerTolking[ 6 ].value = false;
            replicasPlayerTolking[ 7 ].value = true;
            replicasPlayerTolking[ 8 ].value = false;
            replicasPlayerTolking[ 9 ].value = true;
            replicasPlayerTolking[ 10 ].value = false;
            replicasPlayerTolking[ 11 ].value = true;
            totalReplicasInDialog.value = 12;
        }
        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------

        private void LOAD_PATH( LabelGosub label ) {
            load_path( loaded_path );
            wait( is_path_available( loaded_path ) );
        }

        #region OnPassed
        private void DEFAULT_PASSED() {
            show_text_styled( sString.M_PASSD, 5000, 1 );
            play_music( MusicID.MISSION_PASSED );
            set_latest_mission_passed( CURRENT_MISSION_NAME );
            CRASH_TOTAL_MISSION_PASSED += 1;
            set_made_progress();
            and( CRASH_TOTAL_MISSION_PASSED == 2, delegate {
                create_thread<REMAXST>();
            } );
            and( CRASH_TOTAL_MISSION_PASSED == 4, delegate {
                CRASH_START_X.value = 2244.8999;
                CRASH_START_Y.value = 2558.6262;
                CRASH_START_Z.value = 10.8193;
                create_thread<BLSTART>();
                @return();
            } );
            and( 9 > CRASH_TOTAL_MISSION_PASSED, delegate {
                create_thread<CRSTART>();
            } );
        }
        #endregion

        #region OnFailed
        private void DEFAULT_FAILED() {
            show_text_styled( sString.M_FAIL, 5000, 1 );
            and( failedMessage != sString.DUMMY, delegate { show_text_lowpriority( failedMessage, 6000, 1 ); } );
            create_thread<CRSTART>();
        }
        #endregion

        #region OnClear
        private void DEFAULT_CLEAR() {
            __set_entered_names( true );
            __set_traffic( 1.0 );
            __set_player_ignore( false );
            __set_police_generator( true );
            set_sensitivity_to_crime( 1.0 );
            p.enable_group_recruitment( true );
            a.set_muted( false );
            enable_train_traffic( true );
            g.release();
            Gosub += CLEAR_ACTIVE_ENTITIES;
            Gosub += CLEAR_CUTSCENE_ENTITIES;
            Gosub += CLEAR_PATH;
            AUDIO_BG.unload();
            wait( AUDIO_BG.is_stopped );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
        }

        private void CLEAR_ACTIVE_ENTITIES( LabelGosub label ) {
            MISSION_GLOBAL_TIMER_1.stop();
            checkpoint.disable_if_exist();
            friendDecisionMaker.release();
            enemyDecisionMaker.release();
            helpWeapon.destroy_if_exist();
            enemyMarkers.each( index, m => {
                enemyMarkers[ index ].disable_if_exist();
                friendMarkers[ index ].disable_if_exist();
                targetObjects[ index ].destroy_if_exist();
                enemyActors[ index ].destroy_if_exist();
                friendActors[ index ].destroy_if_exist();
                enemyCars[ index ].destroy_if_exist();
            } );
            player_car.destroy_if_exist();
        }

        private void CLEAR_CUTSCENE_ENTITIES( LabelGosub label ) {
            cutcsene_objects.each( index, o => { o.destroy_if_exist(); } );
            cutcsene_actors.each( index, a => { a.destroy_if_exist(); } );
            cutcsene_cars.each( index, v => { v.destroy_if_exist(); } );
        }

        private void CLEAR_PATH( LabelGosub label ) {
            and( loaded_path != -1, delegate { release_path( loaded_path ); } );
            loaded_path.value = -1;
        }
        #endregion

    }

}