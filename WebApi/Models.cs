﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;

namespace WebApi
{
    public class ReserveContext : DbContext
    {
        public ReserveContext(DbContextOptions<ReserveContext> options)
            : base(options)
        {
        }

        public DbSet<Flight_DTO> DTOFlights { get; set; }
    }
}