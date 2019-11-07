using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//1 Есть массив на 100 элементов с разными цифрами.
//Элемент массива передается в функцию для приведения его в тип строки (значения от 1 до 7 - дни недели).
// Функция возвращает название дня недели ввиде строки.Если элемент меньше 1 или больше 7 вызывается исключение NotDayOfWeekException.
//В случае возникновения NotDayOfWeekException написать сообщение, что цифра не допустима и продолжить выполнение этой операции.
//Результат работы фукции также вывести на экран.
//Класс NotDayOfWeekException создайте самостоятельно. Добавьте в класс 2 конструктора:
//- контсруктор без параметров
//- контсруктор, принимающий сообщение

//2 
//Очень требовательный клиент заказывает в ресторане блюда (аперетив, суп, второе, напитки и сладкое). 
//Так случилось, что некоторых блюд нет.Если блюда нет, официант об этом говорит(выдает исключение). 
//В этом случае клиент заказывает другое блюдо, однако не больше 3х раз максимум на каждое блюдо.
//Если лимит превышен - клиент уходит. Должен быть составлен полный набор блюд для клиента. 
//Показать на экран блюда, заказанные клиентом, к-во отказов официанта.Если клиент ушел, то выдать и это сообщение.
//Проделать операцию с 7ю клиентами.

//(объект Официант будет содержаться в классе Клиент)


namespace tryCatchHomework
{
    //NotDayOfWeekException
    class NotDayOfWeekException : ApplicationException
    {
        public override string Message => MyMessage;
        public string MyMessage { get; set; }
        public NotDayOfWeekException()
        {
            MyMessage = "Число за пределами дней недель.";
        }
        public NotDayOfWeekException(string message)
        {
            MyMessage = message;
        }
        
    }

    //funk
    class Work
    {
        public string FunkForArray(int number)
        {
            string text = "";
            if (number < 1 | 7 < number)
                throw new NotDayOfWeekException($"Число {number} за пределами дней недель.");
            
            switch (number)
            {
                case 1:
                    text = "Понедельник";
                    break;
                case 2:
                    text = "Вторник";
                    break;
                case 3:
                    text = "Среда";
                    break;
                case 4:
                    text = "Четверг";
                    break;
                case 5:
                    text = "Пятница";
                    break;
                case 6:
                    text = "Суббота";
                    break;
                case 7:
                    text = "Воскресенье";
                    break;
                default:
                    break;
            }
            return text;
        }
    }


    //================================ВТОРОЕ ЗАДАНИЕ

    class EmptyDishExeption : ApplicationException
    {
        public override string Message => MyMessage;
        public string MyMessage { get; set; }
        public EmptyDishExeption()
        {

        }
        public EmptyDishExeption(string message)
        {
            MyMessage = message;
        }
    }
    class EmptyAllDishesExeption : ApplicationException
    {
        public override string Message => MyMessage;
        public string MyMessage { get; set; }
        public EmptyAllDishesExeption()
        {

        }
        public EmptyAllDishesExeption(string message)
        {
            MyMessage = message;
        }
    }

    class WaiterExeption : ApplicationException
    {
        public override string Message => MyMessage;
        public string MyMessage { get; set; }
        public WaiterExeption()
        {

        }
        public WaiterExeption(string message)
        {
            MyMessage = message;
        }
    }

