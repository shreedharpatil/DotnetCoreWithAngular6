using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Repository.Models;

namespace Data.Repository.Interfaces
{
    public interface ISendMessage
    {
        Task<Tuple<IEnumerable<SendMessageResponse>, IEnumerable<string>>> Send(SendMessageDTO sendMessageDTO);
    }
}
