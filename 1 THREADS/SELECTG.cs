using GTA;

public partial class MAIN {

    /*
        0 : EASY
        1 : NORMAL
        2 : HARD
    */
    public static Int CURRENT_GAME_LEVEL, IS_CURRENT_GAME_SELECTED;

    public class SELECTG : Thread {

        Panel levelSelectorPanel;

        public override void START( LabelJump label ) {
            wait( DefaultWaitTime );
            and( START, p.is_defined() );
            and( START, !a.is_dead(), !a.is_busted() );
            __set_player_ignore( true );
            p.can_move( false );

            sString[] levelNames = new sString[] { "@LSCT@1", "@LSCT@2", "@LSCT@3" };

            levelSelectorPanel.create( "@LSCT@0", 29.0, 100.0, 300.0, 1, true, true, PanelAlign.LEFT )
                              .set_column_data( 0, sString.DUMMY, levelNames )
                              .set_active_row( 1 );
            __clear_text();
            Jump += LOOP;
        }

        private void LOOP( LabelJump label ) {
            wait( 0 );
            and( !is_text_box_displayed( "@CRS@53" ), delegate { show_permanent_text_box( "@CRS@53" ); } );
            or( !p.is_defined(), delegate { Gosub += REMOVE_PANEL; jump( START ); } );
            or( a.is_dead(), a.is_busted(), delegate { Gosub += REMOVE_PANEL; jump( START ); } );
            and( is_game_key_pressed( Keys.PED_SPRINT ), delegate {
                levelSelectorPanel.get_active_row( CURRENT_GAME_LEVEL );
                show_formatted_text_box( "Selected level: %d", CURRENT_GAME_LEVEL );
                Gosub += REMOVE_PANEL;
                IS_CURRENT_GAME_SELECTED.value = true;
                end_thread();
            } );
            jump( label );
        }

        private void REMOVE_PANEL( LabelGosub label ) {
            levelSelectorPanel.remove();
            and( p.is_defined(), delegate { __set_player_ignore( false ); p.can_move( true ); } );
            __clear_text();
        }

    }

}