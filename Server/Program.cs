namespace Server;

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
                    Server.GetCount();
                    Thread.Sleep(200);
                }
            });
        }
        
        for (int i = 0; i < countUsers; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                for (int j = 0; j < 2 ; j++)
                {
                    Server.AddCount(2);
                    Thread.Sleep(200);
                }
            });
        }

        Task.WaitAll(tasks);

    }
}