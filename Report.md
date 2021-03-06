# Dommie Report 馃捇

------



## Preliminares 馃摑:

Este proyecto est谩 orientado a cumplir con la evaluaci贸n anual de la asignatura Programaci贸n en el segundo a帽o de Ciencias de la Computaci贸n de la Universidad de la Habana. Su objetivo es desarrollar una aplicaci贸n que simule el juego de domino, adem谩s de contener ciertos aspectos que van a influir en la mec谩nica de un juego cl谩sico de domino. El principal prop贸sito es demostrar los conocimientos adquiridos durante el curso que respectan a un buen desarrollo de software, que sea mantenible y extensible, que cumplan en su mayor medida los principios del SOLID y otros principios de la programaci贸n en general; todo ello en parte, mediante el uso de interfaces y delegados.

Para ello hemos desarrollado una librer铆a de clases `DominoLibrary` y una aplicaci贸n de consola `ConsoleApp`.

La librer铆a de clases es la encargada de controlar toda la l贸gica del juego, mientras que la aplicaci贸n de consola mostrar谩 al usuario los resultados del juego que haya decidido simular, y le permitir谩 ser part铆cipe durante la ejecuci贸n.

En la carpeta raiz est谩 ubicado el archivo `README.md`, con la informaci贸n necesaria para la ejecuci贸n del proyecto.



## Aspectos variables鉁?:

### Max  Token:

Este aspecto se refiere al n煤mero del doble m谩ximo que estar谩 en el juego. Este par谩metro influir谩 en la posterior generaci贸n de las fichas con que se va a jugar. Por ejemplo, la alternativa cl谩sica del doble 9, va a generar 55 fichas donde la mayor de todas ser谩 el doble-9, 茅sta ser铆a la ficha m谩xima. Su implementaci贸n viene dada como un par谩metro de tipo `int`, que es pasado a las clases y m茅todos tras su selecci贸n.

#### 		Variantes:

- double - 6
- double - 7
- double - 8
- double - 9
- double - 10
- double - 11
- double - 12

De momento han sido las variantes implementadas, pero si fuera necesario se pudiera extender a m谩s opciones en el futuro.



### Number  of  Players:

De igual manera que el aspecto anterior, damos la libertad de elegir cuantos jugadores el usuario quiere sentar en la mesa, no todos tenemos solo cuatro amigos (yo tengo unos poquitos m谩s). Hemos establecido un rango de entre 2 a 6 jugadores, tampoco queremos que la partida dure lo que un monopoly.



### Hand  Out:

No es m谩s que las distintas formas en las que se pueden repartir las fichas del juego. Es el criterio que define la manera en la que las fichas se asignan a los jugadores. No queremos que siempre se jueguen todas las fichas sobre la mesa, por tanto no las vamos a repartir todas; para ellos hemos decidido que se repartan siempre el **71%** de las fichas del juego, dejando una parte fuera de partida, as铆 complicamos un poco las posibles estrategias de jugadores m谩s aventajados.

#### 		Variantes:

- Random: Esta variante es la cl谩sica forma de repartir las fichas del juego. No es mas que 篓dar agua篓, frase t铆pica referida a la acci贸n realizada por los jugadores para mezclar las fichas que luego elegir谩n para jugar la partida.

  

- Bigger First Unfair:Aqu铆 rompemos la tradicional regla de repartir la misma cantidad de fichas por jugador. Dejamos al azar decidir la cantidad de fichas que tendr谩n los mismos, eso s铆, garantizamos siempre que al menos una ficha tendr谩n. Para hacerlo un poco m谩s interesante no solo escogemos los jugadores de forma aleatoria, sino que las fichas a repartir se eligir谩n por la mayor puntuaci贸n de la misma.



### Over  Board  Condition:

Es el criterio que se consulta para determinar si una ronda del juego ya lleg贸 a su fin. 

#### 		Variantes:

- Classic: Con esta variante te sentir谩s familiarizado. La partida llegar谩 a su fin una vez que uno de los jugadores haya puesto en mesa todas sus fichas de la mano, o simplemente haya sido jugada una ficha capaz de pasar a todos los jugadores en mesa.

  

- Crazy Token: Una ficha entre las fichas de los jugadores ser谩 escogida al azar. Los participantes del juego no tendr谩n conocimiento de la misma. En caso de haber sido puesta en juego, la partida habr谩 llegado a su fin. Sin embargo, es posible que todos los jugadores de la mesa se pasen sin haber sido jugada la ficha, por lo que de igual modo la partida habr谩 llegado a su fin.



### Winners  Getter  Judgment:

Se refiere a al criterio de escoger el ganador de una ronda una vez esta ha terminado. 

#### 		Variantes:

