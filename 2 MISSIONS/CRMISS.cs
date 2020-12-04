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

        const ushort MAX_ARRAY_OF_ACTIVE_COUNT = 10;

        // ---------------------------------------------------------------------------------------------------------------------------

        Int index, loaded_path;
        sString failedMessage;
        DecisionMaker enemyDecisionMaker, friendDecisionMaker;
        Pickup helpWeapon;

        Array<Actor> enemyActors = MAX_ARRAY_OF_ACTIVE_COUNT, friendActors = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Marker> enemyMarkers = MAX_ARRAY_OF_ACTIVE_COUNT, friendMarkers = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Car> enemyCars = MAX_ARRAY_OF_ACTIVE_COUNT;
        Array<Object> targetObjects = MAX_ARRAY_OF_ACTIVE_COUNT;

        // 34 + 6 + 6*10 = 100 of 1023

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
            jump_table( CR_TOTAL_MISSION_PASSED, table => {
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

        private void MISSION_0( LabelCase l ) {
            jump_passed();
        }
        private void MISSION_1( LabelCase l ) {
            jump_passed();
        }
        private void MISSION_2( LabelCase l ) {
            jump_passed();
        }
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
        #endregion

        #region CUTSCENES

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
            CR_TOTAL_MISSION_PASSED += 1;
            set_made_progress();
            and( CR_TOTAL_MISSION_PASSED == 5, delegate {
                create_thread<BLSTART>();
                @return();
            } );
            and( 9 > CR_TOTAL_MISSION_PASSED, delegate {
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