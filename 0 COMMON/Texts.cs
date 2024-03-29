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
        .add( "@PHS@20", "~p~Эрик:~s~ Мистер Джонсон, это Эрик Томсон. Для Вас есть работа!" )
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




        #region ZERO
        .add( "@ZRO@00", "Миссия Зиро 0" )
        .add( "@ZRO@01", "Миссия Зиро 1" )
        .add( "@ZRO@02", "Миссия Зиро 2" )
        .add( "@ZRO@03", "Миссия Зиро 3" )
        .add( "@ZRO@04", "Миссия Зиро 4" )
        .add( "@ZRO@05", "~s~Сначала Вы должны купить стрип-клуб!" )

        // REPLICAS
        //.add( "@ZR@000", "" )
        #endregion

        #region INCORPORATION (СКОРЕКТИРОВАТЬ ТЕКСТ)
        .add( "LG_06", "Киностудия" )
        .add( "@INC@00", "Сцена" )
        .add( "@INC@01", "Сцена" )
        .add( "@INC@02", "Сцена" )
        .add( "@INC@03", "Эпизод 1. Сцена 1" )
        .add( "@INC@04", "Эпизод 1. Сцена 2" )
        .add( "@INC@05", "Эпизод 1. Сцена 3" )
        .add( "@INC@06", "Эпизод 1. Сцена 4" )
        .add( "@INC@07", "Эпизод 2. Сцена 1" )
        .add( "@INC@08", "Эпизод 2. Сцена 2" )
        .add( "@INC@09", "Эпизод 2. Сцена 3" )
        .add( "@INC@10", "Эпизод 2. Сцена 4" )
        .add( "@INC@11", "Эпизод 3. Сцена 1" )
        .add( "@INC@12", "Эпизод 3. Сцена 2" )
        .add( "@INC@13", "Эпизод 3. Сцена 3" )
        .add( "@INC@14", "Эпизод 3. Сцена 4" )
        .add( "@INC@15", "~r~Вы покинули периметр съёмки!" )
        .add( "@INC@16", "~r~Выбранное оружие не используется в сценарии!" )
        .add( "@INC@17", "~r~Ваши действия не соответствуют сценарию!" )
        .add( "@INC@18", "Инфицированные приближаются к ~g~вакцине~s~!" )
        .add( "@INC@19", "~r~Вакцина подверглась заражению!" )
        .add( "@INC@20", "Вы получили ключи к ~g~прототипу VITAL~s~. Он всегда будет доступен возле офиса компании." )
        .add( "@INC@21", "Убийств: ~1~" )
        .add( "@INC@22", "Заражение: ~1~" )
        .add( "@INC@23", "~r~Вы заразились инфекцией!" )

        // REPLICAS
        .add( "@IC@000", "~s~У ~g~грузовикика~s~ с вакциной закончилось топливо. Вам нужно не допустить к нему ~r~инфицированных~s~." )
        .add( "@IC@001", "~s~Удерживайте их на расстоянии вместе с Вашими ~b~союзниками~s~, пока не прибудет доставка." )
        .add( "@IC@002", "~s~Избегайте близкого контакта с инфицированными!" )
        #endregion

        #region C.R.A.S.H.
        .add( "@CRS@00", "Друзья" )
        .add( "@CRS@01", "Remax" )
        .add( "@CRS@02", "Простое дело" )
        .add( "@CRS@03", "Идеальное убийство" )
        .add( "@CRS@04", "Конвой" )
        .add( "@CRS@05", "Чужими руками" )
        .add( "@CRS@06", "Опасная работа" )
        .add( "@CRS@07", "Бегство Джона" )
        .add( "@CRS@08", "~r~Вы покинули зону сражения!" )
        .add( "@CRS@09", "~s~Убейте отмеченных ~r~копов~s~!" )
        .add( "@CRS@10", "~r~Вас заметила камера наблюдения!" )
        .add( "@CRS@11", "~r~Вы не успели устранить цели вовремя!" )
        .add( "@CRS@12", "~s~Садитесь в ~b~машину~s~!" )
        .add( "@CRS@13", "~s~Отвезите Алекса ~y~домой~s~!" )
        .add( "@CRS@14", "~r~Транспорт уничтожен!" )
        .add( "@CRS@15", "~r~Алекс умер!" )
        .add( "@CRS@16", "~s~Отвезите машину в гараж ~y~8-Ball~s~." )
        .add( "@CRS@17", "~r~Вы починили машину!" )
        .add( "@CRS@18", "~s~Припаркуйте машину возле дома ~y~прокурора~s~." )
        .add( "@CRS@19", "~r~Вы привлекли внимание полиции!" )
        .add( "@CRS@20", "~r~Транспорт слишком повреждён!" )
        .add( "@CRS@21", "~r~Вы покинули город!" )
        .add( "@CRS@22", "~s~Отправляйтесь в указанную ~у~точку~s~." )
        .add( "@CRS@23", "~s~Устраните ~r~цель~s~!" )
        .add( "@CRS@24", "~s~Ваша цель здесь. Быстро избавьтесь от неё!" )
        .add( "@CRS@25", "~r~Вы не успели устранить цель вовремя!" )
        .add( "@CRS@26", "~r~Вы убили не того!" )
        .add( "@CRS@27", "~r~Вы покинули позицию!" )
        .add( "@CRS@28", "~r~Вы убили цель не тем оружием!" )
        .add( "@CRS@29", "~s~Приходите с ~1~:00 до 00:00." )
        .add( "@CRS@30", "~r~Вы не успели занять позицию!" )
        .add( "@CRS@31", "~s~Цель приближается. Приготовьтесь!" )
        .add( "@CRS@32", "~r~Вы упустили цель!" )
        .add( "@CRS@33", "~s~Поговорите с ~b~Мелом~s~." )
        .add( "@CRS@34", "~r~Мел умер!" )
        .add( "@CRS@35", "~r~У Мела возникли подозрения относительно Вас!" )
        .add( "@CRS@36", "Доверие: ~1~" )
        .add( "@CRS@37", "Ответы" )
        .add( "@CRS@38", "~s~Садитесь в ~b~вертолёт~s~!" )
        .add( "@CRS@39", "~s~Подлетите к ~y~автобусу~s~ и удерживайте позицию." )
        .add( "@CRS@40", "~r~Автобус уничтожен!" )
        .add( "@CRS@41", "~s~Нажмите ~y~~k~~VEHICLE_HORN~~s~, чтобы Мел спрыгнул на крышу автобуса." )
        .add( "@CRS@42", "~r~Мел промахнулся!" )
        .add( "@CRS@43", "~s~Посадите вертолёт у ~y~полицейского участка~s~." )
        .add( "@CRS@44", "Мел: ~1~" )
        .add( "@CRS@45", "Террористы: ~1~" )
        .add( "@CRS@46", "~s~Убей ~r~террористов~s~ и доставь ~g~бомбы~s~ к ~y~поезду~s~." )
        .add( "@CRS@47", "Бомбы: ~1~" )
        .add( "@CRS@48", "Садитесь в ~b~поезд~s~!" )
        .add( "@CRS@49", "~r~У Вас не хватит времени, чтобы увезти бомбы!" )
        .add( "@CRS@50", "Выбор:" )
        .add( "@CRS@51", "Помочь Джону" )
        .add( "@CRS@52", "Помочь Мелу" )
        .add( "@CRS@53", "~s~Нажмите ~y~~k~~PED_SPRINT~~s~ для подтверждения." )
        .add( "@CRS@54", "Противник: ~1~" )
        .add( "@CRS@55", "~r~Вы не смогли оторваться от Мела!" )
        .add( "@CRS@56", "~r~Джон сумел уйти от погони!" )
        .add( "@CRS@57", "~s~Уничтожьте ~r~машину~s~ Мела!" )
        .add( "@CRS@58", "~s~Уничтожьте ~r~машину~s~ Джона!" )
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
        .add( "@CR@022", "~р~CJ:~s~ Цель нейтрализована!" )
        .add( "@CR@023", "~р~Джон:~s~ Одним придурком меньше. Хорошо. Встречаемся в другом месте." )
        .add( "@CR@024", "~р~CJ:~s~ На том свете?" )
        .add( "@CR@025", "~р~Джон:~s~ Координаты я тебе скину. Можешь называть это место как угодно." )
        .add( "@CR@026", "~р~CJ:~s~ Ясно. Чао!" )
        .add( "@CR@027", "~р~Мел:~s~ Ты, должно быть, Джон. Рад тебя видеть! Почему не в униформе?" )
        .add( "@CR@028", "Дома забыл." ) // ~р~CJ:
        .add( "@CR@029", "Я под прикрытием." ) // ~р~CJ:
        .add( "@CR@030", "А какая разница?" ) // ~р~CJ:
        .add( "@CR@031", "У нас нет времени на расспросы!" ) // ~р~CJ:
        .add( "@CR@032", "~р~CJ:~s~ ~a~" )
        .add( "@CR@033", "~р~Мел:~s~ Ясно. Зачем ты здесь?" )
        .add( "@CR@034", "Я должен отвезти тебя на вертолёте к автобусу." ) // ~р~CJ:
        .add( "@CR@035", "Ты сказал, чтобы я приехал сюда." ) // ~р~CJ:
        .add( "@CR@036", "Я думал, что ты в курсе." ) // ~р~CJ:
        .add( "@CR@037", "Мне просто сообщили, что я нужен здесь." ) // ~р~CJ:
        .add( "@CR@038", "~р~Мел:~s~ Прям вот так... Чем занимаешься на работе?" )
        .add( "@CR@039", "Расследую убийства, собираю улики и прочее." ) // ~р~CJ:
        .add( "@CR@040", "Я работаю в патруле. Раньше был в \"C.R.A.S.H.\"." ) // ~р~CJ:
        .add( "@CR@041", "Бумажная работа в офисе и остальная мелкая фигня." ) // ~р~CJ:
        .add( "@CR@042", "Заставляю мелких бандитов работать за меня." ) // ~р~CJ:
        .add( "@CR@043", "~р~Мел:~s~ Неплохо устроился... Как там доктор Шварц?" )
        .add( "@CR@044", "Это ещё кто такой?" ) // ~р~CJ:
        .add( "@CR@045", "Болеет. Я слышал, что скорая увезла его в больницу." ) // ~р~CJ:
        .add( "@CR@046", "Жаль старика. Сейчас он на небесах." ) // ~р~CJ:
        .add( "@CR@047", "Понятия не имею." ) // ~р~CJ:
        .add( "@CR@048", "~р~Мел:~s~ Хм... Сколько зарабатываешь?" )
        .add( "@CR@049", "2000$. Не так много, как хотелось." ) // ~р~CJ:
        .add( "@CR@050", "Это конфиденциальная информация." ) // ~р~CJ:
        .add( "@CR@051", "~р~Джон:~s~ Стоит Карл Джонсон - герой из \"Чёрного списка\"!" )
        .add( "@CR@052", "~р~CJ:~s~ Может, дашь мне поесть? Ни минуты без твоей болтовни." )
        .add( "@CR@053", "~р~Джон:~s~ Ты всё съеденное сейчас вырвешь узнав, какое задание я тебе приготовил!" )
        .add( "@CR@054", "~р~CJ:~s~ Когда-нибудь ты ответишь за свои дела, как это сделал Тенпенни!" )
        .add( "@CR@055", "~р~Джон:~s~ Верно. Но ты меня тоже запомнишь надолго. Слушай задание!" )
        .add( "@CR@056", "~р~Джон:~s~ Мне пришёл приказ из ФБР. Надо на вертолёте подлететь к автобусу." )
        .add( "@CR@057", "~р~Джон:~s~ Он заминирован, так что это будешь делать ты. С террористами я не буду рисковать жизнью." )
        .add( "@CR@058", "~р~Джон:~s~ Я слегка подделал своё дело, которое попало в руки твоего будущего партнёра." )
        .add( "@CR@059", "~р~Джон:~s~ Он очень недоверчив, так что тебе придётся его убедить, что ты - это я." )
        .add( "@CR@060", "~р~CJ:~s~ Сосать у него предлагаешь? Ничего не получится!" )
        .add( "@CR@061", "~р~Джон:~s~ Послушай, парень, не оскорбляй меня. Скоро ты будешь свободен как птица." )
        .add( "@CR@062", "~р~Джон:~s~ Так что выполни для меня ещё пару заданий, и я отстану." )
        .add( "@CR@063", "~р~CJ:~s~ Ты уже тонешь! Надеюсь, что я поставлю точку в твоей кончине!" )
        .add( "@CR@064", "~р~Мел:~s~ Извини за лишние вопросы. Я должен знать, с кем работаю." )
        .add( "@CR@065", "~р~CJ:~s~ Это нормально. Что стряслось?" )
        .add( "@CR@066", "~р~Мел:~s~ В городе орудует банда террористов. Их развелось так много, что людей не хватает!" )
        .add( "@CR@067", "~р~Мел:~s~ Приходится использовать все доступные ресурсы. Повезло, что есть ещё служебный вертолёт." )
        .add( "@CR@068", "~р~Мел:~s~ Он пригодится, так как по-другому подобраться не получится." )
        .add( "@CR@069", "~р~Мел:~s~ Дверь автобуса заварена сваркой, а внутри обнаружена бомба. Та самая бомба!" )
        .add( "@CR@070", "~р~Мел:~s~ Она взрывается, когда транспорт едет слишком медленно. Водитель уже устал петлять по городу." )
        .add( "@CR@071", "~р~Мел:~s~ Из всех сотрудников ФБР и полиции остались только ты и я. Я обезврежу бомбу, а ты веди вертолёт." )
        .add( "@CR@072", "~р~CJ:~s~ Тогда за дело!" )
        .add( "@CR@073", "~р~Мел:~s~ Когда я спрыгну на крышу автобуса, посади вертолёт здесь. Остальное я сделаю сам." )
        .add( "@CR@074", "~р~Мел:~s~ Наши парни повязали несколько группировок. Нам осталась последняя." )
        .add( "@CR@075", "~р~Мел:~s~ Они что-то пытаются сделать в районе восточного Рокшора." )
        .add( "@CR@076", "~р~Мел:~s~ Отряд просит помощи. Похоже, что будет веселье." )
        .add( "@CR@077", "~р~CJ:~s~ Откуда информация, что это последняя?" )
        .add( "@CR@078", "~р~Мел:~s~ Мы проверили каждую задницу в этом городе. Всё указывает на это." )
        .add( "@CR@079", "~р~CJ:~s~ Ясно. Тогда дадим им бой!" )
        .add( "@CR@080", "~р~Мел:~s~ Быстрее садись в машину. Объясню всё по дороге!" )
        .add( "@CR@081", "~р~Мел:~s~ Сейчас рванёт! Выскакивай из поезда!" )
        .add( "@CR@082", "~р~Террорист:~s~ Кучка федеральных гандонов, нас не остановить!" )
        .add( "@CR@083", "~р~Террорист:~s~ Бомбы установлены. Скоро от этого коррумпированного предприятия ничего не останется!" )
        .add( "@CR@084", "~р~CJ:~s~ Ты слышал, у них бомбы!" )
        .add( "@CR@085", "~р~Мел:~s~ Я вижу бомбы. Думаю, что смогу их обезвредить. Но надо увезти их подальше от города." )
        .add( "@CR@086", "~р~Мел:~s~ К счастью, приближается поезд. Осталось только погрузить их на вагон." )
        .add( "@CR@087", "~р~CJ:~s~ Главное, чтобы с нами не случилось то же самое." )
        .add( "@CR@088", "~р~Джон:~s~ Итак, вот тебе последнее задание. Твой новый друг уже начал что-то копать под меня." )
        .add( "@CR@089", "~р~Джон:~s~ Тебе нужно избавиться от него, и я отдам досье. Дальше меня ждёт Египет." )
        .add( "@CR@090", "~р~Мел:~s~ У меня другой вариант: он убивает тебя, забирает досье, а мы этого не заметим!" )
        .add( "@CR@091", "~р~Джон:~s~ Какие люди! Хочешь поднять ставки? Хорошо. Я отдам деньги за Чёрный список в обмен за ваши головы!" )
        .add( "@CR@092", "~р~Джон:~s~ Пришло время прощаться. Я отдаю тебе досье, мне оно уже не нужно." )
        .add( "@CR@093", "~р~CJ:~s~ А деньги?" )
        .add( "@CR@094", "~р~Джон:~s~ Конечно. Но я уже успел просрать их часть. Даю то, что осталось." )
        .add( "@CR@095", "~р~CJ:~s~ Хорошо. Теперь катись отсюда." )
        .add( "@CR@096", "~р~Марси:~s~ Счастливо, красавчик!" )
        .add( "@CR@097", "~р~Мел:~s~ Спасибо, что избавил мир от этого подонка. Как и обещал, тебя в этой погони не было." )
        .add( "@CR@098", "~р~CJ:~s~ Рано или поздно он должен был заплатить за свои дела." )
        .add( "@CR@099", "~р~Мел:~s~ Это точно. Досье и деньги сгорели при взрыве. Я отдам тебе часть зарплаты в качестве вознаграждения." )
        .add( "@CR@100", "~р~CJ:~s~ Это хорошие новости. Но к террористам я больше не пойду." )
        .add( "@CR@101", "~р~Мел:~s~ А это и не нужно. Удачи тебе, незнакомец." )
        .add( "@CR@102", "~р~CJ:~s~ Где этот драный полицейский?" )
        .add( "@CR@103", "~р~Джон:~s~ Руки вверх! Один выстрел - и на одного гангстера меньше." )
        .add( "@CR@104", "~р~CJ:~s~ Какого чёрта? Опусти ствол." )
        .add( "@CR@105", "~р~Джон:~s~ У меня к тебе дело, связанное с прокурором." )
        .add( "@CR@106", "~р~CJ:~s~ С тем, кто увидел превышение полномочий?" )
        .add( "@CR@107", "~р~Джон:~s~ Именно. За это его нужно проучить. И сделаешь это ты." )
        .add( "@CR@108", "~р~CJ:~s~ Очередное убийство? Как-то банально." )
        .add( "@CR@109", "~р~Джон:~s~ Зато эффективно. Эта старая пьянь отсыпается в отеле после вчерашнего застолья." )
        .add( "@CR@110", "~р~Джон:~s~ Но свою разбитую машину он оставил без присмотра." )
        .add( "@CR@111", "~р~Джон:~s~ Тебе нужно отвезти её к дому прокурора в том же состоянии." )
        .add( "@CR@112", "~р~Джон:~s~ Не забудь нацепить на неё бомбу в одном уютном гараже. Пускай полетает." )
        .add( "@CR@113", "~р~CJ:~s~ Это в стиле C.R.A.S.H. Тебе это что-то говорит?" )
        .add( "@CR@114", "~р~Джон:~s~ Да. Я был в этой организации. Она прекратила существование, но только на бумаге." )
        .add( "@CR@115", "~р~Джон:~s~ Но тебе этого знать не нужно. Иначе будешь гнить в тюряге." )
        .add( "@CR@116", "~р~CJ:~s~ Ясно. Тогда займусь прокурором." )
        .add( "@CR@117", "~р~Джон:~s~ Карл, я тебя уже устал ждать. Есть новости." )
        .add( "@CR@118", "~р~CJ:~s~ Новости? Ты решил уничтожить досье?" )
        .add( "@CR@119", "~р~Джон:~s~ Нет, дебил. Два моих сотрудника начинают говорить лишнее." )
        .add( "@CR@120", "~р~Джон:~s~ Я занял снайперскую винтовку у одного из них. Тебе нужно убить ею другого сотрудника." )
        .add( "@CR@121", "~р~Джон:~s~ Когда дело будет сделано, я верну снайперку на место. Копы быстро найдут это оружие." )
        .add( "@CR@122", "~р~Джон:~s~ Дальше ты знаешь, что будет. Сегодня жертва встречается с другими копами." )
        .add( "@CR@123", "~р~Джон:~s~ Это идеальный момент. Только времени осталось немного. За дело!" )
        .add( "@CR@124", "~р~Джон:~s~ Карл, есть хорошие и плохие новости. С каких начать?" )
        .add( "@CR@125", "~р~CJ:~s~ Мне по хер." )
        .add( "@CR@126", "~р~Джон:~s~ Начну с хороших. Я вступил в новый элитный отряд полицейских." )
        .add( "@CR@127", "~р~CJ:~s~ Вечно ты во что-то вступаешь: то в дерьмо, то в отряд." )
        .add( "@CR@128", "~р~Джон:~s~ Это не важно. От этих ребят я узнал плохие новости." )
        .add( "@CR@129", "~р~Джон:~s~ Есть свидетель, который пытается настучать на моего кореша." )
        .add( "@CR@130", "~р~Джон:~s~ Ты должен его ликвидировать. Но не всё так просто." )
        .add( "@CR@131", "~р~CJ:~s~ Я так понял, не так просто для меня." )
        .add( "@CR@132", "~р~Джон:~s~ Да. Свидетель будет в одной из лодок. Тебе нужно уничтожить ту, в которой будет жертва." )
        .add( "@CR@133", "~р~CJ:~s~ Как мне узнать? Я что, телепат?" )
        .add( "@CR@134", "~р~Джон:~s~ Да мне насрать. Оружие уже приготовлено в удобном месте." )
        .add( "@CR@135", "~р~Джон:~s~ Я отвезу тебя к нему, а дальше действуй." )
        #endregion

        #region BLACK LIST
        .add( "LG_57", "Черный список" )
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
        .add( "@BL@8", "~р~Джон:~s~ Не совсем. Нам удалось выследить одного из них. Он будет здесь через несколько минут. Твоя задача - узнать, куда он приедет, и сообщить мне." )
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
        .add( "@BL@91", "~р~СJ:~s~ Проклятье!" )
        .add( "@BL@92", "~р~Марси:~s~ Кратко расскажу тебе о правилах, это недолго. Их всего три." )
        .add( "@BL@93", "~р~Марси:~s~ Первое: чтобы вызвать гонщика на дуэль, нужно выполнить все условия оппонента." )
        .add( "@BL@94", "~р~Марси:~s~ Мы называем эти условия квалификацией. Чем выше ранг, тем больше требований." )
        .add( "@BL@95", "~р~Марси:~s~ Второе: ты обязан вызывать только того гонщика, который на один ранг выше." )
        .add( "@BL@96", "~р~Марси:~s~ Отказ соперника от гонки приводит к исключению этого гонщика из списка." )
        .add( "@BL@97", "~р~Марси:~s~ Третье: если проиграешь, то ты обязан составить свой список условий для гонщика, который на один ранг ниже тебя, и ждать вызова." )
        .add( "@BL@98", "~р~Марси:~s~ Тебе придётся разорвать жопу, чтобы стать лучшим. Здесь все ненавидят друг друга, так что это твой шанс." )
        #endregion

        #region LEVEL SELECTOR
        .add( "@LSCT@0", "Уровень сложности" )
        .add( "@LSCT@1", "Низкий" )
        .add( "@LSCT@2", "Средний" )
        .add( "@LSCT@3", "Высокий" );
        #endregion

    }
}