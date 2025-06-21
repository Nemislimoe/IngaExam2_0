using System;
using System.Data;
using System.Data.SQLite;

namespace IngaExam2_0
{
    public static class DatabaseHelper
    {
        // Шлях до файлу бази даних
        public static string ConnectionString = "Data Source=library.db;Version=3;";

        /// Ініціалізація бази даних: створення таблиць Users, Books та Rentals, якщо їх не існує.

        public static void InitializeDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                // Створення таблиці Users
                string createUsersTable = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                        Login TEXT NOT NULL UNIQUE,
                        HashedPassword TEXT NOT NULL,
                        Role TEXT NOT NULL
                    );";
                using (SQLiteCommand cmd = new SQLiteCommand(createUsersTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // Створення таблиці Books із доданою колонкою DueDate
                string createBooksTable = @"
                    CREATE TABLE IF NOT EXISTS Books (
                        BookId INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Author TEXT NOT NULL,
                        Status TEXT NOT NULL,
                        Year INTEGER,
                        ISBN TEXT NOT NULL UNIQUE,
                        ReturnDate DATETIME NOT NULL
                    );";
                using (SQLiteCommand cmd = new SQLiteCommand(createBooksTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // Створення таблиці Rentals
                string createRentalsTable = @"
                    CREATE TABLE IF NOT EXISTS Rentals (
                        RentalId INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        BookId INTEGER NOT NULL,
                        RentDate DATETIME NOT NULL,
                        ReturnDate DATETIME
                    );";
                using (SQLiteCommand cmd = new SQLiteCommand(createRentalsTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #region Робота з користувачами
        public static DataRow GetUserByLogin(string login)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Login = @Login";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                        return dt.Rows[0];
                }
            }
            return null;
        }

        public static bool RegisterUser(string login, string hashedPassword, string role)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string dupQuery = "SELECT COUNT(*) FROM Users WHERE Login = @Login";
                using (SQLiteCommand dupCmd = new SQLiteCommand(dupQuery, conn))
                {
                    dupCmd.Parameters.AddWithValue("@Login", login);
                    long count = (long)dupCmd.ExecuteScalar();
                    if (count > 0)
                        return false;
                }
                string insertQuery = "INSERT INTO Users (Login, HashedPassword, Role) VALUES (@Login, @HashedPassword, @Role)";
                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
        #endregion

        #region Робота з книгами
        // Отримання списку книг із фільтром
        public static DataTable GetBooks(string filter = "")
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Books WHERE Title LIKE @Filter OR Author LIKE @Filter";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Filter", $"%{filter}%");
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // Додавання нової книги 
        public static bool InsertBook(string title, string author, int year, string isbn, DateTime returnDate, string status = "in stock")
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                //перевіка унікальності ISBN
                string insertQuery = @"
            INSERT INTO Books (Title, Author, Year, ISBN, Status, ReturnDate) 
            VALUES (@Title, @Author, @Year, @ISBN, @Status, @ReturnDate)";
                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Author", author);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@ISBN", isbn);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@ReturnDate", returnDate);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public static bool UpdateBook(int bookId, string title, string author, int year, string isbn, DateTime returnDate)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                // ... проверка уникальности ISBN ...
                string updateQuery = @"
                    UPDATE Books 
                    SET Title = @Title, 
                        Author = @Author, 
                        Year = @Year, 
                        ISBN = @ISBN, 
                        ReturnDate = @ReturnDate
                    WHERE BookId = @BookId";
                using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Author", author);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@ISBN", isbn);
                    cmd.Parameters.AddWithValue("@ReturnDate", returnDate);
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public static bool DeleteBook(int bookId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM Books WHERE BookId = @BookId";
                using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
        #endregion

        #region Робота з орендою
        public static bool RentBook(int bookId, int userId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string checkQuery = "SELECT Status FROM Books WHERE BookId = @BookId";
                using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@BookId", bookId);
                    object result = checkCmd.ExecuteScalar();
                    if (result == null || result.ToString() != "in stock")
                        return false;
                }
                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    string insertQuery = "INSERT INTO Rentals (UserId, BookId, RentDate) VALUES (@UserId, @BookId, @RentDate)";
                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@UserId", userId);
                        insertCmd.Parameters.AddWithValue("@BookId", bookId);
                        insertCmd.Parameters.AddWithValue("@RentDate", DateTime.Now);
                        insertCmd.ExecuteNonQuery();
                    }
                    string updateQuery = "UPDATE Books SET Status = 'in stock' WHERE BookId = @BookId";
                    using (SQLiteCommand updateCmd = new SQLiteCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@BookId", bookId);
                        updateCmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            return true;
        }

        public static DataTable GetRentalHistory(int userId)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT r.RentalId, 
                           b.Title, 
                           b.Author, 
                           r.RentDate, 
                           b.ReturnDate AS ReturnDate
                    FROM Rentals r
                    JOIN Books b ON r.BookId = b.BookId
                    WHERE r.UserId = @UserId";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
        #endregion
    }
}
