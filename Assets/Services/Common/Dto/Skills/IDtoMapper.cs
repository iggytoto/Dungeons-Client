namespace Services.Common.Dto
{
    public interface IDtoMapper<out T> where T : ModelBase
    {
        T ToDomainTyped();
    }
}