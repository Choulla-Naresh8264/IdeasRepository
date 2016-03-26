using IdeasRepository.BL.Interfaces;
using IdeasRepository.DAL.Entities;
using IdeasRepository.Web.Models.Records;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IdeasRepository.Web.Controllers
{
    /// <summary>
    /// Describes the necessary actions to manage information about records.
    /// User must be authorized in the system to get access to this section.
    /// </summary>
    [Authorize]
    public class RecordController : Controller
    {
        /// <summary>
        /// Provides access to the records management logic.
        /// </summary>
        private IRecordsProvider _provider;

        public RecordController(IRecordsProvider provider)
        {
            _provider = provider;
        }

        public ActionResult List()
        {
            var records = _provider.GetAllRecords();
            var recordsViewModel = new List<RecordViewModel>();

            if (records != null)
            {
                foreach (var record in records)
                {
                    recordsViewModel.Add(new RecordViewModel
                    {
                        Id = record.Id,
                        Author = record.Author,
                        CreationDate = record.CreationDate.ToString(),
                        RecordType = record.RecordType.Name,
                        TextBody = record.TextBody,
                        IsDeleted = record.IsDeleted
                    });
                }
            }

            return View(recordsViewModel);
        }

        public ActionResult RecordTypes(string currentRecordTypeName)
        {
            var recordTypes = _provider.GetAllRecordTypes();
            var recordTypesViewModel = new List<RecordTypeViewModel>();

            if (recordTypes != null)
            {
                foreach (var record in recordTypes)
                {
                    recordTypesViewModel.Add(new RecordTypeViewModel
                    {
                        Id = record.Id,
                        Name = record.Name
                    });
                }
            }

            if (currentRecordTypeName != null)
            {
                ViewBag.CurrentRecordName = currentRecordTypeName;
            }

            return PartialView("_PartialRecordTypes", recordTypesViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RecordViewModel record)
        {
            if (ModelState.IsValid)
            {
                var recordDataModel = new Record
                {
                    Id = Guid.NewGuid().ToString(),
                    Author = HttpContext.User.Identity.Name,
                    CreationDate = DateTime.Now,
                    TextBody = record.TextBody,
                    RecordTypeId = record.RecordTypeId
                };

                _provider.AddRecord(recordDataModel);

                return RedirectToAction("List");
            }
            else
            {
                return View(record);
            }
        }

        public ActionResult Edit(string id)
        {
            var record = _provider.GetRecord(id);
            var recordViewModel = new RecordViewModel
            {
                Id = record.Id,
                Author = record.Author,
                CreationDate = record.CreationDate.ToString(),
                RecordType = record.RecordType.Name,
                TextBody = record.TextBody
            };

            return View(recordViewModel);
        }

        [HttpPost]
        public ActionResult Edit(RecordViewModel record)
        {
            if (ModelState.IsValid)
            {
                var recordDataModel = new Record
                {
                    Id = record.Id,
                    TextBody = record.TextBody,
                    RecordTypeId = record.RecordTypeId
                };

                _provider.UpdateRecord(recordDataModel);

                return RedirectToAction("List");
            }
            else
            {
                return View(record);
            }
        }

        [HttpGet]
        public ActionResult Remove(string id)
        {
            if (User.IsInRole("Administrator"))
            {
                var recordDataModel = new Record
                {
                    Id = id,
                };

                _provider.RemoveRecord(recordDataModel);
            }
            else
            {
                var recordDataModel = new Record
                {
                    Id = id,
                    IsDeleted = true
                };

                _provider.UpdateRemovedStatus(recordDataModel);
            }

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Restore(string id)
        {
            var recordDataModel = new Record
            {
                Id = id,
                IsDeleted = false
            };

            _provider.UpdateRemovedStatus(recordDataModel);

            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            _provider.Dispose();
            base.Dispose(disposing);
        }
    }
}
