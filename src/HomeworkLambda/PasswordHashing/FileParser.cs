using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace PasswordHashing 
{
    public class FileParser
    {
        public IEnumerable<SourceRecord> GetRecords(SourceFile sourceFile)
        {
            using var parser = new TextFieldParser(sourceFile.CsvStream)
            {
                TextFieldType = FieldType.Delimited
            };
            parser.SetDelimiters(",");

            var result = new LinkedList<SourceRecord>();

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();
                if (fields.Length != 4)
                {
                    throw new Exception($"Source file '{sourceFile.FileName}' is in a wrong format");
                }

                if (!Guid.TryParse(fields[0], out var id))
                {
                    throw new Exception($"Source file '{sourceFile.FileName}' contains a record with ID='{fields[0]}' which is not a GUID");
                }

                var name = fields[1];
                var password = fields[2];

                if (!DateTime.TryParse(fields[3], out var dateTime))
                {
                    throw new Exception($"Source file '{sourceFile.FileName}' contains a record with datetime '{fields[3]}' in a wrong format");
                }

                result.AddLast(new SourceRecord(id, password, name, dateTime, sourceFile.FileName));
            }

            return result;
        }
    }
}