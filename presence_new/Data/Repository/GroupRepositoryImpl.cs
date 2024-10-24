using Demo.Data.Exceptions;
using Demo.Data.LocalData;
using Demo.domain.Models;
using System.Collections.Generic;
using System.Linq;

public class GroupRepositoryImpl
{
    private List<GroupLocalEntity> _groups = LocalStaticData.groups;


    public GroupLocalEntity? GetGroupById(int groupId)
    {
        foreach (var group in _groups)
        {
            if (group.Id == groupId)
            {
                return group;
            }
        }
        return null;
    }




    // Метод для получения всех групп
    public List<GroupLocalEntity> GetAllGroups() => _groups;

    // Метод для добавления новой группы
    public void AddGroup(GroupLocalEntity group)
    {
        group.Id = _groups.Any() ? _groups.Max(g => g.Id) + 1 : 1;
        _groups.Add(group);
    }

    // Метод для обновления существующей группы
    public void UpdateGroup(GroupLocalEntity group)
    {
        var existingGroup = GetGroupById(group.Id);
        if (existingGroup == null) throw new GroupNotFoundException(group.Id);
    }

    public void RemoveGroupById(GroupLocalEntity group)
    {
        var existingGroup = GetGroupById(group.Id);
        if (existingGroup == null) throw new GroupNotFoundException(group.Id);
        if (_groups.Contains(existingGroup))
        {
            _groups.Remove(existingGroup);
        }
    }
}
