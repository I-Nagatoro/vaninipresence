using Demo.Data.Repository;
using Demo.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.UseCase
{
    public class UseCaseGeneratePresence
    {
        public readonly UserRepositoryImpl _userRepository;
        public readonly IPresenceRepository _presenceRepository;

        public UseCaseGeneratePresence(UserRepositoryImpl userRepository, IPresenceRepository presenceRepository)
        {
            _userRepository = userRepository;
            _presenceRepository = presenceRepository;
        }

        public List<PresenceLocalEntity> GetPresenceByDateAndGroup(int groupId, DateTime date)
        {
            return _presenceRepository.GetPresenceByGroupAndDate(groupId, date);
        }


        public void GeneratePresenceDaily(int firstLesson, int lastLesson, int groupId, DateTime currentDate)
        {
            var users = _userRepository.GetAllUsers.Where(u => u.GroupID == groupId).ToList();
            List<PresenceLocalEntity> presences = new List<PresenceLocalEntity>();
            for (int lessonNumber = firstLesson; lessonNumber <= lastLesson; lessonNumber++)
            {
                foreach (var user in users)
                {
                    presences.Add(new PresenceLocalEntity
                    {
                        UserGuid = user.Guid,
                        GroupId = user.GroupID,
                        Date = currentDate,
                        LessonNumber = lessonNumber,
                        IsAttedance = true
                    });
                }
                _presenceRepository.SavePresence(presences);
            }
        }

        public void GenerateWeeklyPresence(int firstLesson, int lastLesson, int groupId, DateTime startTime)
        {
            for (int i = 0; i < 7; i++)
            {
                DateTime currentTime = startTime.AddDays(i);
                GeneratePresenceDaily(firstLesson, lastLesson, groupId, currentTime);
            }
        }



        // Отметить пользователя как отсутствующего на диапазоне занятий
        public void MarkUserAbsentForLessons(Guid userGuid, int groupId, int firstLesson, int lastLesson, DateTime date)
        {
            var presences = _presenceRepository.GetPresenceByGroupAndDate(groupId, date);
            foreach (var presence in presences.Where(p => p.UserGuid == userGuid && p.LessonNumber >= firstLesson && p.LessonNumber <= lastLesson))
            {
                presence.IsAttedance = false;
            }
            _presenceRepository.SavePresence(presences);
        }



        public List<PresenceLocalEntity> GetAllPresenceByGroup(int groupId)
        {
            return _presenceRepository.GetPresenceByGroup(groupId);
        }




    }
}
