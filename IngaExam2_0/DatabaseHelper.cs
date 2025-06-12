using System;
using System.Data;
using System.Data.SQLite;

namespace IngaExam2_0
{
    public static class DatabaseHelper
    {
        // Шлях до файлу бази даних
        public static string ConnectionString = "Data Source=library.db;Version=3;";

        /// <summary>
        /// Ініціалізація бази даних: створення таблиць Books та Rentals, якщо вони не існують.
        /// </summary>
        public static void InitializeDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // Створення таблиці Books
                string createBooksTable = @"
                    CREATE TABLE IF NOT EXISTS Books (
                        BookId INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Author TEXT NOT NULL,
                        Status TEXT NOT NULL,
                        Year INTEGER,
                        ISBN TEXT NOT NULL UNIQUE
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

        /// <summary>
        /// Повертає дані з таблиці Books з можливістю фільтрації за назвою або автором.
        /// </summary>
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

        /// <summary>
        /// Оренда книги:
        /// Перевіряється, чи книга має статус "в наявності". Якщо так, вставляється запис в таблицю Rentals,
        /// а статус книги оновлюється на "в оренді".
        /// </summary>
        public static bool RentBook(int bookId, int userId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // Перевірка статусу книги
                string checkQuery = "SELECT Status FROM Books WHERE BookId = @BookId";
                using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@BookId", bookId);
                    object result = checkCmd.ExecuteScalar();
                    if (result == null || result.ToString() != "в наявності")
                        return false;
                }

                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    // Вставка запису оренди
                    string insertQuery = "INSERT INTO Rentals (UserId, BookId, RentDate) VALUES (@UserId, @BookId, @RentDate)";
                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@UserId", userId);
                        insertCmd.Parameters.AddWithValue("@BookId", bookId);
                        insertCmd.Parameters.AddWithValue("@RentDate", DateTime.Now);
                        insertCmd.ExecuteNonQuery();
                    }

                    // Оновлення статусу книги
                    string updateQuery = "UPDATE Books SET Status = 'в оренді' WHERE BookId = @BookId";
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

        /// <summary>
        /// Отримання історії оренди для заданого користувача.
        /// </summary>
        public static DataTable GetRentalHistory(int userId)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT r.RentalId, b.Title, b.Author, r.RentDate, r.ReturnDate
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

        /// <summary>
        /// Додавання нової книги. Перед вставкою виконується перевірка унікальності ISBN.
        /// </summary>
        public static bool InsertBook(string title, string author, int year, string isbn, string status = "в наявності")
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // Перевірка на дублювання ISBN
                string dupQuery = "SELECT COUNT(*) FROM Books WHERE ISBN = @ISBN";
                using (SQLiteCommand dupCmd = new SQLiteCommand(dupQuery, conn))
                {
                    dupCmd.Parameters.AddWithValue("@ISBN", isbn);
                    long count = (long)dupCmd.ExecuteScalar();
                    if (count > 0)
                        return false;
                }

                string insertQuery = "INSERT INTO Books (Title, Author, Year, ISBN, Status) VALUES (@Title, @Author, @Year, @ISBN, @Status)";
                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Author", author);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@ISBN", isbn);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        /// <summary>
        /// Оновлення інформації про книгу з перевіркою дублювання ISBN (крім поточної книги).
        /// </summary>
        public static bool UpdateBook(int bookId, string title, string author, int year, string isbn)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // Перевірка дублювання ISBN для інших книг
                string dupQuery = "SELECT COUNT(*) FROM Books WHERE ISBN = @ISBN AND BookId <> @BookId";
                using (SQLiteCommand dupCmd = new SQLiteCommand(dupQuery, conn))
                {
                    dupCmd.Parameters.AddWithValue("@ISBN", isbn);
                    dupCmd.Parameters.AddWithValue("@BookId", bookId);
                    long count = (long)dupCmd.ExecuteScalar();
                    if (count > 0)
                        return false;
                }

                string updateQuery = "UPDATE Books SET Title = @Title, Author = @Author, Year = @Year, ISBN = @ISBN WHERE BookId = @BookId";
                using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Author", author);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@ISBN", isbn);
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        /// <summary>
        /// Видалення книги за її ідентифікатором.
        /// </summary>
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
    }
}
