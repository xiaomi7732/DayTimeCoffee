using System.Runtime.InteropServices;

public static class Native
{
    [Flags]
    public enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_SYSTEM_REQUIRED = 0x00000001
        // Legacy flag, should not be used.
        // ES_USER_PRESENT = 0x00000004
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);

    public static bool NoSleep()
    {
        uint code = SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        return code != 0;
    }

    public static bool OkayToSleep()
    {
        uint returnCode = SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        return returnCode != 0;
    }
}