    //блюдо
    class Dish
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        private Dish()
        {

        }
        public Dish(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
    //официант
    class Waiter
    {
        public Dictionary<int, int> hateClient;//злопамятный официант не позволит одному клиенту брать больше трех раз одно и то же блюдо.
        
        public List<Dish> dishes;
        public Waiter()
        {
            int min = 0;//включительно
            int max = 4;//исключительно
            Random random = new Random();
            dishes = new List<Dish>();
            dishes.Add(new Dish("аперетив", random.Next(min, max)));
            dishes.Add(new Dish("суп", random.Next(min, max)));
            dishes.Add(new Dish("второе", random.Next(min, max)));
            dishes.Add(new Dish("напиток", random.Next(min, max)));
            dishes.Add(new Dish("сладкое", random.Next(min, max)));

            hateClient = new Dictionary<int, int>();
            for (int i = 0; i < dishes.Count; i++)
            {
                hateClient.Add(i, 3);
            }
        }
        public void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var item in dishes)
            {
                Console.WriteLine($"Название: [{item.Name}], кол-во: [{item.Quantity}]");
            }
            Console.ResetColor();
        }
    }

    //клиент
    class Client
    {
        private Dictionary<int, bool> memory;
        private int LastIndex { get; set; } = -1;//запоминает, какое блюдо, он только что заказывал.
        private Waiter waiter;
        public Client()
        {
            waiter = new Waiter();
            memory = new Dictionary<int, bool>();
            for (int i = 0; i < waiter.dishes.Count; i++)
            {
                if (waiter.dishes[i].Quantity == 0)
                {
                    memory.Add(i, false);
                }
                else
                {
                    memory.Add(i, true);
                }
            }
        }
        public bool MakeAnOrder()
        {
            for (int i = 0; i < waiter.dishes.Count; i++)
            {
                if (waiter.dishes[i].Quantity == 0)
                {
                    memory[i] = false;
                }
                else
                {
                    memory[i] = true;
                }
            }
            Random random = new Random();
            int index = 0;
            int counterFood = 0;
            for (int i = 0; i < memory.Count; i++)
            {
                if (memory[i] == true)
                {
                    counterFood++;
                }
            }
            if (counterFood == 0)
                throw new EmptyAllDishesExeption($"Еды в ресторане нет.");
            do//етот блок нужен, чтоб клиент не заказывал два раза под ряд одно и то же блюдо, а так же, если официант сказал, что этого блюда нет в списке.
            {
                index = random.Next(waiter.dishes.Count);
                if (memory[index] != false)
                {
                    if (counterFood == 1)
                        break;
                    else
                    {
                        if (LastIndex != index)
                        {
                            LastIndex = index;
                            break;
                        }
                        else
                            continue;
                    }
                }
            } while (true);
            Console.WriteLine($"Клиент заказывает: {waiter.dishes[index].Name.ToString()}");
            if (waiter.dishes[index].Quantity > 0)
            {
                if (waiter.hateClient[index] > 0)
                {
                    Console.WriteLine($"Подано Блюдо: {waiter.dishes[index].Name.ToString()}");
                    waiter.dishes[index].Quantity--;
                    waiter.hateClient[index]--;
                    return true;
                }
                else
                    throw new WaiterExeption($"Официант сказал, что клиент уже заказывал это блюдо три раза: {waiter.dishes[index].Name.ToString()}");
            }
            else
            {
                memory[index] = false;
                throw new EmptyDishExeption($"Блюдо отсуцтвует: {waiter.dishes[index].Name.ToString()}");
            }
        }
        public void AskMenu()
        {
            waiter.ShowMenu();
        }
    }

    

    class Program
    {
        static void Main(string[] args)
        {
            ////////////// задание 1
            //int[] arrInt = new int[100];
            //Random random = new Random();
            //for (int i = 0; i < arrInt.Length; i++)
            //    arrInt[i] = random.Next(1, 11);//1 - включительно; 11 - исключительно

            //string[] arrStr = new string[arrInt.Length];
            //Work work = new Work();
            //for (int i = 0; i < arrInt.Length; i++)
            //{
            //    try
            //    {
            //        arrStr[i] = work.FunkForArray(arrInt[i]);
            //        Console.WriteLine(arrStr[i]);
            //    }
            //    catch (NotDayOfWeekException e)
            //    {
            //        Console.WriteLine(e.MyMessage);
            //    }
            //}

            ////////////////////// задание 2            

            Client client;
            for (int i = 0; i < 7; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Новый клиент № {i + 1} пришел в ресторан");
                
                Console.ResetColor();
                client = new Client();
                client.AskMenu();
                
                bool key = false;
                do
                {
                    key = false;
                    try
                    {
                        key = client.MakeAnOrder();
                    }
                    catch (EmptyDishExeption e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.MyMessage);
                        Console.ResetColor();
                    }
                    catch (WaiterExeption e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.MyMessage);
                        Console.WriteLine("Клиент уходит из ресторана");
                        Console.ResetColor();
                        break;
                    }
                    catch (EmptyAllDishesExeption e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.MyMessage);
                        Console.WriteLine("Клиент уходит из ресторана");
                        Console.ResetColor();
                        break;
                    }

                    if (key == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Клиен съел блюдо.");
                        Console.ResetColor();
                    }
                    
                        
                } while (/*key != true*/true);
            }



            Console.WriteLine("\n-----Конец-----");
            Console.ReadKey(true);
        }
    }
}
