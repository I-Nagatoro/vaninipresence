using Demo.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Repository
{
    public interface IGroupRepository
    {
        List<GroupLocalEntity> GetAllGroup();
        bool RemoveGroupById(int groupID);
        bool UpdateGroupById(int groupID, GroupLocalEntity updatedGroup);
        GroupLocalEntity GetGroupById(int groupID);
        bool AddGroup(GroupLocalEntity newGroup);
    }
}