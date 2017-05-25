using IService;
using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BookIssueService : IBookIssueService,ICommon
    {

        IGenericRepository<BookIssueMain> _repoBookissueMain = new GenericRepository<BookIssueMain>();
        IGenericRepository<BookIssueDetail> _repoBookissueDetails = new GenericRepository<BookIssueDetail>();
        public void Save(BookIssueMain bookIssueMain)
        {
                _repoBookissueMain.Insert(bookIssueMain);   
        }

        public void SaveChange()
        {
            _repoBookissueMain.Save();
        }

        public void RollBack()
        {
            _repoBookissueMain.RollBack();
        }

    }
}
