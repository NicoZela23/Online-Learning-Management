namespace Online_Learning_Management.Domain.Exceptions.Module
{
    public class ModuleNotFoundException : Exception
    {
        public ModuleNotFoundException() : base("Module not found") { }
    }
}
