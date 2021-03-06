﻿// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.EfClassesSql;
using DataLayerEvents.EfCode;
using EfCoreSqlAndCosmos.HelperCode;
using GenericServices;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.BooksCommon;
using ServiceLayer.BooksSql;
using ServiceLayer.BooksSql.Dtos;
using ServiceLayer.BooksSqlWithEvents;
using ServiceLayer.BooksSqlWithEvents.Dtos;
using ServiceLayer.Logger;

namespace EfCoreSqlAndCosmos.Controllers
{
    public class SqlEventsController : BaseTraceController
    {
        public IActionResult Index (SqlSortFilterPageOptions options, [FromServices]ISqlEventsListBooksService service)
        {
            var output = service.SortFilterPage(options).ToList();
            SetupTraceInfo();
            return View(new SqlBookListCombinedDto(options, output));              
        }

        /// <summary>
        /// This provides the filter search dropdown content
        /// </summary>
        /// <param name="options"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetFilterSearchContent    
            (SqlSortFilterPageOptions options, [FromServices]IBookEventsFilterDropdownService service)         
        {

            var traceIdent = HttpContext.TraceIdentifier; 
            return Json(                            
                new TraceIndentGeneric<IEnumerable<DropdownTuple>>(
                traceIdent,
                service.GetFilterDropDownValues(  options.FilterBy)));            
        }

        //-------------------------------------------------------

        public IActionResult ChangePubDate(Guid id, [FromServices]ICrudServices<SqlEventsDbContext> service)
        {
            var dto = service.ReadSingle<ChangePubDateEventsDto>(id);
            if (!service.IsValid)
            {
                service.CopyErrorsToModelState(ModelState, dto);
            }
            SetupTraceInfo();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePubDate(ChangePubDateEventsDto dto, [FromServices]ICrudServicesAsync<SqlEventsDbContext> service)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await service.UpdateAndSaveAsync(dto);
            SetupTraceInfo();
            if (service.IsValid)
                return View("BookUpdated", service.Message);

            //Error state
            service.CopyErrorsToModelState(ModelState, dto);
            return View(dto);
        }

        public IActionResult AddPromotion(Guid id, [FromServices]ICrudServices<SqlEventsDbContext> service)
        {
            var dto = service.ReadSingle<AddRemovePromotionEventsDto>(id);
            if (!service.IsValid)
            {
                service.CopyErrorsToModelState(ModelState, dto);
            }
            SetupTraceInfo();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPromotion(AddRemovePromotionEventsDto dto, [FromServices]ICrudServices<SqlEventsDbContext> service)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            service.UpdateAndSave(dto, nameof(Book.AddPromotion));
            SetupTraceInfo();
            if (service.IsValid)
                return View("BookUpdated", service.Message);

            //Error state
            service.CopyErrorsToModelState(ModelState, dto);
            return View(dto);
        }

        public IActionResult RemovePromotion(Guid id, [FromServices]ICrudServices<SqlEventsDbContext> service)
        {
            var dto = service.ReadSingle<AddRemovePromotionEventsDto>(id);
            if (!service.IsValid)
            {
                service.CopyErrorsToModelState(ModelState, dto);
            }
            SetupTraceInfo();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemovePromotion(AddRemovePromotionEventsDto dto, [FromServices]ICrudServices<SqlEventsDbContext> service)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            service.UpdateAndSave(dto, nameof(Book.RemovePromotion));
            SetupTraceInfo();
            if (service.IsValid)
                return View("BookUpdated", service.Message);

            //Error state
            service.CopyErrorsToModelState(ModelState, dto);
            return View(dto);
        }


        public IActionResult AddBookReview(Guid id, [FromServices]ICrudServices<SqlEventsDbContext> service)
        {
            var dto = service.ReadSingle<AddReviewEventsDto>(id);
            if (!service.IsValid)
            {
                service.CopyErrorsToModelState(ModelState, dto);
            }
            SetupTraceInfo();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBookReview(AddReviewEventsDto dto, [FromServices]ICrudServices<SqlEventsDbContext> service)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            service.UpdateAndSave(dto);
            SetupTraceInfo();
            if (service.IsValid)
                return View("BookUpdated", service.Message);

            //Error state
            service.CopyErrorsToModelState(ModelState, dto);
            return View(dto);
        }

    }
}
