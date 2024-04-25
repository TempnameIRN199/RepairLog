using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using RepairLogServer.Migrations;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Markup;


namespace RepairLog_Server.Database
{
    public class NintendoContext : DbContext
    {
        public NintendoContext() : base("NintendoContext")
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<NintendoContext, Configuration>());
            Database.Log = sql =>
            {
                using (StreamWriter file = File.AppendText(@"Logs\log.json"))
                {
                    JsonSerializer serializer = new JsonSerializer
                    {
                        Formatting = Formatting.Indented
                    };
                    serializer.Serialize(file, sql);
                }
            };

        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Breakdown> Breakdowns { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Non_repairable> Non_repairables { get; set; }
        public DbSet<Repaired> Repaireds { get; set; }
    }
}
