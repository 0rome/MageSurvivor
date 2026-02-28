using System;

[System.Serializable]
public class UpgradeOption
{
    public string Title;
    public string Description;
    public Action Apply;
}
