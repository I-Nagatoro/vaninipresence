using Demo.domain.Models;
using Demo.Domain.UseCase;
using System;

namespace Demo.UI
{
    public class MainMenuUI
    {
        private readonly UserConsoleUI _userConsoleUI;
        private readonly GroupConsoleUI _groupConsoleUI;
        private readonly PresenceConsole _presenceConsoleUI;

        public MainMenuUI(UserUseCase userUseCase, GroupUseCase groupUseCase, UseCaseGeneratePresence presenceUseCase)
        {
            _userConsoleUI = new UserConsoleUI(userUseCase);
            _groupConsoleUI = new GroupConsoleUI(groupUseCase);
            _presenceConsoleUI = new PresenceConsole(presenceUseCase);
        }

        public void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("\n==================== Главная Панель ====================\n");

                Console.WriteLine("~~~~~~~~~~~~~~~ УПРАВЛЕНИЕ ПОЛЬЗОВАТЕЛЯМИ ~~~~~~~~~~~~~~~");
                Console.WriteLine("1. Показать список всех пользователей");
                Console.WriteLine("2. Удалить пользователя по его ID");
                Console.WriteLine("3. Обновить данные пользователя по ID");
                Console.WriteLine("4. Найти пользователя по его ID");
                Console.WriteLine();

                Console.WriteLine("~~~~~~~~~~~~~~~ УПРАВЛЕНИЕ ГРУППАМИ ~~~~~~~~~~~~~~~");
                Console.WriteLine("5. Показать список всех групп");
                Console.WriteLine("6. Создать новую группу");
                Console.WriteLine("7. Удалить группу по ID");
                Console.WriteLine("8. Изменить название существующей группы");
                Console.WriteLine("9. Найти группу по ее ID");
                Console.WriteLine();

                Console.WriteLine("~~~~~~~~~~~~~~~ УПРАВЛЕНИЕ ПОСЕЩАЕМОСТЬЮ ~~~~~~~~~~~~~~~");
                Console.WriteLine("10. Сгенерировать посещаемость на текущий день");
                Console.WriteLine("11. Сгенерировать посещаемость на текущую неделю");
                Console.WriteLine("12. Показать посещаемость всех пользователей");
                Console.WriteLine("13. Отметить пользователя как отсутствующего");
                Console.WriteLine("14. Вывести посещаемость группы по ID");
                Console.WriteLine();

                Console.WriteLine("========================================================");
                Console.WriteLine("0. Выход из программы");


                Console.Write("\nВыберите команду: ");
                string comand = Console.ReadLine();
                Console.WriteLine();

