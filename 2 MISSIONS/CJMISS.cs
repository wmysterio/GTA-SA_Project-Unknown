using GTA;

#pragma warning disable CS0649

public partial class MAIN {

    public class CJMISS : Mission {

        void ___jump_failed_message( string gxt ) {
            failedMessage.value = gxt;
            jump_failed();
        }

        void ___load_path( ushort id ) {
            loaded_path.value = id;
            Gosub += LOAD_PATH;
        }

        // ---------------------------------------------------------------------------------------------------------------------------

        const ushort MAX_ARRAY_OF_ACTIVE_COUNT = 10;

        // ---------------------------------------------------------------------------------------------------------------------------

        Int plus_reward, plus_respect, plus_temp, loaded_path, play_passed_sound, index, temp;
        Float distance, tempX1, tempY1, tempZ1, tempX2, tempY2, tempZ2;
        sString failedMessage;
        Car playerCar, tmpPlayerCar;
        DecisionMaker enemyDecisionMaker, friendDecisionMaker;
        Pickup helpWeapon;

        Array<Actor> enemyActors = MAX_ARRAY_OF_ACTIVE_COUNT, friendActors = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Marker> enemyMarkers = MAX_ARRAY_OF_ACTIVE_COUNT, friendMarkers = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Car> enemyCars = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Object> targetObjects = MAX_ARRAY_OF_ACTIVE_COUNT;

        // 34 + 20 + 6*10 = 114 of 1023

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            wait( 1000 );
            enable_train_traffic( false );
            destroy_all_trains();
            failedMessage.value = sString.DUMMY;
            loaded_path.value = -1;
            play_passed_sound.value = true;
            p.enable_group_recruitment( false );
            g.release();
            __set_entered_names( false );
            __set_traffic( 0.0 );
            jump_table( CJ_TOTAL_MISSION_PASSED, table => {
                table.Auto += MISSION_0;
                table.Auto += MISSION_1;
                table.Auto += MISSION_2;
                table.Auto += MISSION_3;
                table.Auto += MISSION_4;
                table.Auto += MISSION_5;
                table.Auto += MISSION_6;
                table.Auto += MISSION_7;
                table.Auto += MISSION_8;
                table.Auto += MISSION_9;
                table.Auto += MISSION_10;
                table.Auto += MISSION_11;
                table.Auto += MISSION_12;
                table.Auto += MISSION_13;
                table.Auto += MISSION_14;
                table.Auto += MISSION_15;
            } );

            OnPassed = DEFAULT_PASSED;
            OnFailed = DEFAULT_FAILED;
            OnClear = DEFAULT_CLEAR;
        }

        #region MISSIONS

        #region MISSION 0
        private void MISSION_0( LabelCase l ) {

            Int[] usedModels = { GREENWOO, TOPFUN, VMAFF1, VMAFF2, MICRO_UZI };

            play_passed_sound.value = false;
            a.set_position( 2492.146, -1640.8958, 13.478 );
            chdir( @"Sound\CJMISS" );
            AUDIO_BG.load( 999999, true );
            wait( AUDIO_BG.is_ready );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME ).play();
            Gosub += SCENE_0A;
            load_special_actor( SpecialActor.SWEET, 1 );
            load_requested_models( usedModels );
            ___load_path( 909 );
            wait( is_special_actor_loaded( 1 ) );
            clear_area( 1, 2460.0542, -1656.8071, 12.4056, 300.0 );

            set_next_vehicle_model_variation( TOPFUN, 0 );
            enemyCars[ 0 ].create( TOPFUN, 2460.0542, -1656.8071, 12.4056 )
                          .set_z_angle( 90.16 )
                          .set_immunities( 1, 0, 0, 1, 0 )
                          .set_door_status( DoorStatus.LOCKED_4 )
                          .set_colors( 1, 1 );

