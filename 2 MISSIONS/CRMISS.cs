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
        //,
        //             DELAY_BETWEEN_REPLICAS = 7000;

        // ---------------------------------------------------------------------------------------------------------------------------

        Int index, loaded_path, temp1, temp2, temp3, text_displayed, totalReplicasInDialog;
        Float tempX1, tempY1, tempZ1, tempX2, tempY2, tempZ2, tempAngle;
        sString failedMessage, tempHash;
        DecisionMaker enemyDecisionMaker, friendDecisionMaker;
        Pickup helpWeapon;
        Car player_car, tmpCar;
        Actor tempActor;
        Checkpoint checkpoint;
        Panel panel;

        Array<Int> replicasPlayerTolking = MAX_ARRAY_OF_REPLICAS;
        Array<sString> replicasInDialog = MAX_ARRAY_OF_REPLICAS;
        Array<sString> melAnswers = 4;
        Array<Actor> enemyActors = MAX_ARRAY_OF_ACTIVE_COUNT, friendActors = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Marker> enemyMarkers = MAX_ARRAY_OF_ACTIVE_COUNT, friendMarkers = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Car> enemyCars = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Object> targetObjects = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<ASPack> enemyAS = MAX_ARRAY_OF_ACTIVE_COUNT, friendAS = MAX_ARRAY_OF_ACTIVE_COUNT;

        // 34 + 40 + 8*10 + 2*4 + 2*23 = 208 of 1023

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

            // DEBUG START
            CRASH_FBI_FLAG.value = true;
            // DEBUG END
            and( CRASH_FBI_FLAG == true, delegate { Jump += M5_POLMAV_MISSION; } );

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
            mel.start_facial_talk( 5000 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 5000 );
            show_text_highpriority( "@CR@027", 5000, 1 );
            wait( 5000 );
            mel.stop_facial_talk();

            Gosub += M5_QUESTION_1;

            AUDIO_PL.play( M5_PANEL_CJ_ANSWER_SOUND_ID ); // [ 0, 1, 2, 3 ]
            player.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_1string_highpriority( "@CR@032", tempHash, 4500, 1 );
            wait( 4500 );
            player.stop_facial_talk();

            AUDIO_PL.play( 19 ); // 19
            mel.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_highpriority( "@CR@033", 4500, 1 );
            wait( 4500 );
            mel.stop_facial_talk();

            Gosub += M5_QUESTION_2;

            AUDIO_PL.play( M5_PANEL_CJ_ANSWER_SOUND_ID ); // [ 4, 5, 6, 7 ]
            player.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_1string_highpriority( "@CR@032", tempHash, 4500, 1 );
            wait( 4500 );
            player.stop_facial_talk();

            AUDIO_PL.play( 20 ); // 20
            mel.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_highpriority( "@CR@038", 4500, 1 );
            wait( 4500 );
            mel.stop_facial_talk();

            Gosub += M5_QUESTION_3;

            AUDIO_PL.play( M5_PANEL_CJ_ANSWER_SOUND_ID ); // [ 8, 9, 10, 11 ]
            player.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_1string_highpriority( "@CR@032", tempHash, 4500, 1 );
            wait( 4500 );
            player.stop_facial_talk();

            AUDIO_PL.play( 21 ); // 21
            mel.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_highpriority( "@CR@043", 4500, 1 );
            wait( 4500 );
            mel.stop_facial_talk();

            Gosub += M5_QUESTION_4;

            AUDIO_PL.play( M5_PANEL_CJ_ANSWER_SOUND_ID ); // [ 12, 13, 14, 15 ]
            player.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_1string_highpriority( "@CR@032", tempHash, 4500, 1 );
            wait( 4500 );
            player.stop_facial_talk();

            AUDIO_PL.play( 22 ); // 22
            mel.start_facial_talk( 4500 ).task.perform_animation( "IDLE_chat", "PED", 4.0, 0, 0, 0, 0, 4500 );
            show_text_highpriority( "@CR@048", 4500, 1 );
            wait( 4500 );
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
            __camera_default();
            wait( 1000 );
            __fade( true );
            wait( 500 );
            and( 80 > temp1, delegate { ___jump_failed_message( "@CRS@35" ); } );
            CRASH_FBI_FLAG.value = true;
            CRASH_START_X.value = 2308.8706;
            CRASH_START_Y.value = 2431.3784;
            CRASH_START_Z.value = 10.8203;
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

        // ---[ MISSION POLMAV ]---

        private void M5_POLMAV_MISSION( LabelJump label ) {

            Int[] usedModels = { FBI, POLMAV, COACH };

            Gosub += SCENE_5B;

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
            friendMarkers[ 0 ].disable().create_above_vehicle( enemyCars[ 0 ] ).set_type( 1 ).set_size( 2 )
                              .set_color( MarkerColor.YELLOW ).set_radar_mode( 2 );
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
                        show_formatted_text_highpriority( "M: %f ~n~C: %f", 10000, tempZ1, tempZ2 );
                        //jump_passed();
                    } );
                } );
            };
        }

        private void __checkPoliveMavMelAndOther() {
            player_car.do_if_wrecked( delegate { ___jump_failed_message( "@CRS@14" ); } );
            friendActors[ 0 ].do_if_dead( delegate { ___jump_failed_message( "@CRS@34" ); } );
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
            //
            __toggle_cinematic( true );
            wait( 1000 );
            clear_area( 1, 2360.8169, 2549.6118, 15.8785, 300.0 );
            a.put_at( 2360.8169, 2549.6118, 15.8785 );
            __fade( true, false );
            Scene += delegate {
                wait( 5000 );
                Comment = "Сцена, где Джон рассказывает план внедрения";
            };
            __fade( false, true );
            __toggle_cinematic( false );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_5B( LabelGosub label ) {
            //
            __toggle_cinematic( true );
            wait( 1000 );
            clear_area( 1, 2360.8169, 2549.6118, 15.8785, 300.0 );
            a.put_at( 2360.8169, 2549.6118, 15.8785 );
            __fade( true, false );
            Scene += delegate {
                wait( 5000 );
                Comment = "Сцена, где Мэлл рассказывает план действий";
            };
            __fade( false, true );
            __toggle_cinematic( false );
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
            and( CRASH_TOTAL_MISSION_PASSED == 5, delegate {
                CRASH_START_X.value = 2370.6443;
                CRASH_START_Y.value = 2547.9795;
                CRASH_START_Z.value = 10.8203;
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
            panel.remove();
            p.enable_group_recruitment( true );
            a.set_muted( false );
            enable_train_traffic( true );
            enable_planes_traffic( true );
            enable_emergency_traffic( true );
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
            checkpoint.disable_if_exist();
            friendDecisionMaker.release();
            enemyDecisionMaker.release();
            MISSION_GLOBAL_STATUS_TEXT_1.remove();
            MISSION_GLOBAL_TIMER_1.stop();
            helpWeapon.destroy_if_exist();
            enemyMarkers.each( index, m => {
                enemyMarkers[ index ].disable_if_exist();
                friendMarkers[ index ].disable_if_exist();
                targetObjects[ index ].destroy_if_exist();
                enemyActors[ index ].destroy_if_exist();
                friendActors[ index ].destroy_if_exist();
                enemyCars[ index ].destroy_if_exist();
                enemyAS[ index ].clear();
                friendAS[ index ].clear();
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