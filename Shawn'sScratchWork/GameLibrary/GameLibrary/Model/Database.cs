//Code largely borrowed from a tutorial by:  brennydoogles
//At:http://www.dreamincode.net/forums/topic/157830-using-sqlite-with-c%23/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace GameLibrary
{
    /// <summary>
    /// This class allowys you to link to and access an SQLite database.
    /// </summary>
    public class Database
    {
        String dbConnection;

        SQLiteConnection connection;
        SQLiteCommand command;
        SQLiteDataReader reader;

        /// <summary>
        /// Single Param Constructor for specifying the DB file.
        /// </summary>
        /// <param name="inputFile">The File containing the DB</param>
        public Database(String inputFile)
        {
            dbConnection = String.Format("Data Source={0}", inputFile);
        }

        /// <summary>
        /// Allows the programmer to run a select against the Database.
        /// </summary>
        /// <param name="sql">The SQL to run</param>
        /// <returns>A DataTable containing the result set.</returns>
        private DataTable ExecuteQuerry(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                connection = new SQLiteConnection(dbConnection);
                connection.Open();
                
                command = new SQLiteCommand(connection);
                command.CommandText = sql;
                
                reader = command.ExecuteReader();
               
                dt.Load(reader);
                
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return dt;
        }

        /// <summary>
        /// Allows the programmer to interact with the database for purposes other than a query.
        /// </summary>
        /// <param name="sql">The SQL to be run.</param>
        /// <returns>An Integer containing the number of rows updated.</returns>
        private int ExecuteStatement(string sql)
        {
            connection = new SQLiteConnection(dbConnection);
            connection.Open();
            
            command = new SQLiteCommand(connection);
            command.CommandText = sql;

            int rowsUpdated = command.ExecuteNonQuery();

            connection.Close();

            return rowsUpdated;
        }

        /// <summary>
        /// Allows the programmer to easily insert into the DB
        /// </summary>
        /// <param name="tableName">The table into which we insert the data.</param>
        /// <param name="data">A dictionary containing the column names and data for the insert.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public void Insert(String tableName, Dictionary<String, String> data)
        {
            String columns = "";
            String values = "";

            foreach (KeyValuePair<String, String> val in data)
            {
                columns += String.Format(" {0},", val.Key.ToString());
                values += String.Format(" '{0}',", val.Value);
            }
            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);

            try
            {
                this.ExecuteStatement(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Allows the programmer to easily select rows in the DB.
        /// </summary>
        /// <param name="selectStatement">What to select.</param>
        /// <param name="table">The table from which to select.</param>
        /// <returns></returns>
        public DataTable Select(String selectStatement, String table)
        {
            try
            {
                return this.ExecuteQuerry(String.Format("select {0} from {1};", selectStatement, table));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Allows the programmer to easily select rows in the DB with a condition.
        /// </summary>
        /// <param name="selectStatement">What to select.</param>
        /// <param name="table">The table from which to select.</param>
        /// <param name="where">The where clause for the update statement.</param>
        /// <returns></returns>
        public DataTable Select(String selectStatement, String table, String where)
        {
            try
            {
                return this.ExecuteQuerry(String.Format("select {0} from {1} where {2};", selectStatement, table, where));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Allows the programmer to easily update rows in the DB.
        /// </summary>
        /// <param name="tableName">The table to update.</param>
        /// <param name="data">A dictionary containing Column names and their new values.</param>
        /// <param name="where">The where clause for the update statement.</param>
        public void Update(String tableName, Dictionary<String, String> data, String where)
        {
            String vals = "";

            if (data.Count >= 1)
            {
                foreach (KeyValuePair<String, String> val in data)
                {
                    vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());
                }
                vals = vals.Substring(0, vals.Length - 1);
            }

            try
            {
                this.ExecuteStatement(String.Format("update {0} set {1} where {2};", tableName, vals, where));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Allows the programmer to easily delete rows from the DB.
        /// </summary>
        /// <param name="tableName">The table from which to delete.</param>
        /// <param name="where">The where clause for the delete.</param>
        public void Delete(String tableName, String where)
        {
            try
            {
                this.ExecuteStatement(String.Format("delete from {0} where {1};", tableName, where));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Allows the user to easily clear all data from a specific table.
        /// </summary>
        /// <param name="table">The name of the table to clear.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool ClearTable(String table)
        {
            try
            {
                this.ExecuteStatement(String.Format("delete from {0};", table));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Allows the programmer to easily delete all data from the DB.
        /// </summary>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool ClearDB()
        {
            DataTable tables;
            try
            {
                tables = this.ExecuteQuerry("select NAME from SQLITE_MASTER where type='table' order by NAME;");
                foreach (DataRow table in tables.Rows)
                {
                    this.ClearTable(table["NAME"].ToString());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

