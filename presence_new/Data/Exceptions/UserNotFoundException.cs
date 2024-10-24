using System;

namespace Demo.Data.Exceptions
{
    public class UserNotFoundException : RepositoryException
    {
        public UserNotFoundException(Guid userGuid)
            : base($"Пользователь с GUID {userGuid} не найден.") { }
    }
}