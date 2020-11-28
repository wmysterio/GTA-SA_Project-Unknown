using GTA;
using GTA.Plugins;

public partial class MAIN : Thread {

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
        if( IS_DEBUG ) {
            unsafe_code( "for 0@ = 354164 to 354188\r\n    &0(0@,1i) = 16843009\r\nend" );
            restart_if_busted( 2488.562, -1666.865, 12.8757, 0.0, 0 );
            restart_if_wasted( 2488.562, -1666.865, 12.8757, 0.0, 0 );
            create_thread<STARTGM>();
            end_thread();
        } else {
            Original.Begin( setup => {
                setup.EnableAll = true;
                setup.OpenAllMapZones = true;
                setup.DisableCheats = DISABLE_RELEASE_CHEATS;
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

}