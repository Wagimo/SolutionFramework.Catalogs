namespace SolutionFramework.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key})  no fue encontrado")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}