- Classic: El ganador ser谩 aquel jugador que haya puesto todas sus fichas en mesa. En caso de no haber sido posible lo anterior y todos los jugadores tengan al menos una ficha en su poder, ser谩n contados los puntos de las fichas y obtendr谩 la victoria aquel jugador que obtenga la menor puntuaci贸n. De haber dos jugadores con la m铆nima puntuaci贸n posible el juego ser谩 declarado empate, es decir, ninguno de los participantes habr谩 alcanzado la victoria.

  

- Random: Una vez m谩s dejamos al azar decidir tu suerte. Independientemente de tu puntuaci贸n o de haber sido capaz de poner todas las fichas en mesa, el jugador ser谩 elegido de forma aleatoria. Ahora bien, miremos el lado positivo, si durante la partida no te est谩 yendo demasiado bien, no todo est谩 perdido, a煤n puedes ser t煤 quien obtenga la victoria.

  

- Smallest 5 Multiple: Si decides jugar escogiendo esta variante debes combinar muy bien tus jugadas, pues solo podr谩s obtener la victoria si eres capaz de lograr alcanzar en tu puntuaci贸n el menor m煤ltiplo de cinco distinto de cero al final de cada partida. En caso de no haber alg煤n jugador que cumpla la condici贸n, el juego ser谩 declarado empate.



### Points  Getter  Judgment:

Se refiere al criterio mediante el que se computa una puntuaci贸n que ser谩 asignada al jugador ganador de cada ronda una vez este se conoce y ya ha terminado la ronda.

#### 		Variantes:

- Classic: Si eres capaz de alcanzar la victoria lograr谩s obtener una puntuaci贸n igual a la suma de las puntuaciones de cada uno de los restantes jugadores. En caso de haber empate los jugadores no obtendr谩n ningun tipo de puntuaci贸n.

  

- 5 Multiples: Jugar con esta modalidad te permitir谩 alcanzar una puntuaci贸n con mayor rapidez o no, solo depende de tus habilidades para dejar al contrario con la mayor cantidad de fichas en mano. Tu puntuaci贸n en este caso ser谩 cinco puntos por cada una de las fichas que hayan quedado en manos de los adversarios.



### Game  Mode:

Hemos facilitado que el usuario no solo simule una simple partida de un juego de domino, sino que adem谩s pueda jugar torneos, o sea, un conjunto de varias partidas que al ganarlas un jugador, va acumulando puntos y la victoria se alcanzar谩 una vez se haya obtenido una cantidad de puntos que puede ser especificada por el usuario antes de comenzar.



### Teams  Play:

Otro aspecto que hemos decidido extender es el juego en equipo ya que no hay un buen torneo de domino que no se juegue en parejas; por ellos hemos decidido llevar esta modalidad a Dommie. Nuestra aplicaci贸n no se limita a que solo sea mediante parejas, sino que tambi茅n deja libertades al usuario respecto a la manera de formar los equipos, todo ello completamente personalizable desde los men煤s de configuraci贸n inicial, que bien explicamos en su secci贸n. Para una mejor adaptaci贸n a la l贸gica, consideramos como un equipo de un solo jugador, a los jugadores cuando las partidas son sin equipos.



### Human  Player:

S脥!!!, es los que est谩s pensando, ahora t煤 eres part铆cipe de nuestro juego. Te damos la opci贸n de ser parte de Dommie e involucrarte a nuestros jugadores virtuales. Traza tus propias estrategias, demuestra tu destreza en el domino e intenta llevarte las mayor cantidad de partidas a tu palmar茅s.



## Los jugadores 馃懃:

### HumanPlayer:  

Implementaci贸n de IPlayer que permite al usuario ser part铆cipe del juego.

### SingleStrategyPlayer:   

Esta clase define a los jugadores que utilizan una 煤nica estrategia preestablecida para jugar.



#### 		Strategies:

- ***Botagorda***: El famoso "bota-gorda" tiene como estrategia jugar la ficha de mayor puntuaci贸n entre las posibles fichas v谩lidas (d铆gase ficha v谩lida aquella que cumple que uno de sus extremos coincide con alguno de los extremos de la mesa) en su mano.
- ***Mosaic***: Con esta estrategia el jugador trata de mantener un rango de fichas en su mano para poder acertar tantos n煤meros como sea posible. Si todas tus fichas tienen palos similares, te quedar谩s atrapado si eso es todo lo que est谩 disponible en el tablero. 
- ***Random***: Jugar de forma aleatoria no es m谩s que seleccionar una ficha entre las posibles fichas v谩lidas a jugar. Ten en cuenta que usar esta estrategia es impredecible. Con ella puedes o no alcanzar la victoria.





## C贸mo se elije una plantilla 馃幃:

