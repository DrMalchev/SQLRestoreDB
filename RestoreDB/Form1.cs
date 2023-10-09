using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace RestoreDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Click += RestoreButton_Click;
            comboBox1.SelectedIndex = 0;
            textBox4.Text = textBox4Text;
            textBox5.Text = "12345";

            textBox4.TextChanged += TextBox4_TextChanged;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4Text = textBox4.Text;
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedServer = comboBox1.SelectedItem.ToString();

            // Pre-fill textBox3 based on the selected server
            if (selectedServer != "Local Machine")
            {
                textBox2.Text = @"G:\SampleFolder1\";
                textBox3.Text = @"G:\SampleFolder2\";
            }
            else
            {
                textBox3.Text = "";
            }

        }

        private async void RestoreButton_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            button1.Enabled = false;

            string serverName = comboBox1.SelectedItem.ToString();
            string connectionString = null;
            if (serverName == "Local Machine")
            {
                serverName = Environment.MachineName; // Get the local machine name
                connectionString = $"Data Source={serverName};Initial Catalog=master;Integrated Security=True;";
            }
            else
            {
                connectionString = @$"Data Source={serverName};Initial Catalog=master;User ID={user};Password={password};";
            }
            string databaseName = textBox1.Text;
            string backupFilePath = textBox2.Text;
            string logPATH = textBox3.Text;
            string userName = textBox4Text;
            string userPass = textBox5.Text;

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.ConnectTimeout = 1800; // Set a larger timeout value (in seconds)
            connectionString = builder.ConnectionString;


            restoreQuery = restoreQuery
                .Replace("set @DBNAME = x", $"set @DBNAME = '{databaseName}'")
                .Replace("set @bakPATH = x", $"set @bakPATH = '{backupFilePath}'")
                .Replace("SET @logPATH = x", $"set @logPATH = '{logPATH}'")
                .Replace("set @userName = x", $"set @userName = '{userName}'")
                .Replace("set @userPass = x", $"set @userPass = '{userPass}'");

            bool success = await Task.Run(async () => await RunTheSQLQuery(connectionString, databaseName));
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.MarqueeAnimationSpeed = 0;
            button1.Enabled = true;


        }

        private async Task<bool> RunTheSQLQuery(string connectionString, string databaseName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    KillConnectionsToDatabase(connection, databaseName);

                    using (SqlCommand command = new SqlCommand(restoreQuery, connection))
                    {
                        command.CommandTimeout = 1800;
                        command.ExecuteNonQuery();

                        MessageBox.Show($"Database '{databaseName}' restore completed successfully.", "Restore Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // Check if all required text fields are not empty
            bool allFieldsFilled = !string.IsNullOrWhiteSpace(textBox1.Text) &&
                                   !string.IsNullOrWhiteSpace(textBox2.Text) &&
                                   !string.IsNullOrWhiteSpace(textBox3.Text) &&
                                   !string.IsNullOrWhiteSpace(textBox4.Text) &&
                                   !string.IsNullOrWhiteSpace(textBox5.Text);

            // Enable or disable the "Restore" button based on the condition
            button1.Enabled = allFieldsFilled;
        }

        private void KillConnectionsToDatabase(SqlConnection connection, string dbName)
        {
            try
            {
                using (SqlCommand killCommand = new SqlCommand())
                {
                    killCommand.Connection = connection;

                    // Set parameters for killing connections
                    killCommand.Parameters.AddWithValue("@DBNAME", dbName);

                    // Execute the connection kill code
                    killCommand.CommandText = @"
                    USE master;
                    DECLARE @spid INT;
                    SELECT @spid = min(spid) FROM master.dbo.sysprocesses
                    WHERE dbid = DB_ID(@DBNAME);
                    WHILE @spid IS NOT NULL
                    BEGIN
                        EXEC('KILL ' + @spid); 
                        SELECT @spid = min(spid) FROM master.dbo.sysprocesses
                        WHERE dbid = DB_ID(@DBNAME) AND spid > @spid;
                    END;
                ";

                    killCommand.CommandType = CommandType.Text;
                    killCommand.ExecuteNonQuery();
                }

                Console.WriteLine("Connections to the database have been terminated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error killing connections: " + ex.Message);
            }
        }

        private string textBox4Text = "admin@test.com";
        private string user = "user1";
        private string password = "pass1";
        private string restoreQuery = @"declare @DBNAME NVARCHAR(50)
declare @bakPATH NVARCHAR(100)
DECLARE @Sql NVARCHAR(MAX);
DECLARE @logPATH NVARCHAR(100)
DECLARE @userName NVARCHAR(50)
DECLARE @userPass NVARCHAR(50)

-- Region Parameter setting
set @DBNAME = x
set @bakPATH = x
SET @logPATH = x
set @userName = x
set @userPass = x
-- Endregion

DECLARE @Table TABLE (
    LogicalName varchar(128),
    [PhysicalName] varchar(128), 
    [Type] varchar, 
    [FileGroupName] varchar(128), 
    [Size] varchar(128),
    [MaxSize] varchar(128), 
    [FileId]varchar(128), 
    [CreateLSN]varchar(128), 
    [DropLSN]varchar(128), 
    [UniqueId]varchar(128), 
    [ReadOnlyLSN]varchar(128), 
    [ReadWriteLSN]varchar(128),
    [BackupSizeInBytes]varchar(128), 
    [SourceBlockSize]varchar(128), 
    [FileGroupId]varchar(128), 
    [LogGroupGUID]varchar(128), 
    [DifferentialBaseLSN]varchar(128), 
    [DifferentialBaseGUID]varchar(128), 
    [IsReadOnly]varchar(128), 
    [IsPresent]varchar(128), 
    [TDEThumbprint]varchar(128),
    [SnapshotUrl]varchar(128)
)
DECLARE @Path varchar(1000)=@bakPATH
DECLARE @LogicalNameData varchar(128),@LogicalNameLog varchar(128)
INSERT INTO @table
EXEC('
RESTORE FILELISTONLY
   FROM DISK=''' +@Path+ '''
   ')

   SET @LogicalNameData=(SELECT LogicalName FROM @Table WHERE Type='D')
   SET @LogicalNameLog=(SELECT LogicalName FROM @Table WHERE Type='L')

USE master;
    IF DB_ID(QUOTENAME(@DBNAME)) IS NOT NULL
    BEGIN
        ALTER DATABASE "" + QUOTENAME(@DBNAME) + @"" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    END

-- Restore the database
SET @sql = N'
    RESTORE DATABASE ' + QUOTENAME(@DBNAME) + N'
    FROM DISK = @bakPATH
    WITH REPLACE,
    MOVE ''' + @LogicalNameData + N''' TO ''' + @logPATH + @DBNAME + N'.mdf'',
    MOVE ''' + @LogicalNameLog + N''' TO ''' + @logPATH + @DBNAME + N'.ldf''
';
-- Execute the dynamic SQL command
EXEC sp_executesql @sql, N'@bakPATH NVARCHAR(100)', @bakPATH;

-- Set the database back to multi-user mode
SET @Sql = N'
    USE master;
    ALTER DATABASE ' + QUOTENAME(@DBNAME) + N' SET MULTI_USER;
';
EXEC sp_executesql @Sql;

SET @Sql = N'USE ' + QUOTENAME(@DBNAME) + ';
            CREATE USER [NT AUTHORITY\SYSTEM] FROM LOGIN [NT AUTHORITY\SYSTEM];';
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = @userName)
BEGIN
EXEC sp_executesql @Sql;
END

SET @Sql = N'USE ' + QUOTENAME(@DBNAME) + ';
            ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\SYSTEM];';
EXEC sp_executesql @Sql;


SET @sql = '
USE ' + QUOTENAME(@DBNAME) + ';
DECLARE @secondAdminId NVARCHAR(50);
IF EXISTS (
    SELECT user_name 
    FROM sf_users 
    WHERE id IN (
        SELECT user_id 
        FROM sf_user_link 
        WHERE role_id = (
            SELECT id 
            FROM sf_roles 
            WHERE nme = ''Administrators'')
    ) 
    AND user_name = @userName
)
BEGIN
    SET @secondAdminId = (
        SELECT id 
        FROM sf_users 
        WHERE user_name = @userName
    );
END
ELSE
BEGIN
    SET @secondAdminId = (
        SELECT id 
        FROM sf_users 
        WHERE id IN (
            SELECT user_id 
            FROM sf_user_link 
            WHERE role_id = (
                SELECT id 
                FROM sf_roles 
                WHERE nme = ''Administrators'')
        )
        ORDER BY user_name    
		offset 0 ROWS
        FETCH NEXT 1 ROWS ONLY
    );
END;

UPDATE dbo.sf_users 
SET 
    salt = NULL, 
    password_format = 0,
    user_name = @userName,
    passwd = @userPass
WHERE id = @secondAdminId;

SELECT user_name, id, passwd 
FROM sf_users 
WHERE id IN (
    SELECT user_id 
    FROM sf_user_link 
    WHERE role_id = (
        SELECT id 
        FROM sf_roles 
        WHERE nme = ''Administrators'')
);
';

EXEC sp_executesql @sql, N'@userName NVARCHAR(50), @userPass NVARCHAR(50)', @userName, @userPass;";

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}