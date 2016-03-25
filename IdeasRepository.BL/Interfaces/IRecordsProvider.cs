using IdeasRepository.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeasRepository.BL.Interfaces
{
    public interface IRecordsProvider : IDisposable
    {
        List<Record> GetAllRecords();
        List<RecordType> GetAllRecordTypes();
        Record GetRecord(string id);
        RecordType GetRecordTypeByName(string name);
        void AddRecord(Record record);
        void UpdateRecord(Record record);
        void RemoveRecord(Record record);
    }
}
