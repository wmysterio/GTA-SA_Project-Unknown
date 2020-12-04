using GTA;
using GTA.Plugins;

public partial class MAIN {

    static Phone CJ_PHONE;

    // ---------------------------------------------------------------------------------------------------------------------------

    public class CJPHONE : Thread { 

        public override void START( LabelJump label ) {

            CJ_PHONE = new Phone( setup=> {

                setup.set_max_time_between_calls( 20000 );
                setup.OnLoadData = d => { chdir( $@"Sound\PHONE\DIALOG{d.DialogID}" ); AUDIO_PL.load( d.TotalReplicas ); wait( AUDIO_PL.is_ready ); };
                setup.OnReplicaChanged = delegate { AUDIO_PL.play(); };
                setup.OnUnloadData = delegate { AUDIO_PL.unload(); wait( AUDIO_PL.is_stopped ); };

                setup.add_dialog( 0, dialog => {
                    dialog.add_replica( "@PHS@00", 1000, true );
                    dialog.add_replica( "@PHS@01", 3500 );
                    dialog.add_replica( "@PHS@02", 4000, true );
                    dialog.add_replica( "@PHS@03", 6000 );
                    dialog.add_replica( "@PHS@04", 5000 );
                    dialog.add_replica( "@PHS@05", 4500, true );
                    dialog.add_replica( "@PHS@06", 5000 );
                    dialog.OnComplete = delegate { create_thread<CJSTART>(); };
                } );

                setup.add_dialog( 1, dialog => {
                    dialog.add_replica( "@PHS@07", 3500 );
                    dialog.add_replica( "@PHS@08", 1500, true );
                    dialog.add_replica( "@PHS@09", 5500 );
                    dialog.add_replica( "@PHS@10", 1500, true );
                    dialog.add_replica( "@PHS@11", 5000 );
                    dialog.add_replica( "@PHS@12", 5000 );
                    dialog.OnComplete = delegate { create_thread<CRSTART>(); };
                } );

                setup.add_dialog( 2, dialog => {
                    dialog.add_replica( "@PHS@13", 1000, true );
                    dialog.add_replica( "@PHS@14", 4000 );
                    dialog.add_replica( "@PHS@15", 100, true );
                    dialog.add_replica( "@PHS@16", 5500 );
                    dialog.add_replica( "@PHS@17", 5500, true );
                    dialog.add_replica( "@PHS@18", 6500 );
                    dialog.OnComplete = delegate { /* create_thread<COMPANY>(); */ };
                } );

                setup.add_dialog( 3, dialog => {
                    dialog.add_replica( "@PHS@19", 1000, true );
                    dialog.add_replica( "@PHS@20", 4500 );
                    dialog.add_replica( "@PHS@21", 3500, true );
                    dialog.add_replica( "@PHS@22", 4500 );
                    dialog.add_replica( "@PHS@23", 1000, true );
                    dialog.OnComplete = delegate { create_thread<CJSTART>(); /* create_thread<COMPANY>(); */ };
                } );

            } ); 

        } 

    }

}