namespace Online_Learning_Management.Domain.Exceptions.ModuleProgress
{
    public class ModuleProgressNotFoundException : Exception
    {
        public ModuleProgressNotFoundException() : base("ModuleProgress not found") { }
    }
}
