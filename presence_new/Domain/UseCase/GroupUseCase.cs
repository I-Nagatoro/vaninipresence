using Demo.Data.LocalData;
using Demo.domain.Models;

namespace Demo.Domain.UseCase
{
    public class GroupUseCase
    {
        private readonly GroupRepositoryImpl _repositoryGroupImpl;

        public GroupUseCase(GroupRepositoryImpl repositoryGroupImpl)
        {
            _repositoryGroupImpl = repositoryGroupImpl;
        }

        // Приватный метод для валидации имени группы
        private void ValidateGroupName(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentException("Имя группы не может быть пустым.");
            }
        }

        private void ValidateGroupId(int GroupId)
        {
            if (GroupId < 1)
            {
                throw new ArgumentException("Введите корректный ID группы.");
            }
        }

        // Приватный метод для валидации существования группы по ID
        private GroupLocalEntity ValidateGroupExistence(int groupId)
        {
            var existingGroup = _repositoryGroupImpl.GetAllGroups()
                .FirstOrDefault(g => g.Id == groupId);

            if (existingGroup == null)
            {
                throw new ArgumentException("Группа не найдена.");
            }

            return existingGroup;
        }


        // Метод для получения списка всех групп
        public List<Group> GetAllGroups()
        {
            return [.. _repositoryGroupImpl.GetAllGroups()
                .Select(it => new Group { Id = it.Id, Name = it.Name })];
        }


        public void FindGroupById(int IdGroup)
        {
            List<Group> GetAllGroups()
            {
                return [.. _repositoryGroupImpl
                    .GetAllGroups()
                    .Select(
                        it => new Group
                        { Id = it.Id, Name = it.Name }
                        )
                    ];
            }
            foreach (var group in GetAllGroups())
            {
                if (IdGroup == group.Id)
                {
                    Console.WriteLine($"ID группы: {group.Id} Название группы: {group.Name}");
                }
            }
        }


        // Метод для добавления новой группы
        public void AddGroup(string groupName)
        {
            ValidateGroupName(groupName);

            var newId = _repositoryGroupImpl.GetAllGroups().Any()
                        ? _repositoryGroupImpl.GetAllGroups().Max(g => g.Id) + 1
                        : 1;

            GroupLocalEntity newGroup = new GroupLocalEntity
            {
                Id = newId,
                Name = groupName
            };

            _repositoryGroupImpl.AddGroup(newGroup);
        }

        public void RemoveGroupById(int groupId)
        {
            ValidateGroupId(groupId);
            var existingGroup = ValidateGroupExistence(groupId);
            List<Group> _groups = GetAllGroups();

            // Находим группу по ID и удаляем ее
            var groupToRemove = _groups.FirstOrDefault(g => g.Id == existingGroup.Id);
            if (groupToRemove != null)
            {
                _groups.Remove(groupToRemove);
                _repositoryGroupImpl.RemoveGroupById(existingGroup);
            }
            else
            {
                throw new ArgumentException("Группа не найдена.");
                // Обработка случая, если группа не найдена (например, выброс исключения)
            }
        }


        // Метод для изменения названия группы
        public void UpdateGroup(int groupId, string newGroupName)
        {
            ValidateGroupName(newGroupName);
            var existingGroup = ValidateGroupExistence(groupId);

            existingGroup.Name = newGroupName;
            _repositoryGroupImpl.UpdateGroup(existingGroup);
        }
    }
}