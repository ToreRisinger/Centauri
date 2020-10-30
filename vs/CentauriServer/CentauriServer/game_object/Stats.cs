
public static class Stats
{
    private static float BASE_SPEED = (float)20.0;

    /* MARINES
     * 
     * 
     */

    //Buildings
    public static int COMMAND_CENTER_HP = 1000;
    public static int COMMAND_CENTER_REGEN = 0;

    //Character
    public static int MARINE_HP = 100;
    public static int MARINE_REGEN = 0;
    public static int MARINE_SPEED = (int)BASE_SPEED;

    /* CENTAURI
     * 
     * 
     */

    //Buildings
    public static int HIVE_HP = 1000;
    public static int HIVE_REGEN = 2;

    //Character
    public static int EGG_HP = 50;
    public static int EGG_REGEN = 1;
    public static int EGG_SPEED = 0;

    public static int ROACH_HP = 75;
    public static int ROACH_REGEN = 1;
    public static int ROACH_SPEED = (int)(BASE_SPEED * 1.25);

    public static int STALKER_HP = 60;
    public static int STALKER_REGEN = 1;
    public static int STALKER_SPEED = (int)(BASE_SPEED * 1.25);

    public static int QUEEN_HP = 150;
    public static int QUEEN_REGEN = 2;
    public static int QUEEN_SPEED = (int)(BASE_SPEED * 0.66);

    public static int TYRANT_HP = 250;
    public static int TYRANT_REGEN = 3;
    public static int TYRANT_SPEED = (int)(BASE_SPEED * 0.80);

    public static int ALPHA_HP = 500;
    public static int ALPHA_REGEN = 4;
    public static int ALPHA_SPEED = (int)(BASE_SPEED * 0.66);
}

