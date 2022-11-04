public static class SoundMasterData
{
    static float _masterVolume;
    static float _bgmVolume;
    static float _seVolume;

    public static float MasterValume
    {
        get => _masterVolume;
        set => _masterVolume = value > 1 ? 1 : value;
    }

    public static float BGMVolume
    {
        get => _bgmVolume;
        set => _bgmVolume = value > 1 ? 1 : value;
    }

    public static float SEVoume
    {
        get => _seVolume;
        set => _seVolume = value > 1 ? 1 : value;
    }
}
