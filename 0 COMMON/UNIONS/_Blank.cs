using GTA;
using GTA.Core;
//using GTA.Plugins;

public sealed class Number : Union {

    private Int _i;
    private Float _f;

    public Int vali { set => _i.value = value; }
    public Float valf { set => _f.value = value; }
    
    public Int i { get => _i; set { } }
    public Float f { get => _f; set { } }

    protected override void OnLocalAutoInit() {
        _i = Script.local();
        _f = Script.local( Variable.IndexOf( _i ) );
    }

    protected override void OnGlobalAutoInit() {
        _i = Script.global();
        _f = Script.global( Variable.IndexOf( _i ) );
    }

    public static implicit operator Out<Int>( Number n ) { return n._i; }
    public static implicit operator Out<Float>( Number n ) { return n._f; }

    public static implicit operator Int( Number n ) { return n._i; }
    public static implicit operator Float( Number n ) { return n._f; }

}

partial class MAIN {

    public sealed class TT : Thread {

        Number number;

        public override void START( LabelJump label ) {

            number.vali = 10;
            wait( number );

            number.valf = 0.4;
            set_gamespeed( number );

            get_active_interior( number );
            get_progress_percentage( number );


            end_thread();
        }

    }

}