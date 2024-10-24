using Demo.Data.Exceptions;
using Demo.Data.Repository;
using Demo.domain.Models;

namespace Demo.Domain.UseCase
{
    public class UserUseCase
    {
        private readonly UserRepositoryImpl _repositoryUserImpl;
        private readonly GroupRepositoryImpl _repositoryGroupImpl;

        public UserUseCase(UserRepositoryImpl repositoryImpl, GroupRepositoryImpl repositoryGroupImpl)
        {
            _repositoryUserImpl = repositoryImpl;
            _repositoryGroupImpl = repositoryGroupImpl;
        }

        // Приватный метод для валидации ФИО пользователя
        private void ValidateUserFIO(string fio)
        {
            if (string.IsNullOrWhiteSpace(fio))
            {
                throw new ArgumentException("ФИО не может быть пустым.");
            }
        }

        // Приватный метод для валидации существования пользователя по ID
        private UserLocalEnity ValidateUserExistence(Guid userGuid)
        {
            var user = _repositoryUserImpl.GetAllUsers
                .FirstOrDefault(u => u.Guid == userGuid);

            if (user == null)
            {
                throw new Exception("Пользователь не найден.");
            }

            return user;
        }

        // Приватный метод для валидации существования группы по ID
        private GroupLocalEntity ValidateGroupExistence(int groupId)
        {
            var group = _repositoryGroupImpl.GetAllGroups()
                .FirstOrDefault(g => g.Id == groupId);

            if (group == null)
            {
                throw new Exception("Группа не найдена.");
            }

            return group;
        }

        // Вывести всех пользователей
        public List<User> GetAllUsers() => _repositoryUserImpl.GetAllUsers
            .Join(_repositoryGroupImpl.GetAllGroups(),
            user => user.GroupID,
            group => group.Id,
            (user, group) =>
            new User
            {
                FIO = user.FIO,
                Guid = user.Guid,
                Group = new Group { Id = group.Id, Name = group.Name }
            }).ToList();

        // Удалить пользователя по id
        public bool RemoveUserById(Guid userGuid)
        {
            try
            {
                return _repositoryUserImpl.RemoveUserById(userGuid);
            }
            catch (UserNotFoundException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return false;
            }
            catch (RepositoryException ex)
            {
                Console.WriteLine($"Ошибка в репозитории: {ex.Message}");
                return false;
            }
        }

        // Обновить пользователя по guid
        public User UpdateUser(User user)
        {
            ValidateUserFIO(user.FIO);
            ValidateGroupExistence(user.Group.Id);

            UserLocalEnity userLocalEnity = new UserLocalEnity
            {
                FIO = user.FIO,
                GroupID = user.Group.Id,
                Guid = user.Guid
            };

            UserLocalEnity? result = _repositoryUserImpl.UpdateUser(userLocalEnity);

            if (result == null)
            {
                throw new Exception("Ошибка при обновлении пользователя.");
            }

            var groupEntity = ValidateGroupExistence(result.GroupID);

            return new User
            {
                FIO = result.FIO,
                Guid = result.Guid,
                Group = new Group
                {
                    Id = groupEntity.Id,
                    Name = groupEntity.Name
                }
            };
        }

        // Найти пользователя по id
        public User FindUserById(Guid userGuid)
        {
            var user = ValidateUserExistence(userGuid);
            var group = ValidateGroupExistence(user.GroupID);

            return new User
            {
                FIO = user.FIO,
                Guid = user.Guid,
                Group = new Group { Id = group.Id, Name = group.Name }
            };
        }
    }
}
