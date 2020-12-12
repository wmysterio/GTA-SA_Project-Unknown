﻿using static GTA.Core.Script;
using GTA.Core;

public static class Texts {
    public static void generate() {
        FXT.set_file( $"wmysterio_{nameof( GXTEncoding.RU_SmartLoc )}" ).set_encoding( GXTEncoding.RU_SmartLoc )

        #region PHONE
        .add( "@PHS@00", "~p~CJ:~s~ Да." )
        .add( "@PHS@01", "~p~Сальваторе:~s~ Ты сделал очень глупую вещь, Карл Джонсон!" )
        .add( "@PHS@02", "~p~CJ:~s~ Почему Вы просто не пристрелили нас? Это же было так просто..." )
        .add( "@PHS@03", "~p~Сальваторе:~s~ Воровать очень плохо, тем более у меня! У тебя есть долг, и я дам тебе шанс его искупить." )
        .add( "@PHS@04", "~p~Сальваторе:~s~ Собери всю сумму до цента, и Свит будет целым и невредимым." )
        .add( "@PHS@05", "~p~CJ:~s~ Вы лжёте! Как только я принесу деньги, мы оба мертвецы." )
        .add( "@PHS@06", "~p~Сальваторе:~s~ Это тоже вероятно. Но у тебя нет выбора. Принеси мне мои грёбаные деньги!" )

        .add( "@PHS@07", "~p~Джон:~s~ Здравстуйте, Карл Джонсон!" )
        .add( "@PHS@08", "~p~CJ:~s~ Кто это?" )
        .add( "@PHS@09", "~p~Джон:~s~ Офицер Маркоски. У меня есть пара серьёзных вопросов. Приходите в участок." )
        .add( "@PHS@10", "~p~CJ:~s~ Не припоминаю такого." )
        .add( "@PHS@11", "~p~Джон:~s~ Я составлял протокол по делу аварии. Выяснилось кое-что важное." )
        .add( "@PHS@12", "~p~Джон:~s~ И не заставляйте меня искать Вас. Это будет плохая затея. Пока." )

        .add( "@PHS@13", "~p~CJ:~s~ Слушаю." )
        .add( "@PHS@14", "~p~Эрик:~s~ Мистер Джонсон, говорит Эрик Томсон." )
        .add( "@PHS@15", "~p~CJ:~s~ Кто?" )
        .add( "@PHS@16", "~p~Эрик:~s~ Работник компании, прототип машины которой Вы превратили в мусор." )
        .add( "@PHS@17", "~p~CJ:~s~ Я понял. В чём дело? Я уже выписал чек на крупную сумму. Этого мало?" )
        .add( "@PHS@18", "~p~Эрик:~s~ Достаточно, но я по делу. У нас есть для Вас работа. Приходите. Координаты я Вам скину!" )

        .add( "@PHS@19", "~p~CJ:~s~ Да." )
        .add( "@PHS@20", "~p~Эрик:~s~ Мистер Джонсон, это Эрик Томсон. Есть работа." )
        .add( "@PHS@21", "~p~CJ:~s~ Надеюсь, оплата приличная? Деньги мне нужны." )
        .add( "@PHS@22", "~p~Эрик:~s~ Об этом не беспокойтесь. Жду Вас возле офиса." )
        .add( "@PHS@23", "~p~CJ:~s~ Хорошо." )
        #endregion

        #region CJ
        .add( "@CJS@00", "Похищение и авария" )
        .add( "@CJS@01", "111" )
        .add( "@CJS@02", "Око за око" )
        .add( "@CJS@03", "" )
        .add( "@CJS@04", "" )
        .add( "@CJS@05", "" )
        .add( "@CJS@06", "" )
        .add( "@CJS@07", "" )
        .add( "@CJS@08", "" )
        .add( "@CJS@09", "" )
        .add( "@CJS@10", "" )
        .add( "@CJS@11", "" )
        .add( "@CJS@12", "" )
        .add( "@CJS@13", "" )
        .add( "@CJS@14", "" )
        .add( "@CJS@15", "" )
        .add( "@CJS@16", "~r~Ты утустил похитителей!" )
        .add( "@CJS@17", "~r~Свит умер!" )
        .add( "@CJS@18", "~s~Остановите ~b~похитителей~s~!" )
        .add( "@CJS@19", "~s~Проучите ~r~Балласов~s~!" )
        .add( "@CJS@20", "~r~Сизар умер!" )
        .add( "@CJS@21", "~r~Ты покинул зону сражения!" )



        /*
        .add( "@CJS@0", "" )
        .add( "@CJS@1", "" )
        .add( "@CJS@2", "" )
        .add( "@CJS@3", "" )
        .add( "@CJS@4", "" )
        .add( "@CJS@5", "" )
        .add( "@CJS@6", "" )
        .add( "@CJS@7", "" )
        .add( "@CJS@8", "" )
        .add( "@CJS@9", "" )
        */
        // REPLICAS
        //.add( "@CJ@000", "~р~:~s~ " )
        #endregion

        #region C.R.A.S.H.
        .add( "@CRS@00", "Друзья" )
        .add( "@CRS@01", "Remax" )
        .add( "@CRS@02", "111" )
        .add( "@CRS@03", "Идеальное убийство" )
        .add( "@CRS@04", "" )
        .add( "@CRS@05", "" )
        .add( "@CRS@06", "" )
        .add( "@CRS@07", "" )
        .add( "@CRS@08", "" )
        .add( "@CRS@09", "~s~Убейте отмеченных ~r~копов~s~!" )
        .add( "@CRS@10", "~r~Вас заметила камера наблюдения!" )
        .add( "@CRS@11", "~r~Вы не успели устранить цели вовремя!" )
        .add( "@CRS@12", "~s~Садитесь в ~b~машину~s~!" )
        .add( "@CRS@13", "~s~Отвезите Алекса ~y~домой~s~!" )
        .add( "@CRS@14", "~r~Транспорт уничтожен!" )
        .add( "@CRS@15", "~r~Алекс умер!" )


        #region на проверку VITAL

        .add( "@CRS@16", "~s~Отвезите машину в гараж ~y~8-Ball~s~." )
        .add( "@CRS@17", "~r~Вы починили машину!" )
        .add( "@CRS@18", "~s~Припаркуйте машину возле дома ~y~прокурора~s~." )
        .add( "@CRS@19", "~r~Вы привлекли внимание полиции!" )
        .add( "@CRS@20", "~r~Транспорт слишком повреждён!" )
        .add( "@CRS@21", "~r~Вы покинули город!" )
        .add( "@CRS@22", "~s~Отправляйтесь в указанную ~у~точку~s~!" )
        .add( "@CRS@23", "~s~Устраните ~r~цель~s~!" )
        .add( "@CRS@24", "~s~Ваша цель здесь. Быстро избавьтесь от неё!" )
        .add( "@CRS@26", "~r~Вы убили не того!" )
        .add( "@CRS@27", "~r~Вы покинули позицию!" )
        .add( "@CRS@28", "~r~Вы убили цель не тем оружием!" )
        .add( "@CRS@29", "~s~Приходите с ~1~:00 до 00:00." )

        #endregion
        .add( "@CRS@25", "~r~Вы не успели устранить цель вовремя!" )


        /*
        .add( "@CRS@0", "" )
        .add( "@CRS@1", "" )
        .add( "@CRS@2", "" )
        .add( "@CRS@3", "" )
        .add( "@CRS@4", "" )
        .add( "@CRS@5", "" )
        .add( "@CRS@6", "" )
        .add( "@CRS@7", "" )
        .add( "@CRS@8", "" )
        .add( "@CRS@9", "" )
        */
        // REPLICAS
        .add( "@CR@000", "~р~Джон:~s~ Несколько из моих друзей стали какие-то странные. Возможно я у них под подозрением." )
        .add( "@CR@001", "~р~Джон:~s~ Я попрошу тебя их завалить. Возьми тачку и убей их прежде, чем у них закончится патруль." )
        .add( "@CR@002", "~р~Джон:~s~ Тебе лучше взять пушку, чтобы не пришлось марать руки слишком долго." )
        .add( "@CR@003", "~р~СJ:~s~ Об этом можешь не беспокоиться!.. Я сделаю это." )
        .add( "@CR@004", "~р~Джон:~s~ И не светись перед мотоциклами. В них встроенная видеокамера." )
        .add( "@CR@005", "~р~CJ:~s~ Что, тоже на крючке копов?" )
        .add( "@CR@006", "~р~Алекс:~s~ Нет. Улики против меня оказались притянутыми за уши. Пришлось отпустить." )
        .add( "@CR@007", "~р~Алекс:~s~ Прокурор не нашёл ничего противозаконного. Даже наоборот: превысили полномочия." )
        .add( "@CR@008", "~р~Алекс:~s~ Пришлось отпустить и подвезти домой в качестве компенсации." )
        .add( "@CR@009", "~р~Алекс:~s~ Я - Алекс Ремакс, простой деревенский парень и рэпер-любитель." )
        .add( "@CR@010", "~р~CJ:~s~ Карл Джонсон: гангстер, убийца и банкрот." )
        .add( "@CR@011", "~р~Алекс:~s~ Неплохое резюме. Могу предложить работу, если всё это правда." )
        .add( "@CR@012", "~р~CJ:~s~ Хорошо, буду знать, к кому обращаться. Рэп давно читаешь?" )
        .add( "@CR@013", "~р~Алекс:~s~ Относительно давно. Уже выпустил два альбома, но конкуренция здесь огненная." )
        .add( "@CR@014", "~р~CJ:~s~ Серьёзно? А какое название группы? Послушаю на досуге." )
        .add( "@CR@015", "~р~Алекс:~s~ \"Ремакс\". Можешь меня в деревне в магазине купить. Оценишь." )
        .add( "@CR@016", "~р~CJ:~s~ Хорошо." )
        .add( "@CR@017", "~р~Джон:~s~ О, Карл Джонсон собственной персоной!" )
        .add( "@CR@018", "~р~CJ:~s~ Что тебе от меня опять надо?" )
        .add( "@CR@019", "~р~Джон:~s~ Маленькое поручение. Надо отвести одного мудака." )
        .add( "@CR@020", "~р~CJ:~s~ Ясно... Самому влом это сделать?" )
        .add( "@CR@021", "~р~Джон:~s~ Не задавай лишних вопросов. Мне не нравятся вопросы. Иди и выполняй!" )









        #region на проверку VITAL




        //.add( "CRRE9", "Время пострелять" )
        //.add( "CRRE10", "~s~Не дайте ~r~истцу~s~ доехать до участка!" )
        //.add( "CRRE11", "~r~Истец добрался до участка!" )
        //.add( "CRRE12", "~s~Вернитесь к участку!" )
        #endregion


        #endregion

        #region REMAX REPLICS  
        .add( "@RMS@00", "" )
        .add( "@RMS@01", "" )
        .add( "@RMS@02", "" )
        .add( "@RMS@03", "" )
        .add( "@RMS@04", "" )
        .add( "@RMS@05", "" )
        .add( "@RMS@06", "" )
        .add( "@RMS@07", "" )
        .add( "@RMS@08", "" )
        .add( "@RMS@09", "" )
        .add( "@RMS@10", "" )
        .add( "@RMS@11", "" )
        .add( "@RMS@12", "" )
        .add( "@RMS@13", "" )
        .add( "@RMS@14", "" )

        /*
        .add( "@CRS@0", "" )
        .add( "@CRS@1", "" )
        .add( "@CRS@2", "" )
        .add( "@CRS@3", "" )
        .add( "@CRS@4", "" )
        .add( "@CRS@5", "" )
        .add( "@CRS@6", "" )
        .add( "@CRS@7", "" )
        .add( "@CRS@8", "" )
        .add( "@CRS@9", "" )
        */
        // REPLICAS
        .add( "REMX0", "Стальные трубы" )
        .add( "REMX1", "~р~Rеmах:~s~ Ты все таки пришел! А я уже начал скучать." )
        .add( "REMX2", "~р~СJ:~s~ Как видишь. Что там у тебя за дела?" )
        .add( "REMX3", "~р~Rеmах:~s~ Ты знаешь стоимость больших стальных труб?" )
        .add( "REMX4", "~р~СJ:~s~ В данный момент не знаю. А зачем тебе цена?" )
        .add( "REMX5", "~р~Rеmах:~s~ Я знаю цену. И знаю где их достать. Не совсем легально, конечно." )
        .add( "REMX6", "~р~СJ:~s~ Их можно купить в специальных магазинах и легально." )
        .add( "REMX7", "~р~Rеmах:~s~ Верно. Только, если есть деньги. Но я знаю где их достать бесплатно." )
        .add( "REMX8", "~р~СJ:~s~ И где же?" )
        .add( "REMX9", "~р~Rеmах:~s~ Садись в машину, покажу. Прибыль делим пополам." )
        .add( "REMX10", "~s~Садитесь в ~b~машину~s~!" )
        .add( "REMX11", "~r~Rеmах умер! " )
        .add( "REMX12", "~r~Транспорт уничтожен!" )
        .add( "REMX13", "~s~Двигайтесь в указанную ~у~точку~s~!" )
        #endregion






        #region BLACK LIST
        .add( "@BLS@0", "Черный список" )
        .add( "@BLS@1", "Заезд" )
        .add( "@BLS@2", "Копы" )
        .add( "@BLS@3", "Секундомер" )
        .add( "@BLS@4", "Дуэль" )
        .add( "@BLS@5", "Тип испытания" )
        .add( "@BLS@6", "Сонни. Квалификация" )
        .add( "@BLS@7", "Маркус. Квалификация" )
        .add( "@BLS@8", "Лестер. Квалификация" )
        .add( "@BLS@9", "Марси. Квалификация" )
        .add( "@BLS@10", "Вик. Квалификация" )
        .add( "@BLS@11", "Дариус. Квалификация" )
        .add( "@BLS@12", "Сонни" )
        .add( "@BLS@13", "Маркус" )
        .add( "@BLS@14", "Лестер" )
        .add( "@BLS@15", "Марси" )
        .add( "@BLS@16", "Вик" )
        .add( "@BLS@17", "Дариус" )
        .add( "@BLS@18", "Сонни Вагама" )
        .add( "@BLS@19", "Маркус Флетчер" )
        .add( "@BLS@20", "Лестер Янг" )
        .add( "@BLS@21", "Марси Десскот" )
        .add( "@BLS@22", "Виктор Моррисон" )
        .add( "@BLS@23", "Дариус Фрей" )
        .add( "@BLS@24", "Карл Джонсон" )
        .add( "@BLS@25", "~к~~VЕHIСLЕ_ЕNTЕR_ЕХIT~ - Выход" )
        .add( "@BLS@26", "~1~." )
        .add( "@BLS@27", "Транспорт" )
        .add( "@BLS@28", "Место проведения" )
        .add( "@BLS@29", "Истребитель" )
        .add( "@BLS@30", "~r~Уровень розыска был слишком низким!" )
        .add( "@BLS@31", "~s~Уйдите от полиции!" )
        .add( "@BLS@32", "~s~Удерживайте уровень розыска, пока не истечет время." )
        .add( "@BLS@33", "~s~Успейте доехать к указанной точке за отведенное время." )
        .add( "@BLS@34", "~s~Следуйте за маркерами, пока не достигнете финиша." )
        .add( "@BLS@35", "Скорость: ~1~" )
        .add( "@BLS@36", "Нужно: ~1~" )
        .add( "@BLS@37", "~s~Набери максимально возможную скорость в указанных точках." )
        .add( "@BLS@38", "~r~Вам не удалось набрать необходимую сумму скоростей!" )
        .add( "@BLS@39", "~r~Транспорт соперника уничтожен!" )
        .add( "@BLS@40", "~r~Соперник умер!" )
        .add( "@BLS@41", "~r~Вы проиграли!" )
        .add( "@BLS@42", "~r~Гонщик скрылся!" )
        .add( "@BLS@43", "~s~Следуйте за ~b~гонщиком~s~!" )
        // REPLICAS
        .add( "@BL@0", "~р~Джон:~s~ Мистер Джонсон, как же я рад Вас видеть!" )
        .add( "@BL@1", "~р~СJ:~s~ Что тебе нужно?" )
        .add( "@BL@2", "~р~Джон:~s~ Ты еще смеешь не смотреть на меня?" )
        .add( "@BL@3", "~р~СJ:~s~ Лучше я буду смотреть на пейзаж впереди." )
        .add( "@BL@4", "~р~Джон:~s~ Удаленное место с мусорками возле окон сложно назвать пейзажем. Для тебя есть задание!" )
        .add( "@BL@5", "~р~СJ:~s~ Ага. И что на этот раз?" )
        .add( "@BL@6", "~р~Джон:~s~ В штате появились уличные гонщики, которые не дают нам покоя. Нужно найти их." )
        .add( "@BL@7", "~р~СJ:~s~ Это займет очень много времени. Очень много!" )
        .add( "@BL@8", "~р~Джон:~s~ Не совсем. Нам удалось выследить одного из них. Он будет здесь через несколько минут. Твоя задача - узнать, куда он приедет, и сообщить нам." )
        .add( "@BL@9", "~р~СJ:~s~ Выследить смогли, а дальше - нет? Интересный ты человек!" )
        .add( "@BL@10", "~р~Джон:~s~ Ты так наивен. Думаешь, я сам это делал? Для этого у меня есть гангстеры вроде тебя. Удачи." )
        .add( "@BL@11", "~р~СJ:~s~ Эй..." )
        .add( "@BL@12", "~р~Дариус:~s~ Зачем притащил хвост?" )
        .add( "@BL@13", "~р~Сонни:~s~ Да он сам прицепился!" )
        .add( "@BL@14", "~р~Лестер:~s~ Поэтому ты решил приехать именно сюда?" )
        .add( "@BL@15", "~р~Сонни:~s~ Я..." )
        .add( "@BL@16", "~р~Вик:~s~ Сонни, тебе поручили простое задание, но и в этом ты сумел облажаться!" )
        .add( "@BL@17", "~р~Дариус:~s~ Что ж, давайте поприветствуем нашего гостя. Ты кто такой, и что тебе здесь нужно?" )
        .add( "@BL@18", "~р~СJ:~s~ Меня зовут Карл Джонсон. И я хочу участвовать в гонках!" )
        .add( "@BL@19", "~р~Дариус:~s~ В Черном списке только лучшие. Откуда нам знать, что тебе можно доверять?" )
        .add( "@BL@20", "~р~СJ:~s~ В полиции меня очень не любят, так что можно." )
        .add( "@BL@21", "~р~Лестер:~s~ Дариус, может, стоит дать парню шанс? Мы же не убийцы. Я проверю его, а там будет видно." )
        .add( "@BL@22", "~р~Дариус:~s~ Ладно. Теперь здесь на одного идиота больше." )
        .add( "@BL@23", "~р~СJ:~s~ Офицера Джона Маркоски, пожалуйста." )
        .add( "@BL@24", "~р~Джон:~s~ Это я. Узнал?" )
        .add( "@BL@25", "~р~СJ:~s~ Да. Он приехал в бар в районе Флинтовского округа." )
        .add( "@BL@26", "~р~Джон:~s~ Хорошо. Свободен." )
        .add( "@BL@27", "~р~Сонни:~s~ Не думай, что я тупой. Я тебя раскусил! Ты продаешь нас копам! И ты за это заплатишь!" )
        .add( "@BL@28", "~р~Сонни:~s~ Ты не гонщик и не будешь им никогда! Повторяю для тупых: ни-ког-да." )
        .add( "@BL@29", "~р~СJ:~s~ Да. Сейчас везде слышно звуки вертолетов и сирен. Собери сопли и садись в машину!" )
        .add( "@BL@30", "~р~СJ:~s~ Сонни, ты не умеешь водить машину!" )
        .add( "@BL@31", "~р~Сонни:~s~ Да ты просто завидуешь! Тебе просто повезло!" )
        .add( "@BL@32", "~р~СJ:~s~ Отвечаешь как достойный человек. Мне тебя будет не хватать. Удачи!" )
        .add( "@BL@33", "~р~Сонни:~s~ Эй..." )
        .add( "@BL@34", "~р~Вик:~s~ Маркус, кажется пришел твой конкурент! Гонка ожидается напряженной!" )
        .add( "@BL@35", "~р~Маркус:~s~ Гонки меня волнуют меньше, чем мой бар." )
        .add( "@BL@36", "~р~Маркус:~s~ Но ты не расслабляйся!" )
        .add( "@BL@37", "~р~СJ:~s~ Оу, да я и не напрягаюсь. Может уже черт возьми сядем за руль?" )
        .add( "@BL@38", "~р~Дариус:~s~ Заткнись, щенок! Пусть он сначала обслужит важных людей, а затем поставит отброса на место." )
        .add( "@BL@39", "~р~Дариус:~s~ Какой ты злой, всех обижаешь. Смотри, могут ведь и впрямь обидеться. И прислать кого-нибудь крутого. Чтоб он тебя обидел!" )
        .add( "@BL@40", "~р~СJ:~s~ Как-нибудь переживу." )
        .add( "@BL@41", "~р~Лестер:~s~ Привет, пешеход! Я тут уже без тебя заскучал... Твоя тачка передавала мне привет. Кажется, я ей больше нравлюсь." )
        .add( "@BL@42", "~р~СJ:~s~ Пусть это решит гонка!" )
        .add( "@BL@43", "~р~Лестер:~s~ Я не понял, откуда столько понта? Тебе страшно?" )
        .add( "@BL@44", "~р~СJ:~s~ Это еще что за крендель?" )
        .add( "@BL@45", "~р~Крауч:~s~ Поздравляю с победой!" )
        .add( "@BL@46", "~р~СJ:~s~ Вы кто?" )
        .add( "@BL@47", "~р~Крауч:~s~ Меня зовут Стивен Крауч. Я представитель людей, финансирующих эти гонки." )
        .add( "@BL@48", "~р~СJ:~s~ То есть, Дариус здесь не главный?" )
        .add( "@BL@49", "~р~Крауч:~s~ Он только ищет гонщиков с улиц. Я с Лестером проверяю их надежность." )
        .add( "@BL@50", "~р~СJ:~s~ Тогда все ясно. Зачем Вы здесь?" )
        .add( "@BL@51", "~р~Крауч:~s~ Спонсоры довольны твоей работой. Так держать! Они очень любят зрелище!" )
        .add( "@BL@52", "~р~СJ:~s~ Я здесь, чтобы быть первым! Зрелище меня не волнует!" )
        .add( "@BL@53", "~р~Крауч:~s~ Знаю, этим ты его и обеспечиваешь! Мне пора. Продвигайся дальше в Черном списке." )
        .add( "@BL@54", "~р~Марси:~s~ Обожаю людей, которые верно идут своим путем!" )
        .add( "@BL@55", "~р~СJ:~s~ Да, я тоже. Кажется, ты следующая в списке." )
        .add( "@BL@56", "~р~Марси:~s~ Верно. Но не думай, что я так просто отдам тебе победу. Если выиграешь - пойду с тобой на свидание!" )
        .add( "@BL@57", "~р~СJ:~s~ Тогда я обязан победить!" )
        .add( "@BL@58", "~р~Марси:~s~ Впечатляет! Ты стал на один шаг ближе к своей цели!" )
        .add( "@BL@59", "~р~СJ:~s~ Да. И не одной. Ты мне должна свидание! Я знаю неплохой ресторан неподалеку." )
        .add( "@BL@60", "~р~Марси:~s~ Я помню. Идем!" )
        .add( "@BL@61", "~р~Вик:~s~ Привет, дядька! Решил, что знаешь город? Думаешь, ты здесь король?" )
        .add( "@BL@62", "~р~СJ:~s~ Да, знаю и думаю!" )
        .add( "@BL@63", "~р~Вик:~s~ Тогда знай, что на каждого короля есть своя гильотина. Ты придурок!" )
        .add( "@BL@64", "~р~СJ:~s~ Откуда мне знать, что ты не придурок? Жду тебя на финише." )
        .add( "@BL@65", "~р~Вик:~s~ Размечтался!" )
        .add( "@BL@66", "~р~Дариус:~s~ Ого-го, наш мальчик получил пятерку! Браво. Смотри, не перетрудись." )
        .add( "@BL@67", "~р~Дариус:~s~ А то дальше тебя жду я. А я очень крутой! Очень-очень крутой!" )
        .add( "@BL@68", "~р~Дариус:~s~ И мне все равно, что про тебя плетут. Я поставлю тебя на место!" )
        .add( "@BL@69", "~р~Крауч:~s~ Джентльмены, это будет битва века! Наши спонсоры очень довольны." )
        .add( "@BL@70", "~р~Крауч:~s~ В этом кейсе полмиллиона. Но только один удостоится его сегодня получить." )
        .add( "@BL@71", "~р~Дариус:~s~ Сначала заберу деньги, а затем твою девку. Утрешься, болван!" )
        .add( "@BL@72", "~р~Марси:~s~ Он просто трепло! Ты выиграешь! Я знаю." )
        .add( "@BL@73", "~р~Дариус:~s~ Час настал. ПОГНАЛИ!!!" )
        .add( "@BL@74", "~р~Дариус:~s~ Я выбью тебя с первого места! Я выбью тебя из списка! И я требую реванша!" )
        .add( "@BL@75", "~р~Крауч:~s~ Это можно сделать, но только если ты выполнишь условия мистера Джонсона." )
        .add( "@BL@76", "~р~СJ:~s~ Зачем? Хочет реванша - будет ему реванш!" )
        .add( "@BL@77", "~р~Крауч:~s~ Но когда?" )
        .add( "@BL@78", "~р~СJ:~s~ Здесь и сейчас!" )
        .add( "@BL@79", "~р~Коп:~s~ Вижу подозреваемых! Попались! Сразу все! Несомненно, нам за это медаль дадут!" )
        .add( "@BL@80", "~р~Марси:~s~ Как же я рада вас видеть, друзья! Операция 'Черный список' успешно завершена." )
        .add( "@BL@81", "~р~Дариус:~s~ Ты была одной из нас! Ты продажная шлюха!" )
        .add( "@BL@82", "~р~Марси:~s~ Одной из вас? Нам нужны были только ваши спонсоры. Поймать вас не составило труда." )
        .add( "@BL@83", "~р~Джон:~s~ Попрощайтесь со своими машинами. В тюрьме вам будет и без них тесно!" )
        .add( "@BL@84", "~р~Джон:~s~ Эй, гонщик! Ты нас не хочешь отблагодарить?" )
        .add( "@BL@85", "~р~СJ:~s~ То есть?" )
        .add( "@BL@86", "~р~Джон:~s~ Скажем, триста кусков, и твое досье куда-то исчезает." )
        .add( "@BL@87", "~р~СJ:~s~ Только если навсегда." )
        .add( "@BL@88", "~р~Марси:~s~ Тогда продолжишь свои приключения лет через десять!" )
        .add( "@BL@89", "~р~СJ:~s~ Выбор, как всегда, не из легких. Вышлю вам чек." )
        .add( "@BL@90", "~р~Джон:~s~ Стоять! Наличкой. Сейчас. Я видел кейс, так что не ускользай." )
        .add( "@BL@91", "~р~СJ:~s~ Проклятье!" );
        #endregion

    }
}