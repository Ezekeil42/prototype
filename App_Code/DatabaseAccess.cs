using System;
using WebMatrix.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prototype
{
    public class DatabaseAccess
    {
        private Database db = null;

        public void Seed()
        {
            var stmt = @"IF OBJECT_ID(N'Wish', N'U') IS NULL
                            BEGIN
                                CREATE TABLE Wish(
                                    Id INT IDENTITY(1,1) PRIMARY KEY,
                                    Name NVARCHAR(256) NOT NULL,
                                    Date DATETIME NOT NULL,
                                    Status NVARCHAR(16) NOT NULL,
                                    Votes INT NOT NULL,
                                    Description TEXT NOT NULL
                                )
                            END";
            var stmt2 = @"IF OBJECT_ID(N'UserVotes', N'U') IS NULL
                            BEGIN
                                CREATE TABLE UserVotes(
                                    VoteId INT IDENTITY(1,1) PRIMARY KEY,
                                    Date DATETIME NOT NULL,
                                    Name NVARCHAR(256) NOT NULL,
                                    WishId INT NOT NULL
                                )
                            END";
            try
            {
                db = Database.Open("DefaultConnection");

                db.Execute(stmt);
            } finally
            {
                if (db!= null)
                {
                    db.Close();
                    db = null;
                }
            }

            try
            {
                db = Database.Open("DefaultConnection");

                db.Execute(stmt2);
            } finally
            {
                if (db != null)
                {
                    db.Close();
                    db = null;
                }
            }
        }

        public IEnumerable<dynamic> GetWishesByStatus(string status)
        {
            string stmt = "SELECT * FROM Wish WHERE Status=@0";

            try
            {
                db = Database.Open("DefaultConnection");

                IEnumerable<dynamic> rows = db.Query(stmt, status);
                return rows;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                    db = null;
                }
            }
        }

        public IEnumerable<dynamic> GetWishesByVotes(int votes)
        {
            //SQL: return wishes in relation to a given vote amount

            string stmt = "SELECT * FROM Wish WHERE Votes>=@0 AND Status='Approved'";
            try
            {
                db = Database.Open("DefaultConnection");

                IEnumerable<dynamic> rows = db.Query(stmt, votes);
                return rows;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                    db = null;
                }
            }
        }

        public IEnumerable<dynamic> GetWishesByUser(string name)
        {
            //SQL: return wishes for a given user by name

            string stmt = "SELECT * FROM Wish WHERE Name=@0";
            try
            {
                db = Database.Open("DefaultConnection");

                IEnumerable<dynamic> rows = db.Query(stmt, name);
                return rows;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                    db = null;
                }
            }
        }

        public void DeleteWish(int id)
        {
            //SQL: gets replaced by a DELETE statement

            string stmt = @"DELETE FROM Wish
                            WHERE Id = @0";

            try
            {
                db = Database.Open("DefaultConnection");
                db.Execute(stmt, id);
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
        }

        public void UpdateWish(string status, int votes, int id)
        {
            string stmt = @"UPDATE Wish
                            SET Status=@0,
                                Votes=@1
                            WHERE Id = @2";

            try
            {
                db = Database.Open("DefaultConnection");

                db.Execute(stmt, status, votes, id);
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }

        }

        public void AddWish(Wish newWish)
        {
            string stmt = @"INSERT INTO Wish( Name, Date, Status, Votes, Description)
                                VALUES (@0, @1, @2, @3, @4)";

            try
            {
                db = Database.Open("DefaultConnection");

                db.Execute(stmt, newWish.Name, newWish.Date, 
                    newWish.Status, newWish.Votes, newWish.Description);
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                    db = null;
                }
            }
        }

        public bool HasVoted(string name, int wishId)
        {
            var stmt = @"SELECT * FROM UserVotes WHERE Name=@0 AND WishId=@1";
            bool userHasVoted = false;

            try
            {
                db = Database.Open("DefaultConnection");

                if (db.QuerySingle(stmt, name, wishId) != null)
                {
                    userHasVoted = true;
                }   
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                    db = null;
                }
            }

            return userHasVoted;
        }

        /*public bool HasVoted(string name, int wishId)
        {

            var stmt = @"SELECT * FROM UserVotes WHERE Name=@0 AND WishId=@1";
            bool userHasVoted = false;
            SqlConnection conn = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Justin\\documents\\visual studio 2015\\Projects\\prototype\\prototype\\App_Data\\Identity.mdf\";Initial Catalog=Identity;Integrated Security=True");
            try
            {
                
                conn.Open();
                SqlCommand cmd = new SqlCommand(stmt, conn);
                cmd.Parameters.AddWithValue("@0", name);
                cmd.Parameters.AddWithValue("@1", wishId);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        userHasVoted = true;
                    }
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn = null;
                }
            }

            return userHasVoted;
        }*/

        public void AddVote(DateTime date, string name, int wishId)
        {
            string stmt = @"INSERT INTO UserVotes( Date, Name, WishId)
                                VALUES (@0, @1, @2)";

            try
            {
                db = Database.Open("DefaultConnection");

                db.Execute(stmt, date, name, wishId);
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                    db = null;
                }
            }
        }
    }
}