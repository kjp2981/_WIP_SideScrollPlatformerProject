public interface IRecovery
{
    public float recoveryReduction { get; }

    public bool isRecorvery { get; }

    public void Heal(int heal);
}
