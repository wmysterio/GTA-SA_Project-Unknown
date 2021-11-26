using GTA;
using GTA.Plugins;

public partial class MAIN : Thread {

    static Actor a = PlayerActor;
    static Player p = PlayerChar;
    static Group g = PlayerGroup;

    // ---------------------------------------------------------------------------------------------------------------------------

    public override void START( LabelJump label ) {
        fade( false, 0 );
        set_max_wanted_level( 6 );
        set_int_stat( StatsID.CITY_UNLOCKED, 4 );
        __renderer_at( 2488.562, -1666.865, 13.3757 );
        p.create( 2488.562, -1666.865, 12.8757 ).get_actor( a ).get_group( g ).can_move( false )
         .set_clothes( "VEST", "VEST", ClothesBodyPart.TORSO )
         .set_clothes( "JEANSDENIM", "JEANS", ClothesBodyPart.LEGS )
         .set_clothes( "SNEAKERBINCBLK", "SNEAKER", ClothesBodyPart.SHOES )
         .set_clothes( "PLAYER_FACE", "HEAD", ClothesBodyPart.HEAD )
         .rebuild();
        save_player_clothes();
        release_weather();
        __clear_text();
        Gosub += MAIN_CUTSCENE;

        //create_thread<SELECTG>(); // Uncomment later
        //wait( IS_CURRENT_GAME_SELECTED == 0 ); // Uncomment later

        if( IS_DEBUG ) {
            unsafe_code( "for 0@ = 354164 to 354188\r\n    &0(0@,1i) = 16843009\r\nend" );
            restart_if_busted( 2488.562, -1666.865, 12.8757, 0.0, 0 );
            restart_if_wasted( 2488.562, -1666.865, 12.8757, 0.0, 0 );

            CURRENT_GAME_LEVEL.value = 0; // DEBUG
            and( CURRENT_GAME_LEVEL == 1, delegate { set_float_stat( StatsID_Float.MAX_HEALTH, 800.0 ); } );
            and( CURRENT_GAME_LEVEL == 2, delegate { set_float_stat( StatsID_Float.MAX_HEALTH, 1000.0 ); } );

            create_thread<STARTGM>();
            end_thread();
        } else {
            Original.Begin( setup => {
                setup.EnableAll = true;
                setup.OpenAllMapZones = true;
                setup.DisableCheats = false; // will setup at LEVELSE thread
                setup.EnableBonuses = false;
                setup.EnableImportExport = false;
                setup.EnableCrazyTricks = false;
                setup.EnableDefaultArmourPickups = false;
                setup.EnableDefaultUniqueJumps = false;
                setup.EnableEmmetsGun = false;
                setup.EnablePlayerGangMoney = false;
                setup.EnableWangCarsMoney = false;
                setup.EnableDefaultParkingCars = false;
                setup.EnableDefaultMeleeWeaponPickups = false;
                setup.EnableDefaultWeaponPickups = false;
                setup.After = delegate {
                    set_tags_status_in_area( -3000.0, -3000.0, 3000.0, 3000.0, 100 );
                    set_int_stat( StatsID.TAGS_SPRAYED, 100 );
                    set_int_stat( StatsID.OYSTERS_COLLECTED, 50 );
                    set_int_stat( StatsID.HORSESHOES_COLLECTED, 50 );
                    set_int_stat( StatsID.PHOTOGRAPHS_TAKEN, 50 );
                    set_int_stat( StatsID.UNIQUE_JUMPS_FOUND, 70 );
                    set_int_stat( StatsID.UNIQUE_JUMPS_DONE, 70 );
                    create_thread<STARTGM>();
                };
            } );
        }
        Texts.generate();
    }

    private void MAIN_CUTSCENE( LabelGosub label ) {
        wait( 2000 );
        __clear_text();
        __disable_player_controll_in_cutscene( true );
        __set_player_ignore( true );
        __toggle_cinematic( true );

        clear_area( false, 2471.8965, -1685.6204, 13.5078, 300.0 );
        a.set_position( 2471.8965, -1685.6204, 13.5078 );
        // load and create entities

        wait( 1000 );
        __fade( true, false );
        Scene += delegate {
            wait( 5000 );

        };
        __fade( false, true );

        // destroy all entities

        clear_area( false, 2488.562, -1666.865, 12.8757, 300.0 );
        __renderer_at( 2488.562, -1666.865, 12.8757 );
        a.set_position( 2488.562, -1666.865, 12.8757 );
        __camera_default();
        __toggle_cinematic( false );
        __set_player_ignore( false );
        __disable_player_controll_in_cutscene( false );
        wait( 1500 );
        fade( 1, 1000 );
    }
}