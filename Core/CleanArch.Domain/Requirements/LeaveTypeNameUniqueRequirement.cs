namespace CleanArch.Domain.Requirements;

public class LeaveTypeNameUniqueRequirement
{
    private readonly Lazy<Func<Task<bool>>> _isNameUnique;

    public LeaveTypeNameUniqueRequirement(Func<Task<bool>> isNameUnique)
    {
        _isNameUnique = new Lazy<Func<Task<bool>>>(isNameUnique);
    }

    public bool IsSatified => _isNameUnique.Value().Result;
}
