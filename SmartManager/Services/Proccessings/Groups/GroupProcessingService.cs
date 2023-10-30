﻿//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System.Linq;
using SmartManager.Models.Groups;
using SmartManager.Services.Foundations.Groups;
using System;
using System.Threading.Tasks;
using SmartManager.Models.Applicants;

namespace SmartManager.Services.Proccessings.Groups
{
    public class GroupProcessingService : IGroupProcessingService
    {
        private readonly IGroupService groupService;

        public GroupProcessingService(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public async ValueTask<Group> EnsureGroupExistsByName(string groupName)
        {
            var maybeGroup = RetriveGroupByName(groupName);

            return maybeGroup is null
                ? await AddGroupAsync(groupName)
                : maybeGroup;
        }

        public async ValueTask<Group> AddGroupAsync(Group group) =>
           await this.groupService.AddGroupAsync(group);

        public async ValueTask<Group> RetrieveGroupByIdAsync(Guid groupid) =>
            await this.groupService.RetrieveGroupByIdAsync(groupid);

        public IQueryable<Group> RetrieveAllGroups() =>
            this.groupService.RetrieveAllGroups();

        public async ValueTask<Group> ModifyGroupAsync(Group group) =>
            await this.groupService.ModifyGroupAsync(group);

        public async ValueTask<Group> RemoveGroupAsync(Guid groupid) =>
            await this.groupService.RemoveGroupAsync(groupid);

        private Group RetriveGroupByName(string groupName)
        {
            var allGroups = groupService.RetrieveAllGroups();

            return allGroups.FirstOrDefault(storageGroup =>
                storageGroup.GroupName == groupName);
        }

        private async ValueTask<Group> AddGroupAsync(string groupName)
        {
            var group = new Group
            {
                GroupId = Guid.NewGuid(),
                GroupName = groupName
            };

            return await groupService.AddGroupAsync(group);
        }
    }
}