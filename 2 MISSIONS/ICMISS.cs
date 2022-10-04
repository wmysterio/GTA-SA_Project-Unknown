using GTA;

#pragma warning disable CS0649

partial class MAIN {

    public sealed class ICMISS : Mission {

        const double RESTART_POSITION_X = -2757.6736,
                     RESTART_POSITION_Y = 362.6336,
                     RESTART_POSITION_Z = 3.3727,
                     RESTART_POSITION_A = 270.0;

        const ushort CUTSCENE_ENTITY_ARRAY_SIZE = 10;
        const ushort TOTAL_WEAPONS_GROUPS = 14;

        // ---------------------------------------------------------------------------------------------------------------------------

        private void ___jump_failed( string gxt ) {
            failedMessage.value = gxt;
            jump( FAILED_WITH_TELEPORT );
        }

        // ---------------------------------------------------------------------------------------------------------------------------

        static Int INCORP_MOVIE_FLAG;

        Int reward, index, index2, enemyDefaultHealth, isPlayerWeaponSaved, temp, enabledWeaponNumber, playTune;
        Car player_car;
        DecisionMaker friendDecisionMaker;
        Car tempCar;

        sString failedMessage;

        Array<Actor> enemyActors = CUTSCENE_ENTITY_ARRAY_SIZE, friendActors = CUTSCENE_ENTITY_ARRAY_SIZE;
        Array<Marker> enemyMarkers = CUTSCENE_ENTITY_ARRAY_SIZE, friendMarkers = CUTSCENE_ENTITY_ARRAY_SIZE;
        Array<Int> savedWeaponsNumber = TOTAL_WEAPONS_GROUPS, saveWeaponsAmmos = TOTAL_WEAPONS_GROUPS, savedWeaponsModels = TOTAL_WEAPONS_GROUPS;

        Array<Float> tempX = CUTSCENE_ENTITY_ARRAY_SIZE, tempY = CUTSCENE_ENTITY_ARRAY_SIZE, tempZ = CUTSCENE_ENTITY_ARRAY_SIZE, tempA = CUTSCENE_ENTITY_ARRAY_SIZE;

        // 34 + 10*1 + 1*2 + 8*10 + 14*3 = 168

        Array<Actor> cutcsene_actors = local_array( 0, CUTSCENE_ENTITY_ARRAY_SIZE );
        Array<Car> cutcsene_cars = local_array( 10, CUTSCENE_ENTITY_ARRAY_SIZE );
        Array<Object> cutcsene_objects = local_array( 20, CUTSCENE_ENTITY_ARRAY_SIZE );

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            wait( 1000 );
            cancel_override_restart();
            override_next_restart( RESTART_POSITION_X, RESTART_POSITION_Y, RESTART_POSITION_Z, RESTART_POSITION_A );
            enable_train_traffic( false );
            destroy_all_trains();
            failedMessage.value = sString.DUMMY;
            //loaded_path.value = -1;
            //melPath.value = -1;
            //johnPath.value = -1;
            p.enable_group_recruitment( false );
            g.release();
            __set_entered_names( false );
            __set_police_generator( false );
            enable_planes_traffic( false );
            enable_emergency_traffic( false );
            set_sensitivity_to_crime( 0.0 );
            __set_traffic( 0.0 );

            /* DEBUG START */
            a.put_at( -2763.2051, 358.0647, 4.4237, 270.0 );
            /* DEBUG END */

            jump_table( INCORP_MISSION_PASSED, table => {
                table.Auto += MISSION_0;  // first  contact
                table.Auto += MISSION_1;  // second contact
                table.Auto += MISSION_2;  // Scene
                table.Auto += MISSION_3;  // Ep.1 Scene 1
                table.Auto += MISSION_4;  // Ep.1 Scene 2
                table.Auto += MISSION_5;  // Ep.1 Scene 3
                table.Auto += MISSION_6;  // Ep.1 Scene 4
                table.Auto += MISSION_7;  // Ep.2 Scene 1
                table.Auto += MISSION_8;  // Ep.2 Scene 2
                table.Auto += MISSION_9;  // Ep.2 Scene 3
                table.Auto += MISSION_10; // Ep.2 Scene 4
                table.Auto += MISSION_11; // Ep.3 Scene 1
                table.Auto += MISSION_12; // Ep.3 Scene 2
                table.Auto += MISSION_13; // Ep.3 Scene 3
                table.Auto += MISSION_14; // Ep.3 Scene 4
            } );

