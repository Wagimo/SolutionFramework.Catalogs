namespace SolutionFramework.Core.Abstractions
{
    /// <summary>
    /// Definición para los data transfer object de tipo GUID
    /// </summary>
    /// <typeparam name="TUserKey">Tipo que identificara el usuario</typeparam>
    public interface IDtoGuid<TUserKey> : IDtoBase<Guid, TUserKey>
    {
    }
}
