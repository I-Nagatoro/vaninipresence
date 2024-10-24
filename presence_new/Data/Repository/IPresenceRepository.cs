using Demo.domain.Models;
using System;
using System.Collections.Generic;

namespace Demo.Data.Repository
{
    public interface IPresenceRepository
    {
        void SavePresence(List<PresenceLocalEntity> presences);
        List<PresenceLocalEntity> GetPresenceByGroup(int groupId);
        List<PresenceLocalEntity> GetPresenceByGroupAndDate(int groupId, DateTime date);
        void MarkUserAsAbsent(Guid userGuid, int groupId, int firstLessonNumber, int lastLessonNumber);
        void AddPresence(PresenceLocalEntity presence);
    }
}
