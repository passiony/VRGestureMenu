public class NumData
{
    public static readonly string[] MainNames = new[] { "Settings", "Chat", "Status", "Game" };

    public static readonly string[][] SubNames =
    {
        new[] { "Display Control", "Home", "Volume Control", "Pause" },
        new[] { "Quick Chat", "Pings", "Screenshot Sharing", "Voice" },
        new[] { "Speed", "Chat", "Inventory", "Health" },
        new[] { "Progress", "Motion", "-", "Game" },
    };

    static readonly string[][] array1 =
    {
        new[] { "settings", "display control" },
        new[] { "settings", "pause" },
        new[] { "settings", "volume control" },
        new[] { "settings", "home" },
        new[] { "chat", "quick chat" },
        new[] { "chat", "voice" },
        new[] { "chat", "screenshot sharing" },
        new[] { "chat", "pings" },
        new[] { "status", "speed" },
        new[] { "status", "health" },
        new[] { "status", "inventory" },
        new[] { "status", "motion" },
        new[] { "game", "progress" },
        new[] { "game", "frame" },
        new[] { "game", "map" }
    };

    static readonly string[][] array2 =
    {
        new[] { "chat", "voice" },
        new[] { "chat", "screenshot sharing" },
        new[] { "chat", "pings" },
        new[] { "chat", "quick chat" },
        new[] { "settings", "pause" },
        new[] { "settings", "volume control" },
        new[] { "settings", "home" },
        new[] { "settings", "display control" },
        new[] { "game", "frame" },
        new[] { "game", "map" },
        new[] { "game", "progress" },
        new[] { "status", "health" },
        new[] { "status", "inventory" },
        new[] { "status", "motion" },
        new[] { "status", "speed" },
    };

    static readonly string[][] array3 =
    {
        new[] { "status", "inventory" },
        new[] { "status", "motion" },
        new[] { "status", "speed" },
        new[] { "status", "health" },
        new[] { "game", "map" },
        new[] { "game", "progress" },
        new[] { "game", "frame" },
        new[] { "settings", "volume control" },
        new[] { "settings", "home" },
        new[] { "settings", "display control" },
        new[] { "settings", "pause" },
        new[] { "chat", "screenshot sharing" },
        new[] { "chat", "pings" },
        new[] { "chat", "quick chat" },
        new[] { "chat", "voice" },
    };

    static readonly string[][] array4 =
    {
        new[] { "game", "map" },
        new[] { "game", "progress" },
        new[] { "game", "frame" },
        new[] { "status", "motion" },
        new[] { "status", "speed" },
        new[] { "status", "health" },
        new[] { "status", "inventory" },
        new[] { "chat", "pings" },
        new[] { "chat", "quick chat" },
        new[] { "chat", "voice" },
        new[] { "chat", "screenshot sharing" },
        new[] { "settings", "home" },
        new[] { "settings", "display control" },
        new[] { "settings", "pause" },
        new[] { "settings", "volume control" }
    };

    public static string[][][] DataList =
    {
        array1,
        array2,
        array3,
        array4
    };

    public static string[] CurrentNum => DataList[playerIndex][dataIndex];

    public static int playerIndex;
    public static int dataIndex;
    public static EPanel PanelType;
    public static int times;

    public static int RepeateTime;
    public static int RepeateMax = 4;

    public static void Rest()
    {
        playerIndex = 0;
        dataIndex = 0;
        RepeateTime = 0;
        PanelType = EPanel.Radian;
    }

    public static void ChangePlayer()
    {
        playerIndex = (playerIndex + 1) % 4;
        dataIndex = 0;
        RepeateTime = 0;
    }

    public static void ChangePlayer(int index)
    {
        playerIndex = index;
        dataIndex = 0;
        RepeateTime = 0;
    }

    public static void NextData()
    {
        RepeateTime++;
        if (RepeateTime >= RepeateMax)
        {
            dataIndex++;
            RepeateTime = 0;
            if (dataIndex >= DataList[0].Length)
            {
                dataIndex = 0;
                ChangePlayer();
            }
        }
    }
}