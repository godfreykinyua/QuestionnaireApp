using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuestionnaireApp.Models
{
    public class QuestionaireDBContext : DbContext
    {
        public QuestionaireDBContext()
           : base("name = DefaultConnection1")
        {
        }
        public DbSet<Questionaire> questionaires { get; set; }
        public DbSet<QuestionaireEntries> questionareEntries { get; set; }

    }
}