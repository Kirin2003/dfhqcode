﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Achieve.Repository.BASE;
using Student.Achieve.IRepository;
using Student.Achieve.Model.Models;

namespace Achieve.Repository
{
    public class StudentsRepository : BaseRepository<Students>, IStudentsRepository
    {
    }
}