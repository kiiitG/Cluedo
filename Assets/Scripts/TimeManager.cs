using System;

class TimeManager
{
    private static long MAX_INACTIVE_TIME = TimeSpan.TicksPerMinute * 2;
    private static long leaveTicks = MAX_INACTIVE_TIME + DateTime.Now.Ticks;
    private static bool leaving = false;

    public static void Nullify()
    {
        leaveTicks = MAX_INACTIVE_TIME + DateTime.Now.Ticks;
    }

    public static bool TimeOut()
    {
        if (DateTime.Now.Ticks > leaveTicks && !leaving)
        {
            leaving = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
