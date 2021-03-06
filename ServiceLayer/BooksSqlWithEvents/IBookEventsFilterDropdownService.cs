﻿// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Collections.Generic;
using ServiceLayer.BooksCommon;

namespace ServiceLayer.BooksSqlWithEvents
{
    public interface IBookEventsFilterDropdownService
    {
        IEnumerable<DropdownTuple> GetFilterDropDownValues(BooksFilterBy filterBy);
    }
}