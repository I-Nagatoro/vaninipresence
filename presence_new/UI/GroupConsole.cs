using Demo.Domain.UseCase;
using System;
using System.Text;

namespace Demo.UI
{
    public class GroupConsoleUI
    {
        private readonly GroupUseCase _groupUseCase;

        public GroupConsoleUI(GroupUseCase groupUseCase)
        {
            _groupUseCase = groupUseCase;
        }

        public void FindGroupById(int IdGroup)
        {
            _groupUseCase.FindGroupById(IdGroup);
        }

        // Метод для отображения всех групп
        public void DisplayAllGroups()
        {
            Console.WriteLine("\n=== Список всех групп ===");
            StringBuilder groupOutput = new StringBuilder();

            foreach (var group in _groupUseCase.GetAllGroups())
            {
                groupOutput.AppendLine($"{group.Id}\t{group.Name}");
            }

            Console.WriteLine(groupOutput);
            Console.WriteLine("===========================\n");
        }

        // Метод для добавления новой группы
        public void AddGroup(string groupName)
        {
            try
            {
                _groupUseCase.AddGroup(groupName);
                Console.WriteLine($"\nГруппа {groupName} добавлена.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}\n");
            }
        }

        public void RemoveGroup(string groupIdStr)
        {
            int groupId = int.Parse(groupIdStr);
            _groupUseCase.RemoveGroupById(groupId);
            Console.WriteLine($"Группа с ID: {groupId} удалена");
        }

        // Метод для обновления названия группы
        public void UpdateGroupName(int groupId, string newGroupName)
        {
            _groupUseCase.UpdateGroup(groupId, newGroupName);
            Console.WriteLine($"\nНазвание группы с ID {groupId} изменено на {newGroupName}.\n");
        }
    }
}
