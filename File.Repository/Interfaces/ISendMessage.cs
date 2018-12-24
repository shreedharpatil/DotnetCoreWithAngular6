using Common.Layer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace File.Repository.Interfaces
{
    public interface ISendMessage
    {
        Task<Tuple<IEnumerable<SendMessageResponse>, IEnumerable<string>>> Send(SendMessageDTO sendMessageDTO);
    }
}
