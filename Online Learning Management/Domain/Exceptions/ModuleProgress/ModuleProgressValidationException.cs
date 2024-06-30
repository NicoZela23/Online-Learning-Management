using System;

namespace OnlineLearningManagement.Domain.Exceptions.ModuleProgress
{
    public class ModuleProgressValidationException : Exception
    {
        public ModuleProgressValidationException() { }

        public ModuleProgressValidationException(string message) : base(message) { }

        public ModuleProgressValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}