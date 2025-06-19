namespace Server;

public class Server
{
    private static int count = 0;
    private static readonly ReaderWriterLockSlim RwLockSlim = new ReaderWriterLockSlim();

    public static int GetCount()
    {
        RwLockSlim.EnterReadLock();
        try
        {
            Console.WriteLine($"Значение count: {count} считано!");
            return count;
        }
        finally
        {
            RwLockSlim.ExitReadLock(); 
        }
        
        // rwLockSlim.EnterReadLock();
    }


    public static void AddCount(int value)
    {
        RwLockSlim.EnterWriteLock();
        try
        {
            count += value;
            Console.WriteLine($"К значению переменной count добавлено {value} и теперь составляет {count}!");
        }
        finally
        {
            RwLockSlim.ExitWriteLock();    
        }
        
    }
    
    
}