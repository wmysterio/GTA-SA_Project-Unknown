using GTA;

public partial class MAIN {

    public class DISPLBL : Thread {

        static EntranceMarker BLACK_LIST_ENTRANCE_MARKER;

        Int index, number_of_index, black_list_member_count;
        Float text_offset_Y;
        Array<sString> black_list_names = 7;

        // ---------------------------------------------------------------------------------------------------------------------------

        public override void START( LabelJump label ) {
            BLACK_LIST_ENTRANCE_MARKER.create( 24.1709, -2646.7153, 41.1647, 8 );
            Jump += LOOP;
        }
        private void LOOP( LabelJump label ) {
            wait( DefaultWaitTime );
            and( BLACK_LIST_MISSION_PASSED > 6, delegate { Jump += END; } );
            and( BLACK_LIST_MISSION_PASSED > 0, p.is_defined(), delegate {
                and( !p.is_on_jetpack(), a.is_defined(), delegate {
                    and( OnMission == 0, !a.is_busted(), !a.is_dead(), a.is_near_point_3d_stopped_on_foot( false, 24.1709, -2646.7153, 40.4647, 1.0, 1.0, 2.0 ), delegate {
                        clear_text_with_style( true );
                        __disable_player_controll_in_cutscene( true );
                        __set_traffic( 0.0 );
                        __set_player_ignore( true );
                        clear_area( true, 24.1709, -2646.7153, 40.4647, 10.0 );
                        __set_entered_names( false );
                        a.task.walk_to_point( 22.3857, -2647.0625, 40.4663, 96.4362, 1.0 );
                        wait( a.is_stopped() );
                        __camera_default();
                        black_list_member_count.value = 7;
                        black_list_member_count -= BLACK_LIST_MISSION_PASSED;
                        Gosub += SET_NAMES;
                        show_permanent_text_box( "@BLS@25" );
                        DRAW.enable( true );
                        Jump += LOOP_2;
                    } );
                } );
            } );
            jump( LOOP );
        }
        private void LOOP_2( LabelJump label ) {
            wait( 0 );
            or( OnMission != 0, a.is_busted(), a.is_dead(), is_game_key_pressed( Keys.VEHICLE_ENTER_EXIT ), delegate {
                __clear_text();
                DRAW.enable( false );
                __set_player_ignore( false );
                __set_traffic( 1.0 );
                __set_entered_names( true );
                __disable_player_controll_in_cutscene( false );
                jump( LOOP );
            } );
            DRAW.display_rectangle( 320.0, 240.0, 310.0, 230.0, 0, 0, 0, 120 ).display_box( 160.0, 120.0, 480.0, 360.0, "@BLS@0", 0 );
            text_offset_Y.value = 135.0;
            to( index, 0, 6, delegate {
                number_of_index.value = index;
                number_of_index += 1;
                and( index == black_list_member_count, delegate { DRAW.set_color( 180, 180, 0, 255 ); } );
                Gosub += SET_DEFAULT_TEXT_SETTING;
                DRAW.display_text_with_1number( 170.0, text_offset_Y, "@BLS@26", number_of_index );
                and( index == black_list_member_count, delegate { DRAW.set_color( 180, 180, 0, 255 ); } );
                Gosub += SET_DEFAULT_TEXT_SETTING;
                DRAW.display_text( 200.0, text_offset_Y, black_list_names[ index ] );
                text_offset_Y += 32.0;
            } );
            jump( LOOP_2 );
        }
        private void SET_NAMES( LabelGosub label ) {
            jump_table( BLACK_LIST_MISSION_PASSED, table => {
                table.Auto += delegate { jump( table.EndLabel ); };
                table.Auto += rank1;
                table.Auto += rank2;
                table.Auto += rank3;
                table.Auto += rank4;
                table.Auto += rank5;
                table.Auto += rank6;

                void rank1( LabelCase l ) {
                    black_list_names[ 0 ].value = "@BLS@23"; // #1
                    black_list_names[ 1 ].value = "@BLS@22"; // #2
                    black_list_names[ 2 ].value = "@BLS@21"; // #3
                    black_list_names[ 3 ].value = "@BLS@20"; // #4
                    black_list_names[ 4 ].value = "@BLS@19"; // #5
                    black_list_names[ 5 ].value = "@BLS@18"; // #6
                    black_list_names[ 6 ].value = "@BLS@24"; // #7
                    jump( table.EndLabel );
                }
                void rank2( LabelCase l ) {
                    black_list_names[ 0 ].value = "@BLS@23"; // #1
                    black_list_names[ 1 ].value = "@BLS@22"; // #2
                    black_list_names[ 2 ].value = "@BLS@21"; // #3
                    black_list_names[ 3 ].value = "@BLS@20"; // #4
                    black_list_names[ 4 ].value = "@BLS@19"; // #5
                    black_list_names[ 5 ].value = "@BLS@24"; // #6
                    black_list_names[ 6 ].value = "@BLS@18"; // #7
                    jump( table.EndLabel );
                }
                void rank3( LabelCase l ) {
                    black_list_names[ 0 ].value = "@BLS@23"; // #1
                    black_list_names[ 1 ].value = "@BLS@22"; // #2
                    black_list_names[ 2 ].value = "@BLS@21"; // #3
                    black_list_names[ 3 ].value = "@BLS@20"; // #4
                    black_list_names[ 4 ].value = "@BLS@24"; // #5
                    black_list_names[ 5 ].value = "@BLS@19"; // #6
                    black_list_names[ 6 ].value = "@BLS@18"; // #7
                    jump( table.EndLabel );
                }
                void rank4( LabelCase l ) {
                    black_list_names[ 0 ].value = "@BLS@23"; // #1
                    black_list_names[ 1 ].value = "@BLS@22"; // #2
                    black_list_names[ 2 ].value = "@BLS@21"; // #3
                    black_list_names[ 3 ].value = "@BLS@24"; // #4
                    black_list_names[ 4 ].value = "@BLS@20"; // #5
                    black_list_names[ 5 ].value = "@BLS@19"; // #6
                    black_list_names[ 6 ].value = "@BLS@18"; // #7
                    jump( table.EndLabel );
                }
                void rank5( LabelCase l ) {
                    black_list_names[ 0 ].value = "@BLS@23"; // #1
                    black_list_names[ 1 ].value = "@BLS@22"; // #2
                    black_list_names[ 2 ].value = "@BLS@24"; // #3
                    black_list_names[ 3 ].value = "@BLS@21"; // #4
                    black_list_names[ 4 ].value = "@BLS@20"; // #5
                    black_list_names[ 5 ].value = "@BLS@19"; // #6
                    black_list_names[ 6 ].value = "@BLS@18"; // #7
                    jump( table.EndLabel );
                }
                void rank6( LabelCase l ) {
                    black_list_names[ 0 ].value = "@BLS@23"; // #1
                    black_list_names[ 1 ].value = "@BLS@24"; // #2
                    black_list_names[ 2 ].value = "@BLS@22"; // #3
                    black_list_names[ 3 ].value = "@BLS@21"; // #4
                    black_list_names[ 4 ].value = "@BLS@20"; // #5
                    black_list_names[ 5 ].value = "@BLS@19"; // #6
                    black_list_names[ 6 ].value = "@BLS@18"; // #7
                    jump( table.EndLabel );
                }

            } );
        }
        private void SET_DEFAULT_TEXT_SETTING( LabelGosub label ) { 
            DRAW.set_font( 2 ).set_letter_scale( 0.46, 2.34 ).set_linewidth( DRAW.WIDTH ); 
        }
        private void END( LabelJump label ) { 
            BLACK_LIST_ENTRANCE_MARKER.disable(); 
            end_thread();
        }

    }

}