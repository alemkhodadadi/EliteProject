﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Domain.Models
{
    public class FileOnFileSystemModel : FileModel
    {
        public string FilePath { get; set; }
    }
}
