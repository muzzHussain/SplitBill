using AutoMapper;
using BusinessLayer.Business.BusinessInterface;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Business.BusinessImplementation
{
   
    public class GroupBusinessLogic : IGroupBusinessLogic<GroupRequestDTO, CreateGroupDTO,GroupDetailsDTO>
    {

        private readonly IMapper _mapper;
        private readonly IDataAccessLayer _dataAccessLayer;
        private readonly UserManager<IdentityUser> _userManager;
        public GroupBusinessLogic(IMapper mapper, IDataAccessLayer dataAccessLayer, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _dataAccessLayer = dataAccessLayer;
            _userManager = userManager;
        }


        public async Task<Guid> CreateGroup(string emailId, GroupRequestDTO group)
        {
            var resp = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Email == emailId);
            if (resp == null)
            {
                throw new ArgumentException("Email Id doesn't exist!");
            }
            var groupData = new Group
            {
                Id = new Guid(),
                Title = group.Title,
                CreationDate = DateTime.Now
            };

            await _dataAccessLayer.Write().GroupWrite().AddTask(groupData);

            var usersList = group.UsersList;
            var usersGroup = new UsersGroup()
            {
                UserId = resp.Id,
                GroupId = groupData.Id
            };
            await _dataAccessLayer.Write().UsersGroupWrite().AddTask(usersGroup);
            foreach (var users in usersList)
            {
                var user = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Email == users.EmailId);
                if (user == null)
                {
                    throw new ArgumentException("User is not exist");
                }
                usersGroup = new UsersGroup
                {
                    UserId = user.Id,
                    GroupId = groupData.Id
                };
                await _dataAccessLayer.Write().UsersGroupWrite().AddTask(usersGroup);
            }
            await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            return groupData.Id;
        }

        
        public async Task<IEnumerable<CreateGroupDTO>> FetchAllGroupsById(string emailId)
        {
            User userDetails = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Email == emailId);

            if (userDetails == null)
            {
                throw new ArgumentException("Email Id is not exist!");
            }
            // var id = new Guid(userDetails.Id);
            IList<GroupRequestDTO> groupRequestDTO = new List<GroupRequestDTO>();
            IEnumerable<UsersGroup> userGroups = await _dataAccessLayer.Read().UsersGroupRead().GetAllRecordsByConditionTask((x) => x.UserId == userDetails.Id);//

            IList<Group> groupDetails = new List<Group>();

            List<GroupRequestDTO> groupRequestDTOs = new List<GroupRequestDTO>();

            List<CreateGroupDTO> createGroupRequestDTOs = new List<CreateGroupDTO>();


            foreach (var groups in userGroups)
            {
                Group group = await _dataAccessLayer.Read().GroupRead().GetByConditionTask((x) => x.Id == groups.GroupId);
                if (group != null)
                {
                    List<Users> userEmails = await GetUsersEmailInGroups(group.Id);
                    CreateGroupDTO createGroupDTO = new CreateGroupDTO
                    {
                        Id=group.Id,
                        Title = group.Title,
                        UsersList = userEmails
                    };
                    createGroupRequestDTOs.Add(createGroupDTO);
                }
            }
            return createGroupRequestDTOs;
        }

        public async Task<List<Users>> GetUsersByGroupId(Guid groupId)
        {
            IEnumerable<UsersGroup> groupUsers = await _dataAccessLayer.Read().UsersGroupRead().GetAllRecordsByConditionTask((x) => x.GroupId == groupId);

            List<Users> usersInGroup = new List<Users>();
            foreach (var groupUser in groupUsers)
            {
                User userDetails = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Id == groupUser.UserId);
                if (userDetails != null)
                {
                    Users users = new Users()
                    {
                        Name=userDetails.UserName,
                        EmailId = userDetails.Email
                    };
                    usersInGroup.Add(users);
                }
            }

            return usersInGroup;
        }

        public async Task<IEnumerable<GroupDetailsDTO>> GetDetailsGroupById(Guid id)
        {
            // Fetch the group details by ID
            Group group = await _dataAccessLayer.Read().GroupRead().GetByConditionTask((x) => x.Id == id);

            if (group == null)
            {
                // You can throw an exception or handle the case when the group ID does not exist
                throw new ArgumentException("Group ID does not exist!");
            }

            // Get the group members using the group ID
            List<Users> groupMembers = await GetUsersByGroupId(group.Id);

            // Create the DTO object to hold the group details and members
            GroupDetailsDTO groupDetailsDTO = new GroupDetailsDTO
            {
                Id = group.Id,
                GroupName = group.Title,
                CreatedDate = group.CreationDate,
                GroupMembers = groupMembers
            };

            return Enumerable.Repeat(groupDetailsDTO, 1);
        }
       
        public async Task<bool> RemoveUserFromGroup(Guid groupId, string userEmailId)
        {

            User userDetails = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Email == userEmailId);
            if (userDetails == null)
            {
                throw new ArgumentException("User doesn't exist!");
            }

            IEnumerable<UsersGroup> ListOfUsersGroupDetails = await _dataAccessLayer.Read().UsersGroupRead().GetAllRecordsByConditionTask((x) => x.UserId == userDetails.Id);

            var details = ListOfUsersGroupDetails.FirstOrDefault((x) => x.GroupId == groupId);

            if (userDetails.Id != details.UserId)
            {
                throw new ArgumentException("Current user is not the member of this group");
            }

            bool resp = await _dataAccessLayer.Write().UsersGroupWrite().RemoveTask(details);
            resp &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            return resp;

        }

        public async Task<bool> LeaveGroup(Guid groupId, string emailid)
        {
            User userDetail = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Email == emailid);
            if (userDetail == null)
            {
                throw new ArgumentException("Email Id is not exist!");
            }

            IEnumerable<UsersGroup> ListOfUsersInGroup = await _dataAccessLayer.Read().UsersGroupRead().GetAllRecordsByConditionTask((x) => x.UserId == userDetail.Id);
            var details = ListOfUsersInGroup.FirstOrDefault((x) => x.GroupId == groupId);

            var userCount = await CountUserInGroup(groupId);

            if (userCount<=1)
            {
                throw new ArgumentException("Can't leave the group atleast one member should be in the group");
            }

            bool resp = await _dataAccessLayer.Write().UsersGroupWrite().RemoveTask(details);
            resp &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            return resp;
        }

        private async Task<int> CountUserInGroup(Guid groupId)
        {
            IEnumerable<UsersGroup> listOfUsers = await _dataAccessLayer.Read().UsersGroupRead().GetAllRecordsByConditionTask((x) => x.GroupId == groupId);
            var userCount = listOfUsers.Count();
            return userCount;
        }

        private async Task<List<Users>> GetUsersEmailInGroups(Guid groupId)

                {
                    var usersInGroup = await _dataAccessLayer.Read().UsersGroupRead().GetAllRecordsByConditionTask((x) => x.GroupId == groupId);
                    List<Users> userEmailList = new List<Users>();
                    foreach (var userGroup in usersInGroup)
                    {
                        User user = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Id == userGroup.UserId);
                        if (user != null)
                        {
                            Users users = new Users
                            {
                                EmailId = user.Email
                            };
                            userEmailList.Add(users);
                        }
                    }
                    return userEmailList;
                }
              
        public async Task<bool> DeletegroupById(Guid groupId)
            {

                Group grp = await _dataAccessLayer.Read().GroupRead().GetByConditionTask((x) => x.Id == groupId);
                if (grp != null)
                {
                    Group group = new Group
                    {
                        Id = grp.Id,
                        Title = grp.Title,
                        CreationDate = grp.CreationDate,

                    };
                    var result = await _dataAccessLayer.Write().GroupWrite().RemoveTask(grp);
                    await _dataAccessLayer.Write().CommitWrite().SaveChanges();
                    return result;
                }
                return false;

            }

        public async Task<bool> AddUserIntoExistingGroup(Guid groupId, string userEmailId)
        {
            User user = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Email == userEmailId);
            if (user == null)
            {
                throw new ArgumentException("User does not exist!");
            }

            UsersGroup usersGroup = await _dataAccessLayer.Read().UsersGroupRead().GetByConditionTask((x) => x.GroupId == groupId);
            if (usersGroup == null)
            {
                throw new ArgumentException("Group does not exist");
            }

            //UsersGroup resp = await _dataAccessLayer.Read().UsersGroupRead().GetByConditionTask((x) => x.UserId == new Guid(user.Id) && x.GroupId == groupId);

            UsersGroup newUsersGroupEntry = new UsersGroup
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                GroupId = groupId
            };
            bool resp = await _dataAccessLayer.Write().UsersGroupWrite().AddTask(newUsersGroupEntry);
            resp &= await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            return resp;
        }
    }
}
