using Demo.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.LocalData
{
    public static class LocalStaticData
    {
        public static List<GroupLocalEntity> groups => new List<GroupLocalEntity>

        {
            new GroupLocalEntity{ Id = 1, Name = "ИП1-21" },
            new GroupLocalEntity{ Id = 2, Name = "ИП1-22" },
            new GroupLocalEntity{ Id = 3, Name = "ИП1-23" },
        };

        public static List<UserLocalEnity> users => new List<UserLocalEnity>
        {
            new UserLocalEnity{Guid=Guid.Parse("e6b9964d-ea9f-420a-84b9-af9633bbfab9"), FIO = "RandomFio", GroupID = 1 },
            new UserLocalEnity{Guid=Guid.Parse("8388d931-5bef-41be-a152-78f1aca980ed"), FIO = "RandomFio1", GroupID = 2 },
            new UserLocalEnity{Guid=Guid.Parse("ed174548-49ed-4503-a902-c970cbf27173"), FIO = "RandomFio2", GroupID = 3 },
            new UserLocalEnity{Guid=Guid.Parse("614c0a23-5bd5-43ae-b48e-d5750afbc282"), FIO = "RandomFio3", GroupID = 1 },
            new UserLocalEnity{Guid=Guid.Parse("efcc1473-c116-4244-b3f7-f2341a5c3003"), FIO = "RandomFio4", GroupID = 2 },
            new UserLocalEnity{Guid=Guid.Parse("60640fb3-ace2-4cad-81d5-a0a58bc2dbbd"), FIO = "RandomFio5", GroupID = 3 },
        };

        public static List<PresenceLocalEntity> presences => new List<PresenceLocalEntity>
        {

        };
    }
}
