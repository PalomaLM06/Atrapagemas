INSERT INTO Jugadores (Id, Nombre, x, y, Gemas, IdCasilla) VALUES
(1, 'Duende1', 0, 0, 0, 1),
(2, 'Duende2', 0, 0, 0, 1),
(3, 'Duende3', 0, 0, 0, 1),
(4, 'Duende4', 0, 0, 0, 1);

INSERT INTO Tablero (Id_tablero, Nombre) VALUES
(1, 'Normalito');


INSERT INTO Casilla (Id_casilla, Tipo, Orden, x, y, Id_reto, Id_tablero) VALUES
(1, 'Start', 1, 0, 0, 1, 1),
(2, '1vs1', 2, 1, 0, 2, 1),
(3, 'Normal', 3, 2, 0, 1, 1),
(4, 'ReRoll', 4, 3, 0, 17, 1),
(5, 'DesafioPersonal', 5, 4, 0, 10, 1),
(6, 'Portal', 6, 5, 0, 21, 1),
(7, 'TodosVsTodos', 7, 6, 0, 14, 1),
(8, 'Swap', 8, 7, 0, 18, 1),
(9, 'MitadvsMitad', 9, 7, 1, 6, 1),
(10, 'Normal', 10, 6, 1, 1, 1),
(11, 'SkipTurn', 11, 5, 1, 20, 1),
(12, '1vs1', 12, 4, 1, 3, 1),
(13, 'Normal', 13, 3, 1, 1, 1),
(14, 'Portal', 14, 2, 1, 21, 1),
(15, 'TodosVsTodos', 15, 1, 1, 15, 1),
(16, 'Normal', 16, 0, 1, 1, 1),
(17, '1vs1', 17, 0, 2, 4, 1),
(18, 'MitadvsMitad', 18, 1, 2, 7, 1),
(19, 'ReRoll', 19, 2, 2, 17, 1),
(20, 'SkipTurn', 20, 3, 2, 20, 1),
(21, 'DesafioPersonal', 21, 4, 2, 11, 1),
(22, 'Normal', 22, 5, 2, 1, 1),
(23, '1vs1', 23, 6, 2, 5, 1),
(24, 'SkipTurn', 24, 7, 2, 20, 1),
(25, 'TodosVsTodos', 25, 7, 3, 16, 1),
(26, 'Normal', 26, 6, 3, 1, 1),
(27, 'MitadvsMitad', 27, 5, 3, 8, 1),
(28, 'Swap', 28, 4, 3, 18, 1),
(29, 'DesafioPersonal', 29, 3, 3, 12, 1),
(30, 'Normal', 30, 2, 3, 1, 1),
(31, '1vs1', 31, 1, 3, 2, 1),
(32, 'Cofre', 32, 0, 3, 1, 1);





INSERT INTO Gadgets (Id_gadget, Descripcion, Coste) VALUES
(1, 'Cambia la posición con el jugador que escojas', 5),
(2, 'Si pierdes un minijuego, intercambias el resultado y pasas a ser el ganador', 5),
(3, 'El jugador que elijas pierde todas sus gemas y las devuelve a la mina', 4),
(4, 'Después de lanzar el dado, puedes volver a lanzar y avanzar nuevamente', 3),
(5, 'Cancelas el turno de un jugador en esta ronda', 3),
(6, 'Lanzas el dado y avanzas las casillas que indique', 2);


INSERT INTO Retos (Id_reto, Tipo_casilla, Descripcion) VALUES
(1, 'Normal', 'Casilla sin efecto, el turno continúa normalmente.'),
(2, '1vs1', 'Dos jugadores eligen un número del 1 al 10. Gana quien esté más cerca del número aleatorio generado por el sistema.'),
(3, '1vs1', 'Dos jugadores compiten por presionar un botón apenas aparezca en pantalla. El más rápido gana y avanza 1 casilla.'),
(4, '1vs1', 'Cada jugador recibe un número aleatorio del 1 al 10. El mayor gana y avanza 1 casilla.'),
(5, '1vs1', 'Se genera un número aleatorio. Los jugadores eligen par o impar. Quien acierte gana.'),
(6, 'MitadvsMitad', 'Se muestra una suma sencilla. El equipo que responda primero correctamente gana.'),
(7, 'MitadvsMitad', 'Cada equipo obtiene números aleatorios. El equipo con mayor suma total gana.'),
(8, 'MitadvsMitad', 'Cada equipo elige cara o cruz. Se lanza una moneda y el equipo correcto gana.'),
(9, 'MitadvsMitad', 'Cada equipo resuelve una operación diferente. El primero en acertar gana y el equipo perdedor retrocede casillas.'),
(10, 'DesafioPersonal', 'Aparece una suma en pantalla y tienes pocos segundos para responder. Si aciertas, avanzas 1 casilla; si fallas, retrocedes 1.'),
(11, 'DesafioPersonal', 'Elige un número del 1 al 5. El sistema genera uno al azar. Si aciertas, avanzas 2 casillas; si no, te quedas igual.'),
(12, 'DesafioPersonal', 'Aparece un número oculto. Debes decidir si es mayor o menor que 5. Si aciertas, avanzas 1 casilla; si fallas, retrocedes 1.'),
(13, 'DesafioPersonal', 'Debes presionar un botón cuando aparezca una señal en pantalla. Si reaccionas a tiempo, ganas; si fallas, pierdes una casilla.'),
(14, 'TodosVsTodos', 'Todos los jugadores deben presionar un botón cuando aparezca una señal en pantalla. El último en reaccionar pierde y retrocede casillas.'),
(15, 'TodosVsTodos', 'Se hace una cuenta regresiva y deben presionar un botón al llegar a 0. El que se adelante o se demore más pierde.'),
(16, 'TodosVsTodos', 'Cada jugador elige un número del 1 al 5. Si dos o más jugadores repiten número, todos ellos pierden y retroceden casillas. Si nadie repite, no pasa nada.'),
(17, 'ReRoll', 'Puedes lanzar el dado nuevamente y avanzar ese número adicional.'),
(18, 'Swap', 'Intercambias posición con el jugador más cercano, ya sea adelante o atrás.'),
(19, 'SkipTurn', 'Pierdes tu siguiente turno o retrocedes 1 casilla.'),
(20, 'Portal', 'Te mueves automáticamente a la casilla de portal más cercana, ya sea avanzando o retrocediendo.');



