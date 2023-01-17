using System.Globalization;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace ArchiveNet.Repository;

internal class DateTimeUtcConverter
    {
        // public DynamoDBEntry ToEntry(object value) => (DateTime) value;

        // public object FromEntry(DynamoDBEntry entry)
        // {
		// 	var primitive = entry as Primitive;
        //     if (primitive == null || !(primitive.Value is String) || string.IsNullOrEmpty((string)primitive.Value))
        //         throw new ArgumentOutOfRangeException();

        //     var dateTime = entry.AsDateTime();
        //     return dateTime.ToUniversalTime();
        // }

		//private const string DateTimeFormat = "yyyyMMdd:hhmmss";
		private static readonly CultureInfo CultureInfo = CultureInfo.InvariantCulture;
		//public static AttributeValue ToEntry(DateTime value) => new AttributeValue { S = value.ToUniversalTime().ToString(DateTimeFormat, CultureInfo) };
		public static AttributeValue ToEntry(DateTime value) => new AttributeValue { S = value.ToUniversalTime().ToString(CultureInfo) };

        public static DateTime FromEntry(AttributeValue entry)
		{
			if (string.IsNullOrEmpty(entry.S))
				throw new ArgumentOutOfRangeException("Invalid date time received from table.");
			//return DateTime.ParseExact(entry.S, DateTimeFormat, CultureInfo);
			return DateTime.Parse(entry.S, CultureInfo);
		}
    }
