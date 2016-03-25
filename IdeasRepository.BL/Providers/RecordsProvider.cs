using IdeasRepository.BL.Interfaces;
using IdeasRepository.DAL.Contexts;
using IdeasRepository.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeasRepository.BL.Providers
{
    public class RecordsProvider : IRecordsProvider
    {
        private ApplicationDbContext _context;

        public RecordsProvider()
        {
            _context = new ApplicationDbContext();
        }

        public List<Record> GetAllRecords()
        {
            var records = _context.Records.ToList();
            return records;
        }

        public List<RecordType> GetAllRecordTypes()
        {
            var recordTypes = _context.RecordTypes.ToList();
            return recordTypes;
        }

        public Record GetRecord(string id)
        {
            var record = _context.Records.Where(r => r.Id == id).SingleOrDefault();
            return record;
        }

        public RecordType GetRecordTypeByName(string name)
        {
            var record = _context.RecordTypes.Where(r => r.Name == name).SingleOrDefault();
            return record;
        }

        public void AddRecord(Record record)
        {
            _context.Records.Add(record);
            _context.SaveChanges();
        }

        public void UpdateRecord(Record record)
        {
            _context.Records.Attach(record);
            var entry = _context.Entry(record);
            entry.Property(p => p.RecordTypeId).IsModified = true;
            entry.Property(p => p.TextBody).IsModified = true;

            _context.SaveChanges();
        }

        public void RemoveRecord(Record record)
        {
            var entry = _context.Entry(record);
            entry.State = EntityState.Deleted;

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
