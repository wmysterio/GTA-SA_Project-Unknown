using System;
using GTA;
using GTA.Core;

namespace demo_app_v7 {

    class Program {

        static void Main( string[] args ) {

            Generator.Language = Language.RU;
            Generator.SetFXTFolder( @"D:\Programm\GTA_SA\modloader\wmysterio\CLEO\CLEO_TEXT" );
            Generator.SetMainSCMFolder( @"D:\Programm\GTA_SA\modloader\wmysterio\data\script" );
            //Generator.SetGTAFolder( @"D:\Programm\GTA_SA" );
            Generator.SetSannyBuidlerFolder( @"D:\Programm\Sanny Builder 3" );

            //Generator.Start<MAIN>( /*true*/ );
            Generator.Compile<MAIN>( true );

            Console.ReadKey();
        }
    }

}