using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using PlugPool.ViewModels.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlugPool.Controllers
{
    public class JobController : Controller
    {
        private IJobDAL jobDAL;

        public JobController(IJobDAL jobDAL)
        {
            this.jobDAL = jobDAL;
        }

        public JobController() { }

        [HttpGet]
        public ActionResult Create()
        {
            CreateViewModel model = new CreateViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Job job = new Job
                {
                    name = model.name,
                    createDate = DateTime.Now
                };
                jobDAL.Create(job);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Index()
        {
            var jobs = jobDAL.FetchAll();
            IndexViewModel model = new IndexViewModel(jobs);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var job = jobDAL.FetchByID(id);
            EditViewModel model = new EditViewModel(job);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Job job = new Job
                {
                    jobID = model.jobID,
                    name = model.name,
                    updateDate = DateTime.Now
                };
                jobDAL.Update(job);
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}

