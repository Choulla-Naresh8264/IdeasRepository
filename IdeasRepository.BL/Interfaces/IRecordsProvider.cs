using IdeasRepository.DAL.Entities;
using System;
using System.Collections.Generic;

namespace IdeasRepository.BL.Interfaces
{
    public interface IRecordsProvider : IDisposable
    {
        List<Record> GetAllRecords();
        List<RecordType> GetAllRecordTypes();
        Record GetRecord(string id);
        void AddRecord(Record record);
        void UpdateRecord(Record record);
        void RemoveRecord(Record record);
        void UpdateRemovedStatus(Record record);
    }
}
