﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookingStoreProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE GetPaginatedBookings(
                IN PageNumber INT,
                IN PageSize INT,
                IN SearchTerm VARCHAR(255),
                OUT TotalCount INT
            )
            BEGIN
                DECLARE Offset INT;
                SET Offset = (PageNumber - 1) * PageSize;
                
                -- Get total count
              SELECT COUNT(*) INTO TotalCount
              FROM Bookings b LEFT JOIN InmateProfiles i ON b.InmateId = i.Id
              WHERE SearchTerm IS NULL 
              OR b.BookingNumber LIKE CONCAT('%', SearchTerm, '%')
              OR CONCAT(i.FirstName, ' ', i.MiddleName, ' ', i.LastName) LIKE CONCAT('%', SearchTerm, '%')
              OR b.BookingLocation LIKE CONCAT('%', SearchTerm, '%');
                -- Get paginated results
                SELECT 
                    b.Id,
                    b.BookingNumber,
                    b.InmateId,
                    CONCAT(i.FirstName, ' ', i.MiddleName, ' ', i.LastName) AS InmateName,
                    b.BookingLocation,
                    b.FacilityName,
                    b.BookingDate
                FROM Bookings b
                LEFT JOIN InmateProfiles i ON b.InmateId = i.Id
                WHERE SearchTerm IS NULL 
                    OR b.BookingNumber LIKE CONCAT('%', SearchTerm, '%')
                    OR CONCAT(i.FirstName, ' ', i.MiddleName, ' ', i.LastName) LIKE CONCAT('%', SearchTerm, '%')
                    OR b.BookingLocation LIKE CONCAT('%', SearchTerm, '%')
                ORDER BY b.BookingDate DESC
                LIMIT PageSize
                OFFSET Offset;
            END;
        ");

            // CreateBooking stored procedure
            migrationBuilder.Sql(@"
            CREATE PROCEDURE CreateBooking(
                IN Id VARCHAR(50),
                IN InmateId VARCHAR(50),
                IN BookingNumber VARCHAR(255),
                IN BookingDate DATETIME,
                IN FacilityName VARCHAR(255),
                IN BookingLocation VARCHAR(255),
                IN Status VARCHAR(50),
                IN Comments TEXT,
                IN CreatedOn DATETIME,
                IN CreatedBy VARCHAR(50)
            )
            BEGIN
                INSERT INTO Bookings (
                    Id, InmateId, BookingNumber, BookingDate, 
                    FacilityName, BookingLocation, Status, 
                    Comments, CreatedOn, CreatedBy
                )
                VALUES (
                    Id, InmateId, BookingNumber, BookingDate,
                    FacilityName, BookingLocation, Status,
                    Comments, CreatedOn, CreatedBy
                );
            END;
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE UpdateBooking(
                IN Id VARCHAR(50),
                IN InmateId VARCHAR(50),
                IN BookingDate DATETIME,
                IN FacilityName VARCHAR(255),
                IN BookingLocation VARCHAR(255),
                IN ReleaseDate DATETIME,
                IN ReleaseReason VARCHAR(255),
                IN Comments TEXT,
                IN UpdatedOn DATETIME,
                IN UpdatedBy VARCHAR(50)
            )
            BEGIN
                UPDATE Bookings
                SET 
                    InmateId = InmateId,
                    BookingDate = BookingDate,
                    FacilityName = FacilityName,
                    BookingLocation = BookingLocation,
                    ReleaseDate = ReleaseDate,
                    ReleaseReason = ReleaseReason,
                    Comments = Comments,
                    UpdatedOn = UpdatedOn,
                    UpdatedBy = UpdatedBy
                WHERE Id = Id;
            END;
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE BookingStatusUpdate(
                IN Id VARCHAR(50),
                IN Status VARCHAR(50)
            )
            BEGIN
                UPDATE Bookings
                SET Status = Status
                WHERE Id = Id;
            END;
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE DeleteBooking(
                IN BookingIdentity VARCHAR(50)
            )
            BEGIN
                DELETE FROM Bookings
                WHERE Id = BookingIdentity;
            END;
        ");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetPaginatedBookings");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetBookingByIdentity");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS CreateBooking");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateBooking");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS BookingStatusUpdate");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteBooking");
        }
    }
}