            enemyActors[ 0 ].create_in_vehicle_driverseat( ActorType.MISSION1, VMAFF1, enemyCars[ 0 ] )
                            .set_cant_be_dragged_out( true )
                            .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                            .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 )
                            .set_immunities( 1 );

            enemyActors[ 1 ].create_in_vehicle_passenger_seat( ActorType.MISSION1, VMAFF2, enemyCars[ 0 ], 0 )
                            .set_cant_be_dragged_out( true )
                            .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                            .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 )
                            .set_immunities( 1 )
                            .give_weapon( WeaponNumber.MICRO_UZI, 9999 )
                            .set_armed_weapon( WeaponNumber.MICRO_UZI )
                            .set_drops_weapons_when_dead( false )
                            .set_weapon_attack_rate( 10 )
                            .set_weapon_accuracy( 10 );

            friendActors[ 0 ].create_in_vehicle_passenger_seat( ActorType.MISSION1, SPECIAL01, enemyCars[ 0 ], 1 )
                             .set_cant_be_dragged_out( true )
                             .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                             .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 )
                             .set_immunities( 1 );

            enemyMarkers[ 0 ].create_above_vehicle( enemyCars[ 0 ] )
                             .set_radar_mode( 1 )
                             .set_type( 1 );

            playerCar.create( GREENWOO, 2503.5286, -1664.7495, 12.0653 )
                     .set_z_angle( 72.39 )
                     .set_colors( 7, 7 )
                     .set_health( 2000 );

            a.put_into_vehicle_as_driver( playerCar );
            destroy_model( usedModels );
            unload_special_actor( 1 );
            __camera_default();
            p.clear_wanted_level();
            set_radio_station( RadioStation.OFF );
            __set_entered_names( true );
            wait( 1000 );
            __fade( true );
            AUDIO_BG.set_volume( 1.0 );
            __disable_player_controll_in_cutscene( false );
            enemyActors[ 1 ].task.drive_by( a, Car.empty, 5.0, 0.0, 0.0, 50.0, 8, 0, 10 );
            set_vehicle_traffic_density_multiplier( 0.3 );
            set_ped_traffic_density_multiplier( 1.0 );
            enemyCars[ 0 ].start_path( loaded_path ).set_path_speed( 0.9 );
            show_text_highpriority( "@CJS@18", 6000, 1 );
            Jump += M0_LOOP;
        }

        private void M0_LOOP( LabelJump label ) {

            var enemyCar = enemyCars[ 0 ];

            wait( 0 );
            and( enemyCar.is_near_point_3d_stopped( 0, 1032.0916, -1953.396, 11.973, 10.0, 10.0, 10.0 ), delegate { enemyCar.stop_path().explode(); } );
            enemyCar.do_if_wrecked( delegate { ___jump_failed_message( "@CJS@17" ); } );
            a.get_position( tempX1, tempY1, tempZ1 );
            enemyCar.get_position( tempX2, tempY2, tempZ2 );
            get_distance_3d( tempX1, tempY1, tempZ1, tempX2, tempY2, tempZ2, distance );
            and( distance >= 80.0, !enemyCar.is_on_screen(), delegate { ___jump_failed_message( "@CJS@16" ); } );
            and( a.is_in_area_3d( 1, 1165.3536, -1812.5808, 6.0, 1188.983, -1782.2349, 18.0 ), delegate { Jump += M0_END; } );
            jump( label );
        }

        private void M0_END( LabelJump label ) {
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            __disable_player_controll_in_cutscene( true );
            a.extinguish_current_car_if_exist( tmpPlayerCar );
            __fade( 0, true );
            a.teleport_without_car( 1176.5929, -1783.285, 21.588 );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME );
            gosub( CLEAR_ACTIVE_ENTITIES );
            Gosub += SCENE_0B;
            a.put_at( 1159.9031, -1837.1759, 12.6152, 234.8241 );
            __camera_default();
            wait( 1000 );
            fade( true, 500 );
            __disable_player_controll_in_cutscene( false );
            CJ_START_X.value = 276.703;
            CJ_START_Y.value = -1433.3204;
            CJ_START_Z.value = 13.8713;
            wait( 500 );
            jump_passed();
        }
        #endregion

        #region MISSION 1
        private void MISSION_1( LabelCase l ) {







            CJ_START_X.value = 2498.9802;
            CJ_START_Y.value = -1685.6517;
            CJ_START_Z.value = 13.4478;
            fade( 1, 1500 );
            //Int[] usedModels = { MICRO_UZI, TEC9, COLT45, FAM1, FAM2, FAM3, BALLAS1, BALLAS2, BALLAS3, MAJESTIC };

            //plus_respect.value = 10;
            //a.set_position( 2513.3271, -1670.5092, 13.5149 );
            //chdir( @"Sound\CJMISS" );
            //AUDIO_BG.load( 999998, true );
            //wait( AUDIO_BG.is_ready );
            //AUDIO_BG.set_volume( CUTSCENE_VOLUME ).play();
            //Gosub += SCENE_1A;
            //load_special_actor( SpecialActor.CESAR, 1 );
            //load_requested_models( usedModels );
            //wait( is_special_actor_loaded( 1 ) );
            //clear_area( 1, 2041.3564, -1672.2263, 13.3828, 300.0 );
            //__renderer_at( 2041.2562, -1618.3821, 12.5469 );
            //PlayerActor.put_at( 2041.2562, -1618.3821, 12.5469, 180.0 );
            //__camera_default();

            //enemyDecisionMaker.create_normal();

            //helpWeapon.create_if_need( WeaponNumber.MICRO_UZI, WeaponModel.MICRO_UZI, 250, 2044.4025, -1624.189, 13.5469, temp );

            //enemyCars[ 0 ].create( MAJESTIC, 2042.4576, -1706.4899, 13.1329 ).set_z_angle( 330.4716 );

            //enemyActors[ 0 ].create( ActorType.MISSION1, BALLAS2, 2038.8091, -1697.7739, 12.5469 ).set_z_angle( 358.1433 );
            //enemyActors[ 1 ].create( ActorType.MISSION1, BALLAS1, 2040.7072, -1704.4471, 12.5547 ).set_z_angle( 355.95 );
            //enemyActors[ 2 ].create( ActorType.MISSION1, BALLAS2, 2039.3774, -1709.9406, 12.5469 ).set_z_angle( 353.0234 );
            //enemyActors[ 3 ].create( ActorType.MISSION1, BALLAS3, 2032.6824, -1690.8292, 12.5469 ).set_z_angle( 342.6833 );
            //enemyActors[ 4 ].create( ActorType.MISSION1, BALLAS1, 2049.7957, -1694.0028, 16.4531 ).set_z_angle( 26.8402 );
            //enemyActors[ 5 ].create( ActorType.MISSION1, BALLAS2, 2049.4373, -1688.2037, 12.5547 ).set_z_angle( 16.4947 );

            //friendActors[ 0 ].create( ActorType.MISSION2, FAM1, 2038.3684, -1657.6135, 12.5469 ).set_z_angle( 182.8817 );
            //friendActors[ 1 ].create( ActorType.MISSION2, FAM2, 2042.4958, -1657.3539, 12.5469 ).set_z_angle( 179.7483 );
            //friendActors[ 2 ].create( ActorType.MISSION2, FAM3, 2040.7076, -1652.5468, 12.5469 ).set_z_angle( 182.8817 );
            //friendActors[ 3 ].create( ActorType.MISSION2, SPECIAL01, 2043.8499, -1628.0507, 12.5469 ).set_z_angle( 180.0616 );

            //to( index, 0, 5, delegate {
            //    enemyActors[ index ].set_acquaintance( AcquaintanceType.HATE, ActorType.MISSION2 )
            //                        .set_acquaintance( AcquaintanceType.HATE, ActorType.PLAYER )
            //                        .give_weapon( WeaponNumber.TEC9, 9999 )
            //                        .set_armed_weapon( WeaponNumber.TEC9 )
            //                        .set_decision_maker( enemyDecisionMaker )
            //                        .set_drops_weapons_when_dead( false )
            //                        .set_suffers_critical_hits( false )
            //                        .set_max_health( 500 )
            //                        .set_health( 500 )
            //                        .set_weapon_accuracy( 30 )
            //                        .set_weapon_attack_rate( 35 );
            //    enemyMarkers[ index ].create_above_actor( enemyActors[ index ] ).set_size( 2 );
            //} );
            //to( index, 0, 3, delegate {
            //    friendActors[ index ].set_acquaintance( AcquaintanceType.HATE, ActorType.MISSION1 )
            //                         .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
            //                         .give_weapon( WeaponNumber.MICRO_UZI, 9999 )
            //                         .set_armed_weapon( WeaponNumber.MICRO_UZI )
            //                         .set_decision_maker( enemyDecisionMaker )
            //                         .set_drops_weapons_when_dead( false )
            //                         .set_suffers_critical_hits( false )
            //                         .set_max_health( 150 )
            //                         .set_health( 150 )
            //                         .set_weapon_accuracy( 45 )
            //                         .set_weapon_attack_rate( 35 )
            //                         .set_untargetable( true );
            //} );

            //enemyActors[ 0 ].task.kill_actor_on_foot( friendActors[ 0 ] );
            //enemyActors[ 1 ].set_weapon_skill( 2 ).task.kill_actor_on_foot( friendActors[ 1 ] );
            //enemyActors[ 2 ].task.crouch( true ).kill_actor_on_foot( friendActors[ 2 ] );
            //enemyActors[ 3 ].give_weapon( WeaponNumber.PISTOL, 9999 ).set_weapon_skill( 2 ).task.kill_actor_on_foot( friendActors[ 3 ] );
            //enemyActors[ 4 ].set_weapon_accuracy( 85 ).set_weapon_attack_rate( 30 ).set_stay_in_same_place( true ).task.crouch( true ).kill_actor_on_foot( a );
            //enemyActors[ 5 ].give_weapon( WeaponNumber.PISTOL, 9999 ).set_weapon_accuracy( 50 ).set_weapon_attack_rate( 35 ).task.kill_actor_on_foot( a );

            //friendActors[ 0 ].task.crouch( true ).kill_actor_on_foot( enemyActors[ 0 ] );
            //friendActors[ 1 ].task.kill_actor_on_foot( enemyActors[ 1 ] );
            //friendActors[ 2 ].task.kill_actor_on_foot( enemyActors[ 2 ] );
            //friendActors[ 3 ].set_weapon_accuracy( 65 )
            //                 .set_max_health( 400 )
            //                 .set_health( 400 )
            //                 .set_only_damaged_by_player( true )
            //                 .put_in_group( g )
            //                 .set_never_leaves_group( true );

            //friendMarkers[ 0 ].create_above_actor( friendActors[ 3 ] ).set_size( 2 ).set_type( true );

            //destroy_model( usedModels );
            //unload_special_actor( 1 );
            //__set_entered_names( true );
            //wait( 1000 );
            //__set_player_ignore( false );
            //set_sensitivity_to_crime( 0.3 );
            //set_ped_traffic_density_multiplier( 1.0 );
            //set_vehicle_traffic_density_multiplier( 0.2 );
            //__disable_player_controll_in_cutscene( false );
            //AUDIO_BG.set_volume( 1.0 );
            //p.clear_wanted_level();
            //__fade( true );
            //show_text_highpriority( "@CJS@19", 6000, 1 );



            jump_passed();
        }
        
        
        #endregion

        #region MISSION 2
        private void MISSION_2( LabelCase l ) {

            Int[] usedModels = { MICRO_UZI, TEC9, COLT45, FAM1, FAM2, FAM3, BALLAS1, BALLAS2, BALLAS3, MAJESTIC };

            plus_respect.value = 10;
            a.set_position( 2513.3271, -1670.5092, 13.5149 );
            chdir( @"Sound\CJMISS" );
            AUDIO_BG.load( 999998, true );
            wait( AUDIO_BG.is_ready );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME ).play();
            Gosub += SCENE_1A;
            load_special_actor( SpecialActor.CESAR, 1 );
            load_requested_models( usedModels );
            wait( is_special_actor_loaded( 1 ) );
            __set_police_generator( false );
            clear_area( 1, 2041.3564, -1672.2263, 13.3828, 300.0 );
            __renderer_at( 2041.2562, -1618.3821, 12.5469 );
            PlayerActor.put_at( 2041.2562, -1618.3821, 12.5469, 180.0 );
            __camera_default();

            enemyDecisionMaker.create_normal();

            helpWeapon.create_if_need( WeaponNumber.MICRO_UZI, WeaponModel.MICRO_UZI, 250, 2044.4025, -1624.189, 13.5469, temp );

            enemyCars[ 0 ].create( MAJESTIC, 2042.4576, -1706.4899, 13.1329 ).set_z_angle( 330.4716 );

            enemyActors[ 0 ].create( ActorType.MISSION1, BALLAS2, 2038.8091, -1697.7739, 12.5469 ).set_z_angle( 358.1433 );
            enemyActors[ 1 ].create( ActorType.MISSION1, BALLAS1, 2040.7072, -1704.4471, 12.5547 ).set_z_angle( 355.95 );
            enemyActors[ 2 ].create( ActorType.MISSION1, BALLAS2, 2039.3774, -1709.9406, 12.5469 ).set_z_angle( 353.0234 );
            enemyActors[ 3 ].create( ActorType.MISSION1, BALLAS3, 2032.6824, -1690.8292, 12.5469 ).set_z_angle( 342.6833 );
            enemyActors[ 4 ].create( ActorType.MISSION1, BALLAS1, 2049.7957, -1694.0028, 16.4531 ).set_z_angle( 26.8402 );
            enemyActors[ 5 ].create( ActorType.MISSION1, BALLAS2, 2049.4373, -1688.2037, 12.5547 ).set_z_angle( 16.4947 );

            friendActors[ 0 ].create( ActorType.MISSION2, FAM1, 2038.3684, -1657.6135, 12.5469 ).set_z_angle( 182.8817 );
            friendActors[ 1 ].create( ActorType.MISSION2, FAM2, 2042.4958, -1657.3539, 12.5469 ).set_z_angle( 179.7483 );
            friendActors[ 2 ].create( ActorType.MISSION2, FAM3, 2040.7076, -1652.5468, 12.5469 ).set_z_angle( 182.8817 );
            friendActors[ 3 ].create( ActorType.MISSION2, SPECIAL01, 2043.8499, -1628.0507, 12.5469 ).set_z_angle( 180.0616 );

            to( index, 0, 5, delegate {
                enemyActors[ index ].set_acquaintance( AcquaintanceType.HATE, ActorType.MISSION2 )
                                    .set_acquaintance( AcquaintanceType.HATE, ActorType.PLAYER )
                                    .give_weapon( WeaponNumber.TEC9, 9999 )
                                    .set_armed_weapon( WeaponNumber.TEC9 )
                                    .set_decision_maker( enemyDecisionMaker )
                                    .set_drops_weapons_when_dead( false )
                                    .set_suffers_critical_hits( false )
                                    .set_max_health( 500 )
                                    .set_health( 500 )
                                    .set_weapon_accuracy( 30 )
                                    .set_weapon_attack_rate( 35 );
                enemyMarkers[ index ].create_above_actor( enemyActors[ index ] ).set_size( 2 );
            } );
            to( index, 0, 3, delegate {
                friendActors[ index ].set_acquaintance( AcquaintanceType.HATE, ActorType.MISSION1 )
                                     .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                                     .give_weapon( WeaponNumber.MICRO_UZI, 9999 )
                                     .set_armed_weapon( WeaponNumber.MICRO_UZI )
                                     .set_decision_maker( enemyDecisionMaker )
                                     .set_drops_weapons_when_dead( false )
                                     .set_suffers_critical_hits( false )
                                     .set_max_health( 150 )
                                     .set_health( 150 )
                                     .set_weapon_accuracy( 45 )
                                     .set_weapon_attack_rate( 35 )
                                     .set_untargetable( true );
            } );

            enemyActors[ 0 ].task.kill_actor_on_foot( friendActors[ 0 ] );
            enemyActors[ 1 ].set_weapon_skill( 2 ).task.kill_actor_on_foot( friendActors[ 1 ] );
            enemyActors[ 2 ].task.crouch( true ).kill_actor_on_foot( friendActors[ 2 ] );
            enemyActors[ 3 ].give_weapon( WeaponNumber.PISTOL, 9999 ).set_weapon_skill( 2 ).task.kill_actor_on_foot( friendActors[ 3 ] );
            enemyActors[ 4 ].set_weapon_accuracy( 85 ).set_weapon_attack_rate( 30 ).set_stay_in_same_place( true ).task.crouch( true ).kill_actor_on_foot( a );
            enemyActors[ 5 ].give_weapon( WeaponNumber.PISTOL, 9999 ).set_weapon_accuracy( 50 ).set_weapon_attack_rate( 35 ).task.kill_actor_on_foot( a );

            friendActors[ 0 ].task.crouch( true ).kill_actor_on_foot( enemyActors[ 0 ] );
            friendActors[ 1 ].task.kill_actor_on_foot( enemyActors[ 1 ] );
            friendActors[ 2 ].task.kill_actor_on_foot( enemyActors[ 2 ] );
            friendActors[ 3 ].set_weapon_accuracy( 65 )
                             .set_max_health( 400 )
                             .set_health( 400 )
                             .set_only_damaged_by_player( true )
                             .put_in_group( g )
                             .set_never_leaves_group( true );

            friendMarkers[ 0 ].create_above_actor( friendActors[ 3 ] ).set_size( 2 ).set_type( true );

            destroy_model( usedModels );
            unload_special_actor( 1 );
            __set_entered_names( true );
            wait( 1000 );
            __set_player_ignore( false );
            set_sensitivity_to_crime( 0.3 );
            set_ped_traffic_density_multiplier( 1.0 );
            set_vehicle_traffic_density_multiplier( 0.1 );
            __disable_player_controll_in_cutscene( false );
            AUDIO_BG.set_volume( 1.0 );
            p.clear_wanted_level();
            __fade( true );
            show_text_highpriority( "@CJS@19", 6000, 1 );
            Jump += M2_LOOP;
        }

        private void M2_LOOP( LabelJump label ) {
            wait( 0 );
            and( !a.is_near_point_3d( 0, 2041.7961, -1672.145, 13.3828, 100.0, 100.0, 100.0 ), delegate {
                ___jump_failed_message( "@CJS@21" );
            } );
            friendActors[ 3 ].do_if_dead( delegate { ___jump_failed_message( "@CJS@20" ); } );
            temp.value = 0;
            to( index, 0, 5, delegate {
                enemyActors[ index ].do_if_dead( delegate {
                    enemyMarkers[ index ].disable_if_exist();
                    and( 3 > index, delegate {
                        and(
                            !friendActors[ index ].is_dead(),
                            !friendActors[ index ].is_group_member( g )
                        , delegate { friendActors[ index ].put_in_group( g ).set_never_leaves_group( true ); } );
                    } );
                    temp += 1;
                } );
            } );
            and( temp == 6, delegate { Jump += M2_END; } );
            friendActors[ 0 ].do_if_dead( delegate {
                and( !enemyActors[ 0 ].is_dead(), delegate {
                    enemyActors[ 0 ].get_task_status( 1506, temp );
                    and( temp == 7, delegate { enemyActors[ 0 ].task.kill_actor_on_foot( a ); } );
                } );
            } );
            friendActors[ 1 ].do_if_dead( delegate {
                and( !enemyActors[ 1 ].is_dead(), delegate {
                    enemyActors[ 1 ].get_task_status( 1506, temp );
                    and( temp == 7, delegate { enemyActors[ 1 ].task.kill_actor_on_foot( friendActors[ 3 ] ); } );
                } );
            } );
            friendActors[ 2 ].do_if_dead( delegate {
                and( !enemyActors[ 2 ].is_dead(), delegate {
                    enemyActors[ 2 ].get_task_status( 1506, temp );
                    and( temp == 7, delegate { enemyActors[ 2 ].task.kill_actor_on_foot( friendActors[ 3 ] ); } );
                } );
            } );
            jump( M2_LOOP );
        }

        private void M2_END( LabelJump label ) {
            __set_player_ignore( true );
            __set_traffic( 0.0 );
            __disable_player_controll_in_cutscene( true );
            a.extinguish_current_car_if_exist( tmpPlayerCar );
            __fade( 0, true );
            a.teleport_without_car( 2041.8087, -1662.9612, 13.5469 );
            AUDIO_BG.set_volume( CUTSCENE_VOLUME );
            gosub( CLEAR_ACTIVE_ENTITIES );
            Gosub += SCENE_1B;
            a.put_at( 2041.5757, -1691.3855, 12.5469, 0.9074 );
            __camera_default();
            wait( 1000 );
            fade( true, 500 );
            __disable_player_controll_in_cutscene( false );
            CJ_START_X.value = 1799.4656;
            CJ_START_Y.value = -2120.8831;
            CJ_START_Z.value = 13.5543;
            wait( 500 );
            jump_passed();
        }
        #endregion


        private void MISSION_3( LabelCase l ) {
            jump_passed();
        }
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
        private void MISSION_9( LabelCase l ) {
            jump_passed();
        }
        private void MISSION_10( LabelCase l ) {
            jump_passed();
        }
        private void MISSION_11( LabelCase l ) {
            jump_passed();
        }
        private void MISSION_12( LabelCase l ) {
            jump_passed();
        }
        private void MISSION_13( LabelCase l ) {
            jump_passed();
        }
        private void MISSION_14( LabelCase l ) {
            jump_passed();
        }
        private void MISSION_15( LabelCase l ) {
            jump_passed();
        }
        #endregion

        #region CUTSCENES
        private void SCENE_0A( LabelGosub label ) {
            //
            __toggle_cinematic( true );
            wait( 1000 );
            //clear_area( 1, 1177.72, -1852.2987, 12.3984, 300.0 );
            __fade( true, false );
            Scene += delegate {
                wait( 5000 );
                Comment = "Сцена похищения Свита";
            };
            __fade( false, true );
            __toggle_cinematic( false );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_0B( LabelGosub label ) {
            //
            __toggle_cinematic( true );
            wait( 1000 );
            clear_area( 1, 1177.72, -1852.2987, 12.3984, 300.0 );
            __fade( true, false );
            Scene += delegate {
                wait( 5000 );
                Comment = "Сцена аварии и составления протокола";
            };
            __fade( false, true );
            __toggle_cinematic( false );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_1A( LabelGosub label ) {
            //
            __toggle_cinematic( true );
            wait( 1000 );
            //clear_area( 1, 1177.72, -1852.2987, 12.3984, 300.0 );
            __fade( true, false );
            Scene += delegate {
                wait( 5000 );
                Comment = "Сцена CJ и Цезаря. Подбигает дружок и говорит, что напали Балласы.";
            };
            __fade( false, true );
            __toggle_cinematic( false );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }

        private void SCENE_1B( LabelGosub label ) {
            //
            __toggle_cinematic( true );
            wait( 1000 );
            clear_area( 1, 2041.6276, -1693.5046, 13.5469, 300.0 );
            __fade( true, false );
            Scene += delegate {
                wait( 5000 );
                Comment = "Цезарь говорит, что что-то не чисто и надо разобраться в этом.";
            };
            __fade( false, true );
            __toggle_cinematic( false );
            Gosub += CLEAR_CUTSCENE_ENTITIES;
        }
        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------

        private void LOAD_PATH( LabelGosub label ) {
            load_path( loaded_path );
            wait( is_path_available( loaded_path ) );
        }

        #region OnPassed
        private void DEFAULT_PASSED() {
            and( plus_reward > 0, delegate { plus_temp += 1; } );
            and( plus_respect > 0, delegate { plus_temp += 2; } );
            and( plus_temp == 3, delegate { show_text_1number_styled( sString.M_PASSS, plus_reward, 5000, 1 ); add_respect( plus_respect ); p.add_money( plus_reward ); } );
            and( plus_temp == 2, delegate { show_text_styled( sString.M_PASSR, 5000, 1 ); add_respect( plus_respect ); } );
            and( plus_temp == 1, delegate { show_text_1number_styled( sString.M_PASS, plus_reward, 5000, 1 ); p.add_money( plus_reward ); } );
            and( plus_temp == 0, delegate { show_text_styled( sString.M_PASSD, 5000, 1 ); } );
            and( play_passed_sound == true, delegate { play_music( MusicID.MISSION_PASSED ); } );
            set_latest_mission_passed( CURRENT_MISSION_NAME );
            CJ_TOTAL_MISSION_PASSED += 1;
            set_made_progress();
            and( CJ_TOTAL_MISSION_PASSED == 3, delegate {
                CJ_PHONE.call( 1 );
                @return();
            } );
            and( CJ_TOTAL_MISSION_PASSED == 4, delegate {
                CJ_PHONE.call( 2 );
                @return();
            } );
            and( CJ_TOTAL_MISSION_PASSED == 6, delegate {
                CJ_PHONE.call( 3 );
                @return();
            } );
            and( CJ_TOTAL_MISSION_PASSED == 12, delegate {
                // create_thread<ZERO>
            } );
            and( CJ_TOTAL_MISSION_PASSED == 13, delegate {
                // create_thread<MAFIA>
                @return();
            } );
            and( 16 > CJ_TOTAL_MISSION_PASSED, delegate {
                create_thread<CJSTART>();
            } );
        }
        #endregion

        #region OnFailed
        private void DEFAULT_FAILED() {
            show_text_styled( sString.M_FAIL, 5000, 1 );
            and( failedMessage != sString.DUMMY, delegate { show_text_lowpriority( failedMessage, 6000, 1 ); } );
            create_thread<CJSTART>();
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
            playerCar.destroy_if_exist();
        }

        private void CLEAR_CUTSCENE_ENTITIES( LabelGosub label ) {

        }

        private void CLEAR_PATH( LabelGosub label ) {
            and( loaded_path != -1, delegate { release_path( loaded_path ); } );
            loaded_path.value = -1;
        }
        #endregion

    }

}