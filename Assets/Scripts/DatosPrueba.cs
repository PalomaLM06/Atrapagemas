using System.Collections.Generic;

public static class DatosPrueba
{
    public static List<Jugador> ObtenerJugadores()
    {
        List<Jugador> jugadores = new List<Jugador>();

        jugadores.Add(new Jugador(1, "Duende1", 0, 1, 0, 0));
        jugadores.Add(new Jugador(2, "Duende2", 0, 1, 0, 0));
        jugadores.Add(new Jugador(3, "Duende3", 0, 1, 0, 0));
        jugadores.Add(new Jugador(4, "Duende4", 0, 1, 0, 0));

        return jugadores;
    }

    public static List<Casilla> ObtenerCasillas()
    {
        List<Casilla> casillas = new List<Casilla>();

        casillas.Add(new Casilla(1, 1, "Start", 1, 0, 0));
        casillas.Add(new Casilla(2, 2, "1vs1", 2, 1, 0));
        casillas.Add(new Casilla(3, 3, "Normal", 1, 2, 0));
        casillas.Add(new Casilla(4, 4, "ReRoll", 17, 3, 0));
        casillas.Add(new Casilla(5, 5, "DesafioPersonal", 10, 4, 0));
        casillas.Add(new Casilla(6, 6, "Portal", 20, 5, 0));
        casillas.Add(new Casilla(7, 7, "TodosVsTodos", 14, 6, 0));
        casillas.Add(new Casilla(8, 8, "Swap", 18, 7, 0));
        casillas.Add(new Casilla(9, 9, "MitadvsMitad", 6, 7, 1));
        casillas.Add(new Casilla(10, 10, "Normal", 1, 6, 1));
        casillas.Add(new Casilla(11, 11, "SkipTurn", 19, 5, 1));
        casillas.Add(new Casilla(12, 12, "1vs1", 3, 4, 1));
        casillas.Add(new Casilla(13, 13, "Normal", 1, 3, 1));
        casillas.Add(new Casilla(14, 14, "Portal", 20, 2, 1));
        casillas.Add(new Casilla(15, 15, "TodosVsTodos", 15, 1, 1));
        casillas.Add(new Casilla(16, 16, "Normal", 1, 0, 1));
        casillas.Add(new Casilla(17, 17, "1vs1", 4, 0, 2));
        casillas.Add(new Casilla(18, 18, "MitadvsMitad", 7, 1, 2));
        casillas.Add(new Casilla(19, 19, "ReRoll", 17, 2, 2));
        casillas.Add(new Casilla(20, 20, "SkipTurn", 19, 3, 2));
        casillas.Add(new Casilla(21, 21, "DesafioPersonal", 11, 4, 2));
        casillas.Add(new Casilla(22, 22, "Normal", 1, 5, 2));
        casillas.Add(new Casilla(23, 23, "1vs1", 5, 6, 2));
        casillas.Add(new Casilla(24, 24, "SkipTurn", 19, 7, 2));
        casillas.Add(new Casilla(25, 25, "TodosVsTodos", 16, 7, 3));
        casillas.Add(new Casilla(26, 26, "Normal", 1, 6, 3));
        casillas.Add(new Casilla(27, 27, "MitadvsMitad", 8, 5, 3));
        casillas.Add(new Casilla(28, 28, "Swap", 18, 4, 3));
        casillas.Add(new Casilla(29, 29, "DesafioPersonal", 12, 3, 3));
        casillas.Add(new Casilla(30, 30, "Normal", 1, 2, 3));
        casillas.Add(new Casilla(31, 31, "1vs1", 2, 1, 3));
        casillas.Add(new Casilla(32, 32, "Cofre", 21, 0, 3));

        return casillas;
    }

    public static List<Reto> ObtenerRetos()
    {
        List<Reto> retos = new List<Reto>();

        retos.Add(new Reto(1, "Normal", "Casilla sin efecto, el turno continúa normalmente."));

        retos.Add(new Reto(2, "1vs1", "Dos jugadores eligen un número del 1 al 10. Gana quien esté más cerca del número aleatorio generado por el sistema."));
        retos.Add(new Reto(3, "1vs1", "Dos jugadores compiten por presionar un botón apenas aparezca en pantalla. El más rápido gana y avanza 1 casilla."));
        retos.Add(new Reto(4, "1vs1", "Cada jugador recibe un número aleatorio del 1 al 10. El mayor gana y avanza 1 casilla."));
        retos.Add(new Reto(5, "1vs1", "Se genera un número aleatorio. Los jugadores eligen par o impar. Quien acierte gana."));

        retos.Add(new Reto(6, "MitadvsMitad", "Se muestra una suma sencilla. El equipo que responda primero correctamente gana."));
        retos.Add(new Reto(7, "MitadvsMitad", "Cada equipo obtiene números aleatorios. El equipo con mayor suma total gana."));
        retos.Add(new Reto(8, "MitadvsMitad", "Cada equipo elige cara o cruz. Se lanza una moneda y el equipo correcto gana."));
        retos.Add(new Reto(9, "MitadvsMitad", "Cada equipo resuelve una operación diferente. El primero en acertar gana y el equipo perdedor retrocede casillas."));

        retos.Add(new Reto(10, "DesafioPersonal", "Aparece una suma en pantalla y tienes pocos segundos para responder. Si aciertas, avanzas 1 casilla; si fallas, retrocedes 1."));
        retos.Add(new Reto(11, "DesafioPersonal", "Elige un número del 1 al 5. El sistema genera uno al azar. Si aciertas, avanzas 2 casillas; si no, te quedas igual."));
        retos.Add(new Reto(12, "DesafioPersonal", "Aparece un número oculto. Debes decidir si es mayor o menor que 5. Si aciertas, avanzas 1 casilla; si fallas, retrocedes 1."));
        retos.Add(new Reto(13, "DesafioPersonal", "Debes presionar un botón cuando aparezca una señal en pantalla. Si reaccionas a tiempo, ganas; si fallas, pierdes una casilla."));

        retos.Add(new Reto(14, "TodosVsTodos", "Todos los jugadores deben presionar un botón cuando aparezca una señal en pantalla. El último en reaccionar pierde y retrocede casillas."));
        retos.Add(new Reto(15, "TodosVsTodos", "Se hace una cuenta regresiva y deben presionar un botón al llegar a 0. El que se adelante o se demore más pierde."));
        retos.Add(new Reto(16, "TodosVsTodos", "Cada jugador elige un número del 1 al 5. Si dos o más jugadores repiten número, todos ellos pierden y retroceden casillas. Si nadie repite, no pasa nada."));

        retos.Add(new Reto(17, "ReRoll", "Puedes lanzar el dado nuevamente y avanzar ese número adicional."));
        retos.Add(new Reto(18, "Swap", "Intercambias posición con el jugador más cercano, ya sea adelante o atrás."));
        retos.Add(new Reto(19, "SkipTurn", "Pierdes tu siguiente turno o retrocedes 1 casilla."));
        retos.Add(new Reto(20, "Portal", "Te mueves automáticamente a la casilla de portal más cercana, ya sea avanzando o retrocediendo."));
        retos.Add(new Reto(21, "Cofre", "Abres un cofre y ganas gemas."));

        return retos;
    }
}