﻿using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * All new changes are made by:
 * @Author: s204197
 */
 
namespace RabbitMQLibrary.Messages.Repository
{
    public class DeleteRepositoryFromOrganizationMessage : IQueueMessage
    {
        public Guid MessageId { get; set; }
        public Guid ProcessId { get; set; }
        public TimeSpan TimeToLive { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid RepositoryId { get; set; }
    }
}
