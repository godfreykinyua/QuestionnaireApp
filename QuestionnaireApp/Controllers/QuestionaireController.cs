using QuestionnaireApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QuestionnaireApp.ViewModel;

namespace QuestionnaireApp.Controllers
{
    public class QuestionaireController : Controller
    {
        QuestionaireDBContext context = new QuestionaireDBContext();
        // GET: Questionaire
        [HttpGet]
        public ActionResult Index()
        {
            //fetch questionaire
            Root root = new Root();
            List<Questionaire> list = new List<Questionaire>();
            var questionare = context.questionaires.ToList();
            foreach(var item in questionare)
            {
                Questionaire model = new Questionaire();
                model.ID = item.ID;
                model.Questionaire_Description = item.Questionaire_Description;
                list.Add(model);
            }
            root.list = list;

            return View(root);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Root model)
        {
            // Create new Questionaire
            if(ModelState.IsValid)
            {
                var Questionaire = context.questionaires.SingleOrDefault(x => x.Questionaire_Description == model.questionaire.Questionaire_Description);

                if(Questionaire == null)
                {
                    Questionaire questionaire = new Questionaire();
                    questionaire.Questionaire_Description = model.questionaire.Questionaire_Description;
                    questionaire.createdBy = User.Identity.Name;
                    questionaire.dateCreated = DateTime.Now;
                    context.questionaires.Add(questionaire);
                    context.SaveChanges();
                    TempData["success"] = "Questionaire successfully created";
                    ModelState.Clear();
                    return RedirectToAction("Index", "Questionaire");

                }
                else
                {
                    TempData["error"] = "Questionaire already exist";
                }

            }

            return View(model);
        }


        [HttpGet]
        public ActionResult Edit(int ? id)
        {
            // fetch   questionaire to delete  by admin 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
           
                var query = context.questionaires.Find(id);
                if(query == null)
                {
                    return HttpNotFound();

                }
           

            return View(query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Questionaire model)
        {
            // update questionaire  by admin 
            if (ModelState.IsValid)
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                TempData["success"] = "Questionaire successfully updated";

                return RedirectToAction("Index", "Questionaire");

            }


            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            // fetch   questionaire after  by admin 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var query = context.questionaires.Find(id);
            if (query == null)
            {
                return HttpNotFound();

            }


            return View(query);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // delete   questionaire after  by admin 
            Questionaire questionaire = context.questionaires.Find(id);
            context.questionaires.Remove(questionaire);
            context.SaveChanges();
            return RedirectToAction("Index", "Questionaire");
        }
        public ActionResult ViewQuestionaire()
        {
            // fetch   questionaire after  by admin 
            Root root = new Root();
            List<Questionaire> list = new List<Questionaire>();
            var query = context.questionaires.ToList();

            foreach(var item in query)
            {
                Questionaire model = new Questionaire();
                model.ID = item.ID;
                model.Questionaire_Description = item.Questionaire_Description;
                list.Add(model);

            }
            root.list = list;

            return View(root);
        }


        public ActionResult MyQuestionaire()
        {
            // fetch user questionaire entries
            // var currentUser=   User.Identity.GetUserId();
            var currentUser = User.Identity.Name;
            List<QuestionaireEntries> list = new List<QuestionaireEntries>();
            var query = context.questionareEntries.Where(x=>x.user_id== currentUser).ToList();

            foreach (var item in query)
            {
                QuestionaireEntries model = new QuestionaireEntries();
                model.ID = item.ID;
                model.Questionaire_Description = item.Questionaire_Description;
                model.Answer = item.Answer;
                list.Add(model);

            }
           

            return View(list);
        }
       

        public ActionResult CreateQuestionaire(Root model)
        {
//create survey by user
            if(model.list != null)
            {
                for(int i= 0; i<model.list.Count;i++)
                {

                    


                    QuestionaireEntries q = new QuestionaireEntries();
                    q.Questionaire_Id = model.list[i].ID;
                    q.Questionaire_Description = model.list[i].Questionaire_Description;
                    q.Answer = model.list[i].Answer;
                    q.user_id = User.Identity.Name;
                    q.dateCreated = DateTime.Now;

                    var query = context.questionareEntries.SingleOrDefault(x=>x.Questionaire_Description == q.Questionaire_Description);
                    if(query==null)
                    {
                        context.questionareEntries.Add(q);
                        context.SaveChanges();
                    }
                    else
                    {
                        TempData["error"] = "Questionaire already exists in the system";

                    }

                    

                    

                }
                TempData["success"] = "Questionaire successfully submitted";
            }

            return RedirectToAction("ViewQuestionaire", "Questionaire");
        }


        public ActionResult ViewSurvey()
        {
            // fetch all user questionaire entries by admin
            ApplicationDbContext db = new ApplicationDbContext();
            var row = context.questionareEntries.ToList();
            List<SurveyUsers> list = new List<SurveyUsers>();
            foreach(var item in row)
            {
                SurveyUsers survey = new SurveyUsers();
                survey.Id = item.ID;
                survey.Description = item.Questionaire_Description;
                survey.answer = item.Answer;
                survey.userName = item.user_id;

                list.Add(survey);

            }

            return View(list);
        }


    }
}