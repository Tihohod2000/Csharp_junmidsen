namespace Server;

public class Server
{
    private static int count = 0;
    private static readonly ReaderWriterLockSlim rwLockSlim = new ReaderWriterLockSlim();

    public static int GetCount()
    {
        return count;
    }


    public static void AddCount(int value)
    {
        count += value;
    }
    
    
}