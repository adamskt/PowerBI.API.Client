﻿using Microsoft.PowerBI.Api.V2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace gbrueckl.PowerBI.API.PowerBIObjects
{
    public class PBIColumn : Column
    {
        #region Constructors
        public PBIColumn(string name, PBIDataType dataType)
        {
            Name = name;
            DataType = dataType.ToString();
        }
        #endregion
        #region Private Properties for Serialization
        [JsonProperty(PropertyName = "dataCategory", NullValueHandling = NullValueHandling.Ignore)]
        private string _dataCategoryString;

        [JsonProperty(PropertyName = "summarizeBy", NullValueHandling = NullValueHandling.Ignore)]
        private string _summarizeByString;
        #endregion

        #region Public Properties
        [JsonProperty(PropertyName = "formatString", NullValueHandling = NullValueHandling.Ignore)]
        public string FormatString { get; set; }

        [JsonProperty(PropertyName = "sortByColumn", NullValueHandling = NullValueHandling.Ignore)]
        public string SortByColumn { get; set; }

        [JsonProperty(PropertyName = "isHidden", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsHidden { get; set; }
        [JsonIgnore]
        public PBIDataType PBIDataType
        {
            get
            {
                return (PBIDataType)Enum.Parse(typeof(PBIDataType), DataType, true);
            }
            set
            {
                DataType = value.ToString();
            }
        }
        [JsonIgnore]
        public PBIDataCategory? DataCategory
        {
            get
            {
                if (string.IsNullOrEmpty(_dataCategoryString))
                    return null;

                return (PBIDataCategory)Enum.Parse(typeof(PBIDataCategory), _dataCategoryString, true);
            }
            set
            {
                if (value == null)
                    _dataCategoryString = null;
                else
                    _dataCategoryString = value.ToString();
            }
        }

        [JsonIgnore]
        public PBISummarizeBy? PBISummarizeBy
        {
            get
            {
                if (string.IsNullOrEmpty(_summarizeByString))
                    return null;

                return (PBISummarizeBy)Enum.Parse(typeof(PBISummarizeBy), _summarizeByString, true);
            }
            set
            {
                if (value == null)
                    _summarizeByString = null;
                else
                    _summarizeByString = value.ToString();
            }
        }

        [JsonIgnore]
        public PBITable ParentTable { get; set; }
        #endregion

        #region ShouldSerialize-Functions
        public bool ShouldSerialize_formatString()
        {
            if (ParentTable == null || ParentTable.ParentDataset == null || ParentTable.ParentDataset.PBIDefaultMode == PBIDefaultMode.Streaming)
            {
                if (!string.IsNullOrEmpty(FormatString))
                    Console.WriteLine("FormatStrings are not supported in Streaming-Mode (column [{0}])!", Name);
                return false;
            }

            return true;
        }

        public bool ShouldSerialize_sortByColumn()
        {
            if (ParentTable == null || ParentTable.ParentDataset == null || ParentTable.ParentDataset.PBIDefaultMode == PBIDefaultMode.Streaming)
            {
                if (!string.IsNullOrEmpty(SortByColumn))
                    Console.WriteLine("SortByColumns are not supported in Streaming-Mode (column [{0}])!", Name);
                return false;
            }

            return true;
        }

        public bool ShouldSerialize_isHidden()
        {
            if (ParentTable == null || ParentTable.ParentDataset == null || ParentTable.ParentDataset.PBIDefaultMode == PBIDefaultMode.Streaming)
            {
                if (IsHidden.HasValue)
                    Console.WriteLine("IsHidden is not supported in Streaming-Mode (column [{0}])!", Name);
                return false;
            }

            return true;
        }

        public bool ShouldSerialize_dataCategory()
        {
            if (ParentTable == null || ParentTable.ParentDataset == null || ParentTable.ParentDataset.PBIDefaultMode == PBIDefaultMode.Streaming)
            {
                if (DataCategory.HasValue)
                    Console.WriteLine("DataCategories are not supported in Streaming-Mode (column [{0}])!", Name);
                return false;
            }

            return true;
        }

        public bool ShouldSerialize_summarizeBy()
        {
            if (ParentTable == null || ParentTable.ParentDataset == null || ParentTable.ParentDataset.PBIDefaultMode == PBIDefaultMode.Streaming)
            {
                if (PBISummarizeBy.HasValue)
                    Console.WriteLine("SummarizeBy is not supported in Streaming-Mode (column [{0}])!", Name);
                return false;
            }

            return true;
        }
        #endregion
    }
}
