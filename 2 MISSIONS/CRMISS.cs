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

        const ushort MAX_ARRAY_OF_ACTIVE_COUNT = 10, CUTSCENE_ENTITY_ARRAY_SIZE = 10, MAX_ARRAY_OF_REPLICAS = 23;

        private TrainType M6_TRAIN_TYPE = TrainType.FREIGHT_FREIFLAT1;

        // ---------------------------------------------------------------------------------------------------------------------------

        Int index, index2, loaded_path, temp1, temp2, temp3, text_displayed, totalReplicasInDialog, reward, is_m6_mission,
            melPath, johnPath;
        Float tempX1, tempY1, tempZ1, tempX2, tempY2, tempZ2, tempAngle, tempSpeed;
        sString failedMessage, tempHash;
        DecisionMaker enemyDecisionMaker, friendDecisionMaker;
        Pickup helpWeapon;
        Car player_car, tmpCar;
        Actor tempActor;
        Checkpoint checkpoint;
        Panel panel;
        Train train;

        Array<Float> tempArrayF = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Int> replicasPlayerTolking = MAX_ARRAY_OF_REPLICAS;
        Array<sString> replicasInDialog = MAX_ARRAY_OF_REPLICAS;
        Array<sString> melAnswers = 4;
        Array<Actor> enemyActors = MAX_ARRAY_OF_ACTIVE_COUNT, friendActors = MAX_ARRAY_OF_ACTIVE_COUNT, terroristsActors = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Marker> enemyMarkers = MAX_ARRAY_OF_ACTIVE_COUNT, friendMarkers = MAX_ARRAY_OF_ACTIVE_COUNT, terroristsMarkers = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Car> enemyCars = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Object> targetObjects = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<ASPack> enemyAS = MAX_ARRAY_OF_ACTIVE_COUNT, friendAS = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Pickup> puckups = MAX_ARRAY_OF_ACTIVE_COUNT;

        // 34 + 33/47 + 12*10 + 23*3 + 2*4 = 230/254 of 1023

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
            melPath.value = -1;
            johnPath.value = -1;
            p.enable_group_recruitment( false );
            g.release();
            __set_entered_names( false );
            __set_police_generator( false );
            enable_planes_traffic( false );
            enable_emergency_traffic( false );
            set_sensitivity_to_crime( 0.0 );
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
            } );

            OnPassed = DEFAULT_PASSED;
            OnFailed = DEFAULT_FAILED;
            OnClear = DEFAULT_CLEAR;
        }

        #region MISSIONS

        #region Mission 0
        private void MISSION_0( LabelCase l ) {

            Int[] usedModels = { TAMPA, COPBIKE, LAPDM1, COLT45 };

            chdir( @"Sound\CRMISS" );
            AUDIO_BG.load( 999999, true );
            wait( AUDIO_BG.is_ready );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME ).play();

            Gosub += SCENE_0B;

            load_requested_models( usedModels );
            clear_area( true, 1607.5181, -1697.6582, 12.5469, 300.0 );
            __renderer_at( 1607.5181, -1697.6582, 12.5469 );
            a.set_position( 1607.5181, -1697.6582, 12.5469 ).set_z_angle( 180.4397 );
            player_car.create( TAMPA, 1607.8346, -1709.9978, 13.1263 )
                      .set_z_angle( 180.0 )
                      .set_colors( 1, 1 )
                      .remove_references()
                      .set_is_considered_by_player( true );
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

            player_car.create( TAMPA, 1588.3674, -1717.3674, 13.0984 )
                      .set_z_angle( 90.0 )
                      .set_colors( 1, 1 )
                      .set_is_considered_by_player( true );

            friendActors[ 0 ].create_in_vehicle_passenger_seat( ActorType.MISSION1, SPECIAL01, player_car, 0 )
                             .set_max_health( 2000 )
                             .set_health( 2000 )
                             .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                             .set_cant_be_dragged_out( true );

            destroy_model( usedModels );
            unload_special_actor( 1 );
            __camera_default();
            __set_entered_names( true );
            chdir( @"Sound\CRMISS\1Z" );
            AUDIO_PL.load( 12 );
            wait( AUDIO_PL.is_ready );
            wait( 1000 );
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
                temp1.value = 6000;
            } );
            Cycle += delegate {
                wait( 0 );
                player_car.do_if_wrecked( delegate { ___jump_failed_message( "@CRS@14" ); } );
                and( !friendActors[ 0 ].is_in_vehicle( player_car ), delegate { friendActors[ 0 ].task.die(); } );
                friendActors[ 0 ].do_if_dead( delegate { ___jump_failed_message( "@CRS@15" ); } );
                and( !a.is_in_vehicle( player_car ), delegate { Jump += M1_ENTER_REMAX_CAR; } );
                and( totalReplicasInDialog > index, delegate {
                    and( LocalTimer1 > temp1, delegate {
                        a.stop_facial_talk();
                        AUDIO_PL.play();
                        wait( DefaultWaitTime );
                        AUDIO_PL.get_current_length_in_ms( temp1 );
                        and( 1 > temp1, delegate { temp1.value = 7000; } );
                        and( replicasPlayerTolking[ index ] == true, delegate { a.start_facial_talk( temp1 ); } );
                        show_text_highpriority( replicasInDialog[ index ], temp1, 1 );
                        index += 1;
                        LocalTimer1.value = 0;
                    } );
                } );
                and( index == totalReplicasInDialog, delegate {
                    and( LocalTimer1 > temp1, delegate {
                        a.stop_facial_talk();
                        AUDIO_BG.set_volume( 1.0 );
                        index += 1;
                    } );
                } );
                and(
                    a.is_near_point_3d_stopped_in_vehicle( 1, 259.063, -272.0966, 1.5836, 6.0, 6.0, 6.0 ),
                    friendActors[ 0 ].is_in_vehicle( player_car )
                , delegate { Jump += M1_END; } );
            };
        }

        private void M1_END( LabelJump label ) {
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
                      .set_health( 500 )
                      .set_is_considered_by_player( true );
            destroy_model( usedModels );
            __camera_default();
            __set_entered_names( true );
            wait( 1000 );
            set_sensitivity_to_crime( 1.0 );
            __set_police_generator( true );
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
                                    .set_muted( true )
                                    .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                                    .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 );
            } );
            helpWeapon.create_if_need( WeaponNumber.SNIPERRIFLE, WeaponModel.SNIPER, 10, 1588.5155, -992.762, 38.5221, temp1 );
            destroy_model( usedModels );
            __camera_default();
            __set_entered_names( true );
            wait( 1000 );
            set_current_time( 22, 0 );
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
                get_current_time( temp1, temp2 );
                and( 7 > temp1, delegate { ___jump_failed_message( "@CRS@30" ); } );
                and( p.is_wanted_level_greater( 0 ), delegate { ___jump_failed_message( "@CRS@19" ); } );
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

        #region Mission 4
        private void MISSION_4( LabelCase l ) {

            chdir( @"Sound\CRMISS" );
            AUDIO_BG.load( 999995, true );
            wait( AUDIO_BG.is_ready );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME ).play();
            Gosub += SCENE_4B;

            clear_area( true, 1814.7668, 60.716, 35.3936, 30.0 );
            __renderer_at( 1814.7668, 60.716, 35.3936 );
            a.put_at( 1814.7668, 60.716, 34.3936, 270.6572 );
            load_requested_models( ROCKETLA );
            helpWeapon.create_if_need( WeaponNumber.ROCKET, ROCKETLA, 5, 2100.8489, 180.3319, 2.1087, temp1 );
            destroy_model( ROCKETLA );
            __camera_default();
            __set_entered_names( true );
            wait( 1000 );
            __disable_player_controll_in_cutscene( false );
            __set_traffic( 1.0 );
            AUDIO_BG.set_volume( 1.0 );
            p.clear_wanted_level();
            __fade( true );
            Jump += M4_GOTO_POINT;
        }

        private void M4_GOTO_POINT( LabelJump label ) {
            checkpoint.create( 2098.8525, 175.1555, 2.8462 );
            show_text_highpriority( "@CRS@22", 6000, 1 );
            Cycle += delegate {
                wait( 0 );
                and( p.is_wanted_level_greater( 0 ), delegate { ___jump_failed_message( "@CRS@19" ); } );
                and( a.is_near_point_3d_on_foot( 1, 2098.8525, 175.1555, 2.8462, 1.75, 1.75, 1.75 ), delegate {
                    Jump += M4_KILL_TARGET;
                } );
            };
        }

        private void M4_KILL_TARGET( LabelJump label ) {
            friendMarkers[ 0 ].disable();
            checkpoint.disable();

            Int[] usedModels = { LAPDM1, PREDATOR, WMYPLT };

            load_model( usedModels );
            wait( is_model_available( usedModels ) );
            Array<Boat> boats = local_array( Int.IndexOf( enemyCars ), 3 );
            boats[ 0 ].create( PREDATOR, 2210.0, 396.0, 0.75 ).set_z_angle( 180.0 );
            boats[ 1 ].create( PREDATOR, 2217.0, 424.0, 0.75 ).set_z_angle( 180.0 );
            boats[ 2 ].create( PREDATOR, 2225.0, 447.0, 0.75 ).set_z_angle( 180.0 );
            enemyActors[ 0 ].create_in_vehicle_driverseat( ActorType.MISSION1, LAPDM1, boats[ 0 ] );
            enemyActors[ 1 ].create_in_vehicle_driverseat( ActorType.MISSION1, LAPDM1, boats[ 1 ] );
            enemyActors[ 2 ].create_in_vehicle_driverseat( ActorType.MISSION1, LAPDM1, boats[ 2 ] );
            temp1.random( 0, 3 );
            boats[ temp1 ].get_driver( tempActor );
            tempActor.store_coords( tempX1, tempY1, tempZ1, 0.0, -0.35, 0.0 );
            tempZ1 += 0.02;
            enemyActors[ 3 ].create( ActorType.MISSION1, WMYPLT, tempX1, tempY1, tempZ1 )
                            .set_health( 10 )
                            .task.crouch( true );
            // DEBUG START
            //enemyMarkers[ 3 ].create_above_actor( enemyActors[ 3 ] ).set_size( 1 );
            // DEBUG END
            destroy_model( usedModels );
            to( index, 0, 2, h => {
                boats[ index ].set_to_normal_driver();
                enemyAS[ index ].define( t => {
                    t.drive_car_to_point( boats[ index ], 2203.7324, 320.5402, 0.25, 24.0, 2, NULL, 0 )
                     .drive_car_to_point( boats[ index ], 2184.7998, 237.1036, 0.25, 26.0, 2, NULL, 0 )
                     .drive_car_to_point( boats[ index ], 2162.3745, 181.0627, 0.25, 28.0, 2, NULL, 0 )
                     .drive_car_to_point( boats[ index ], 2131.6267, 125.3257, 0.25, 28.0, 2, NULL, 0 )
                     .drive_car_to_point( boats[ index ], 2105.6108, 51.2862, 0.25, 28.0, 2, NULL, 0 )
                     .drive_car_to_point( boats[ index ], 2043.5387, -142.1585, 0.25, 28.0, 2, NULL, 0 );
                } );
                enemyActors[ index ].set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                                    .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 )
                                    .assign_to_AS_pack( enemyAS[ index ] );
                enemyAS[ index ].clear();
                enemyMarkers[ index ].create_above_vehicle( boats[ index ] ).set_size( 1 );
            } );
            enemyActors[ 3 ].set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                            .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 );
            show_text_highpriority( "@CRS@31", 6000, 1 );
            Cycle += delegate {
                wait( 0 );
                and( !a.is_in_area_3d( 0, 2011.8293, 27.9995, -900.0, 2259.7551, 275.5478, 900.0 ), delegate { ___jump_failed_message( "@CRS@27" ); } );
                to( index, 0, 2, h => {
                    and( boats[ index ].is_wrecked(), delegate {
                        enemyMarkers[ index ].disable_if_exist();
                    } );
                } );
                and( enemyActors[ 3 ].is_dead(), delegate {
                    wait( 3500 );
                    Jump += M4_END;
                } );
                and( enemyActors[ 3 ].is_near_point_3d( 0, 2043.5387, -142.1585, 0.25, 3.0, 3.0, 3.0 ), delegate {
                    ___jump_failed_message( "@CRS@32" );
                } );
            };
        }

        private void M4_END( LabelJump label ) {
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            __disable_player_controll_in_cutscene( true );
            a.extinguish_current_car_if_exist( tmpCar );
            __fade( 0, true );
            a.teleport_without_car( 1955.7073, 152.5207, 35.5643 );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME );
            gosub( CLEAR_ACTIVE_ENTITIES );
            Gosub += SCENE_4A;
            clear_area( true, 1971.9474, 155.4079, 34.3411, 230.7617 );
            a.put_at( 1971.9474, 155.4079, 34.3411, 230.7617 );
            __camera_default();
            wait( 1000 );
            fade( true, 500 );
            __disable_player_controll_in_cutscene( false );
            wait( 500 );
            jump_passed();
        }
        #endregion



        #region Mission 5

        Int M5_PANEL_SELECTED_ANSWER { get; set; }
        Int M5_PANEL_INDEX_OF_QUESTION { get; set; }
        Int M5_PANEL_CREDIT_MULTIPLIER { get; set; }
        Int M5_PANEL_CJ_ANSWER_SOUND_ID { get; set; }

        private void MISSION_5( LabelCase l ) {

            chdir( @"Sound\CRMISS" );
            AUDIO_BG.load( 999994, true );
            wait( AUDIO_BG.is_ready );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME ).play();

            and( M5_POLMAV_MISSION, CRASH_FBI_FLAG == false );

            Gosub += SCENE_5C;

            Int[] usedModels = { FBI, POLMAV };

            load_requested_models( usedModels );
            clear_area( true, 2370.6177, 2535.188, 10.8203, 300.0 );
            __renderer_at( 2370.6177, 2535.188, 10.8203 );
            a.put_at( 2370.6177, 2535.188, 10.8203, 180.0 );

            player_car.create( POLMAV, 2310.6858, 2445.8228, 10.8935 )
                      .set_z_angle( 155.0 )
                      .set_door_status( DoorStatus.LOCKED_4 )
                      .set_health( 5000 );

            friendDecisionMaker.create_normal( true );
            friendActors[ 0 ].create( ActorType.MISSION1, FBI, 2314.739, 2443.5134, 9.8203 )
                             .set_z_angle( 143.7115 )
                             .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                             .set_suffers_critical_hits( false )
                             .set_max_health( 5000 )
                             .set_health( 5000 )
                             .set_untargetable( false )
                             .set_decision_maker( friendDecisionMaker )
                             .lock_position( true )
                             .task.stay_put( true );

            destroy_model( usedModels );
            __camera_default();
            __set_entered_names( true );
            wait( 1000 );
            __disable_player_controll_in_cutscene( false );
            set_ped_traffic_density_multiplier( 0.5 );
            set_vehicle_traffic_density_multiplier( 0.1 );
            set_sensitivity_to_crime( 1.0 );
            AUDIO_BG.set_volume( 1.0 );
            p.clear_wanted_level();
            __fade( true );
            friendMarkers[ 0 ].create_above_actor( friendActors[ 0 ] ).set_size( 2 ).set_type( true );
            show_text_highpriority( "@CRS@33", 6000, 1 );
            LocalTimer1.value = 0;
            Cycle += delegate {
                wait( 0 );
                friendActors[ 0 ].do_if_dead( delegate { ___jump_failed_message( "@CRS@34" ); } );
                friendActors[ 0 ].set_z_angle( 143.7115 );
                player_car.do_if_wrecked( delegate { ___jump_failed_message( "@CRS@14" ); } );
                and( p.is_wanted_level_greater( 0 ), delegate { ___jump_failed_message( "@CRS@19" ); } );
                and( LocalTimer1 > 120000, delegate { ___jump_failed_message( "@CRS@35" ); } );
                and(
                    a.is_near_actor_3d( 0, friendActors[ 0 ], 30.0, 30.0, 30.0 ),
                    friendActors[ 0 ].is_on_screen()
                , delegate {
                    or(
                        p.is_aiming_at_actor( friendActors[ 0 ] ),
                        a.is_firing_weapon(),
                        friendActors[ 0 ].is_damaged_by_actor( a ),
                        player_car.is_damaged_by_actor( a )
                    , delegate { ___jump_failed_message( "@CRS@35" ); } );
                    friendActors[ 0 ].store_coords( tempX1, tempY1, tempZ1, 0.0, 1.0, 0.0 );
                    and(
                        a.is_near_point_3d_on_foot( 1, tempX1, tempY1, tempZ1, 0.75, 0.75, 2.0 ),
                        !p.is_on_jetpack()
                    , delegate {
                        Jump += M5_TESTING_CARL;
                    } );
                } );
            };
        }

        private void M5_TESTING_CARL( LabelJump label ) {

            Actor mel = friendActors[ 0 ], player = PlayerActor;

            M5_PANEL_SELECTED_ANSWER = temp1;
            M5_PANEL_INDEX_OF_QUESTION = temp2;
            M5_PANEL_CREDIT_MULTIPLIER = temp3;
            M5_PANEL_CJ_ANSWER_SOUND_ID = text_displayed;

            AUDIO_BG.set_volume( CUTSCENE_VOLUME );
            __disable_player_controll_in_cutscene( true );
            __set_traffic( 0.0 );
            set_sensitivity_to_crime( 0.0 );
            a.set_armed_weapon( 0 ).task.crouch( false ).rotate_to_actor( friendActors[ 0 ] );
            friendActors[ 0 ].set_immunities( true ).task.rotate_to_actor( a );
            __fade( false, true );
            chdir( @"Sound\CRMISS\5Z" );
            AUDIO_PL.load( 23 );
            wait( AUDIO_PL.is_ready );
            MISSION_GLOBAL_STATUS_TEXT_1.value = 50;
            MISSION_GLOBAL_STATUS_TEXT_1.create( StatusTextType.LINE, "@CRS@36" );
            enable_radar( false );
            M5_PANEL_INDEX_OF_QUESTION.value = -1;
            CAMERA.set_position( 2317.2949, 2441.4167, 10.8203 ).set_point_at( 2314.9446, 2442.2773, 10.8203, 2 );
            wait( 500 );
            __fade( true, true );

            AUDIO_PL.play( 18 ); // 18
            mel.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
            show_text_highpriority( "@CR@027", 4000, 1 );
            wait( 4000 );
            mel.stop_facial_talk();

            Gosub += M5_QUESTION_1;

            AUDIO_PL.play( M5_PANEL_CJ_ANSWER_SOUND_ID ); // [ 0, 1, 2, 3 ]
            player.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_1string_highpriority( "@CR@032", tempHash, 4500, 1 );
            wait( 4500 );
            player.stop_facial_talk();

            AUDIO_PL.play( 19 ); // 19
            mel.start_facial_talk( 3500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 3500 );
            show_text_highpriority( "@CR@033", 3500, 1 );
            wait( 3500 );
            mel.stop_facial_talk();

            Gosub += M5_QUESTION_2;

            AUDIO_PL.play( M5_PANEL_CJ_ANSWER_SOUND_ID ); // [ 4, 5, 6, 7 ]
            player.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_1string_highpriority( "@CR@032", tempHash, 4500, 1 );
            wait( 4500 );
            player.stop_facial_talk();

            AUDIO_PL.play( 20 ); // 20
            mel.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
            show_text_highpriority( "@CR@038", 4000, 1 );
            wait( 4000 );
            mel.stop_facial_talk();

            Gosub += M5_QUESTION_3;

            AUDIO_PL.play( M5_PANEL_CJ_ANSWER_SOUND_ID ); // [ 8, 9, 10, 11 ]
            player.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_1string_highpriority( "@CR@032", tempHash, 4500, 1 );
            wait( 4500 );
            player.stop_facial_talk();

            AUDIO_PL.play( 21 ); // 21
            mel.start_facial_talk( 3500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 3500 );
            show_text_highpriority( "@CR@043", 3500, 1 );
            wait( 3500 );
            mel.stop_facial_talk();

            Gosub += M5_QUESTION_4;

            AUDIO_PL.play( M5_PANEL_CJ_ANSWER_SOUND_ID ); // [ 12, 13, 14, 15 ]
            player.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_1string_highpriority( "@CR@032", tempHash, 4500, 1 );
            wait( 4500 );
            player.stop_facial_talk();

            AUDIO_PL.play( 22 ); // 22
            mel.start_facial_talk( 2500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 2500 );
            show_text_highpriority( "@CR@048", 2500, 1 );
            wait( 2500 );
            mel.stop_facial_talk();

            Gosub += M5_QUESTION_5;

            AUDIO_PL.play( M5_PANEL_CJ_ANSWER_SOUND_ID ); // [ 16, 17 ]
            player.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_1string_highpriority( "@CR@032", tempHash, 4500, 1 );
            wait( 4500 );
            player.stop_facial_talk();

            __fade( false, true );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            temp1.value = MISSION_GLOBAL_STATUS_TEXT_1;
            Gosub += CLEAR_ACTIVE_ENTITIES;
            and( 80 > temp1, delegate {
                wait( 1000 );
                __camera_default();
                __fade( true );
                wait( 500 );
                ___jump_failed_message( "@CRS@35" );
            } );
            MISSION_GLOBAL_STATUS_TEXT_1.remove();
            CRASH_FBI_FLAG.value = true;
            CRASH_START_X.value = 2308.8706;
            CRASH_START_Y.value = 2431.3784;
            CRASH_START_Z.value = 10.8203;
            CRASH_ICON.value = RadarIconID.POLICE;
            jump( M5_POLMAV_MISSION );
        }

        private void M5_QUESTION_1( LabelGosub label ) {
            melAnswers[ 0 ].value = "@CR@028";
            melAnswers[ 1 ].value = "@CR@029";
            melAnswers[ 2 ].value = "@CR@030";
            melAnswers[ 3 ].value = "@CR@031";
            M5_PANEL_INDEX_OF_QUESTION += 1;
            Gosub += M5_CREATE_TABLE;
            Cycle += delegate {
                wait( 0 );
                or( is_game_key_pressed( Keys.PED_SPRINT ), is_game_key_pressed( Keys.VEHICLE_ENTER_EXIT ), delegate {
                    panel.get_active_row( M5_PANEL_SELECTED_ANSWER );
                    M5_PANEL_CREDIT_MULTIPLIER.value = 0;
                    and( M5_PANEL_SELECTED_ANSWER == 0, delegate {
                        M5_PANEL_CREDIT_MULTIPLIER.value = -10;
                    } );
                    and( M5_PANEL_SELECTED_ANSWER == 1, delegate {
                        M5_PANEL_CREDIT_MULTIPLIER.value = 10;
                    } );
                    and( M5_PANEL_SELECTED_ANSWER == 3, delegate {
                        M5_PANEL_CREDIT_MULTIPLIER.value = -5;
                    } );
                    MISSION_GLOBAL_STATUS_TEXT_1 += M5_PANEL_CREDIT_MULTIPLIER;
                    tempHash.value = melAnswers[ M5_PANEL_SELECTED_ANSWER ];
                    Gosub += M5_FIND_CJ_ANSWER_REPLICA;
                    panel.remove();
                    @return();
                } );
            };
        }

        private void M5_QUESTION_2( LabelGosub label ) {
            melAnswers[ 0 ].value = "@CR@034";
            melAnswers[ 1 ].value = "@CR@035";
            melAnswers[ 2 ].value = "@CR@036";
            melAnswers[ 3 ].value = "@CR@037";
            M5_PANEL_INDEX_OF_QUESTION += 1;
            Gosub += M5_CREATE_TABLE;
            Cycle += delegate {
                wait( 0 );
                or( is_game_key_pressed( Keys.PED_SPRINT ), is_game_key_pressed( Keys.VEHICLE_ENTER_EXIT ), delegate {
                    panel.get_active_row( M5_PANEL_SELECTED_ANSWER );
                    M5_PANEL_CREDIT_MULTIPLIER.value = 0;
                    and( M5_PANEL_SELECTED_ANSWER == 0, delegate {
                        M5_PANEL_CREDIT_MULTIPLIER.value = -5;
                    } );
                    and( M5_PANEL_SELECTED_ANSWER == 1, delegate {
                        M5_PANEL_CREDIT_MULTIPLIER.value = -10;
                    } );
                    and( M5_PANEL_SELECTED_ANSWER == 3, delegate {
                        M5_PANEL_CREDIT_MULTIPLIER.value = 10;
                    } );
                    MISSION_GLOBAL_STATUS_TEXT_1 += M5_PANEL_CREDIT_MULTIPLIER;
                    tempHash.value = melAnswers[ M5_PANEL_SELECTED_ANSWER ];
                    Gosub += M5_FIND_CJ_ANSWER_REPLICA;
                    panel.remove();
                    @return();
                } );
            };
        }

        private void M5_QUESTION_3( LabelGosub label ) {
            melAnswers[ 0 ].value = "@CR@039";
            melAnswers[ 1 ].value = "@CR@040";
            melAnswers[ 2 ].value = "@CR@041";
            melAnswers[ 3 ].value = "@CR@042";
            M5_PANEL_INDEX_OF_QUESTION += 1;
            Gosub += M5_CREATE_TABLE;
            Cycle += delegate {
                wait( 0 );
                or( is_game_key_pressed( Keys.PED_SPRINT ), is_game_key_pressed( Keys.VEHICLE_ENTER_EXIT ), delegate {
                    panel.get_active_row( M5_PANEL_SELECTED_ANSWER );
                    M5_PANEL_CREDIT_MULTIPLIER.value = -10;
                    and( M5_PANEL_SELECTED_ANSWER == 0, delegate {
                        M5_PANEL_CREDIT_MULTIPLIER.value = -5;
                    } );
                    and( M5_PANEL_SELECTED_ANSWER == 1, delegate {
                        M5_PANEL_CREDIT_MULTIPLIER.value = 10;
                    } );
                    MISSION_GLOBAL_STATUS_TEXT_1 += M5_PANEL_CREDIT_MULTIPLIER;
                    tempHash.value = melAnswers[ M5_PANEL_SELECTED_ANSWER ];
                    Gosub += M5_FIND_CJ_ANSWER_REPLICA;
                    panel.remove();
                    @return();
                } );
            };
        }

        private void M5_QUESTION_4( LabelGosub label ) {
            melAnswers[ 0 ].value = "@CR@044";
            melAnswers[ 1 ].value = "@CR@045";
            melAnswers[ 2 ].value = "@CR@046";
            melAnswers[ 3 ].value = "@CR@047";
            M5_PANEL_INDEX_OF_QUESTION += 1;
            Gosub += M5_CREATE_TABLE;
            Cycle += delegate {
                wait( 0 );
                or( is_game_key_pressed( Keys.PED_SPRINT ), is_game_key_pressed( Keys.VEHICLE_ENTER_EXIT ), delegate {
                    panel.get_active_row( M5_PANEL_SELECTED_ANSWER );
                    M5_PANEL_CREDIT_MULTIPLIER.value = -10;
                    and( M5_PANEL_SELECTED_ANSWER == 0, delegate {
                        M5_PANEL_CREDIT_MULTIPLIER.value = 10;
                    } );
                    and( M5_PANEL_SELECTED_ANSWER == 3, delegate {
                        M5_PANEL_CREDIT_MULTIPLIER.value = 5;
                    } );
                    MISSION_GLOBAL_STATUS_TEXT_1 += M5_PANEL_CREDIT_MULTIPLIER;
                    tempHash.value = melAnswers[ M5_PANEL_SELECTED_ANSWER ];
                    Gosub += M5_FIND_CJ_ANSWER_REPLICA;
                    panel.remove();
                    @return();
                } );
            };
        }

        private void M5_QUESTION_5( LabelGosub label ) {
            melAnswers[ 0 ].value = "@CR@049";
            melAnswers[ 1 ].value = "@CR@050";
            melAnswers[ 2 ].value = sString.DUMMY;
            melAnswers[ 3 ].value = sString.DUMMY;
            M5_PANEL_INDEX_OF_QUESTION += 1;
            Gosub += M5_CREATE_TABLE;
            Cycle += delegate {
                wait( 0 );
                or( is_game_key_pressed( Keys.PED_SPRINT ), is_game_key_pressed( Keys.VEHICLE_ENTER_EXIT ), delegate {
                    panel.get_active_row( M5_PANEL_SELECTED_ANSWER );
                    M5_PANEL_CREDIT_MULTIPLIER.value = -10;
                    and( M5_PANEL_SELECTED_ANSWER == 1, delegate {
                        M5_PANEL_CREDIT_MULTIPLIER.value = 10;
                    } );
                    MISSION_GLOBAL_STATUS_TEXT_1 += M5_PANEL_CREDIT_MULTIPLIER;
                    tempHash.value = melAnswers[ M5_PANEL_SELECTED_ANSWER ];
                    Gosub += M5_FIND_CJ_ANSWER_REPLICA;
                    panel.remove();
                    @return();
                } );
            };
        }

        private void M5_CREATE_TABLE( LabelGosub label ) {
            panel.create( "@CRS@37", 29.0, 305.0, 566.0, 1, 1, 1, PanelAlign.LEFT )
                 .set_column_data( 0, sString.DUMMY, melAnswers );
        }

        private void M5_FIND_CJ_ANSWER_REPLICA( LabelGosub label ) {
            M5_PANEL_CJ_ANSWER_SOUND_ID.mul( M5_PANEL_INDEX_OF_QUESTION, 4 );
            M5_PANEL_CJ_ANSWER_SOUND_ID += M5_PANEL_SELECTED_ANSWER;
        }

        private void M5_POLMAV_MISSION( LabelJump label ) {

            Int[] usedModels = { FBI, POLMAV, COACH };

            Gosub += SCENE_5B; // надо коррекцию текста и озвучки, с нужным таймингом

            reward.value = 10000;
            load_requested_models( usedModels );
            clear_area( true, 2314.739, 2443.5134, 9.8203, 300.0 );
            __renderer_at( 2314.739, 2443.5134, 9.8203 );
            a.put_at( 2314.739, 2443.5134, 9.8203, 62.1856 );

            player_car.create( POLMAV, 2310.6858, 2445.8228, 10.8935 )
                      .set_z_angle( 155.0 )
                      .set_is_considered_by_player( true )
                      .store_coords( tempX1, tempY1, tempZ1, 1.0, 0.0, 0.0 );

            friendDecisionMaker.create_normal( true );
            friendActors[ 0 ].create( ActorType.MISSION1, FBI, tempX1, tempY1, tempZ1 )
                             .set_z_angle( 62.1856 )
                             .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                             .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION2 )
                             .set_decision_maker( friendDecisionMaker )
                             .attach_to_vehicle( player_car, 1.0, 0.0, 0.0, 2, 180.0, 0 )
                             .task.crouch( true );

            clear_area( false, 2711.326, 1567.045, 6.2, 50.0 );
            enemyCars[ 0 ].create( COACH, 2711.326, 1567.045, 6.2 )
                          .set_z_angle( 175.1379 )
                          .set_health( 2500 )
                          .set_door_status( DoorStatus.LOCKED_4 )
                          .set_immunities( 0, 1, 0, 0, 0 )
                          .set_tires_vulnerable( true );

            friendActors[ 1 ].create_in_vehicle_driverseat( ActorType.MISSION2, MALE01, enemyCars[ 0 ] )
                             .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                             .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 )
                             .set_decision_maker( friendDecisionMaker )
                             .set_immunities( true );

            __normalize_AI_for_vehicle( enemyCars[ 0 ] );
            __normalize_AI_for_driver( friendActors[ 1 ] );

            destroy_model( usedModels );
            __camera_default();
            __set_entered_names( true );
            wait( 1000 );
            __disable_player_controll_in_cutscene( false );
            set_ped_traffic_density_multiplier( 1.0 );
            AUDIO_BG.set_volume( 1.0 );
            p.clear_wanted_level();
            __fade( true );
            temp1.value = -1;
            Gosub += M5_BUS_DRIVING_SYSTEM;
            Jump += M5_ENTER_HELI;
        }

        private void M5_ENTER_HELI( LabelJump label ) {
            friendMarkers[ 0 ].disable().create_above_vehicle( player_car ).set_type( 1 ).set_size( 2 );
            show_text_highpriority( "@CRS@38", 6000, 1 );
            remove_text_box();
            Cycle += delegate {
                wait( 0 );
                __checkPoliveMavMelAndOther();
                and( a.is_in_vehicle( player_car ), delegate { Jump += M5_GOTO_BUS; } );
            };
            jump_passed();
        }

        private void M5_GOTO_BUS( LabelJump label ) {
            friendMarkers[ 0 ].disable().create_above_vehicle( enemyCars[ 0 ] ).set_type( 1 ).set_size( 2 ).set_radar_mode( 2 )
                              .set_color( MarkerColor.YELLOW );
            show_text_highpriority( "@CRS@39", 6500, 1 );
            Cycle += delegate {
                wait( 0 );
                __checkPoliveMavMelAndOther();
                and( !a.is_in_vehicle( player_car ), delegate { Jump += M5_ENTER_HELI; } );
                enemyCars[ 0 ].get_position( tempX2, tempY2, tempZ2 );
                tempZ2 += 4.0;
                draw_weaponshop_corona( tempX2, tempY2, tempZ2, 2.0, 9, 0, 255, 0, 0 );
                and( friendActors[ 0 ].is_attached_to_any_vehicle(), delegate {
                    and(
                        a.is_near_vehicle_3d( 0, enemyCars[ 0 ], 30.0, 30.0, 30.0 )
                    , delegate {
                        and( !is_text_box_displayed( "@CRS@41" ), delegate { show_permanent_text_box( "@CRS@41" ); } );
                        and( is_game_key_pressed( Keys.CAR_HORN ), delegate {
                            remove_text_box();
                            friendActors[ 0 ].detach_from_vehicle().task.jump( true );
                        } );
                    }, delegate { remove_text_box(); } );
                }, delegate {
                    and(
                        friendActors[ 0 ].is_stopped(),
                        !friendActors[ 0 ].is_dead()
                    , delegate {
                        and( !friendActors[ 0 ].is_colliding_with_vehicle( enemyCars[ 0 ] ), delegate {
                            ___jump_failed_message( "@CRS@42" );
                        } );
                        friendActors[ 0 ].get_position( tempX2, tempY2, tempZ2 );
                        tempZ1.value = tempZ2;
                        enemyCars[ 0 ].get_position( tempX2, tempY2, tempZ2 );
                        and( tempZ1 > tempZ2, delegate {
                            player_car.lock_position( true );
                            __fade( false, true );
                            Gosub += CLEAR_ACTIVE_ENTITIES;
                            __fade( true, false );
                            player_car.add_reference().lock_position( false );
                            wait( 500 );
                            Jump += M5_GOTO_POLICE_HOUSE;
                        } );
                        ___jump_failed_message( "@CRS@42" );
                    } );
                } );
            };
        }

        private void M5_GOTO_POLICE_HOUSE( LabelJump label ) {
            friendMarkers[ 0 ].disable();
            checkpoint.create( 2310.6858, 2445.8228, 10.8935 );
            show_text_highpriority( "@CRS@43", 6500, 1 );
            Cycle += delegate {
                wait( 0 );
                player_car.do_if_wrecked( delegate { ___jump_failed_message( "@CRS@14" ); } );
                and( !a.is_in_vehicle( player_car ), delegate { Jump += M5_ENTER_HELI2; } );
                and(
                    player_car.is_near_point_3d( true, 2310.6858, 2445.8228, 10.8935, 4.0, 4.0, 4.0 ),
                    a.is_in_vehicle( player_car )
                , delegate {
                    checkpoint.disable();
                    player_car.lock_position( true ).set_immunities( true ).extinguish();
                    __fade( false, true );
                    a.teleport_without_car( 2297.9482, 2425.2913, 10.8203, 184.9004 );
                    wait( 1000 );
                    __camera_default();
                    __fade( true, false );
                    wait( 1000 );
                    CRASH_FBI_FLAG.value = false;
                    jump_passed();
                } );
            };
        }

        private void M5_ENTER_HELI2( LabelJump label ) {
            checkpoint.disable();
            show_text_highpriority( "@CRS@38", 6000, 1 );
            Cycle += delegate {
                wait( 0 );
                player_car.do_if_wrecked( delegate { ___jump_failed_message( "@CRS@14" ); } );
                and( a.is_in_vehicle( player_car ), delegate { Jump += M5_GOTO_POLICE_HOUSE; } );
            };
        }

        private void __checkPoliveMavMelAndOther() {
            and( !a.is_in_area_3d( 0, 804.8857, 583.0725, -300.0, 3000.0, 3000.0, 300.0 ), delegate {
                ___jump_failed_message( "@CRS@21" );
            } );
            player_car.do_if_wrecked( delegate { ___jump_failed_message( "@CRS@14" ); } );
            friendActors[ 0 ].do_if_dead( delegate { ___jump_failed_message( "@CRS@34" ); } );
            enemyCars[ 0 ].get_speed( tempSpeed );
            and( 7.0 > tempSpeed, enemyCars[ 0 ].is_on_screen(), delegate { enemyCars[ 0 ].explode(); } );
            enemyCars[ 0 ].do_if_wrecked( delegate { ___jump_failed_message( "@CRS@40" ); } );
            player_car.get_z_angle( tempAngle );
            tempAngle -= 90.0;
            friendActors[ 0 ].set_z_angle( tempAngle );
            Gosub += M5_BUS_DRIVING_SYSTEM_FIX_BUS;
            and( enemyCars[ 0 ].is_near_point_2d( 0, tempX1, tempY1, 15.0, 15.0 ), delegate {
                Gosub += M5_BUS_DRIVING_SYSTEM;
            } );
        }

        private void M5_BUS_DRIVING_SYSTEM_FIX_BUS( LabelGosub label ) {
            or(
                enemyCars[ 0 ].is_stuck_on_roof(),
                enemyCars[ 0 ].is_stuck()
            , delegate {
                and( !enemyCars[ 0 ].is_on_screen(), delegate {
                    and( !is_sphere_onscreen( tempX1, tempY1, 6.0, 4.0 ), delegate {
                        clear_area( false, tempX1, tempY1, 7.0, 10.0 );
                        enemyCars[ 0 ].set_position( tempX1, tempY1, 10.0 )
                                      .store_coords( tempX2, tempY2, tempZ2, 0.0, 8.0, 0.0 );
                        store_path_coords_closest_to_vehicle( tempX2, tempY2, tempZ2, tempX2, tempY2, tempZ2 );
                        get_angle_between_2d_vectors( tempX1, tempY1, tempX2, tempY2, tempAngle );
                        enemyCars[ 0 ].set_z_angle( tempAngle );
                    } );
                } );
            } );
        }

        private void M5_BUS_DRIVING_SYSTEM( LabelGosub label ) {

            Float[] arrX = { 2710.623, 2711.071, 2590.812, 2203.896, 1833.473, 1541.602, 1288.89, 1224.975, 1224.064, 1224.173, 1279.245, 1566.816, 1931.45, 2442.573, 2704.658, 2710.891, 2711.892 };
            Float[] arrY = { 1397.883, 1175.003, 898.2437, 849.0437, 850.4182, 848.788, 901.789, 1240.59, 1744.703, 2071.703, 2398.683, 2457.233, 2521.858, 2609.429, 2343.654, 2065.317, 1830.5 };

            temp1 += 1;
            and( temp1 == arrX.Length, delegate { temp1.value = 0; } );
            for( int i = 0; i < arrX.Length; i++ ) {
                and( temp1 == i, delegate {
                    tempX1.value = arrX[ i ];
                    tempY1.value = arrY[ i ];
                } );
            }
            and( !is_sphere_onscreen( tempX1, tempY1, 6.0, 4.0 ), delegate {
                clear_area( false, tempX1, tempY1, 7.0, 10.0 );
            } );
            Gosub += M5_BUS_DRIVING_SYSTEM_SET_PED_TASK;
        }

        private void M5_BUS_DRIVING_SYSTEM_SET_PED_TASK( LabelGosub label ) {
            friendActors[ 1 ].clear_tasks().task.drive_car_to_point( enemyCars[ 0 ], tempX1, tempY1, 6.0, 15.0, 0, NULL, 1 );
        }

        #endregion

        #region Mission 6

        private void MISSION_6( LabelCase l ) {

            Int[] usedModels = { MP5LNG, FBI, FBIRANCH };

            chdir( @"Sound\CRMISS" );
            AUDIO_BG.load( 999993, true );
            wait( AUDIO_BG.is_ready );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME ).play();

            Gosub += SCENE_6A;

            clear_area( true, 2318.4385, 2446.1021, 9.8203, 80.0 );
            __renderer_at( 2318.4385, 2446.1021, 9.8203 );
            a.put_at( 2318.4385, 2446.1021, 9.8203, 74.5741 );
            load_requested_models( usedModels );
            helpWeapon.create_if_need( WeaponNumber.MP5, MP5LNG, 650, 2316.6155, 2450.6572, 10.8203, temp1 );

            Gosub += M6_SETUP_DIALOG;

            player_car.create( FBIRANCH, 2312.1667, 2449.2207, 10.4306 )
                      .set_z_angle( 154.9585 )
                      .set_door_status( DoorStatus.UNLOCKED );

            friendDecisionMaker.create_normal( true );
            friendActors[ 0 ].create_in_vehicle_passenger_seat( ActorType.MISSION1, FBI, player_car, 0 )
                             .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                             .set_suffers_critical_hits( false )
                             .set_max_health( 5000 )
                             .set_health( 5000 )
                             .set_untargetable( false )
                             .set_decision_maker( friendDecisionMaker )
                             .set_cant_be_dragged_out( true )
                             .set_can_be_knocked_off_bike( true );

            destroy_model( usedModels );
            __camera_default();

            wait( 1000 );
            __disable_player_controll_in_cutscene( false );
            __set_entered_names( true );
            __set_traffic( 1.0 );
            p.clear_wanted_level().ignored_by_cops( true );
            __fade( true );
            temp1.value = -1;
            temp3.value = false;
            Jump += M6_ENTER_A_CAR;
        }

        private void M6_ENTER_A_CAR( LabelJump label ) {
            checkpoint.disable();
            friendMarkers[ 0 ].create_above_vehicle( player_car ).set_type( true );
            show_text_highpriority( "@CRS@12", 4000, 1 );
            Cycle += delegate {
                wait( 0 );
                checkMel( true );
                and( a.is_in_vehicle( player_car ), delegate { Jump += M6_GOTO_AREA; } );
            };
        }

        private void M6_GOTO_AREA( LabelJump label ) {
            checkpoint.create( 2818.2397, 933.8743, 10.9766 );
            friendMarkers[ 0 ].disable();
            and( temp3 == true, delegate {
                show_text_highpriority( "@CRS@22", 6000, 1 );
            }, delegate {
                temp2.value = 2000;
                LocalTimer1.value = 0;
            } );
            Cycle += delegate {
                wait( 0 );
                checkMel( true );
                and( !a.is_in_vehicle( player_car ), delegate { Jump += M6_ENTER_A_CAR; } );
                and(
                    player_car.is_in_area_2d( 0, 2738.6042, 826.7645, 2916.0, 1041.7644 ),
                    a.is_in_vehicle( player_car ),
                    friendActors[ 0 ].is_in_vehicle( player_car )
                , delegate { Jump += M6_INIT_AREA_TO_FIGHT; } );
                Gosub += M6_UPDATE_DIALOGS;
            };
        }

        private void M6_UPDATE_DIALOGS( LabelGosub label ) {
            and( temp2 > LocalTimer1, @return );
            temp1 += 1;
            and( temp1 > totalReplicasInDialog, delegate {
                temp1.value = totalReplicasInDialog;
                temp3.value = true;
                AUDIO_BG.set_volume( 1.0 );
            } );
            and( temp1 == totalReplicasInDialog, @return );
            AUDIO_PL.play();
            wait( DefaultWaitTime );
            AUDIO_PL.get_current_length_in_ms( temp2 );
            and( 1 > temp2, delegate { temp2.value = 6000; } );
            show_text_highpriority( replicasInDialog[ temp1 ], temp2, 1 );
            and( replicasPlayerTolking[ temp1 ] == true, delegate {
                a.start_facial_talk( temp2 );
            }, delegate {
                friendActors[ 0 ].start_facial_talk( temp2 );
            } );
            LocalTimer1.value = 0;
        }

        private void M6_INIT_AREA_TO_FIGHT( LabelJump label ) {
            friendMarkers[ 0 ].disable();
            checkpoint.disable();
            __clear_text();
            __set_traffic( 0.0 );
            __disable_player_controll_in_cutscene( true );
            player_car.extinguish();
            __fade( 0, true );
            clear_area( 1, 2787.6943, 961.908, 14.2559, 2.0 );
            a.stop_facial_talk().teleport_without_car( 2787.6943, 961.908, 14.2559, 0.0 );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME );
            Gosub += CLEAR_ACTIVE_ENTITIES;
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );

            Int[] usedModels = { MP5LNG, TEC9, MICRO_UZI, M4, AK47, CHROMEGUN, FBI, FORKLIFT, FBIRANCH, SWAT, GRENADE, INFO, ObjectModel.BODYARMOR, ObjectModel.BARREL1, 1558, 1654, BMOST, HMOST, BMYPOL2, SWMYHP2, WMYCR, DNMYLC };

            Gosub += SCENE_6B;

            clear_area( true, 2820.1787, 934.1736, 10.9766, 300.0 );
            __renderer_at( 2820.1787, 934.1736, 10.9766 );
            a.put_at( 2782.3083, 913.4617, 9.75, 285.5869 );
            load_requested_models( usedModels );
            var trainModels = Train.GetModelsByType( M6_TRAIN_TYPE );
            load_requested_models( trainModels );

            friendDecisionMaker.create_normal( true );
            enemyDecisionMaker.create_normal();

            puckups[ 0 ].create( ObjectModel.BODYARMOR, PickupType.ONCE, 2805.7061, 933.652, 10.9766 );
            puckups[ 1 ].create( ObjectModel.BODYARMOR, PickupType.ONCE, 2879.1917, 894.5878, 10.75 );

            friendActors[ 0 ].create( ActorType.MISSION1, FBI, 2779.2637, 916.5197, 9.7578 )
                             .set_z_angle( 264.2801 )
                             .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                             .set_acquaintance( AcquaintanceType.HATE, ActorType.MISSION2 )
                             .set_suffers_critical_hits( false )
                             .set_max_health( 900 )
                             .set_health( 900 )
                             .set_untargetable( false )
                             .set_muted( true )
                             .set_decision_maker( friendDecisionMaker )
                             .give_weapon( WeaponNumber.M4, 9999 )
                             .set_armed_weapon( WeaponNumber.M4 )
                             .put_in_group( g )
                             .set_uses_upperbody_damage_anims_only( false )
                             .set_weapon_accuracy( 70 )
                             .set_weapon_attack_rate( 30 )
                             .set_never_leaves_group( true );

            targetObjects[ 0 ].create( 1558, 2870.4951, 857.5322, 9.75 );
            targetObjects[ 1 ].create( 1558, 2846.9124, 998.6584, 9.75 );
            targetObjects[ 2 ].create( 1558, 2849.4995, 892.1309, 9.75 );
            targetObjects[ 3 ].create( ObjectModel.BARREL1, 0.0, 0.0, 0.0 ).attach( targetObjects[ 0 ], 0.0, 0.0, 0.9, 0.0, 0.0, 0.0 );
            targetObjects[ 4 ].create( ObjectModel.BARREL1, 0.0, 0.0, 0.0 ).attach( targetObjects[ 1 ], 0.0, 0.0, 0.9, 0.0, 0.0, 0.0 );
            targetObjects[ 5 ].create( ObjectModel.BARREL1, 0.0, 0.0, 0.0 ).attach( targetObjects[ 2 ], 0.0, 0.0, 0.9, 0.0, 0.0, 0.0 );
            targetObjects[ 6 ].create( 1654, 0.0, 0.0, 0.0 ).attach( targetObjects[ 3 ], 0.0, 0.0, 0.6, 270.0, 0.0, 0.0 ).set_collision_detection( true );
            targetObjects[ 7 ].create( 1654, 0.0, 0.0, 0.0 ).attach( targetObjects[ 4 ], 0.0, 0.0, 0.6, 270.0, 0.0, 0.0 ).set_collision_detection( true );
            targetObjects[ 8 ].create( 1654, 0.0, 0.0, 0.0 ).attach( targetObjects[ 5 ], 0.0, 0.0, 0.6, 270.0, 0.0, 0.0 ).set_collision_detection( true );
            to( index, 0, 8, h => { targetObjects[ index ].set_immunities( true ); } );
            targetObjects[ 9 ].create( INFO, 2785.4346, 923.0491, 10.75 )
                              .set_immunities( true )
                              .set_targetable( false )
                              .set_collision_detection( false )
                              .set_visibility( false );

            friendMarkers[ 0 ].create_above_object( targetObjects[ 6 ] ).set_size( 1 );
            friendMarkers[ 1 ].create_above_object( targetObjects[ 7 ] ).set_size( 1 );
            friendMarkers[ 2 ].create_above_object( targetObjects[ 8 ] ).set_size( 1 );
            friendMarkers[ 3 ].create_above_actor( friendActors[ 0 ] ).set_type( true ).set_size( 1 ).set_radar_mode( 2 );

            enemyCars[ 0 ].create( FORKLIFT, 2791.7876, 971.4381, 10.9115 ).set_z_angle( 267.8398 );
            enemyCars[ 1 ].create( FORKLIFT, 2883.1638, 942.9713, 10.9131 ).set_z_angle( 92.4155 );
            enemyCars[ 2 ].create( FORKLIFT, 2843.2263, 856.4691, 10.9205 ).set_z_angle( 89.9764 );

            player_car.create( FBIRANCH, 2766.2629, 884.5315, 10.4982 ).explode_in_cutscene();
            train.create( 2764.7012, 898.6685, 10.8984, M6_TRAIN_TYPE, 1 );
            var tmpVeh = ( Car ) train;
            tmpVeh.set_immunities( true )
                  .set_door_status( DoorStatus.LOCKED_4 )
                  .lock_position( true )
                  .set_targettable_by_heatseeker( false );
            tmpVeh.get_driver( tempActor );
            tempActor.destroy_if_exist();

            enemyActors[ 0 ].create( ActorType.MISSION2, BMOST, 2803.7876, 889.3749, 19.1008 )
                            .set_z_angle( 49.5359 )
                            .give_weapon( WeaponNumber.MP5, 9999 )
                            .set_armed_weapon( WeaponNumber.MP5 )
                            .enable_validate_position( true )
                            .lock_position( true )
                            .task.stay_put( true );

            enemyActors[ 1 ].create( ActorType.MISSION2, HMOST, 2790.9929, 948.3256, 16.6896 )
                            .set_z_angle( 186.7538 )
                            .give_weapon( WeaponNumber.MP5, 9999 )
                            .set_armed_weapon( WeaponNumber.MP5 )
                            .enable_validate_position( true )
                            .lock_position( true )
                            .task.stay_put( true );

            enemyActors[ 2 ].create( ActorType.MISSION2, BMYPOL2, 2812.9075, 969.3105, 9.75 )
                            .set_z_angle( 152.9136 )
                            .give_weapon( WeaponNumber.TEC9, 9999 )
                            .set_armed_weapon( WeaponNumber.TEC9 )
                            .set_weapon_skill( 2 );

            enemyActors[ 3 ].create( ActorType.MISSION2, SWMYHP2, 2815.8704, 944.0104, 9.75 )
                            .set_z_angle( 121.58 )
                            .give_weapon( WeaponNumber.MICRO_UZI, 9999 )
                            .set_armed_weapon( WeaponNumber.MICRO_UZI )
                            .set_weapon_skill( 2 )
                            .task.crouch( true );

            enemyActors[ 4 ].create( ActorType.MISSION2, WMYCR, 2809.1558, 917.9577, 9.75 )
                            .set_z_angle( 93.6931 )
                            .give_weapon( WeaponNumber.SHOTGUN, 9999 )
                            .set_armed_weapon( WeaponNumber.SHOTGUN );

            enemyActors[ 5 ].create( ActorType.MISSION2, DNMYLC, 2812.0042, 896.8455, 9.7578 )
                            .set_z_angle( 59.2261 )
                            .give_weapon( WeaponNumber.MP5, 9999 )
                            .set_armed_weapon( WeaponNumber.MP5 )
                            .task.crouch( true );

            enemyActors[ 6 ].create( ActorType.MISSION2, BMOST, 2843.8345, 899.3383, 9.7578 )
                            .set_z_angle( 65.1794 )
                            .give_weapon( WeaponNumber.AK47, 9999 )
                            .set_armed_weapon( WeaponNumber.AK47 )
                            .task.crouch( true ).stay_put( true );

            enemyActors[ 7 ].create( ActorType.MISSION2, HMOST, 2858.3853, 948.1014, 17.0369 )
                            .set_z_angle( 160.457 )
                            .give_weapon( WeaponNumber.AK47, 9999 )
                            .set_armed_weapon( WeaponNumber.AK47 )
                            .enable_validate_position( true )
                            .lock_position( true )
                            .task.crouch( true ).stay_put( true );

            enemyActors[ 8 ].create( ActorType.MISSION2, SWMYHP2, 2831.6243, 934.5535, 9.9766 )
                            .set_z_angle( 90.8964 )
                            .give_weapon( WeaponNumber.MP5, 9999 )
                            .set_armed_weapon( WeaponNumber.MP5 )
                            .task.crouch( true );

            enemyActors[ 9 ].create( ActorType.MISSION2, BMYPOL2, 2840.4399, 988.0525, 9.75 )
                            .set_z_angle( 169.2304 )
                            .give_weapon( WeaponNumber.SHOTGUN, 9999 )
                            .set_armed_weapon( WeaponNumber.SHOTGUN )
                            .task.stay_put( true );

            terroristsActors[ 0 ].create( ActorType.MISSION2, WMYCR, 2787.5935, 884.882, 9.7578 )
                                 .set_z_angle( 353.8708 )
                                 .give_weapon( WeaponNumber.SHOTGUN, 9999 )
                                 .set_armed_weapon( WeaponNumber.SHOTGUN );

            terroristsActors[ 1 ].create( ActorType.MISSION2, DNMYLC, 2794.2998, 844.486, 9.75 )
                                 .set_z_angle( 14.5509 )
                                 .give_weapon( WeaponNumber.GRENADE, 9999 )
                                 .set_armed_weapon( WeaponNumber.GRENADE )
                                 .set_only_damaged_by_player( true )
                                 .enable_validate_position( true )
                                 .task.stay_put( true );


            terroristsActors[ 2 ].create( ActorType.MISSION2, BMOST, 2846.9619, 851.9721, 9.75 )
                                 .set_z_angle( 124.8453 )
                                 .give_weapon( WeaponNumber.AK47, 9999 )
                                 .set_armed_weapon( WeaponNumber.AK47 )
                                 .task.stay_put( true );

            terroristsActors[ 3 ].create( ActorType.MISSION2, HMOST, 2882.1882, 913.7486, 16.6896 )
                                 .set_z_angle( 88.1615 )
                                 .give_weapon( WeaponNumber.TEC9, 9999 )
                                 .set_armed_weapon( WeaponNumber.TEC9 )
                                 .set_weapon_skill( 2 )
                                 .enable_validate_position( true )
                                 .lock_position( true )
                                 .task.stay_put( true );

            terroristsActors[ 4 ].create( ActorType.MISSION2, BMYPOL2, 2870.0371, 902.1294, 9.75 )
                                 .set_z_angle( 64.0112 )
                                 .give_weapon( WeaponNumber.SHOTGUN, 9999 )
                                 .set_armed_weapon( WeaponNumber.SHOTGUN );

            terroristsActors[ 5 ].create( ActorType.MISSION2, SWMYHP2, 2879.1431, 948.0914, 17.0337 )
                                 .set_z_angle( 147.0453 )
                                 .give_weapon( WeaponNumber.MP5, 9999 )
                                 .set_armed_weapon( WeaponNumber.MP5 )
                                 .enable_validate_position( true )
                                 .lock_position( true )
                                 .task.crouch( true ).stay_put( true );

            terroristsActors[ 6 ].create( ActorType.MISSION2, WMYCR, 2833.5073, 983.9175, 9.75 )
                                 .set_z_angle( 182.5225 )
                                 .give_weapon( WeaponNumber.MP5, 9999 )
                                 .set_armed_weapon( WeaponNumber.MP5 )
                                 .task.crouch( true ).stay_put( true );

            terroristsActors[ 7 ].create( ActorType.MISSION2, DNMYLC, 2847.9412, 889.8962, 18.0478 )
                                 .set_z_angle( 60.0314 )
                                 .give_weapon( WeaponNumber.MP5, 9999 )
                                 .set_armed_weapon( WeaponNumber.MP5 )
                                 .enable_validate_position( true )
                                 .lock_position( true )
                                 .task.crouch( true ).stay_put( true );

            terroristsActors[ 8 ].create( ActorType.MISSION2, BMOST, 2816.4658, 910.3267, 9.75 )
                                 .set_z_angle( 119.8792 ) 
                                 .give_weapon( WeaponNumber.MICRO_UZI, 9999 )
                                 .set_armed_weapon( WeaponNumber.MICRO_UZI )
                                 .set_weapon_skill( 2 );

            terroristsActors[ 9 ].create( ActorType.MISSION2, DNMYLC, 2834.0479, 920.4642, 9.75 )
                                 .set_z_angle( 90.7383 )
                                 .give_weapon( WeaponNumber.AK47, 9999 )
                                 .set_armed_weapon( WeaponNumber.AK47 )
                                 .set_only_damaged_by_player( true );

            g.set_separation_range( 6.0 ).set_follow_status( true );
            destroy_model( usedModels );
            destroy_model( trainModels );
            switch_roads_off( 2738.6042, 826.7645, -10.0, 2916.0, 1041.7644, 40.0 );
            __set_traffic( 1.0 );
            AUDIO_BG.set_volume( 1.0 );
            __camera_default();
            wait( 500 );

            friendActors[ 1 ].create( ActorType.MISSION1, SWAT, 2792.9885, 895.3325, 9.75 ).set_z_angle( 90.2697 ).task.die();
            friendActors[ 2 ].create( ActorType.MISSION1, SWAT, 2846.8499, 925.8057, 9.75 ).set_z_angle( 60.2697 ).task.die();
            friendActors[ 3 ].create( ActorType.MISSION1, SWAT, 2834.6038, 958.3311, 9.75 ).set_z_angle( 0.2697 ).task.die();
            friendActors[ 4 ].create( ActorType.MISSION1, SWAT, 2834.5508, 842.1335, 9.75 ).set_z_angle( 302.3025 ).task.die();
            friendActors[ 5 ].create( ActorType.MISSION1, SWAT, 2816.7439, 926.1787, 9.7578 ).set_z_angle( 300.4459 ).task.die();
            friendActors[ 6 ].create( ActorType.MISSION1, SWAT, 2859.1084, 916.2571, 9.7578 ).set_z_angle( 251.5655 ).task.die();
            friendActors[ 7 ].create( ActorType.MISSION1, SWAT, 2817.689, 956.4893, 9.7578 ).set_z_angle( 38.8335 ).task.die();
            friendActors[ 8 ].create( ActorType.MISSION1, SWAT, 779.2659, 902.1603, 9.7578 ).set_z_angle( 211.4584 ).task.die();
            friendActors[ 9 ].create( ActorType.MISSION1, SWAT, 2768.2334, 914.84, 10.0937 ).set_z_angle( 296.6036 ).task.die();

            wait( 500 );
            __set_entered_names( true );
            __disable_player_controll_in_cutscene( false );
            __fade( true );
            setup_random_terrorist( enemyActors, enemyMarkers );
            setup_random_terrorist( terroristsActors, terroristsMarkers );
            temp3.value = 0;
            tempArrayF[ 2 ].value = 911.0;
            tempArrayF[ 3 ].value = 916.0;
            tempArrayF[ 4 ].value = 921.0;
            is_m6_mission.value = true;
            update_status_text_normalized();
            MISSION_GLOBAL_STATUS_TEXT_1.create( StatusTextType.LINE, 1, "@CRS@44" );
            MISSION_GLOBAL_STATUS_TEXT_2.value = 20;
            MISSION_GLOBAL_STATUS_TEXT_2.create( StatusTextType.NUMBER, 2, "@CRS@45" );
            MISSION_GLOBAL_STATUS_TEXT_3.value = 3;
            MISSION_GLOBAL_STATUS_TEXT_3.create( StatusTextType.NUMBER, 3, "@CRS@47" );
            MISSION_GLOBAL_TIMER_1.value = 300000; // 5 * 60 * 1000
            MISSION_GLOBAL_TIMER_1.start( TimerType.DOWN, "BB_19" );
            Jump += M6_KILL_ALL_TERRORISTS_AND_DISARM_BOMBS;
        }

        private void M6_KILL_ALL_TERRORISTS_AND_DISARM_BOMBS( LabelJump label ) {
            checkpoint.create( 2765.4556, 912.6846, 11.0937 );
            show_text_highpriority( "@CRS@46", 7000, 1 );
            Cycle += delegate {
                wait( 0 );

                checkMel();
                update_status_text_normalized();
                and( 0 >= MISSION_GLOBAL_TIMER_1, delegate { ___jump_failed_message( "@CRS@49" ); } );
                and( !a.is_in_area_2d( false, 2727.3364, 830.3085, 2909.3364, 1040.3086 ), delegate { ___jump_failed_message( "@CRS@08" ); } );

                // TERRORIST COUNTER
                temp1.value = 20;
                enemyActors.each( index, actor => {
                    and( actor.is_dead(), delegate {
                        enemyMarkers[ index ].disable_if_exist();
                        temp1 -= 1;
                    }, delegate {
                        actor.get_task_status( 1506, temp2 );
                        and( temp2 == 7, delegate {
                            temp2.random( 0, 101 );
                            and(
                                31 > temp2,
                                friendActors[ 0 ].is_near_actor_2d( false, a, 4.0, 4.0 )
                            , delegate {
                                actor.task.kill_actor_on_foot( friendActors[ 0 ] );
                            }, delegate {
                                actor.task.kill_actor_on_foot( a );
                            } );
                        } );
                    } );
                } );
                terroristsActors.each( index, actor => {
                    and( actor.is_dead(), delegate {
                        terroristsMarkers[ index ].disable_if_exist();
                        temp1 -= 1;
                    }, delegate {
                        actor.get_task_status( 1506, temp2 );
                        and( temp2 == 7, delegate {
                            temp2.random( 0, 101 );
                            and( 
                                31 > temp2,
                                friendActors[ 0 ].is_near_actor_2d( false, a, 4.0, 4.0 )
                            , delegate {
                                actor.task.kill_actor_on_foot( friendActors[ 0 ] );
                            }, delegate {
                                actor.task.kill_actor_on_foot( a );
                            } );
                        } );
                    } );
                } );
                and( temp1 > 0, delegate {
                    MISSION_GLOBAL_STATUS_TEXT_2.value = temp1;
                }, delegate {
                    MISSION_GLOBAL_STATUS_TEXT_2.remove();
                    temp3.set_bit( 0 );
                    and( temp3.is_bit_set( 5 ), delegate {
                        friendActors[ 0 ].get_task_status( 1762, temp2 );
                        and(
                            temp2 == 7,
                            !friendActors[ 0 ].is_near_object_2d( targetObjects[ 9 ], 2.5, 2.5, false )
                        , delegate {
                            friendActors[ 0 ].task.goto_object( targetObjects[ 9 ], 99999, 2.0 );
                        } );
                    }, delegate {
                        g.set_follow_status( false );
                        friendActors[ 0 ].set_listen_group_commands( false )
                                         .remove_from_group()
                                         .clear_tasks()
                                         .clear_tasks_immediately();
                        g.release();
                        temp3.set_bit( 5 );
                        play_audio_event_at_position( 0.0, 0.0, 0.0, 1058 );
                    } );
                } );

                // BOMB COUNTER
                temp1.value = 3;
                to( index2, 2, 4, h => {
                    and( temp3.is_bit_set( index2 ), delegate {
                        temp1 -= 1;
                        h.@continue();
                    } );
                    draw_corona( 0.5, 9, 0, 255, 128, 0, 2764.7012, tempArrayF[ index2 ], 12.355 );
                    to( index, 0, 2, delegate {
                        targetObjects[ index ].get_z_angle( tempAngle )
                                              .set_rotation( 0.0, 0.0, tempAngle );
                        targetObjects[ index ].get_position( tempX1, tempY1, tempZ1 );
                        and(
                            targetObjects[ index ].is_near_point_3d( 2764.7012, tempArrayF[ index2 ], 12.0, 1.5, 1.5, 1.0, false ),
                            tempZ1 > 11.2
                        , delegate {
                            var tmpTrain = ( Car ) train;
                            tmpTrain.get_position( tempX1, tempY1, tempZ1 );
                            tempY1 -= tempArrayF[ index2 ];
                            targetObjects[ index ].attach( tmpTrain, 0.0, tempY1, -0.5, 0.0, 0.0, 0.0 );
                            friendMarkers[ index ].disable_if_exist();
                            temp3.set_bit( index2 );
                            play_audio_event_at_position( 0.0, 0.0, 0.0, 1058 );
                            h.@break();
                        } );
                    } );
                } );
                and( temp1 > 0, delegate {
                    MISSION_GLOBAL_STATUS_TEXT_3.value = temp1;
                }, delegate {
                    MISSION_GLOBAL_STATUS_TEXT_3.remove();
                    temp3.set_bit( 1 );
                } );
                and( temp3 == 0b111111, delegate { Jump += M6_ENTER_A_TRAIN; } );
            };
        }

        private void M6_ENTER_A_TRAIN( LabelJump label ) {
            checkpoint.disable();
            var tmpTrain = ( Car ) train;
            tmpTrain.set_immunities( false ).set_door_status( DoorStatus.UNLOCKED );
            friendMarkers[ 2 ].create_above_vehicle( tmpTrain ).set_type( true ).set_size( 2 ).set_radar_mode( 2 );
            show_text_highpriority( "@CRS@48", 6000, 1 );
            Cycle += delegate {
                wait( 0 );
                checkMel();
                tmpTrain.do_if_wrecked( delegate { ___jump_failed_message( "@CRS@14" ); } );
                and( train.is_derailed(), delegate { ___jump_failed_message( "@CRS@14" ); } );
                and( 0 >= MISSION_GLOBAL_TIMER_1, delegate { ___jump_failed_message( "@CRS@49" ); } );
                and( a.is_in_vehicle( tmpTrain ), delegate { Jump += M6_END; } );
            };
        }

        private void M6_END( LabelJump label ) {
            MISSION_GLOBAL_TIMER_1.stop();
            MISSION_GLOBAL_STATUS_TEXT_1.remove();
            friendMarkers[ 2 ].disable();
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            __disable_player_controll_in_cutscene( true );
            __fade( 0, true );
            a.teleport_without_car( 2755.4592, 515.2879, 3.1817, 77.4662 );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME );
            gosub( CLEAR_ACTIVE_ENTITIES );
            Gosub += SCENE_6C; 
            wait( 1000 );
            clear_area( true, 2755.4592, 515.2879, 3.1817, 0.0 );
            a.put_at( 2755.4592, 515.2879, 3.1817, 77.4662 );
            __camera_default();
            fade( true, 500 );
            __disable_player_controll_in_cutscene( false );
            wait( 500 );
            reward.value = 15000;
            CRASH_START_X.value = 2370.6443;
            CRASH_START_Y.value = 2547.9795;
            CRASH_START_Z.value = 10.8203;
            CRASH_ICON.value = RadarIconID.CRASH;
            jump_passed();
        }

        private void checkMel( bool withCar = false ) {
            if( withCar ) {
                player_car.do_if_wrecked( delegate {
                    a.stop_facial_talk();
                    ___jump_failed_message( "@CRS@14" );
                } );
                and( !friendActors[ 0 ].is_in_vehicle( player_car ), delegate { friendActors[ 0 ].task.die(); } );
            }
            friendActors[ 0 ].do_if_dead( delegate {
                if( withCar )
                    a.stop_facial_talk();
                ___jump_failed_message( "@CRS@34" );
            } );
        }
        private void setup_random_terrorist( Array<Actor> hActors, Array<Marker> hMarkers ) {
            hActors.each( index, actor => {
                hMarkers[ index ].create_above_actor( actor ).set_type( false ).set_size( 1 ).set_radar_mode( 2 );
                actor.set_suffers_critical_hits( false )
                     .set_acquaintance( AcquaintanceType.HATE, ActorType.PLAYER )
                     .set_acquaintance( AcquaintanceType.HATE, ActorType.MISSION1 )
                     .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION2 );
                temp1.random( 250, 400 );
                actor.set_max_health( temp1 )
                     .set_health( temp1 );
                temp1.random( 0, 101 );
                and( temp1 > 35, delegate {
                    actor.set_drops_weapons_when_dead( false )
                         .set_death_weapons_persist( false );
                } );
                temp1.random( 35, 65 );
                actor.set_weapon_accuracy( temp1 );
                temp1.random( 30, 60 );
                actor.set_weapon_attack_rate( temp1 )
                     .set_decision_maker( enemyDecisionMaker )
                     .task.set_ignore_weapon_range( true );
            } );
            terroristsActors[ 1 ].set_weapon_accuracy( 40 )
                                 .set_weapon_attack_rate( 55 );
        }
        private void update_status_text_normalized() {
            friendActors[ 0 ].get_health( temp1 );
            temp1 /= 9;
            MISSION_GLOBAL_STATUS_TEXT_1.value = temp1;
        }
        
        #endregion

        #region Mission 7

        Int[] M7_USED_MODELS = { MP5LNG, FBI, FBIRANCH, SWAT, COPCARLA, MP5LNG, AK47, M4, WFYST };

        private void MISSION_7( LabelCase l ) {

            johnPath.value = 910;
            melPath.value = 911;
            CRASH_FBI_FLAG.value = 0;
            chdir( @"Sound\CRMISS" );
            AUDIO_BG.load( 999992, true );
            wait( AUDIO_BG.is_ready );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME ).play();

            Gosub += SCENE_7A;
            //Gosub += SCENE_7B_J;
            //Gosub += SCENE_7B_M;

            clear_area( true, 2365.4351, 2548.2319, 14.8785, 100.0 );
            __renderer_at( 2365.4351, 2548.2319, 14.8785 );
            a.put_at( 2365.4351, 2548.2319, 14.8785, 0.0 );
            load_special_actor( "COPJOHN", 1 );
            load_path( johnPath );
            load_path( melPath );
            load_requested_models( M7_USED_MODELS );

            wait( is_special_actor_loaded( 1 ), is_path_available( johnPath ), is_path_available( melPath ) );

            enemyCars[ 0 ].create( COPCARLA, 2342.8955, 2515.7017, 10.8607 ).set_z_angle( 100.8599 );
            enemyCars[ 1 ].create( FBIRANCH, 2368.1697, 2524.2292, 11.0118 ).set_z_angle( 123.9879 );

            to( index, 0, 1, h => {
                enemyCars[ index ].set_can_be_visibly_damaged( false )
                                  .set_tires_vulnerable( true )
                                  .set_door_status( DoorStatus.LOCKED_4 )
                                  .set_immunities( true )
                                  .set_tires_vulnerable( false );
            } );

            var johnCar = enemyCars[ 0 ];
            var melCar = enemyCars[ 1 ];

            enemyActors[ 0 ].create_in_vehicle_driverseat( ActorType.MISSION1, SPECIAL01, johnCar );
            enemyActors[ 1 ].create_in_vehicle_passenger_seat( ActorType.MISSION1, WFYST, johnCar, 1 );
            enemyActors[ 2 ].create_in_vehicle_driverseat( ActorType.MISSION1, FBI, melCar );
            enemyActors[ 3 ].create_in_vehicle_passenger_seat( ActorType.MISSION1, SWAT, melCar, 1 );
            enemyActors[ 4 ].create_in_vehicle_passenger_seat( ActorType.MISSION1, SWAT, melCar, 2 );

            CAMERA.set_position( 2385.0896, 2513.0559, 11.8487 ).set_point_at( 2353.3452, 2519.5208, 10.8203, 2 );
            enable_widescreen( false );

            __fade( true );

            temp1.value = 0;
            friendMarkers[ 0 ].create_above_vehicle( johnCar ).set_type( true );

            panel.create( "@CRS@50", 29.0, 100.0, 250.0, 1, true, true, PanelAlign.LEFT )
                 .set_column_data( 0, sString.DUMMY, "@CRS@51", "@CRS@52" );

            Cycle += delegate {
                wait( 0 );
                and( !is_text_box_displayed( "@CRS@53" ), delegate { show_permanent_text_box( "@CRS@53" ); } );
                panel.get_active_row( temp2 );
                and( temp1 != temp2, delegate {
                    friendMarkers[ 0 ].disable().create_above_vehicle( enemyCars[ temp2 ] ).set_type( true );
                    temp1.value = temp2;
                } );
                and( is_game_key_pressed( Keys.PED_SPRINT ), delegate { Jump += M7_START; } );
            };
        }

        private void M7_START( LabelJump label ) {

            var johnCar = enemyCars[ 0 ];
            var melCar = enemyCars[ 1 ];

            remove_text_box();
            panel.remove();
            friendMarkers[ 0 ].disable();
            CRASH_FBI_FLAG.value = temp2;
            CRASH_FBI_FLAG += 5;
            __fade( 0, true );
            __set_player_ignore( false );
            temp2 -= 1;
            and( 0 > temp2, delegate { temp2 *= -1; } );
            tmpCar.value = enemyCars[ temp2 ];
            enemyMarkers[ 0 ].create_above_vehicle( tmpCar ).set_size( 2 );

            temp2.value = 4;
            and( m7_is_john_friend(), delegate {
                enemyActors[ 5 ].create_in_vehicle_passenger_seat( ActorType.MISSION1, SWAT, melCar, 0 );
                a.put_into_vehicle_as_passenger( johnCar, 2 );
                temp2.value = 5;
                set_sensitivity_to_crime( 1.7 );
                p.set_wanted_level( 4 );
                johnCar.set_health( 41000 );
                melCar.set_health( 25000 );
                temp3.value = 250;
                reward.value = 200000;
            }, delegate {
                a.put_into_vehicle_as_passenger( melCar, 0 );
                p.ignored_by_cops( true );
                johnCar.set_health( 59000 );
                melCar.set_health( 11000 );
                temp3.value = 590;
                reward.value = 10000;
            } );
            to( index, 0, temp2, h => {
                enemyActors[ index ].give_weapon( WeaponNumber.M4, 9999 )
                                    .set_armed_weapon( WeaponNumber.M4 )
                                    .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 )
                                    .set_max_health( 2000 )
                                    .set_health( 2000 )
                                    .set_suffers_critical_hits( false )
                                    .set_cant_be_dragged_out( true )
                                    .set_can_be_knocked_off_bike( true )
                                    .set_immunities( true )
                                    .set_weapon_accuracy( 100 )
                                    .set_weapon_attack_rate( 100 );
            } );

            enemyActors[ 0 ].set_armed_weapon( 0 );
            enemyActors[ 2 ].set_armed_weapon( 0 );

            __camera_default();
            and( m7_is_john_friend(), delegate {
                enemyActors[ 1 ].give_weapon( WeaponNumber.AK47, 9999 )
                                .set_armed_weapon( WeaponNumber.AK47 );
                CAMERA.set_in_front_of_player();
            } );

            a.give_weapon( WeaponNumber.MP5, 800 ).set_armed_weapon( WeaponNumber.MP5 ).get_ammo_in_weapon( WeaponNumber.MP5, temp2 );
            and( 600 > temp2, delegate { a.set_ammo( WeaponNumber.MP5, 800 ); } );

            unload_special_actor( 1 );
            destroy_model( M7_USED_MODELS );

            johnCar.set_immunities( 0, 1, 1, 1, 1 ).start_path( johnPath ).set_path_speed( 0.7 );
            melCar.set_immunities( 0, 1, 1, 1, 1 ).start_path( melPath ).set_path_speed( 0.76 ).enable_siren( true );
            
            normalize_status_text_for_enemy_car();
            MISSION_GLOBAL_STATUS_TEXT_1.create( StatusTextType.LINE, "@CRS@54" );
            enemyActors[ 1 ].task.drive_by( Actor.empty, melCar, 0.0, 0.0, 0.0, 300.0, 8, 0, 100 );
            enemyActors[ 3 ].task.drive_by( Actor.empty, johnCar, 0.0, 0.0, 0.0, 300.0, 8, 0, 100 );
            enemyActors[ 4 ].task.drive_by( Actor.empty, johnCar, 0.0, 0.0, 0.0, 300.0, 8, 1, 100 );
            and( m7_is_john_friend(), delegate {
                enemyActors[ 5 ].task.drive_by( Actor.empty, johnCar, 0.0, 0.0, 0.0, 300.0, 8, 1, 100 );
            } );
            a.set_can_be_knocked_off_bike( true ).task.drive_by( Actor.empty, tmpCar, 0.0, 0.0, 0.0, 300.0, 8, 1, 100 );
            p.set_heading_for_attached( 95.5, 200.0 );
            wait( 1000 );
            __set_entered_names( true );
            __set_traffic( 0.4 );
            AUDIO_BG.set_volume( 1.0 );
            __fade( true );
            __disable_player_controll_in_cutscene( false );
            and( m7_is_john_friend(), delegate {
                show_text_highpriority( "@CRS@57", 6000, 1 );
            }, delegate {
                show_text_highpriority( "@CRS@58", 6000, 1 );
            } );
            Jump += M7_UPDATE_CHASE;
        }

        private void M7_UPDATE_CHASE( LabelJump label ) {

            var johnCar = enemyCars[ 0 ];
            var melCar = enemyCars[ 1 ];

            wait( 0 );
            and( m7_is_john_friend(), delegate {
                melCar.do_if_wrecked( delegate { Jump += M7_END; } );
                and( johnCar.is_near_point_2d( false, 1144.2385, 888.0544, 2.0, 2.0 ), delegate {
                    MISSION_GLOBAL_STATUS_TEXT_1.remove();
                    enemyMarkers[ 0 ].disable();
                    Jump += M7_TO_FAIL_POSITION_JOHN;
                } );
            }, delegate {
                johnCar.do_if_wrecked( delegate { Jump += M7_END; } );
                and( melCar.is_near_point_2d( false, 904.3927, 716.2091, 2.0, 2.0 ), delegate {
                    MISSION_GLOBAL_STATUS_TEXT_1.remove();
                    enemyMarkers[ 0 ].disable();
                    Jump += M7_TO_FAIL_POSITION_MEL;
                } );
            } );
            normalize_status_text_for_enemy_car();
            m7_control_john_car_speed();
            m7_control_mel_car_speed();
            jump( label );
        }

        private void M7_TO_FAIL_POSITION_JOHN( LabelJump label ) {
            enemyCars[ 0 ].set_immunities( true ).repair();
            enemyCars[ 1 ].set_immunities( true ).repair();
            a.set_immunities( true );
            p.can_move( false );
            __fade( 0, true );
            enemyCars[ 0 ].pause_path();
            Jump += M7_TO_FAIL_POSITION_END;
        }
        private void M7_TO_FAIL_POSITION_MEL( LabelJump label ) {
            enemyCars[ 0 ].set_immunities( true ).repair();
            enemyCars[ 1 ].set_immunities( true ).repair();
            __toggle_cinematic( true );
            a.set_immunities( true );
            p.can_move( false );
            CAMERA.set_position( 865.1666, 622.1799, 23.6718 )
                  .set_point_on_vehicle( enemyCars[ 1 ], 15, 1 )
                  .set_point_on_vehicle( enemyCars[ 1 ], 15, 2 );
            wait( enemyCars[ 1 ].is_in_water() );
            wait( 5000 );
            __fade( 0, true );
            enemyCars[ 0 ].pause_path();
            Jump += M7_TO_FAIL_POSITION_END;
        }
        private void M7_TO_FAIL_POSITION_END( LabelJump label ) {
            clear_area( true, 861.0975, 680.951, 11.9127, 2.0 );
            a.clear_tasks().clear_tasks_immediately().teleport_without_car( 861.0975, 680.951, 11.9127, 4.1669 );
            __toggle_cinematic( false );
            wait( 1000 );
            __camera_default();
            __fade( 1, true );
            a.set_immunities( false );
            p.can_move( true );
            and( m7_is_john_friend(), delegate {
                ___jump_failed_message( "@CRS@55" );
            } );
            ___jump_failed_message( "@CRS@56" );
        }

        private void M7_END( LabelJump label ) {
            MISSION_GLOBAL_STATUS_TEXT_1.remove();
            enemyMarkers[ 0 ].disable();
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            __disable_player_controll_in_cutscene( true );
            __fade( 0, true );
            clear_area( 1, 775.935, 724.6248, 25.2648, 2.0 );
            a.clear_tasks().clear_tasks_immediately().teleport_without_car( 775.935, 724.6248, 25.2648, 0.0 );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME );
            gosub( CLEAR_ACTIVE_ENTITIES );
            wait( 1000 );
            and( m7_is_john_friend(), delegate {
                Gosub += SCENE_7B_J;
                clear_area( true, -1386.5468, -348.9564, 13.1484, 0.0 );
                a.put_at( -1386.5468, -348.9564, 13.1484, 150.6914 );
            }, delegate {
                Gosub += SCENE_7B_M;
                clear_area( true, 432.9172, 614.9666, 18.2925, 0.0 );
                a.put_at( 432.9172, 614.9666, 18.2925, 80.8175 );
            } );
            __camera_default();
            wait( 500 );
            fade( true, 500 );
            __disable_player_controll_in_cutscene( false );
            wait( 500 );
            CRASH_FBI_FLAG.value = 0;
            jump_passed();
        }

        private Condition m7_is_john_friend() => CRASH_FBI_FLAG == 5;
        private void normalize_status_text_for_enemy_car() {
            tmpCar.get_health( temp1 );
            temp1 /= temp3;
            MISSION_GLOBAL_STATUS_TEXT_1.value = temp1;
        }
        private void m7_control_john_car_speed() {
            and( enemyCars[ 0 ].is_near_point_2d( false, 2247.6694, 2513.1118, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.8 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 2220.1733, 2363.3208, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 2062.1589, 2322.4963, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.8 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 2017.2786, 2392.2583, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 1897.2264, 2293.3486, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 1613.3016, 2272.0312, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.8 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 1536.6481, 2317.0481, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 1380.7388, 2333.0264, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.9 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 1443.2782, 2444.1008, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.8 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 1382.4327, 2475.5146, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.8 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 1284.0574, 2425.771, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 1205.1655, 2237.2349, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.8 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 1207.9871, 1950.4336, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.8 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 1204.8982, 1619.2277, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 1204.4657, 1308.5847, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 0.8 );
            } );
            and( enemyCars[ 0 ].is_near_point_2d( false, 787.2744, 665.5538, 2.0, 2.0 ), delegate {
                enemyCars[ 0 ].set_path_speed( 1.0 );
            } );
        }
        private void m7_control_mel_car_speed() {
            and( enemyCars[ 1 ].is_near_point_2d( false, 2239.3494, 2513.6897, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 2189.8662, 2348.4993, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 2104.6575, 2340.1838, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 2037.7318, 2348.3462, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.6 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1936.6909, 2394.6707, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1866.9294, 2272.8589, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1708.3767, 2271.2874, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1587.6283, 2272.137, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1529.1652, 2315.7615, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.8 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1386.6962, 2341.4468, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.88 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1417.6074, 2456.3643, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.52 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1284.4127, 2422.2039, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.67 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1207.7909, 2220.7036, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.9 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1210.4625, 2015.6985, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.95 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1210.3379, 1755.5181, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.8 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1210.1821, 1491.0479, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.9 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1209.9908, 1161.4871, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.8 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 1101.2146, 832.3546, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.7 );
            } );
            and( enemyCars[ 1 ].is_near_point_2d( false, 920.9622, 726.3133, 2.0, 2.0 ), delegate {
                enemyCars[ 1 ].set_path_speed( 0.9 );
            } );
        }

        #endregion

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
                john.start_facial_talk( 19000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 19000 );
                show_text_highpriority( "@CR@000", 7000, 1 );
                wait( 7000 );

                AUDIO_PL.play(); // 1
                show_text_highpriority( "@CR@001", 7000, 1 );
                wait( 7000 );

                AUDIO_PL.play(); // 2
                show_text_highpriority( "@CR@002", 5000, 1 );
                wait( 5000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 3
                player.start_facial_talk( 4000 ).task.perform_animation( "endchat_01", "PED", 4.0, 0, 0, 0, 0, 2000 );
                show_text_highpriority( "@CR@003", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 4
                john.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
                show_text_highpriority( "@CR@004", 4500, 1 );
                wait( 4500 );
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
                john.start_facial_talk( 3000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 3000 );
                show_text_highpriority( "@CR@017", 3000, 1 );
                wait( 3000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 1
                player.start_facial_talk( 2000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 2000 );
                show_text_highpriority( "@CR@018", 2000, 1 );
                wait( 2000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 2
                john.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@CR@019", 4000, 1 );
                wait( 4000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 3
                player.start_facial_talk( 3000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 3000 );
                show_text_highpriority( "@CR@020", 3000, 1 );
                wait( 3000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 4
                john.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 5000 );
                show_text_highpriority( "@CR@021", 5000, 1 );
                wait( 5000 );
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
            enemyActors[ 1 ].task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 60000 );
            enemyActors[ 2 ].task.look_at_actor( enemyActors[ 0 ], -1 );
            enemyActors[ 3 ].task.look_at_actor( enemyActors[ 1 ], -1 );
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

        private void SCENE_4B( LabelGosub label ) {
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

        private void SCENE_4A( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];

            chdir( @"Sound\CRMISS\4A" );
            AUDIO_PL.load( 5 );
            wait( AUDIO_PL.is_ready );
            __toggle_cinematic( true );
            load_requested_models( CELLPHONE );
            clear_area( 1, 1980.228, 154.8453, 33.2473, 300.0 );
            player.create( ActorType.MISSION1, NULL, 1980.228, 154.8453, 33.2473 )
                  .set_z_angle( 131.6025 )
                  .task.hold_cellphone( true );
            CAMERA.set_position( 1977.042, 151.7121, 33.3706 ).set_point_at( 1980.228, 154.8453, 33.2473, 2 );
            wait( 1000 );
            __fade( true, false );
            Scene += delegate {
                wait( 1500 );

                AUDIO_PL.play(); // 0
                player.start_facial_talk( 4000 );
                show_text_highpriority( "@CR@022", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 1
                show_text_highpriority( "@CR@023", 5500, 1 );
                wait( 5500 );

                AUDIO_PL.play(); // 2
                player.start_facial_talk( 4000 );
                show_text_highpriority( "@CR@024", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 3
                show_text_highpriority( "@CR@025", 5500, 1 );
                wait( 5500 );

                AUDIO_PL.play(); // 4
                player.start_facial_talk( 4000 );
                show_text_highpriority( "@CR@026", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk().task.hold_cellphone( false );

                wait( 1000 );
            };
            __fade( false, true );
            __toggle_cinematic( false );
            destroy_model( CELLPHONE );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_5C( LabelGosub label ) {

            var player = cutcsene_actors[ 0 ];
            var john = cutcsene_actors[ 1 ];

            __toggle_cinematic( true );
            clear_area( 1, 2360.8169, 2549.6118, 15.8785, 300.0 );
            a.put_at( 2360.8169, 2549.6118, 15.8785 );
            wait( 1000 );
            __renderer_at( 2374.3628, 2534.7563, 9.8203 );
            load_special_actor( "COPJOHN", 1 );
            chdir( @"Sound\CRMISS\5C" );
            AUDIO_PL.load( 13 );
            wait( is_special_actor_loaded( 1 ), AUDIO_PL.is_ready );

            player.create( ActorType.MISSION1, NULL, 2374.3628, 2534.7563, 9.8203 )
                  .set_z_angle( 26.0302 )
                  .store_coords( tempX1, tempY1, tempZ1, -4.0, 1.0, 0.0 )
                  .store_coords( tempX2, tempY2, tempZ2, 2.0, 1.0, 0.0 );
            john.create( ActorType.MISSION1, SPECIAL01, 2368.8728, 2540.1721, 9.8203 ).set_z_angle( 235.7738 );

            player.task.look_at_actor( john, -1 );
            john.task.look_at_actor( player, -1 );
            unload_special_actor( 1 );

            CAMERA.attach_to_actor_look_at_actor( player, -2.0, -5.0, 1.5, john, 0.0, 2 );

            wait( 1000 );
            __fade( true, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                john.start_facial_talk( 4000 ).task.walk_to_point_and_perform_animation( 2373.3987, 2535.7458, 10.8203, 215.262, 1.0, "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@CR@051", 4000, 1 );
                wait( 4000 );
                john.stop_facial_talk();

                CAMERA.set_position( tempX1, tempY1, tempZ1 ).set_point_at( tempX2, tempY2, tempZ2, 2 );

                john.task.rotate_to_actor( player );
                player.task.rotate_to_actor( john );

                AUDIO_PL.play(); // 1
                player.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@CR@052", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk()
                      .store_coords( tempX1, tempY1, tempZ1, -4.0, 1.0, 0.0 )
                      .store_coords( tempX2, tempY2, tempZ2, 2.0, 1.0, 0.0 );

                AUDIO_PL.play(); // 2
                john.start_facial_talk( 6000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 6000 );
                show_text_highpriority( "@CR@053", 6000, 1 );
                wait( 6000 );
                john.stop_facial_talk();

                CAMERA.set_position( tempX2, tempY2, tempZ2 ).set_point_at( tempX1, tempY1, tempZ1, 2 );

                AUDIO_PL.play(); // 3
                player.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@CR@054", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 4
                john.start_facial_talk( 27000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 27000 );
                show_text_highpriority( "@CR@055", 5000, 1 );
                wait( 5000 );

                john.store_coords( tempX1, tempY1, tempZ1, 4.0, 1.0, 0.0 )
                    .store_coords( tempX2, tempY2, tempZ2, -2.0, 1.0, 0.0 );
                CAMERA.set_position( tempX1, tempY1, tempZ1 ).set_point_at( tempX2, tempY2, tempZ2, 2 );

                AUDIO_PL.play(); // 5
                show_text_highpriority( "@CR@056", 5000, 1 );
                wait( 5000 );

                AUDIO_PL.play(); // 6
                show_text_highpriority( "@CR@057", 6000, 1 );
                wait( 6000 );

                CAMERA.set_position( tempX2, tempY2, tempZ2 ).set_point_at( tempX1, tempY1, tempZ1, 2 );

                AUDIO_PL.play(); // 7
                show_text_highpriority( "@CR@058", 6000, 1 );
                wait( 6000 );

                AUDIO_PL.play(); // 8
                show_text_highpriority( "@CR@059", 5000, 1 );
                wait( 5000 );
                john.stop_facial_talk();

                CAMERA.set_position( tempX1, tempY1, tempZ1 ).set_point_at( tempX2, tempY2, tempZ2, 2 );

                AUDIO_PL.play(); // 9
                player.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@CR@060", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 10
                john.start_facial_talk( 10000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 10000 );
                show_text_highpriority( "@CR@061", 5000, 1 );
                wait( 5000 );

                AUDIO_PL.play(); // 11
                show_text_highpriority( "@CR@062", 5000, 1 );
                wait( 5000 );
                john.stop_facial_talk();

                player.store_coords( tempX1, tempY1, tempZ1, 1.0, 4.0, 0.0 )
                      .store_coords( tempX2, tempY2, tempZ2, 1.0, -4.0, 0.0 );
                CAMERA.set_position( tempX1, tempY1, tempZ1 ).set_point_at( tempX2, tempY2, tempZ2, 2 );

                AUDIO_PL.play(); // 12
                player.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@CR@063", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                wait( 1000 );
            };
            __fade( false, true );
            __toggle_cinematic( false );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_5B( LabelGosub label ) {

            Int[] usedModels = { FBI, POLMAV };

            Actor player = cutcsene_actors[ 0 ];
            Actor mel = cutcsene_actors[ 1 ];
            Car heli = cutcsene_cars[ 0 ];

            __toggle_cinematic( true );
            clear_area( true, 2310.6858, 2445.8228, 10.8935, 300.0 );
            __renderer_at( 2370.6177, 2535.188, 10.8203 );
            a.put_at( 2324.8923, 2438.2585, 16.4844, 180.0 );
            wait( 500 );
            chdir( @"Sound\CRMISS\5B" );
            AUDIO_PL.load( 10 );
            wait( AUDIO_PL.is_ready );
            load_requested_models( usedModels );

            heli.create( POLMAV, 2310.6858, 2445.8228, 10.8935 ).set_z_angle( 155.0 );

            mel.create( ActorType.MISSION1, FBI, 2314.739, 2443.5134, 9.8203 ).set_z_angle( 144.0 )
               .store_coords( tempX1, tempY1, tempZ1, 0.0, 1.0, -0.5 );

            player.create( ActorType.MISSION1, NULL, tempX1, tempY1, tempZ1 ).set_z_angle( 324.0 );

            destroy_model( usedModels );
            wait( 1000 );
            player.store_coords( tempX1, tempY1, tempZ1, 0.0, 0.5, 0.0 );
            mel.store_coords( tempX2, tempY2, tempZ2, 3.0, 0.0, 0.0 );
            CAMERA.set_position( tempX2, tempY2, tempZ2 ).set_point_at( tempX1, tempY1, tempZ1, 2 );
            __fade( true, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                mel.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@CR@064", 4000, 1 );
                wait( 4000 );
                mel.stop_facial_talk();

                AUDIO_PL.play(); // 1
                player.start_facial_talk( 3500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 3500 );
                show_text_highpriority( "@CR@065", 3500, 1 );
                wait( 3500 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 2
                mel.start_facial_talk( 39000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 39000 );
                show_text_highpriority( "@CR@066", 6000, 1 );
                wait( 6000 );

                AUDIO_PL.play(); // 3
                show_text_highpriority( "@CR@067", 7000, 1 );
                wait( 7000 );

                AUDIO_PL.play(); // 4
                show_text_highpriority( "@CR@068", 5000, 1 );
                wait( 5000 );

                AUDIO_PL.play(); // 5
                show_text_highpriority( "@CR@069", 6000, 1 );
                wait( 6000 );

                AUDIO_PL.play(); // 6
                show_text_highpriority( "@CR@070", 7000, 1 );
                wait( 7000 );

                AUDIO_PL.play(); // 7
                show_text_highpriority( "@CR@071", 8000, 1 );
                wait( 8000 );
                mel.stop_facial_talk();

                AUDIO_PL.play(); // 8
                player.start_facial_talk( 2000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 2000 );
                show_text_highpriority( "@CR@072", 2000, 1 );
                wait( 2000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 9
                mel.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 7000 );
                show_text_highpriority( "@CR@073", 7000, 1 );
                wait( 7000 );
                mel.stop_facial_talk();

                wait( 1000 );
            };
            __fade( false, true );
            __toggle_cinematic( false );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_6A( LabelGosub label ) {

            Int[] usedModels = { FBI, FBIRANCH };

            Actor player = cutcsene_actors[ 0 ];
            Actor mel = cutcsene_actors[ 1 ];
            Car fbicar = cutcsene_cars[ 0 ];

            __toggle_cinematic( true );
            clear_area( true, 2303.0903, 2459.2107, 9.8203, 300.0 );
            __renderer_at( 2303.0903, 2459.2107, 9.8203 );
            a.put_at( 2303.0903, 2459.2107, 9.8203, 206.1753 );
            wait( 500 );
            chdir( @"Sound\CRMISS\6A" );
            AUDIO_PL.load( 1 );
            wait( AUDIO_PL.is_ready );
            load_requested_models( usedModels );

            fbicar.create( FBIRANCH, 2312.1667, 2449.2207, 10.4306 )
                  .set_z_angle( 154.9585 );

            mel.create_in_vehicle_passenger_seat( ActorType.MISSION1, FBI, fbicar, 1 );

            player.create( ActorType.MISSION1, NULL, 2308.6233, 2447.5957, 9.8203 ).set_z_angle( 300.7795 );

            destroy_model( usedModels );
            wait( 1000 );
            CAMERA.set_position( 2308.4143, 2453.7427, 10.8203 ).set_point_at( 2310.3921, 2449.2397, 10.8203, 2 );
            __fade( true, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                mel.start_facial_talk( 4000 );
                show_text_highpriority( "@CR@080", 4000, 1 );
                wait( 4000 );
                mel.stop_facial_talk();

                wait( 1000 );
            };
            __fade( false, true );
            __toggle_cinematic( false );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_6B( LabelGosub label ) {

            Int[] usedModels = { FBI, FBIRANCH, SWAT, M4, HMOST, SWMYHP2, MP5LNG, AK47 };

            Actor player = cutcsene_actors[ 0 ];
            Actor mel = cutcsene_actors[ 1 ];
            Actor swat = cutcsene_actors[ 2 ];
            Actor terrorist1 = cutcsene_actors[ 3 ];
            Actor terrorist2 = cutcsene_actors[ 4 ];

            Car fbicar = cutcsene_cars[ 0 ];

            __toggle_cinematic( true );
            clear_area( true, 2771.0461, 924.2379, 10.8984, 300.0 );
            __renderer_at( 2771.0461, 924.2379, 10.8984 );
            wait( 500 );
            chdir( @"Sound\CRMISS\6B" );
            AUDIO_PL.load( 6 );
            wait( AUDIO_PL.is_ready );
            load_requested_models( usedModels );

            fbicar.create( FBIRANCH, 2766.2629, 884.5315, 10.4982 ).set_immunities( true, true, 0, 0, true );
            
            player.create( ActorType.MISSION1, NULL, 2771.0461, 924.2379, 9.8984 ).set_z_angle( 51.2613 ).task.crouch( true );
             
            mel.create( ActorType.MISSION1, FBI, 2770.7444, 926.152, 9.8984 ).set_z_angle( 186.4117 )
                .give_weapon( WeaponNumber.M4, 30 ).set_armed_weapon( WeaponNumber.M4 ).task.crouch( true );

            swat.create( ActorType.MISSION1, SWAT, 2768.2334, 914.84, 10.0937 ).set_z_angle( 296.6036 )
                .give_weapon( WeaponNumber.MP5, 9999 ).set_armed_weapon( WeaponNumber.MP5 )
                .set_weapon_attack_rate( 30 );

            terrorist1.create( ActorType.MISSION1, SWMYHP2, 2789.9006, 928.4091, 9.75 ).set_z_angle( 121.617 )
                      .give_weapon( WeaponNumber.AK47, 9999 ).set_armed_weapon( WeaponNumber.AK47 ).set_weapon_attack_rate( 40 );

            terrorist2.create( ActorType.MISSION1, HMOST, 2796.772, 902.8798, 9.75 ).set_z_angle( 48.8013 )
                      .give_weapon( WeaponNumber.MP5, 9999 ).set_armed_weapon( WeaponNumber.MP5 ).set_weapon_attack_rate( 35 );

            destroy_model( usedModels );
            to( index, 0, 4, h => {
                cutcsene_actors[ index ].set_muted( true )
                                        .set_immunities( true )
                                        .set_weapon_accuracy( 100 )
                                        .enable_validate_position( true )
                                        .set_stay_in_vehicle_when_jacked( true )
                                        .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 );
            } );

            terrorist1.task.shoot_at_actor( swat, 50000 );
            terrorist2.task.shoot_at_actor( swat, 50000 );
            swat.task.shoot_at_actor( terrorist1, 30000 );
            mel.task.clear_look_at().stay_put( true );
            player.task.clear_look_at().stay_put( true );

            CAMERA.set_position( 2791.8818, 928.5994, 10.8 ).set_point_at( 2768.2334, 914.84, 10.0937, 2 );
            wait( 1000 );
            __fade( true, false );
            Scene += delegate {
                wait( 1500 );

                AUDIO_PL.play(); // 0
                terrorist1.start_facial_talk( 12000 );
                show_text_highpriority( "@CR@082", 4000, 1 );
                wait( 4000 );

                AUDIO_PL.play(); // 1
                show_text_highpriority( "@CR@083", 6000, 1 );
                wait( 3000 );
                swat.task.dead();
                terrorist1.clear_tasks().clear_tasks_immediately().task.shoot_at( 2768.2334, 914.84, 10.0937, 50000 );
                terrorist2.clear_tasks().clear_tasks_immediately().task.shoot_at( 2766.2629, 884.5315, 10.4982, 50000 );
                wait( 3000 );
                terrorist1.stop_facial_talk();

                CAMERA.set_position( 2768.3489, 926.9156, 10.3984 ).set_point_at( 2770.8152, 925.2829, 10.3984, 2 );

                AUDIO_PL.play(); // 2
                player.start_facial_talk( 3000 );
                show_text_highpriority( "@CR@084", 3000, 1 );
                wait( 3000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 3
                mel.start_facial_talk( 12000 );
                show_text_highpriority( "@CR@085", 7000, 1 );
                wait( 7000 );

                AUDIO_PL.play(); // 4
                show_text_highpriority( "@CR@086", 5000, 1 );
                wait( 2000 );
                CAMERA.set_position( 2771.082, 928.8619, 11.3984 ).set_point_at( 2769.1467, 909.7482, 10.3984, 1 );
                wait( 3000 );
                mel.stop_facial_talk();

                wait( 1000 );
                mel.task.look_at_car( fbicar, 10000 );
                fbicar.explode_in_cutscene();
                wait( 1000 );

                AUDIO_PL.play(); // 5
                player.start_facial_talk( 3000 ).task.look_at_car( fbicar, 10000 );
                show_text_highpriority( "@CR@087", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                wait( 1000 );
            };
            __fade( false, true );
            __toggle_cinematic( false );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_6C( LabelGosub label ) {

            Actor player = cutcsene_actors[ 0 ];
            Actor mel = cutcsene_actors[ 1 ];
            Car train = cutcsene_cars[ 0 ];
            Train tmpTrain = local( Int.IndexOf( cutcsene_cars[ 0 ] ) );

            __toggle_cinematic( true );
            clear_area( true, 2778.8447, 511.1415, 3.8413, 300.0 );
            __renderer_at( 2778.8447, 511.1415, 3.8413 );
            a.put_at( 2778.8447, 511.1415, 3.8413, 206.1753 );
            wait( 500 );
            chdir( @"Sound\CRMISS\6C" );
            AUDIO_PL.load( 1 );
            wait( AUDIO_PL.is_ready );

            var usedModels = Train.GetModelsByType( M6_TRAIN_TYPE );
            System.Array.Resize( ref usedModels, usedModels.Length + 2 );
            usedModels[ usedModels.Length - 2 ] = FBI;
            usedModels[ usedModels.Length - 1 ] = ObjectModel.BARREL1;
            
            load_requested_models( usedModels );

            tmpTrain.create( 2764.8235, 664.4359, 9.6747, M6_TRAIN_TYPE, 1 );
            train.set_immunities( true ).get_driver( tempActor );
            tempActor.destroy_if_exist();

            mel.create( ActorType.MISSION1, FBI, 2764.8289, 682.9635, 10.7229 ).set_z_angle( 92.4377 )
                .set_immunities( true );

            player.create_in_vehicle_driverseat( ActorType.MISSION1, NULL, train )
                  .task.drive_car_to_point( train, 2765.3542, 301.6933, 8.2742, 18.0, 0, 0, 2 );

            tmpTrain.set_cruise_speed( 18.0 ).set_speed( 18.0 );

            cutcsene_objects[ 0 ].create( ObjectModel.BARREL1, 0.0, 0.0, 0.0 ).attach( train, 0.0, -12.0, -0.4, 0.0, 0.0, 0.0 );
            cutcsene_objects[ 1 ].create( ObjectModel.BARREL1, 0.0, 0.0, 0.0 ).attach( train, 0.0, -17.0, -0.4, 0.0, 0.0, 0.0 );
            cutcsene_objects[ 2 ].create( ObjectModel.BARREL1, 0.0, 0.0, 0.0 ).attach( train, 0.0, -22.0, -0.4, 0.0, 0.0, 0.0 );

            destroy_model( usedModels );
            CAMERA.set_position( 2738.1899, 481.7682, 16.6488 ).set_point_at( 2764.8235, 664.4359, 9.6747, 2 );
            wait( 250 );
            CAMERA.set_point_on_vehicle( train, 15, 1 );
            wait( 750 );
            __fade( true, false );
            Scene += delegate {
                wait( 3500 );
                AUDIO_PL.play(); // 0
                show_text_highpriority( "@CR@081", 4000, 1 );
                wait( 4250 );
                mel.task.jump( true );
                wait( 1100 );
                cutcsene_objects[ 2 ].get_position( tempX1, tempY1, tempZ1 ).set_visibility( false );
                create_explosion( ExplosionType.LARGE_8, tempX1, tempY1, tempZ1 );
                wait( 250 );
                cutcsene_objects[ 1 ].get_position( tempX1, tempY1, tempZ1 ).set_visibility( false );
                create_explosion( ExplosionType.LARGE_8, tempX1, tempY1, tempZ1 );
                wait( 250 );
                cutcsene_objects[ 0 ].get_position( tempX1, tempY1, tempZ1 ).set_visibility( false );
                create_explosion( ExplosionType.LARGE_8, tempX1, tempY1, tempZ1 );
                wait( 500 );
                train.get_position( tempX1, tempY1, tempZ1 );
                create_explosion( ExplosionType.LARGE_8, tempX1, tempY1, tempZ1 );
                wait( 500 );
                train.get_position( tempX1, tempY1, tempZ1 );
                create_explosion( ExplosionType.LARGE_8, tempX1, tempY1, tempZ1 );
                wait( 1000 );
            };
            __fade( false, true );
            __toggle_cinematic( false );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            player.destroy_if_exist();
            tmpTrain.destroy_if_exist();
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_7A( LabelGosub label ) {

            Int[] usedModels = { MP5LNG, FBI, SWAT, MP5LNG, WFYST };

            Actor player = cutcsene_actors[ 0 ];
            Actor john = cutcsene_actors[ 1 ];
            Actor mel = cutcsene_actors[ 2 ];
            Actor marcy = cutcsene_actors[ 3 ];
            Actor swat1 = cutcsene_actors[ 4 ];
            Actor swat2 = cutcsene_actors[ 5 ];

            __toggle_cinematic( true );
            clear_area( true, 2363.6682, 2548.1548, 14.8785, 300.0 );
            __renderer_at( 2363.6682, 2548.1548, 14.8785 );
            a.put_at( 2363.6682, 2548.1548, 14.8785, 206.1753 );
            load_special_actor( "COPJOHN", 1 );
            load_requested_models( usedModels );
            wait( 500 );
            chdir( @"Sound\CRMISS\7A" );
            AUDIO_PL.load( 4 );
            wait( AUDIO_PL.is_ready, is_special_actor_loaded( 1 ) );

            player.create( ActorType.MISSION1, NULL, 2369.2771, 2533.5, 9.8203 ).set_z_angle( 270.0 );
            john.create( ActorType.MISSION1, SPECIAL01, 2370.7437, 2533.5, 9.8203 ).set_z_angle( 90.0 );
            mel.create( ActorType.MISSION1, FBI, 2370.7437, 2546.0989, 9.8203 ).set_z_angle( 180.0 );
            marcy.create( ActorType.MISSION1, WFYST, 2370.2178, 2532.1067, 9.8203 ).set_z_angle( 0.0 );

            swat1.create( ActorType.MISSION1, SWAT, 2368.8118, 2545.7102, 9.8203 ).set_z_angle( 180.0 )
                 .give_weapon( WeaponNumber.MP5, 9999 ).set_armed_weapon( WeaponNumber.MP5 )
                 .task.crouch( true ).aim_at_actor( john, -1 );

            swat2.create( ActorType.MISSION1, SWAT, 2373.8711, 2545.21, 9.8203 ).set_z_angle( 161.5444 )
                 .give_weapon( WeaponNumber.MP5, 9999 ).set_armed_weapon( WeaponNumber.MP5 )
                 .task.crouch( true ).aim_at_actor( john, -1 );

            to( index, 0, 5, h => {
                cutcsene_actors[ index ].set_immunities( true ).set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 );
            } );

            destroy_model( usedModels );
            unload_special_actor( 1 );
            wait( 1000 );
            CAMERA.set_position( 2370.4312, 2536.2625, 10.8203 ).set_point_at( 2370.2178, 2532.1067, 10.8203, 2 );
            __fade( true, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                john.start_facial_talk( 12000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 12000 );
                show_text_highpriority( "@CR@088", 7000, 1 );
                wait( 7000 );

                AUDIO_PL.play(); // 1
                show_text_highpriority( "@CR@089", 5000, 1 );
                wait( 5000 );
                john.stop_facial_talk();

                wait( 750 );
                CAMERA.set_position( 2375.418, 2543.2854, 10.8203 ).set_point_at( 2370.7046, 2545.1643, 10.8203, 2 );
                wait( 750 );

                AUDIO_PL.play(); // 2
                mel.start_facial_talk( 7000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 7000 );
                john.task.rotate_to_actor( mel );
                marcy.task.look_at_actor( mel, 10000 );
                player.task.rotate_to_actor( mel );
                show_text_highpriority( "@CR@090", 7000, 1 );
                wait( 7000 );
                mel.stop_facial_talk();

                CAMERA.set_position( 2370.4312, 2536.2625, 10.8203 ).set_point_at( 2370.2178, 2532.1067, 10.8203, 2 );

                AUDIO_PL.play(); // 3
                john.start_facial_talk( 8000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 8000 );
                show_text_highpriority( "@CR@091", 8000, 1 );
                wait( 8000 );
                john.stop_facial_talk();

                wait( 1000 );
            };
            __fade( false, true );
            __toggle_cinematic( false );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_7B_J( LabelGosub label ) {

            Int[] usedModels = { WFYST };

            Actor player = cutcsene_actors[ 0 ];
            Actor john = cutcsene_actors[ 1 ];
            Actor marcy = cutcsene_actors[ 2 ];

            __toggle_cinematic( true );
            clear_area( true, -1386.3844, -331.8712, 24.4375, 300.0 );
            __renderer_at( -1386.3844, -331.8712, 24.4375 );
            a.put_at( -1386.3844, -331.8712, 24.4375, 206.1753 );
            load_special_actor( "COPJOHN", 1 );
            load_requested_models( usedModels );
            wait( 500 );
            chdir( @"Sound\CRMISS\7BJ" );
            AUDIO_PL.load( 5 );
            wait( AUDIO_PL.is_ready, is_special_actor_loaded( 1 ) );

            player.create( ActorType.MISSION1, NULL, -1389.067, -329.4352, 13.1484  ).set_z_angle( 26.9704 );
            john.create( ActorType.MISSION1, SPECIAL01, -1389.8976, -327.9271, 13.1484 ).set_z_angle( 204.6087 );
            marcy.create( ActorType.MISSION1, WFYST, -1392.1089, -329.324, 13.1484 ).set_z_angle( 233.1223 );

            to( index, 0, 2, h => {
                cutcsene_actors[ index ].set_immunities( true ).set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 );
            } );

            john.task.rotate_to_actor( player );
            player.task.rotate_to_actor( john );
            marcy.task.rotate_to_actor( john );

            destroy_model( usedModels );
            unload_special_actor( 1 );
            wait( 1000 );
            CAMERA.set_position( -1394.1989, -333.6159, 14.1411 ).set_point_at( -1389.8976, -327.9271, 14.1484, 2 );
            __fade( true, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                john.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 5000 );
                show_text_highpriority( "@CR@092", 5000, 1 );
                wait( 5000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 1
                player.start_facial_talk( 1500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 1500 );
                show_text_highpriority( "@CR@093", 1500, 1 );
                wait( 1500 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 2
                john.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 5000 );
                show_text_highpriority( "@CR@094", 5000, 1 );
                wait( 5000 );
                john.stop_facial_talk();

                AUDIO_PL.play(); // 3
                player.start_facial_talk( 3000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 3000 );
                show_text_highpriority( "@CR@095", 3000, 1 );
                wait( 3000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 4
                marcy.start_facial_talk( 2000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 2000 );
                show_text_highpriority( "@CR@096", 2000, 1 );
                wait( 2000 );
                marcy.stop_facial_talk();

                wait( 1000 );
            };
            __fade( false, true );
            __toggle_cinematic( false );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_7B_M( LabelGosub label ) {

            Int[] usedModels = { FBI };

            Actor player = cutcsene_actors[ 0 ];
            Actor mel = cutcsene_actors[ 1 ];

            __toggle_cinematic( true );
            clear_area( true, 410.2709, 610.4506, 18.1636, 300.0 );
            __renderer_at( 410.2709, 610.4506, 18.1636 );
            a.put_at( 410.2709, 610.4506, 18.1636, 206.1753 );
            load_requested_models( usedModels );
            wait( 500 );
            chdir( @"Sound\CRMISS\7BM" );
            AUDIO_PL.load( 5 );
            wait( AUDIO_PL.is_ready );

            player.create( ActorType.MISSION1, NULL, 428.8662, 618.6672, 18.1696 ).set_z_angle( 214.6355 );
            mel.create( ActorType.MISSION1, FBI, 429.7695, 616.8617, 19.1638 ).set_z_angle( 36.9972 );

            to( index, 0, 1, h => {
                cutcsene_actors[ index ].set_immunities( true ).set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 );
            } );

            destroy_model( usedModels );
            wait( 1000 );
            CAMERA.set_position( 424.1183, 617.882, 19.1595 ).set_point_at( 429.1049, 617.882, 19.1595, 2 );
            __fade( true, false );
            Scene += delegate {
                wait( 500 );

                AUDIO_PL.play(); // 0
                mel.start_facial_talk( 7000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 7000 );
                show_text_highpriority( "@CR@097", 7000, 1 );
                wait( 7000 );
                mel.stop_facial_talk();

                AUDIO_PL.play(); // 1
                player.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@CR@098", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 2
                mel.start_facial_talk( 8000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 8000 );
                show_text_highpriority( "@CR@099", 8000, 1 );
                wait( 8000 );
                mel.stop_facial_talk();

                CAMERA.set_position( 427.3363, 612.6359, 18.9362 ).set_point_at( 428.8662, 618.6672, 19.1696, 2 );

                AUDIO_PL.play(); // 3
                player.start_facial_talk( 4000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4000 );
                show_text_highpriority( "@CR@100", 4000, 1 );
                wait( 4000 );
                player.stop_facial_talk();

                AUDIO_PL.play(); // 4
                mel.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 5000 );
                show_text_highpriority( "@CR@101", 5000, 1 );
                wait( 5000 );
                mel.stop_facial_talk();

                wait( 1000 );
            };
            __fade( false, true );
            __toggle_cinematic( false );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
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

        private void M6_SETUP_DIALOG( LabelGosub label ) {
            replicasInDialog[ 0 ].value = "@CR@074";
            replicasInDialog[ 1 ].value = "@CR@075";
            replicasInDialog[ 2 ].value = "@CR@076";
            replicasInDialog[ 3 ].value = "@CR@077";
            replicasInDialog[ 4 ].value = "@CR@078";
            replicasInDialog[ 5 ].value = "@CR@079";
            totalReplicasInDialog.value = 6;
            replicasPlayerTolking[ 3 ].value = true;
            replicasPlayerTolking[ 5 ].value = true;
            chdir( @"Sound\CRMISS\6Z" );
            AUDIO_PL.load( 6 );
            wait( AUDIO_PL.is_ready );
        }
        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------

        private void LOAD_PATH( LabelGosub label ) {
            load_path( loaded_path );
            wait( is_path_available( loaded_path ) );
        }

        #region OnPassed
        private void DEFAULT_PASSED() {
            and( reward > 0, delegate {
                show_text_1number_styled( sString.M_PASS, reward, 5000, 1 );
                p.add_money( reward );
            }, delegate {
                show_text_styled( sString.M_PASSD, 5000, 1 );
            } );
            play_music( MusicID.MISSION_PASSED );
            set_latest_mission_passed( CURRENT_MISSION_NAME );
            CRASH_TOTAL_MISSION_PASSED += 1;
            set_made_progress();
            and( CRASH_TOTAL_MISSION_PASSED == 2, delegate {
                create_thread<REMAXST>();
            } );
            and( CRASH_TOTAL_MISSION_PASSED == 5, delegate {
                CRASH_START_X.value = 2370.6443;
                CRASH_START_Y.value = 2547.9795;
                CRASH_START_Z.value = 10.8203;
                create_thread<BLSTART>();
                @return();
            } );
            and( 8 > CRASH_TOTAL_MISSION_PASSED, delegate {
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
            panel.remove();
            p.enable_group_recruitment( true );
            a.set_muted( false ).set_can_be_knocked_off_bike( false );
            enable_train_traffic( true );
            enable_planes_traffic( true );
            enable_emergency_traffic( true );
            g.release();
            switch_roads_on( 2738.6042, 826.7645, -10.0, 2916.0, 1041.7644, 40.0 );
            Gosub += CLEAR_ACTIVE_ENTITIES;
            Gosub += CLEAR_CUTSCENE_ENTITIES;
            Gosub += CLEAR_PATH;
            AUDIO_BG.unload();
            wait( AUDIO_BG.is_stopped );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
        }

        private void CLEAR_ACTIVE_ENTITIES( LabelGosub label ) {
            checkpoint.disable_if_exist();
            friendDecisionMaker.release();
            enemyDecisionMaker.release();
            MISSION_GLOBAL_STATUS_TEXT_1.remove();
            MISSION_GLOBAL_STATUS_TEXT_2.remove();
            MISSION_GLOBAL_STATUS_TEXT_3.remove();
            MISSION_GLOBAL_TIMER_1.stop();
            helpWeapon.destroy_if_exist();
            enemyMarkers.each( index, m => {
                enemyMarkers[ index ].disable_if_exist();
                friendMarkers[ index ].disable_if_exist();
                targetObjects[ index ].destroy_if_exist();
                enemyActors[ index ].destroy_if_exist();
                friendActors[ index ].destroy_if_exist();
                enemyCars[ index ].destroy_if_exist();
                puckups[ index ].destroy_if_exist();
                enemyAS[ index ].clear();
                friendAS[ index ].clear();
            } );
            and( is_m6_mission == true, delegate {
                terroristsMarkers.each( index, m => {
                    terroristsMarkers[ index ].disable_if_exist();
                    terroristsActors[ index ].destroy_if_exist();
                } );
                train.destroy_if_exist();
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
            and( CRASH_FBI_FLAG > 4, delegate {
                and( melPath != -1, johnPath != -1, delegate { release_path( melPath ); release_path( johnPath ); } );
            } );
            loaded_path.value = -1;
            melPath.value = -1;
            johnPath.value = -1;
        }
        #endregion

    }

}