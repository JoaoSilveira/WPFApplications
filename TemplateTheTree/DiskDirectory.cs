using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTheTree
{
    public class DiskDirectory
    {
        DirectoryInfo dirInfo;

        public string Name => dirInfo.Name;

        public List<DiskDirectory> Subdirectories
        {
            get
            {
                var dirs = new List<DiskDirectory>();
                DirectoryInfo[] subdirs;

                try
                {
                    subdirs = dirInfo.GetDirectories();
                } catch { return dirs; }

                foreach (var subdir in subdirs)
                    dirs.Add(new DiskDirectory(subdir));

                return dirs;
            }
        }

        public DiskDirectory(DirectoryInfo dirInfo)
        {
            this.dirInfo = dirInfo;
        }
    }
}