            OnPassed = DEFAULT_PASSED;
            OnFailed = DEFAULT_FAILED;
            OnClear = DEFAULT_CLEAR;

        }

        // ---------------------------------------------------------------------------------------------------------------------------

        #region Mission 0
        private void MISSION_0( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 1
        private void MISSION_1( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 2
        private void MISSION_2( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 3
        private void MISSION_3( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 4
        private void MISSION_4( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 5
        private void MISSION_5( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 6
        private void MISSION_6( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 7
        private void MISSION_7( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 8
        private void MISSION_8( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 9
        private void MISSION_9( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 10
        private void MISSION_10( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 11
        private void MISSION_11( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 12
        private void MISSION_12( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 13
        private void MISSION_13( LabelCase l ) {
            fade( 1, 250 );
            __disable_player_controll_in_cutscene( false );
            wait( 1000 );
            jump_passed();
        }
        #endregion

        #region Mission 14
        private void MISSION_14( LabelCase l ) {

            Int[] used_models = new Int[] { FBITRUCK, 981, AK47, CHROMEGUN, SHOTGSPA, COLT45, MP5LNG, WMYCD1, BMYCON, SWMOTR5, SBMOCD };

            playTune.value = true;
            reward.value = 5000;
            MISSION_GLOBAL_STATUS_TEXT_1.value = 0;
            switch_roads_off( -361.7219, 962.9586, -10.0, 177.2781, 1260.9586, 40.0 );
            set_weather( WeatherID.SUNNY_VEGAS );
            set_current_time( 10, 0 );
            load_requested_models( used_models );
            __renderer_at( -82.8439, 1155.6731, 18.7422 );
            clear_area( 0, -82.8439, 1155.6731, 18.7422, 300.0 );
            a.put_at( -82.8439, 1155.6731, 18.7422, 310.2032 ).set_muted( true ).clear_last_damage();
            Gosub += STORE_PLAYER_WEAPONS;
            set_next_vehicle_model_numplate_text( FBITRUCK, "VITAL" );
            player_car.create( FBITRUCK, -83.7493, 1159.569, 19.1728 )
                      .set_z_angle( 299.4474 )
                      .set_colors( 0, 1 )
                      .set_immunities( true )
                      //.set_door_status( DoorStatus.UNLOCKED ) // uncomment for debug
                      .set_health( 2500 )
                      .lock_position( true );
            //                                      0       1           2          3         4        5           6         7
            Float[] tmpCSArrayX = new Float[] { -31.9119, -25.9817, -140.7807, -130.9469, -46.103, -142.2492, -46.9359, -66.828 };
            Float[] tmpCSArrayY = new Float[] { 1185.714, 1148.4099, 1101.364, 1174.5115, 1091.8779, 1135.4916, 1124.705, 1070.6288 };
            Float[] tmpCSArrayZ = new Float[] { 18.3594, 18.5914, 18.5937, 18.7422, 18.7422, 18.75, 18.9137, 18.5937 };
            Float[] tmpCSArrayA = new Float[] { 141.0952, 87.5147, 271.733, 222.5627, 36.4409, 296.51, 77.1043, 2.8437 };
            ushort TOTAL_ENEMIES = ( ushort ) ( tmpCSArrayX.Length - 1 );
            for( ushort i = 0; i < tmpCSArrayX.Length; i++ ) {
                tempX[ i ].value = tmpCSArrayX[ i ];
                tempY[ i ].value = tmpCSArrayY[ i ];
                tempZ[ i ].value = tmpCSArrayZ[ i ];
                tempA[ i ].value = tmpCSArrayA[ i ];
            }

            cutcsene_objects[ 0 ].create( 981, -56.38, 1148.17, 18.5 ).set_z_angle( 90.0 ); //.set_z_angle( 270.0 );
            cutcsene_objects[ 1 ].create( 981, -70.05, 1188.30, 18.5 ).set_z_angle( 180.0 ); //.set_z_angle( 0.0 );
            cutcsene_objects[ 2 ].create( 981, -115.6, 1188.30, 18.5 ).set_z_angle( 180.0 ); //.set_z_angle( 0.0 );
            cutcsene_objects[ 3 ].create( 981, -125.3, 1148.23, 18.5 ).set_z_angle( 270.0 ); //.set_z_angle( 90.0 );
            cutcsene_objects[ 4 ].create( 981, -125.3, 1098.22, 18.5 ).set_z_angle( 270.0 ); //.set_z_angle( 90.0 );
            cutcsene_objects[ 5 ].create( 981, -70.05, 1088.50, 18.5 ).set_z_angle( 0.0 );   //.set_z_angle( 180.0 );
            cutcsene_objects[ 6 ].create( 981, -56.38, 1105.37, 18.5 ).set_z_angle( 90.0 );  //.set_z_angle( 270.0 );

            friendActors[ 0 ].create( ActorType.MISSION1, WMYCD1, -100.6243, 1098.1888, 18.5937 ).set_z_angle( 90.0 );
            friendActors[ 1 ].create( ActorType.MISSION1, BMYCON, -108.9, 1151.5237, 18.6783 ).set_z_angle( 40.4 );
            friendActors[ 2 ].create( ActorType.MISSION1, SBMOCD, -63.2661, 1165.952, 18.5846 ).set_z_angle( 296.7295 );
            friendActors[ 3 ].create( ActorType.MISSION1, SWMOTR5, -72.137, 1145.4802, 18.7422 ).set_z_angle( 224.03 );

            friendDecisionMaker.copy_shared_from( 65537 )
                               .clear_event_response( 27 )
                               .clear_event_response( 48 );

            and( CURRENT_GAME_LEVEL == 0, delegate {
                friendActors[ 0 ].give_weapon( WeaponNumber.MP5, 800 ).set_armed_weapon( WeaponNumber.SPAS12 );
                friendActors[ 1 ].give_weapon( WeaponNumber.AK47, 800 ).set_armed_weapon( WeaponNumber.AK47 );
                friendActors[ 2 ].give_weapon( WeaponNumber.PISTOL, 800 ).set_armed_weapon( WeaponNumber.MP5 );
                friendActors[ 3 ].give_weapon( WeaponNumber.SPAS12, 800 ).set_armed_weapon( WeaponNumber.PISTOL );
                enemyDefaultHealth.value = 350;
                MISSION_GLOBAL_TIMER_1.value = 180000; // 1000ms * 60s * 3m
            } );
            and( CURRENT_GAME_LEVEL == 1, delegate {
                friendActors[ 0 ].give_weapon( WeaponNumber.PISTOL, 400 ).set_armed_weapon( WeaponNumber.MP5 );
                friendActors[ 1 ].give_weapon( WeaponNumber.MP5, 400 ).set_armed_weapon( WeaponNumber.MP5 );
                friendActors[ 2 ].give_weapon( WeaponNumber.PISTOL, 500 ).set_armed_weapon( WeaponNumber.PISTOL );
                friendActors[ 3 ].give_weapon( WeaponNumber.MP5, 500 ).set_armed_weapon( WeaponNumber.PISTOL );
                enemyDefaultHealth.value = 400;
                MISSION_GLOBAL_TIMER_1.value = 180000; // 1000ms * 60s * 3m
            } );
            and( CURRENT_GAME_LEVEL == 2, delegate {
                friendActors[ 0 ].give_weapon( WeaponNumber.MP5, 300 ).set_armed_weapon( WeaponNumber.MP5 );
                friendActors[ 1 ].give_weapon( WeaponNumber.PISTOL, 400 ).set_armed_weapon( WeaponNumber.PISTOL );
                friendActors[ 2 ].give_weapon( WeaponNumber.PISTOL, 400 ).set_armed_weapon( WeaponNumber.PISTOL );
                friendActors[ 3 ].give_weapon( WeaponNumber.PISTOL, 400 ).set_armed_weapon( WeaponNumber.PISTOL );
                enemyDefaultHealth.value = 450;
                MISSION_GLOBAL_TIMER_1.value = 240000; // 1000ms * 60s * 4m
                reward.value = 3500;
            } );

            and( INCORP_MOVIE_FLAG == false, delegate {
                INCORP_MOVIE_FLAG.value = true;
                to( index, 0, 3, h => { friendActors[ index ].set_immunities( true ); } );
                CAMERA.set_position( -49.3827, 1098.3329, 36.1666 ).set_point_at( -82.8439, 1155.6731, 22.7422, 2 );
                wait( 2000 );
                __toggle_cinematic( true );
                __fade( true );
                Scene += delegate {
                    wait( 1500 );

                    show_text_highpriority( "@IC@000", 8000, 1 );
                    wait( 8000 );

                    show_text_highpriority( "@IC@001", 8000, 1 );
                    wait( 8000 );

                };
                __fade( false, true );
                __toggle_cinematic( false );
                to( index, 0, 3, h => { friendActors[ index ].set_immunities( false ); } );
            }, delegate { wait( 500 ); } );

            to( index, 0, 3, h => {
                and( CURRENT_GAME_LEVEL == 0, delegate {
                    friendActors[ index ].set_only_damaged_by_player( true );
                }, delegate {
                    friendActors[ index ].set_drops_weapons_when_dead( false );
                } );
                friendActors[ index ].set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                                     .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 )
                                     .set_acquaintance( AcquaintanceType.HATE, ActorType.MISSION2 )
                                     .set_max_health( enemyDefaultHealth ).set_health( enemyDefaultHealth )
                                     .set_suffers_critical_hits( false )
                                     .set_decision_maker( friendDecisionMaker )
                                     .enable_validate_position( true )
                                     .set_money( 0 )
                                     .set_weapon_attack_rate( 70 )
                                     .set_weapon_accuracy( 50 );
                friendMarkers[ index ].create_above_actor( friendActors[ index ] ).set_radar_mode( 1 ).set_type( true );
            } );
            enabledWeaponNumber.value = 25;
            friendMarkers[ 4 ].create_above_vehicle( player_car ).set_color( MarkerColor.GREEN ).set_size( 2 ).set_type( true );
            a.give_weapon( enabledWeaponNumber, 300 ).set_armed_weapon( enabledWeaponNumber );
            destroy_model( used_models );
            player_car.set_immunities( false, false, true, true, false );
            __camera_default();
            wait( 1500 );
            set_radar_zoom( 4 );
            __fade( true, true );
            p.ignored_by_cops( true );
            set_sensitivity_to_crime( 0.0 );
            __disable_player_controll_in_cutscene( false );
            MISSION_GLOBAL_TIMER_1.start( TimerType.DOWN, "BB_19" );
            MISSION_GLOBAL_STATUS_TEXT_1.create( StatusTextType.NUMBER, "@INC@21" );

            Cycle += delegate {
                wait( 0 );

                and( !a.is_in_area_2d( 0, -132.8564, 1085.3704, -49.7765, 1192.6573 ), delegate {
                    ___jump_failed( "@INC@15" );
                } );

                player_car.do_if_wrecked( delegate { ___jump_failed( "@INC@17" ); } );

                a.get_current_weapon( temp );
                and( temp != enabledWeaponNumber, delegate {
                    and( temp != 0, temp != 1, delegate {
                        ___jump_failed( "@INC@16" );
                    } );
                } );

                or( a.is_in_any_vehicle(), p.is_on_jetpack(), delegate {
                    ___jump_failed( "@INC@17" );
                } );

                to( index, 0, 3, h => {
                    and( friendActors[ index ].is_defined(), delegate {
                        and( friendActors[ index ].is_dead(), delegate {
                            friendMarkers[ index ].disable_if_exist();
                            friendActors[ index ].remove_references().destroy_with_fade();
                        }, delegate {
                            friendActors[ index ].get_task_status( 1869, temp );
                            and( temp == 7, delegate {
                                friendActors[ index ].clear_tasks();
                                to( index2, 0, TOTAL_ENEMIES, h2 => {
                                    and( enemyActors[ index2 ].is_defined(), delegate {
                                        and( !enemyActors[ index2 ].is_dead(), delegate {
                                            friendActors[ index ].get_position( tempX[ 9 ], tempY[ 9 ], tempZ[ 9 ] );
                                            enemyActors[ index2 ].get_position( tempX[ 8 ], tempY[ 8 ], tempZ[ 8 ] );
                                            get_distance_2d( tempX[ 9 ], tempY[ 9 ], tempX[ 8 ], tempY[ 8 ], tempX[ 9 ] );
                                            and( 40.0 > tempX[ 9 ], delegate {
                                                friendActors[ index ].task.shoot_at_actor( enemyActors[ index2 ], 3000 );
                                            } );
                                        } );
                                    } );
                                } );
                            } );
                        } );
                    } );
                } );

                to( index, 0, TOTAL_ENEMIES, h => {
                    and( enemyActors[ index ].is_defined(), delegate {
                        and( enemyActors[ index ].is_dead(), delegate {
                            and( enemyMarkers[ index ].is_enabled(), delegate {
                                MISSION_GLOBAL_STATUS_TEXT_1 += 1;
                                enemyActors[ index ].remove_references().destroy_with_fade();
                            } );
                            enemyMarkers[ index ].disable_if_exist();
                        }, delegate {
                            enemyActors[ index ].set_bleeding( true ).get_task_status( 1997, temp );
                            and( temp == 7, delegate {
                                enemyActors[ index ].clear_tasks().task.walk_to_point( -83.7493, 1159.569, 19.1728, 0.0, 2.0 );
                            } );
                            and( enemyActors[ index ].is_near_vehicle_3d( false, player_car, 20.0, 20.0, 4.0 ), delegate {
                                and( !is_text_priority_displayed(), delegate {
                                    show_text_highpriority( "@INC@18", 2000, 1 );
                                } );
                                or( enemyActors[ index ].is_colliding_with_vehicle( player_car ),
                                    enemyActors[ index ].is_near_vehicle_3d( false, player_car, 2.5, 2.5, 4.0 ),
                                delegate {
                                    ___jump_failed( "@INC@19" );
                                } );
                            } );
                        } );
                    }, delegate {
                        and( !is_sphere_onscreen( tempX[ index ], tempY[ index ], tempZ[ index ], 4.0 ), delegate {
                            enemyActors[ index ].create_random( tempX[ index ], tempY[ index ], tempZ[ index ] )
                                                .set_z_angle( tempA[ index ] )
                                                .set_walk_style( WalkStyle.FATMAN )
                                                .set_acquaintance( AcquaintanceType.RESPECT, ActorType.MISSION1 )
                                                .set_acquaintance( AcquaintanceType.RESPECT, ActorType.PLAYER )
                                                .set_suffers_critical_hits( false )
                                                .set_decision_maker( friendDecisionMaker ) // enemyDecisionMaker
                                                .set_money( 0 );
                            enemyMarkers[ index ].create_above_actor( enemyActors[ index ] ).set_size( 1 );
                        } );
                    } );
                } );

                and( 1 > MISSION_GLOBAL_TIMER_1, delegate {
                    temp.value = MISSION_GLOBAL_STATUS_TEXT_1;
                    temp *= 75;
                    reward += temp;
                    and( 1 > CURRENT_GAME_LEVEL, delegate {
                        temp.value = 0;
                        to( index, 0, 3, h => {
                            and( friendActors[ index ].is_defined(), delegate {
                                and( !friendActors[ index ].is_dead(), delegate {
                                    temp += 250;
                                } );
                            } );
                        } );
                        reward += temp;
                    } );
                    and( a.is_has_weapon( enabledWeaponNumber ), delegate {
                        a.get_ammo_in_weapon( enabledWeaponNumber, temp );
                        temp *= 2;
                        reward += temp;
                    } );
                    jump( PASSED_WITH_TELEPORT );
                } );

            };

        }
        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------

        #region CUTSCENES
        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------

        private void STORE_PLAYER_WEAPONS( LabelGosub label ) {
            isPlayerWeaponSaved.value = true;
            to( index, 1, ( TOTAL_WEAPONS_GROUPS - 1 ), h => {
                a.get_weapon_data( index, savedWeaponsNumber[ index ], saveWeaponsAmmos[ index ], savedWeaponsModels[ index ] );
            } );
            and( savedWeaponsNumber[ 1 ] == WeaponNumber.UNARMED, delegate {
                saveWeaponsAmmos[ 1 ].value = 0;
            } );
            a.remove_all_weapons();
        }
        private void RESTORE_PLAYER_WEAPONS( LabelGosub label ) {
            and( isPlayerWeaponSaved == true, delegate {
                a.remove_all_weapons();
                to( index, 1, ( TOTAL_WEAPONS_GROUPS - 1 ), h => {
                    and( saveWeaponsAmmos[ index ] > 0, delegate {
                        load_requested_models( savedWeaponsModels[ index ] );
                        a.give_weapon( savedWeaponsNumber[ index ], saveWeaponsAmmos[ index ] );
                        destroy_model( savedWeaponsModels[ index ] );
                    } );
                } );
                a.set_armed_weapon( WeaponNumber.UNARMED );
            } );
        }

        private void MOVE_PLAYER_TO_RESTART_POINT( LabelGosub label ) {
            cancel_override_restart();
            __disable_player_controll_in_cutscene( true );
            a.extinguish_current_car_if_exist( tempCar );
            __fade( false, true );
            clear_area( true, RESTART_POSITION_X, RESTART_POSITION_Y, RESTART_POSITION_Z, 30.0 );
            __renderer_at( RESTART_POSITION_X, RESTART_POSITION_Y, RESTART_POSITION_Z );
            a.teleport_without_car( RESTART_POSITION_X, RESTART_POSITION_Y, RESTART_POSITION_Z, RESTART_POSITION_A );
            Gosub += RESTORE_PLAYER_WEAPONS;
            wait( 500 );
            __camera_default();
            __fade( true );
            __disable_player_controll_in_cutscene( false );
        }

        #region PASSED
        private void PASSED_WITH_TELEPORT( LabelJump label ) {
            Gosub += MOVE_PLAYER_TO_RESTART_POINT;
            jump_passed();
        }
        private void DEFAULT_PASSED() {
            and( reward > 0, delegate {
                show_text_1number_styled( sString.M_PASS, reward, 5000, 1 );
                p.add_money( reward );
            }, delegate {
                show_text_styled( sString.M_PASSD, 5000, 1 );
            } );
            and( playTune == true, delegate {
                play_music( MusicID.MISSION_PASSED );
            } );
            set_latest_mission_passed( CURRENT_MISSION_NAME );
            INCORP_MISSION_PASSED += 1;
            INCORP_MOVIE_FLAG.value = false;
            set_made_progress();
            and( 14 > INCORP_MISSION_PASSED, delegate {
                create_thread<INCORST>();
            } );
            and( INCORP_MISSION_PASSED >= 14, delegate {
                CAR_PARK.set_chance_to_generate( CJ_PROTOTYPE_CAR, 101 );
                show_text_lowpriority( "@INC@20", 12000, 1 );
            } );
        }
        #endregion

        #region FAILED
        private void FAILED_WITH_TELEPORT( LabelJump label ) {
            Gosub += MOVE_PLAYER_TO_RESTART_POINT;
            jump_failed();
        }
        private void DEFAULT_FAILED() {
            show_text_styled( sString.M_FAIL, 5000, 1 );
            and( failedMessage != sString.DUMMY, delegate { show_text_lowpriority( failedMessage, 6000, 1 ); } );
            create_thread<INCORST>();
        }
        #endregion

        #region CLEAR
        private void DEFAULT_CLEAR() {
            __set_entered_names( true );
            __set_traffic( 1.0 );
            __set_player_ignore( false );
            __set_police_generator( true );
            set_sensitivity_to_crime( 1.0 );
            release_weather();
            //panel.remove();
            p.enable_group_recruitment( true );
            set_radar_zoom( 1 );
            a.set_muted( false ).set_can_be_knocked_off_bike( false );
            enable_train_traffic( true );
            enable_planes_traffic( true );
            enable_emergency_traffic( true );
            g.release();
            switch_roads_on( -361.7219, 962.9586, -10.0, 177.2781, 1260.9586, 40.0 );
            Gosub += CLEAR_ACTIVE_ENTITIES;
            Gosub += CLEAR_CUTSCENE_ENTITIES;
            //Gosub += CLEAR_PATH;
            AUDIO_BG.unload();
            wait( AUDIO_BG.is_stopped );
            AUDIO_PL.unload();
            wait( AUDIO_PL.is_stopped );
        }

        private void CLEAR_ACTIVE_ENTITIES( LabelGosub label ) {
            //checkpoint.disable_if_exist();
            friendDecisionMaker.release();
            //enemyDecisionMaker.release();
            MISSION_GLOBAL_STATUS_TEXT_1.remove();
            //MISSION_GLOBAL_STATUS_TEXT_2.remove();
            //MISSION_GLOBAL_STATUS_TEXT_3.remove();
            MISSION_GLOBAL_TIMER_1.stop();
            //helpWeapon.destroy_if_exist();
            enemyActors.each( index, m => {
                enemyMarkers[ index ].disable_if_exist();
                friendMarkers[ index ].disable_if_exist();
                enemyActors[ index ].destroy_if_exist();
                friendActors[ index ].destroy_if_exist();
                //    targetObjects[ index ].destroy_if_exist();
                //    enemyCars[ index ].destroy_if_exist();
                //    puckups[ index ].destroy_if_exist();
                //    enemyAS[ index ].clear();
                //    friendAS[ index ].clear();
            } );
            player_car.destroy_if_exist();
        }

        private void CLEAR_CUTSCENE_ENTITIES( LabelGosub label ) {
            cutcsene_objects.each( index, o => { o.destroy_if_exist(); } );
            cutcsene_actors.each( index, a => { a.destroy_if_exist(); } );
            cutcsene_cars.each( index, v => { v.destroy_if_exist(); } );
        }
        #endregion

    }

}