Una vez el juego le da la bienvenida al usuario, se muestran una serie de men煤s que permiten la elecci贸n de los aspectos que modifican el juego. Una vez determinado si el propio usuario va a participar en la experiencia de poner fichas sobre la mesa, y el tipo de competici贸n, el motor del juego carga una serie de **plantillas** que el usuario podr谩 elegir para jugar directamente. Quedan explicadas a continuaci贸n:

- ***Classic double-9 (Teams)***: Es el cl谩sico juego en parejas que se juega en las esquinas. Las partidas se juegan entre 4 jugadores, donde los que se sientan a lados opuestos de la mesa conforman los equipos. Se juega con una data m谩xima de doble-9 donde se reparten 10 fichas a cada jugador de manera pseudoaleatoria. Una ronda se acaba cuando un jugador pone todas sus fichas sobre la mesa o ninguno tiene una ficha capaz de encajar con los extremos de la mesa. 驴qui茅n gana?, pues el jugador que se qued贸 sin fichas, y en caso de tranque, el que tenga la mano con menos puntos, donde los puntos los conforma la suma de ambos lados de todas las fichas. Una vez hay un ganador, los puntos que este recauda es la suma de las manos de todos los jugadores que se consideren contrarios. En caso de torneo, se ganar谩 la competici贸n cuando se re煤nan 100 puntos.
- ***Classic double-9 (Single Player)***: Configuraci贸n muy similar a la anterior, solo que esta vez no habr谩 equipos, es una modalidad Deathmatch pero sobre la mesa.
- ***Classic double-6***: Siguiendo el hilo de una partida cl谩sica, esta vez se juega con una data m谩xima de doble-6. Con el compa帽ero del frente en el mismo equipo, y respetando la convenci贸n antes explicada de la cantidad de fichas a repartir, los jugadores buscan ganar puntos como lo juegan los cl谩sicos chinos.
- ***Crazy Token***: Modalidad propia de Dommie, traemos esta modalidad donde las cosas se salen un poco de lo habitual. Para empezar esta vez se sentar谩n en la mesa 6 jugadores y con 10 fichas cada uno, aunque esta vez hasta el doble-12; con fichas obtenidas de manera pseudoaleatoria, intentar谩n ganar una partida qued谩ndose sin fichas en mano o teniendo la menor cantidad de puntos. 驴equipos?, por supuesto, pero esta vez ser谩n dos tr铆os donde nunca juegan dos compa帽eros de manera consecutiva. Pero ahora, una peculiaridad es que durante todo el juego, existe una ficha muy particular que ser谩 repartida a uno de los jugadores de manera asegurada. Nadie conoce esta ficha ni quien la porta, pero una vez que la ficha se ponga sobre la mesa, la partida terminar谩 inmediatamente. Puede pasar que un jugador nunca llegue a poner la ficha y ninguno, ni siquiera 茅l, lleve una ficha que pueda jugar por uno de los extremos, si esto ocurre la partida acabar谩 igualmente. Los puntos obtenidos para el torneo se computan de igual manera que el juego cl谩sico.
- ***Customize your own template:*** Permite al usuario acceder a un nuevo men煤 de personalizaci贸n del juego, a trav茅s del cual podr谩 combinar a placer, todas las opciones de personalizaci贸n anteriores y hacer del juego de domin贸 una experiencia m谩s que preferida. 

 

## Detalles de la implementaci贸n de los men煤s 馃搵:

Para los men煤s del juego ha sido necesario la creaci贸n de clases que permitan un funcionamiento los m谩s amistoso posible con el usuario. Durante la navegaci贸n por la interfaz gr谩fica el usuario interact煤a con varios tipos de men煤s. La navegaci贸n por los men煤s se hace mediante las flechas de direcci贸n del teclado, presionando la tecla **Enter** para confirmar selecciones.

El m谩s com煤n es el `SingleSelectionMenu<T>`, como su nombre indica es un men煤 de selecci贸n simple, que tiene un m茅todo llamado `Show()` que no termina hasta que el usuario confirma una selecci贸n. Sus opciones de tipo gen茅rico son almacenadas en una lista y durante la navegaci贸n muta constantemente un valor que contiene a la selecci贸n actual. Como una extensi贸n de este men煤, se decidi贸 implementar sobre 茅l mismo una opci贸n que mediante un valor booleano, habilita una opci贸n extra llamada `Continue`, que en caso de usarla, es quien da la salida del m茅todo. 

Quiz谩s el m谩s complejo sea el men煤 de personalizaci贸n de plantilla: `CustomizeGame`. Este men煤 carga unos valores de los par谩metros personalizables por defecto para cubrir el caso en que el jugador contin煤e a la partida sin establecer todos los valores. Sabiendo ya si el usuario va a poner fichas en juego y el modo de juego (partida o torneo), la configuraci贸n iniciar铆a de la siguiente manera:

