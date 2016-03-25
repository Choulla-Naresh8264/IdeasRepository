using IdeasRepository.BL.Interfaces;
using IdeasRepository.DAL.Contexts;
using IdeasRepository.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IdeasRepository.BL.Providers
{
    public class RecordsProvider : IRecordsProvider
    {
        /// <summary>
        /// An instance of the database context.
        /// </summary>
        private ApplicationDbContext _context;

        public RecordsProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all records from the database.
        /// </summary>
        /// <returns>List of all records.</returns>
        public List<Record> GetAllRecords()
        {
            var records = _context.Records.ToList();
            return records;
        }

        /// <summary>
        /// Gets all record types from the database.
        /// </summary>
        /// <returns>List of all record types.</returns>
        public List<RecordType> GetAllRecordTypes()
        {
            var recordTypes = _context.RecordTypes.ToList();
            return recordTypes;
        }

        /// <summary>
        /// Gets the record from the database by its Id.
        /// </summary>
        /// <param name="id">Id of the expecter record.</param>
        /// <returns>Record with the given Id or null.</returns>
        public Record GetRecord(string id)
        {
            var record = _context.Records.Where(r => r.Id == id).SingleOrDefault();
            return record;
        }

        /// <summary>
        /// Adds the given entity to the context and store it in the
        /// database.
        /// </summary>
        /// <param name="record">Record to add.</param>
        public void AddRecord(Record record)
        {
            _context.Records.Add(record);
            _context.SaveChanges();
        }

        /// <summary>
        /// Attaches the given entity to the context underlying the set
        /// and updates changed fields in appropriate database entity
        /// that searches by the primary key value.
        /// </summary>
        /// <param name="record">Entity to the record. Id must be declared.</param>
        public void UpdateRecord(Record record)
        {
            _context.Records.Attach(record);
            var entry = _context.Entry(record);
            entry.Property(p => p.RecordTypeId).IsModified = true;
            entry.Property(p => p.TextBody).IsModified = true;

            _context.SaveChanges();
        }

        /// <summary>
        /// Attaches the given entity to the context underlying the set
        /// and updates the deleted status in appropriate database entity
        /// that searches by the primary key value.
        /// </summary>
        /// <param name="record">Entity to the record. Id must be declared.</param>
        public void UpdateRemovedStatus(Record record)
        {
            _context.Records.Attach(record);
            var entry = _context.Entry(record);
            entry.Property(p => p.IsDeleted).IsModified = true;

            _context.SaveChanges();
        }

        /// <summary>
        /// Removes the given entity from the context underlying the set.
        /// </summary>
        /// <param name="record">Record to remove. Id must be declared.</param>
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
