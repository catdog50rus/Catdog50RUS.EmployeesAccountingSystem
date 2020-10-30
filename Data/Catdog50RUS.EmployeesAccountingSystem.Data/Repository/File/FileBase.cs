using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File
{
    public class FileBase
    {
        protected readonly string path = "";

        public FileBase(string fileName)
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory()).FullName;
            path = Path.Combine(directory, fileName);

        }
    }
}