**Players**: 4 (Generados de manera aleatoria)

**Teams**: No

**Max Token**: double - 6

**Hand Out Judgment**: Random (cl谩sico)

**Over Board Condition**: Classic

**Get Winner Judgment**: Classic

**Points Getter**: Classic

**Tournament Win Score**: 100 points

En base a los preajustes anteriores el usuario puede modificar los aspectos del juego. Una vez establecido todo, se selecciona la opci贸n `Continue` y tras confirmar, comienza el juego.

En el menu de personalizaci贸n de jugadores no solo se eligen la cantidad de jugadores que rodear谩n la mesa, sino tambi茅n las diferentes estrategias que estos van a adoptar durante todo el juego.



## Detalles de la implementaci贸n l贸gica 馃:

Una vez configurada por completo una plantilla, el siguiente paso ser铆a ejecutar el IGame, donde IGame ser铆a tanto un torneo como una simple partida.

### El  Torneo: 

Un torneo no es m谩s que una secuencia de partidas de la que los diferentes equipos buscan sacar las mejores puntuaciones y as铆 ganar la competici贸n. Su implementaci贸n est谩 en el archivo `Tournament.cs`.

### El  Board: 

Un Board no es m谩s que una ronda del juego de domino. Una ronda es una iteraci贸n continua sobre los jugadores que solo tiene fin cuando esta se da por finalizada. Por defecto una jugada se preestablece como un pase, luego esta condici贸n cambia una vez se le permita a un jugador realizar una jugada. Solo se deja a un miembro de la mesa jugar si  tiene al menos una ficha v谩lida de poner por alguno de los extremos. En caso contrario la jugada no cambiar谩 su estado y se quedar谩 como un pase. Si un jugador intenta poner 3 veces una ficha inv谩lida en la mesa, su turno se ceder谩 al siguiente jugador y su jugada durante la ronda ser谩 un pase. Tras completarse una jugada, la mesa se actualizar谩 y se guardar谩 la 煤ltima jugada. Luego entra en juego el delegado que comprueba si la partida ha llegado a su fin; en dicho caso se escoger谩 el ganador de la ronda junto a sus puntos y se devolver谩 a la instancia anterior del torneo (fin en caso de jugarse solo una ronda).



## Conexi贸n con la interfaz gr谩fica 馃摵:

La clase `GamePrinter.cs`, es la encargada de mostrar en consola todos los resultados durante el transcurso del juego. Para adjuntar el GamePrinter al juego es necesario: al crear cada instancia de un IGame, ejecutar el m茅todo

```cs
void SetGamePrinter(GamePrinter gamePrinter)
```

perteneciente a la interfaz IGame, de esta manera el GamePrinter se guardar谩 como una propiedad de la clase que implementa IGame, y adem谩s se a帽adir谩 la instancia del Board o Tournament a dicha clase, as铆 esta tendr谩 acceso a sus componentes para poder mostrar resultados en pantalla.

Esta clase tiene una serie de m茅todos que son llamados desde los diferentes momentos del juego y definen la forma en la que se muestra dicha informaci贸n al usuario.

Por otra parte tenemos la clase `PlaySelectorMenu.cs` que es utilizada por el GamePrinter como el men煤 que permite al usuario ser el jugador humano.



## Clases que est谩n presentes durante el juego 馃З:

### `CircularList.cs`

Esta clase es una implementaci贸n gen茅rica de una estructura de dato que funciona de manera similar a una lista enlazada mediante nodos. Su principal raz贸n de ser es que implementa la interfaz IEnumerable de manera que pas谩ndole un IEnumerator por constructor nos permite variar la forma de recorrerla. La idea de su creaci贸n fue para realizar el ciclo `foreach` sobre los jugadores de manera circular, es decir, que tras el 煤ltimo jugador aparezca el primero en el ciclo y solo se termine el mismo al llamar a `break`.



### `Token.cs`

Es la clase que englova el concepto de ficha. Adem谩s `Token_onBoard` define a una ficha una vez puesta en la mesa, una ficha que ahora tiene due帽o, una orientaci贸n, y el lugar de la mesa por que fue jugada.



### `Team.cs`

Define lo que es un equipo.



### `Judge.cs`

Esta clase es la que contiene los delegados que el juego ejecuta para hacer cumplir las reglas. Adem谩s tiene un m茅todo `IsValid()` que determina si una jugada es v谩lida.



### `Setting.cs`

Esta clase es un simple contenedor de informaci贸n que guarda los datos que necesitan el `Board.cs` y el `Tournament.cs`



### `Lapse.cs`

Se encarga de relentizar la ejecuci贸n, el tiempo que se determina en su constructor.



### `GameResult.cs`

Contenedor de la informaci贸n de un ganador de ronda o torneo.







