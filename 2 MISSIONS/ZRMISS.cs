using GTA;

partial class MAIN {

    public sealed class ZRMISS : Mission {

        private void ___jump_failed( string gxt ) {
            failedMessage.value = gxt;
            jump_failed();
        }

        // ---------------------------------------------------------------------------------------------------------------------------

        const ushort CUTSCENE_ENTITY_ARRAY_SIZE = 10, STRIP_CLUP_MONEY_PER_DAY = 6000;

        // ---------------------------------------------------------------------------------------------------------------------------

        Int reward, index;

        sString failedMessage;

        // 34 + 2*1 + 1*2 = 38

        Array<Actor> cutcsene_actors = local_array( 0, CUTSCENE_ENTITY_ARRAY_SIZE );
        Array<Car> cutcsene_cars = local_array( 10, CUTSCENE_ENTITY_ARRAY_SIZE );
        Array<Object> cutcsene_objects = local_array( 20, CUTSCENE_ENTITY_ARRAY_SIZE );

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            wait( 1000 );
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
            and( ZERO_TOTAL_MISSION_PASSED > 0, delegate {
                a.set_position( 2511.0583, 2124.2844, 9.8203 ).set_z_angle( 2.1699 );
            } );
            /* DEBUG END */

            jump_table( ZERO_TOTAL_MISSION_PASSED, table => {
                table.Auto += MISSION_0;
                table.Auto += l => { jump( l.EndJumpTable ); }; // Skip "ZERO_TOTAL_MISSION_PASSED == 1" as strip club buying
                table.Auto += MISSION_1;
                table.Auto += MISSION_2;
                table.Auto += MISSION_3;
                table.Auto += MISSION_4;
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

        // ---------------------------------------------------------------------------------------------------------------------------

        #region CUTSCENES
        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------

        #region PASSED
        private void DEFAULT_PASSED() {
            and( reward > 0, delegate {
                show_text_1number_styled( sString.M_PASS, reward, 5000, 1 );
                p.add_money( reward );
            }, delegate {
                show_text_styled( sString.M_PASSD, 5000, 1 );
            } );
            play_music( MusicID.MISSION_PASSED );
            set_latest_mission_passed( CURRENT_MISSION_NAME );
            ZERO_TOTAL_MISSION_PASSED += 1;
            set_made_progress();
            and( ZERO_TOTAL_MISSION_PASSED == 1, delegate {
                ZERO_START_X.value = 2503.9441;
                ZERO_START_Y.value = 2124.4758;
                ZERO_START_Z.value = 9.8203;
                create_thread<UNLOCK2>();
            } );
            and( ZERO_TOTAL_MISSION_PASSED == 6, delegate {
                STRIP_IN_LV_ASSET_MONEY.create( UNLOCK2.X, UNLOCK2.Y, UNLOCK2.Z, STRIP_CLUP_MONEY_PER_DAY, STRIP_CLUP_MONEY_PER_DAY );
            } );
            and( 6 > ZERO_TOTAL_MISSION_PASSED, delegate {
                create_thread<ZRSTART>();
            } );
        }
        #endregion

        #region FAILED
        private void DEFAULT_FAILED() {
            show_text_styled( sString.M_FAIL, 5000, 1 );
            and( failedMessage != sString.DUMMY, delegate { show_text_lowpriority( failedMessage, 6000, 1 ); } );
            create_thread<ZRSTART>();
        }
        #endregion

        #region CLEAR
        private void DEFAULT_CLEAR() {
            __set_entered_names( true );
            __set_traffic( 1.0 );
            __set_player_ignore( false );
            __set_police_generator( true );
            set_sensitivity_to_crime( 1.0 );
            //panel.remove();
            p.enable_group_recruitment( true );
            a.set_muted( false ).set_can_be_knocked_off_bike( false );
            enable_train_traffic( true );
            enable_planes_traffic( true );
            enable_emergency_traffic( true );
            g.release();
            //switch_roads_on( 2738.6042, 826.7645, -10.0, 2916.0, 1041.7644, 40.0 );
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
            //friendDecisionMaker.release();
            //enemyDecisionMaker.release();
            //MISSION_GLOBAL_STATUS_TEXT_1.remove();
            //MISSION_GLOBAL_STATUS_TEXT_2.remove();
            //MISSION_GLOBAL_STATUS_TEXT_3.remove();
            //MISSION_GLOBAL_TIMER_1.stop();
            //helpWeapon.destroy_if_exist();
            //enemyMarkers.each( index, m => {
            //    enemyMarkers[ index ].disable_if_exist();
            //    friendMarkers[ index ].disable_if_exist();
            //    targetObjects[ index ].destroy_if_exist();
            //    enemyActors[ index ].destroy_if_exist();
            //    friendActors[ index ].destroy_if_exist();
            //    enemyCars[ index ].destroy_if_exist();
            //    puckups[ index ].destroy_if_exist();
            //    enemyAS[ index ].clear();
            //    friendAS[ index ].clear();
            //} );
            //and( is_m6_mission == true, delegate {
            //    terroristsMarkers.each( index, m => {
            //        terroristsMarkers[ index ].disable_if_exist();
            //        terroristsActors[ index ].destroy_if_exist();
            //    } );
            //    train.destroy_if_exist();
            //} );
            //player_car.destroy_if_exist();
        }

        private void CLEAR_CUTSCENE_ENTITIES( LabelGosub label ) {
            cutcsene_objects.each( index, o => { o.destroy_if_exist(); } );
            cutcsene_actors.each( index, a => { a.destroy_if_exist(); } );
            cutcsene_cars.each( index, v => { v.destroy_if_exist(); } );
        }
        #endregion

    }

}