                switch (comand)
                {
                    case "1":
                        // Отображение всех пользователей
                        _userConsoleUI.DisplayAllUsers();
                        break;

                    case "2":
                        // Удаление пользователя по ID
                        Console.Write("Введите ID пользователя для удаления: ");
                        string inputGuid = Console.ReadLine();
                        if (Guid.TryParse(inputGuid, out Guid userGuid))
                        {
                            _userConsoleUI.RemoveUserById(userGuid);
                        }
                        else
                        {
                            Console.WriteLine("Неверный формат ID");
                        }
                        break;

                    case "3":
                        // Обновление пользователя по ID
                        Console.Write("Введите ID пользователя для обновления: ");
                        string updateGuidInput = Console.ReadLine();
                        if (Guid.TryParse(updateGuidInput, out Guid updateUserGuid))
                        {
                            _userConsoleUI.UpdateUserById(updateUserGuid);
                        }
                        else
                        {
                            Console.WriteLine("Неверный формат ID");
                        }
                        break;

                    case "4":
                        // Поиск пользователя по ID
                        Console.Write("Введите ID пользователя для поиска: ");
                        string findGuidInput = Console.ReadLine();
                        if (Guid.TryParse(findGuidInput, out Guid findUserGuid))
                        {
                            _userConsoleUI.FindUserById(findUserGuid);
                        }
                        else
                        {
                            Console.WriteLine("Неверный формат ID");
                        }
                        break;

                    case "5":
                        // Отображение всех групп
                        _groupConsoleUI.DisplayAllGroups();
                        break;

                    case "6":
                        // Добавление новой группы
                        Console.Write("Введите название новой группы: ");
                        string newGroupName = Console.ReadLine();
                        _groupConsoleUI.AddGroup(newGroupName);
                        break;

                    case "7":
                        // Удаление группы
                        Console.Write("Введите ID группы  для удаления: ");
                        string groupIdForDelete = Console.ReadLine();
                        _groupConsoleUI.RemoveGroup(groupIdForDelete);
                        break;

                    case "8":
                        // Изменение названия группы
                        Console.Write("Введите ID группы для изменения: ");
                        if (int.TryParse(Console.ReadLine(), out int groupId))
                        {
                            Console.Write("Введите новое название группы: ");
                            string newName = Console.ReadLine();
                            _groupConsoleUI.UpdateGroupName(groupId, newName);
                        }
                        else
                        {
                            Console.WriteLine("Неверный формат ID группы");
                        }
                        break;

                    case "9":
                        // Поиск группы
                        Console.Write("Введите ID группы для поиска : ");
                        if (int.TryParse(Console.ReadLine(), out int IdGroup))
                        {
                            _groupConsoleUI.FindGroupById(IdGroup);
                        }
                        break;

                    case "10":
                        // Генерация посещаемости на день
                        Console.Write("Введите номер первого занятия: ");
                        int firstLesson = int.Parse(Console.ReadLine());
                        Console.Write("Введите номер последнего занятия: ");
                        int lastLesson = int.Parse(Console.ReadLine());
                        Console.Write("Введите ID группы: ");
                        int groupIdForPresence = int.Parse(Console.ReadLine());

                        _presenceConsoleUI.GeneratePresenceForDay(DateTime.Now, groupIdForPresence, firstLesson, lastLesson);
                        Console.WriteLine("Посещаемость на день сгенерирована.");
                        break;

                    case "11":
                        // Генерация посещаемости на неделю
                        Console.Write("Введите номер первого занятия: ");
                        int firstLessonForWeek = int.Parse(Console.ReadLine());
                        Console.Write("Введите номер последнего занятия: ");
                        int lastLessonForWeek = int.Parse(Console.ReadLine());
                        Console.Write("Введите ID группы: ");
                        int groupIdForWeekPresence = int.Parse(Console.ReadLine());

                        _presenceConsoleUI.GeneratePresenceForWeek(DateTime.Now, groupIdForWeekPresence, firstLessonForWeek, lastLessonForWeek);
                        Console.WriteLine("Посещаемость на неделю сгенерирована.");
                        break;

                    case "12":
                        // Отображение посещаемости
                        Console.Write("Введите дату (гггг-мм-дд): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите ID группы: ");
                        int groupForPresenceView = int.Parse(Console.ReadLine());

                        _presenceConsoleUI.DisplayPresence(date, groupForPresenceView);
                        break;

                    case "13":
                        // Отметить пользователя как отсутствующего
                        Console.Write("Введите ID пользователя: ");
                        userGuid = Guid.Parse(Console.ReadLine());
                        Console.Write("Введите номер первого занятия: ");
                        int firstAbsLesson = int.Parse(Console.ReadLine());
                        Console.Write("Введите номер последнего занятия: ");
                        int lastAbsLesson = int.Parse(Console.ReadLine());
                        Console.Write("Введите ID группы: ");
                        int absGroupId = int.Parse(Console.ReadLine());

                        _presenceConsoleUI.MarkUserAbsent(DateTime.Now, absGroupId, userGuid, firstAbsLesson, lastAbsLesson);
                        Console.WriteLine("Пользователь отмечен как отсутствующий.");
                        break;

                    case "14":
                        Console.Write("Введите ID группы: ");
                        int groupIdForAllPresence = int.Parse(Console.ReadLine());
                        _presenceConsoleUI.DisplayAllPresenceByGroup(groupIdForAllPresence);
                        break;


                    case "0":
                        Console.WriteLine("Выход...");
                        return;

                    default:
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}