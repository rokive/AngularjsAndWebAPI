using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VModel;

namespace IService
{
    public interface IBookIssueService
    {
        void Save(BookIssueMain bookIssueMain);
    }
}
