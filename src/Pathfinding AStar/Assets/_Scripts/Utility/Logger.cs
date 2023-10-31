using UnityEngine;

public enum LogTypeEnum
{
    Default = 0,
    Service = 1,
    Warning = 2,
}

public static class Logger
{
    private static readonly bool LOG_ENABLED = true;

    public static void Log(string message, LogTypeEnum logTypeEnum = LogTypeEnum.Default)
    {
        if (!LOG_ENABLED)
            return;

        if (!TryGetLogCollor(logTypeEnum, out var color))
        {
            Debug.Log(message);

            return;
        }

        Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>[{logTypeEnum.ToString()}]</color> {message}");
    }

    private static bool TryGetLogCollor(LogTypeEnum logTypeEnum, out Color color)
    {
        color = Color.clear;

        switch (logTypeEnum)
        {
            case LogTypeEnum.Service:
                {
                    color = Color.cyan;
                    break;
                }
            case LogTypeEnum.Warning:
                {
                    color = Color.yellow;
                    break;
                }
            default:
                return false;
        }

        return true;
    }
}
