namespace Server;

public static class Server
{
    private static int _count = 0;
    private static readonly ReaderWriterLockSlim RwLockSlim = new ReaderWriterLockSlim();

    public static int GetCount()
    {
        RwLockSlim.EnterReadLock();
        try
        {
            Console.WriteLine($"Значение count: {_count} считано!");
            return _count;
        }
        finally
        {
            RwLockSlim.ExitReadLock(); 
        }
    }


    public static void AddCount(int value)
    {
        RwLockSlim.EnterWriteLock();
        try
        {
            _count += value;
            Console.WriteLine($"К значению переменной count добавлено {value} и теперь составляет {_count}!");
        }
        finally
        {
            RwLockSlim.ExitWriteLock();    
        }
    }
}