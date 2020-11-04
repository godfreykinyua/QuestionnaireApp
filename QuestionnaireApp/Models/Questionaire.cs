using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuestionnaireApp.Models
{

    public class Root
    {
        
        public Questionaire questionaire { get; set; }
        public List<Questionaire> list { get; set; }


    }






    public class Questionaire
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Questionaire Name")]
        public String Questionaire_Description { get; set; }
        public string Answer { get; set; }
        public String createdBy { get; set; }
        public DateTime dateCreated { get; set; }


    }

    public class QuestionaireEntries
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Questionaire_Id { get; set; }
        public String Questionaire_Description { get; set; }
        public string Answer { get; set; }
        public Boolean status { get; set; }
        public String user_id { get; set; }
        public DateTime dateCreated { get; set; }


    }


   

}