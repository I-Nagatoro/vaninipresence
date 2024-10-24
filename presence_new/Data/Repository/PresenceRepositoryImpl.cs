using Demo.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Data.Repository
{
    public class PresenceRepositoryImpl : IPresenceRepository
    {
        private readonly List<PresenceLocalEntity> _presences = new List<PresenceLocalEntity>();

        public void SavePresence(List<PresenceLocalEntity> presences)
        {
            foreach (var presence in presences)
            {
                var existingPresence = _presences.FirstOrDefault(p =>
                    p.Date == presence.Date &&
                    p.UserGuid == presence.UserGuid && // Используем UserGuid
                    p.LessonNumber == presence.LessonNumber);

                if (existingPresence == null)
                {
                    _presences.Add(presence);
                }
                else
                {
                    existingPresence.IsAttedance = presence.IsAttedance;
                }
            }
        }

        public void AddPresence(PresenceLocalEntity presence)
        {
            _presences.Add(presence);
        }

        public List<PresenceLocalEntity> GetPresenceByGroup(int groupId)
        {
            return _presences.Where(p => p.GroupId == groupId).ToList();
        }

        public List<PresenceLocalEntity> GetPresenceByGroupAndDate(int groupId, DateTime date)
        {
            return _presences.Where(p => p.GroupId == groupId && p.Date.Date == date.Date).ToList();
        }

        public void MarkUserAsAbsent(Guid userGuid, int groupId, int firstLessonNumber, int lastLessonNumber)
        {
            foreach (var lesson in Enumerable.Range(firstLessonNumber, lastLessonNumber - firstLessonNumber + 1))
            {
                var presence = _presences.FirstOrDefault(p => p.UserGuid == userGuid && p.GroupId == groupId && p.LessonNumber == lesson);
                if (presence != null)
                {
                    presence.IsAttedance = false; // Помечаем как отсутствующего
                }
            }
        }
    }
}
