namespace Server;

/// <summary>
/// This class needed only for server testing
/// </summary>
class Program
{
    static void Main()
    {

        int countUsers = 8;
        Task[] tasks = new Task[countUsers];

        for (int i = 0; i < countUsers / 2; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                for (int j = 0; j < 3 ; j++)
                {
                    int result = Server.GetCount();
                    Thread.Sleep(100);
                    Console.WriteLine($"Значение count: {result} считано!");
                }
            });
        }
        
        for (int i = 0; i < countUsers; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                for (int j = 0; j < 2 ; j++)
                {
                    Server.AddCount(j + 1);
                    Thread.Sleep(100);
                    Console.WriteLine($"К значению переменной count добавлено {j + 1}!");
                }
            });
        }

        Task.WaitAll(tasks);
        Console.ReadLine();

    }
}