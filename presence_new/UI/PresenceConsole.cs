using Demo.domain.Models;
using Demo.Domain.UseCase;
using System;
using System.Collections.Generic;

namespace Demo.UI
{
    public class PresenceConsole
    {
        private readonly UseCaseGeneratePresence _presenceUseCase;

        public PresenceConsole(UseCaseGeneratePresence presenceUseCase)
        {
            _presenceUseCase = presenceUseCase;
        }

        // Метод для генерации посещаемости на день
        public void GeneratePresenceForDay(DateTime date, int groupId, int firstLesson, int lastLesson)
        {
            try
            {
                _presenceUseCase.GeneratePresenceDaily(firstLesson, lastLesson, groupId, date);
                Console.WriteLine("Посещаемость на день успешно сгенерирована.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при генерации посещаемости: {ex.Message}");
            }
        }

        // Метод для генерации посещаемости на неделю
        public void GeneratePresenceForWeek(DateTime date, int groupId, int firstLesson, int lastLesson)
        {
            try
            {
                _presenceUseCase.GenerateWeeklyPresence(firstLesson, lastLesson, groupId, date);
                Console.WriteLine("Посещаемость на неделю успешно сгенерирована.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при генерации посещаемости: {ex.Message}");
            }
        }

        // Метод для отображения посещаемости на конкретную дату и группу
        public void DisplayPresence(DateTime date, int groupId)
        {
            try
            {
                List<PresenceLocalEntity> presences = _presenceUseCase.GetPresenceByDateAndGroup(groupId, date);

                if (presences == null || presences.Count == 0)
                {
                    Console.WriteLine("Нет данных о посещаемости на выбранную дату.");
                    return;
                }

                Console.WriteLine($"\n                Посещаемость на {date:dd.MM.yyyy}                ");
                Console.WriteLine("-----------------------------------------------------");

                // Сохраняем номер занятия для сравнения
                int previousLessonNumber = -1;

                foreach (var presence in presences)
                {
                    // Проверяем, изменился ли номер занятия
                    if (previousLessonNumber != presence.LessonNumber)
                    {
                        Console.WriteLine("-----------------------------------------------------");
                        Console.WriteLine($"                   Занятие: {presence.LessonNumber}                   ");
                        Console.WriteLine("-----------------------------------------------------");
                        previousLessonNumber = presence.LessonNumber;
                    }

                    // Форматируем статус присутствия
                    string status = presence.IsAttedance ? "Присутствует" : "Отсутствует";
                    Console.WriteLine($"Пользователь (ID: {presence.UserGuid}) - Статус: {status}");
                }

                Console.WriteLine("-----------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отображении посещаемости: {ex.Message}");
            }
        }


        public void MarkUserAbsent(DateTime date, int groupId, Guid userGuid, int firstLesson, int lastLesson)
        {
            _presenceUseCase.MarkUserAbsentForLessons(userGuid, groupId, firstLesson, lastLesson, date);
        }




        public void DisplayAllPresenceByGroup(int groupId)
        {
            try
            {
                var presences = _presenceUseCase.GetAllPresenceByGroup(groupId);

                if (presences == null || !presences.Any())
                {
                    Console.WriteLine($"Нет данных о посещаемости для группы с ID: {groupId}.");
                    return;
                }

                // Группируем записи посещаемости по дате
                var groupedPresences = presences.GroupBy(p => p.Date);

                foreach (var group in groupedPresences)
                {
                    Console.WriteLine("===================================================");
                    Console.WriteLine($"                Дата: {group.Key:dd.MM.yyyy}                 ");
                    Console.WriteLine("===================================================");

                    // Сохраняем номер занятия для сравнения
                    int previousLessonNumber = -1;

                    foreach (var presence in group)
                    {
                        // Проверяем, изменился ли номер занятия
                        if (previousLessonNumber != presence.LessonNumber)
                        {
                            Console.WriteLine("---------------------------------------------------");
                            Console.WriteLine($"              Занятие: {presence.LessonNumber}               ");
                            Console.WriteLine("---------------------------------------------------");
                            previousLessonNumber = presence.LessonNumber;
                        }

                        // Форматируем статус присутствия
                        string status = presence.IsAttedance ? "✅ Присутствует" : "❌ Отсутствует";
                        Console.WriteLine($"Пользователь (ID: {presence.UserGuid}) - Статус: {status}");
                    }

                    Console.WriteLine("---------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отображении посещаемости: {ex.Message}");
            }
        }



    }
}
