using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            PlayerStatementHistory history = new PlayerStatementHistory();
            
            player.TakeDamage(5);//Нанесли 3 единицы урона: 10 - 3 = 7
            history.History.Push(player.Save());//Сейвим данные Жизни = 7
            player.TakeDamage(4);//Нанесли 5 единиц урона. Данные Жизни = 2
            player.Load(history.History.Pop());//Восстановили данные по жизням (7)
            Console.ReadKey();
        }



        
    }
    //Хранитель
    public class PlayerMemento
    {

        private int _health;
        public int Health { get => _health; set => _health = value; }
        public PlayerMemento(int health)
        {
            Health = health;
        }
        
    }

    //Храним объект хранителя без полного доступа к нему
    public class PlayerStatementHistory
    { 
        //Впервые в жизни понадобился стек в шарпе, пора уходить на покой
        public Stack<PlayerMemento> History { get; set; }
        public PlayerStatementHistory()
        {
            History = new Stack<PlayerMemento>();
        }
    }



    //Объект хранителя для сейва личного состояния
    public class Player
    {
        private int _lives = 10;

        public void TakeDamage(int damage)
        {
            if(_lives > 0)
            {
                _lives -= damage;
                Console.WriteLine("[Вам нанесено {0} урона. Жизни: {1}]", damage, _lives);
            }
        }

        //Сейвимс состояние
        public PlayerMemento Save()
        {
            Console.WriteLine("Сохранение игры...");
            Task.Delay(3000).GetAwaiter().GetResult();// имитируем сохранение
            Console.WriteLine("Сохранено!");
            return new PlayerMemento(_lives);
        }

        //Загружаем данные
        public void Load(PlayerMemento data)
        {
            _lives = data.Health;
            Console.WriteLine("Восстановление состояния...");
            Task.Delay(3000).GetAwaiter().GetResult();// имитируем сохранение
            Console.WriteLine("Восстановлено! Параметры жизни: {0}", _lives);
        }
    }



}
