using System;

namespace Demo.Data.Exceptions
{
    public class GroupNotFoundException : RepositoryException
    {
        public GroupNotFoundException(int groupId)
            : base($"Группа с ID {groupId} не найдена.") { }
    }
}